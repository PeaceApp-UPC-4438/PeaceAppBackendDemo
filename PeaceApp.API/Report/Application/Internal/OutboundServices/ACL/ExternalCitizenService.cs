using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Citizen.Interfaces.ACL;

namespace PeaceApp.API.Report.Application.Internal.OutboundServices.ACL;

public class ExternalCitizenService(ICitizensContextFacade citizensContextFacade)
{
    public async Task<CitizenId?> FetchCitizenIdByEmail(string email)
    {
        var citizenId = await citizensContextFacade.FetchCitizenIdByEmail(email);
        if (citizenId == 0) return await Task.FromResult<CitizenId?>(null);
        return new CitizenId(citizenId);
    }
    public async Task<CitizenId?> CreateCitizen(string firstName, string lastName, string email, string street, string number, string city, string postalCode, string country)
    {
        var citizenId = await citizensContextFacade.CreateCitizen(firstName, lastName, email, street, number, city,
            postalCode, country);
        if (citizenId == 0) return await Task.FromResult<CitizenId?>(null);
        return new CitizenId(citizenId);
    }
}
