using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Citizen.Domain.Repositories;

public interface ICitizenRepository : IBaseRepository<Model.Aggregates.Citizen>
{
    Task<Model.Aggregates.Citizen?> FindCitizenByEmailAsync(EmailAddress email);
    Task<Model.Aggregates.Citizen?> GetByIdAsync(int id);
    Task UpdateAsync(Model.Aggregates.Citizen citizen);  
    Task DeleteAsync(Model.Aggregates.Citizen citizen);  
}