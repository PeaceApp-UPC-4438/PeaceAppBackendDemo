using PeaceApp.API.Organization.Domain.Model.Aggregates;
using PeaceApp.API.Organization.Interfaces.REST.Resources;

namespace PeaceApp.API.Organization.Interfaces.REST.Transform;

public  static class OrganizationAccountResourceFromEntityAssembler
{
    public static OrganizationAccountResource ToResourceFromEntity(OrganizationAccount entity)
    {
        return new OrganizationAccountResource(entity.Id, entity.OrganizationName, entity.Location,
            entity.Cellphone);  
    }
}