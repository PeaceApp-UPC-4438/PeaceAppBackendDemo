using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Organization.Domain.Repositories;

public interface IOrganizationAccountRepository : IBaseRepository<Model.Aggregates.OrganizationAccount>
{
    //I can add more in the future
    Task<Model.Aggregates.OrganizationAccount> FindByOrganizationNameAsync(string organizationName);
}