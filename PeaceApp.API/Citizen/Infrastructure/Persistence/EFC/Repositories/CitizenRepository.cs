using Microsoft.EntityFrameworkCore;
using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Citizen.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PeaceApp.API.Citizen.Infrastructure.Persistence.EFC.Repositories;

public class CitizenRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.Citizen>(context), ICitizenRepository
{
    public Task<Domain.Model.Aggregates.Citizen?> FindCitizenByEmailAsync(EmailAddress email)
    {
        return Context.Set<Domain.Model.Aggregates.Citizen>().Where(p => p.Email == email).FirstOrDefaultAsync();
    }

    public Task<Domain.Model.Aggregates.Citizen?> GetByIdAsync(int id)
    {
        return Context.Set<Domain.Model.Aggregates.Citizen>().FindAsync(id).AsTask();
    }

    public async Task UpdateAsync(Domain.Model.Aggregates.Citizen citizen)
    {
        Context.Set<Domain.Model.Aggregates.Citizen>().Update(citizen);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Domain.Model.Aggregates.Citizen citizen)
    {
        Context.Set<Domain.Model.Aggregates.Citizen>().Remove(citizen);
        await Context.SaveChangesAsync();
    }
}