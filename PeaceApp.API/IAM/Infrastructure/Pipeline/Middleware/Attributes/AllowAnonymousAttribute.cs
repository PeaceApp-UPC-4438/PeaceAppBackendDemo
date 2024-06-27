namespace PeaceApp.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

[AttributeUsage(validOn: AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute 
{
    
}