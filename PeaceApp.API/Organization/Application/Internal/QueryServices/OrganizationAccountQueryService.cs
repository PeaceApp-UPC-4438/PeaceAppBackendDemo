using PeaceApp.API.Organization.Domain.Model.Aggregates;
using PeaceApp.API.Organization.Domain.Queries;
using PeaceApp.API.Organization.Domain.Repositories;
using PeaceApp.API.Organization.Domain.Services;

namespace PeaceApp.API.Organization.Application.Internal.QueryServices;

public class OrganizationAccountQueryService(IOrganizationAccountRepository organizationAccountRepository)
    : IOrganizationAccountQueryService
{

    public async Task<OrganizationAccount> Handle(GetOrganizationAccountByOrganizationNameQuery query)
    {
        return await organizationAccountRepository.FindByOrganizationNameAsync(query.OrganizationName);
    }
}