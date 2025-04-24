using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PeaceApp.API.Citizen.Application.Internal.CommandServices;
using PeaceApp.API.Citizen.Application.Internal.QueryServices;
using PeaceApp.API.Citizen.Domain.Repositories;
using PeaceApp.API.Citizen.Domain.Services;
using PeaceApp.API.Citizen.Infrastructure.Persistence.EFC.Repositories;
using PeaceApp.API.Citizen.Interfaces.ACL;
using PeaceApp.API.Citizen.Interfaces.ACL.Services;
using PeaceApp.API.Organization.Application.Internal.CommandServices;
using PeaceApp.API.Organization.Application.Internal.QueryServices;
using PeaceApp.API.Organization.Domain.Repositories;
using PeaceApp.API.Organization.Domain.Services;
using PeaceApp.API.Organization.Infrastructure.Persistance.EFC.Repositories;
using PeaceApp.API.Report.Application.Internal.CommandServices;
using PeaceApp.API.Report.Application.Internal.QueryServices;
using PeaceApp.API.Report.Domain.Repositories;
using PeaceApp.API.Report.Domain.Services;
using PeaceApp.API.Report.Infrastructure.Persistance.EFC.Repositories;
using PeaceApp.API.Shared.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using PeaceApp.API.Shared.Interfaces.ASP.Configuration;
using PeaceApp.API.Communication.Application.Internal.CommandServices;
using PeaceApp.API.Communication.Application.Internal.QueryServices;
using PeaceApp.API.Communication.Domain.Repositories;
using PeaceApp.API.Communication.Domain.Services;
using PeaceApp.API.Communication.Infrastructure.Persistance.EFC.Repositories;
using PeaceApp.API.IAM.Application.Internal.CommandServices;
using PeaceApp.API.IAM.Application.Internal.OutboundServices;
using PeaceApp.API.IAM.Application.Internal.QueryServices;
using PeaceApp.API.IAM.Domain.Repositories;
using PeaceApp.API.IAM.Domain.Services;
using PeaceApp.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using PeaceApp.API.IAM.Infrastructure.Persitence.EFC.Repositories;
using PeaceApp.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using PeaceApp.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using PeaceApp.API.IAM.Infrastructure.Tokens.JWT.Services;
using PeaceApp.API.IAM.Interfaces.ACL;
using PeaceApp.API.IAM.Interfaces.ACL.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();    
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "PeaceApp.API",
                Version = "v1",
                Description = "Peace App Web App API",
                TermsOfService = new Uri("https://peace-app/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Peace App",
                    Email = "contact@peace.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer", Type = ReferenceType.SecurityScheme
                    } 
                }, 
                Array.Empty<string>()
            }
        });
    });

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Dependency Injections
// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Report Bounded Context Injection Configuration
builder.Services.AddScoped<IReportManagementRepository, ReportManagementRepository>();
builder.Services.AddScoped<IReportManagementCommandService, ReportManagementCommandService>();
builder.Services.AddScoped<IReportManagementQueryService, ReportManagementQueryService>();

// Organization Bounded Context Injection Configuration

builder.Services.AddScoped<IOrganizationAccountRepository, OrganizationAccountRepository>();
builder.Services.AddScoped<IOrganizationAccountCommandService, OrganizationAccountCommandService>();
builder.Services.AddScoped<IOrganizationAccountQueryService, OrganizationAccountQueryService>();

// Citizen Bounded Context Injection Configuration

builder.Services.AddScoped<ICitizenRepository, CitizenRepository>();
builder.Services.AddScoped<ICitizenCommandService, CitizenCommandService>();
builder.Services.AddScoped<ICitizenQueryService, CitizenQueryService>();
builder.Services.AddScoped<ICitizensContextFacade, CitizensContextFacade>();


// Communication Bounded Context Injection Configuration
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();

// IAM Bounded Context

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Verify Database objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add authoirization middleware to pipeline
app.UseRequestAuthorization();



app.UseHttpsRedirection();

app.UseCors("AllowViteDev");

app.UseRequestAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();











// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
//
// builder.Services.AddControllers(options =>options.Conventions.Add(new KebabCaseRouteNamingConvention()));
// // Add Database Connection
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//
// // Configure Database Context and Logging Levels
//
// builder.Services.AddDbContext<AppDbContext>(
//     options =>
//     {
//         if (connectionString != null)
//             if (builder.Environment.IsDevelopment())
//                 options.UseMySQL(connectionString)
//                     .LogTo(Console.WriteLine, LogLevel.Information)
//                     .EnableSensitiveDataLogging()
//                     .EnableDetailedErrors();
//             else if (builder.Environment.IsProduction())
//                 options.UseMySQL(connectionString)
//                     .LogTo(Console.WriteLine, LogLevel.Error)
//                     .EnableDetailedErrors();    
//     });
//
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(
//     c =>
// {
//     c.SwaggerDoc("v1",
//         new OpenApiInfo
//         {
//             Title = "ACME.LearningCenterPlatform.API",
//             Version = "v1",
//             Description = "ACME Learning Center Platform API",
//             TermsOfService = new Uri("https://acme-learning.com/tos"),
//             Contact = new OpenApiContact
//             {
//                 Name = "ACME Studios",
//                 Email = "contact@acme.com"
//             },
//             License = new OpenApiLicense
//             {
//                 Name = "Apache 2.0",
//                 Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
//             }
//         });
//     c.EnableAnnotations();
// });
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();