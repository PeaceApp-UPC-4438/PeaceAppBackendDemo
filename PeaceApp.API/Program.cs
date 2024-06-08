using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PeaceApp.API.Report.Application.Internal.CommandServices;
using PeaceApp.API.Report.Application.Internal.QueryServices;
using PeaceApp.API.Report.Domain.Repositories;
using PeaceApp.API.Report.Domain.Services;
using PeaceApp.API.Report.Infrastructure.Persistance.EFC.Repositories;
using PeaceApp.API.Shared.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using PeaceApp.API.Shared.Interfaces.ASP.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.

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

app.UseHttpsRedirection();

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