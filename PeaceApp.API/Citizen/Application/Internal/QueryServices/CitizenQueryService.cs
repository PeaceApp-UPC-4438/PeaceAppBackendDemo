using PeaceApp.API.Citizen.Domain.Model.Aggregates;
using PeaceApp.API.Citizen.Domain.Model.Queries;
using PeaceApp.API.Citizen.Domain.Repositories;
using PeaceApp.API.Citizen.Domain.Services;

namespace PeaceApp.API.Citizen.Application.Internal.QueryServices;

public class CitizenQueryService(ICitizenRepository citizenRepository) : ICitizenQueryService
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Citizen>> Handle(GetAllCitizensQuery query)
    {
        return await citizenRepository.ListAsync();
    }

    public async Task<Domain.Model.Aggregates.Citizen?> Handle(GetCitizenByEmailQuery query)
    {
        return await citizenRepository.FindCitizenByEmailAsync(query.Email);
    }

    public async Task<Domain.Model.Aggregates.Citizen?> Handle(GetCitizenByIdQuery query)
    {
        return await citizenRepository.FindByIdAsync(query.CitizenId);
    }
}