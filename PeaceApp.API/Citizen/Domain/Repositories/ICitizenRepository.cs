using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Citizen.Domain.Repositories;

public interface ICitizenRepository : IBaseRepository<Model.Aggregates.Citizen>
{
    Task<Model.Aggregates.Citizen?> FindProfileByEmailAsync(EmailAddress email);
}