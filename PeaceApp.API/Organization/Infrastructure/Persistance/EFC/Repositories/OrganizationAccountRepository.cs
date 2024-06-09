using Microsoft.EntityFrameworkCore;
using PeaceApp.API.Organization.Domain.Model.Aggregates;
using PeaceApp.API.Organization.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PeaceApp.API.Organization.Infrastructure.Persistance.EFC.Repositories;

public class OrganizationAccountRepository : BaseRepository<OrganizationAccount>, IOrganizationAccountRepository
{
    public OrganizationAccountRepository(AppDbContext context) : base(context)
    {
    }

    public async  Task<OrganizationAccount> FindByOrganizationNameAsync(string organizationName)
    {
        return await Context.Set<OrganizationAccount>()
            .FirstOrDefaultAsync(f => f.OrganizationName == organizationName);
    }
}