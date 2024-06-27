using PeaceApp.API.IAM.Domain.Model.Commands;
using PeaceApp.API.IAM.Interfaces.REST.Resources;

namespace PeaceApp.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}