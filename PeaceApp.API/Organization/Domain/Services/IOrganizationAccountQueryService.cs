using PeaceApp.API.Organization.Domain.Model.Aggregates;
using PeaceApp.API.Organization.Domain.Model.Queries;

namespace PeaceApp.API.Organization.Domain.Services;

public interface IOrganizationAccountQueryService
{
    //I can add more in the future
    Task<OrganizationAccount> Handle(GetOrganizationAccountByOrganizationNameQuery query);
}