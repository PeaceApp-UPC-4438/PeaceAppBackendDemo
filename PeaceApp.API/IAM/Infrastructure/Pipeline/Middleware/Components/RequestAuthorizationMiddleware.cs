using PeaceApp.API.IAM.Application.Internal.OutboundServices;
using PeaceApp.API.IAM.Domain.Model.Queries;
using PeaceApp.API.IAM.Domain.Services;
using PeaceApp.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace PeaceApp.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        var allowAnonymous =
            context.Request.HttpContext.GetEndpoint()!.Metadata.Any(n =>
                n.GetType() == typeof(AllowAnonymousAttribute));
        Console.WriteLine($"AllowAnonymous is  {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }
        Console.WriteLine("Checking Authorization");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split( "  " ).Last();
        if (token == null) { throw new Exception("Null or invalid token"); }

        var userId = await tokenService.ValidateToken(token);
        if (userId == null) throw new Exception("Invalid token");
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user == null) throw new Exception("User not found");
        Console.WriteLine("Authorization successful");
        context.Items["Users"] = user;
        Console.WriteLine("Continuing Middleware pipeline");
        await next(context);

    }
}
