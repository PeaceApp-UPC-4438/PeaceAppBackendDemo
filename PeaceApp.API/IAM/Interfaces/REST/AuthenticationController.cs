using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PeaceApp.API.IAM.Domain.Services;
using PeaceApp.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using PeaceApp.API.IAM.Interfaces.REST.Resources;
using PeaceApp.API.IAM.Interfaces.REST.Transform;

namespace PeaceApp.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(signUpCommand);
        return Ok(new{message ="User created successfully"});
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
            authenticatedUser.user,
            authenticatedUser.token);
        return Ok(authenticatedUserResource);
    }
    
    
    
    
    
}