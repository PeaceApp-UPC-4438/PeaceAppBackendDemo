using PeaceApp.API.Citizen.Domain.Model.Commands;
using PeaceApp.API.Citizen.Domain.Model.Queries;
using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Citizen.Domain.Services;

namespace PeaceApp.API.Citizen.Interfaces.ACL.Services;

public class CitizensContextFacade(ICitizenCommandService citizenCommandService, ICitizenQueryService citizenQueryService) : ICitizensContextFacade
{
    public async Task<int> CreateCitizen(string firstName, string lastName, string email, string street, string number, string city, string postalCode, string country)
    {
        var createCitizenCommand = new CreateCitizenAccountCommand(firstName, lastName, email, street, number, city, postalCode, country);
        var citizen = await citizenCommandService.Handle(createCitizenCommand);
        return citizen?.Id ?? 0;
    }
    
    public async Task<int> FetchCitizenIdByEmail(string email)
    {
        var getCitizenByEmailQuery = new GetCitizenByEmailQuery(new EmailAddress(email));
        var citizen = await citizenQueryService.Handle(getCitizenByEmailQuery);
        return citizen?.Id ?? 0;
    }

    public async Task<bool> UpdateCitizen(int id, string firstName, string lastName, string email, string street, string number, string city, string postalCode, string country)
    {
        var updateCitizenCommand = new UpdateCitizenAccountCommand(id, firstName, lastName, email, street, number, city, postalCode, country);
        var updatedCitizen = await citizenCommandService.Handle(updateCitizenCommand);
        return updatedCitizen != null;
    }

    public async Task<bool> DeleteCitizen(int id)
    {
        var deleteCitizenCommand = new DeleteCitizenAccountCommand(id);
        return await citizenCommandService.Handle(deleteCitizenCommand);
    }
}