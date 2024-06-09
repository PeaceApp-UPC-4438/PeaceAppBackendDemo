using PeaceApp.API.Organization.Domain.Model.Commands;
using PeaceApp.API.Organization.Interfaces.REST.Resources;

namespace PeaceApp.API.Organization.Interfaces.REST.Transform;

public class CreateOrganizationAccountCommandFromResourceAssembler
{
    public static CreateOrganizationAccountCommand ToCommandFromResource(CreateOrganizationAccountResource resource)
    {
        return new CreateOrganizationAccountCommand(resource.OrganizationName, resource.Location, resource.Cellphone);  
    }
}