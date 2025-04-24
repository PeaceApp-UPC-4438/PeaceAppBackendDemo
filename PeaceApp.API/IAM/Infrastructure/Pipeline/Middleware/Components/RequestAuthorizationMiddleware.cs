using PeaceApp.API.IAM.Application.Internal.OutboundServices;
using PeaceApp.API.IAM.Domain.Model.Queries;
using PeaceApp.API.IAM.Domain.Services;
using PeaceApp.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace PeaceApp.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService
    )
    {
        Console.WriteLine("Entering InvokeAsync");

        // Manejar las preflight OPTIONS
        if (context.Request.Method == HttpMethods.Options)
        {
            context.Response.StatusCode = 204;
            context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:5173";
            context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization";
            context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
            context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
            await context.Response.CompleteAsync();
            return;
        }

        var allowAnonymous =
            context.Request.HttpContext.GetEndpoint()?.Metadata.Any(m =>
                m.GetType() == typeof(AllowAnonymousAttribute)) ?? false;

        Console.WriteLine($"Allow Anonymous is {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping Authorization");
            await next(context);
            return;
        }

        Console.WriteLine("Checking Authorization");

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?
            .Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            await RespondWithCorsError(context, "Null or invalid token");
            return;
        }

        var userId = await tokenService.ValidateToken(token);
        if (userId == null)
        {
            await RespondWithCorsError(context, "Invalid token");
            return;
        }

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user == null)
        {
            await RespondWithCorsError(context, "User not found");
            return;
        }

        Console.WriteLine("Authorization successful");
        context.Items["User"] = user;
        await next(context);
    }

    private async Task RespondWithCorsError(HttpContext context, string message)
    {
        context.Response.StatusCode = 401;
        context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:5173";
        context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization";
        context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
        context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
        await context.Response.WriteAsync(message);
    }
}
