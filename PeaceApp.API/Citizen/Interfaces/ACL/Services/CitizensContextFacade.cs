using PeaceApp.API.Citizen.Interfaces.ACL;
using PeaceApp.API.Citizen.Domain.Model.Commands;
using PeaceApp.API.Citizen.Domain.Model.Queries;
using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Citizen.Domain.Services;
using PeaceApp.API.Citizen.Domain.Model.ValueObjects;
using PeaceApp.API.Citizen.Domain.Services;

namespace PeaceApp.API.Citizen.Interfaces.ACL.Services;

/**
 * Profiles context facade.
 *
 * <summary>
 * This class represents the profiles context facade, part of the profiles anti-corruption layer.
 * It contains the methods to interact with the profiles context from other bounded context.
 * </summary>
 */
public class CitizensContextFacade(ICitizenCommandService citizenCommandService, ICitizenQueryService citizenQueryService) : ICitizensContextFacade
{
    /**
     * Create a profile.
     *
     * <param name="firstName">The first name of the profile</param>
     * <param name="lastName">The last name of the profile</param>
     * <param name="email">The email of the profile</param>
     * <param name="street">The street of the profile</param>
     * <param name="number">The number of the profile</param>
     * <param name="city">The city of the profile</param>
     * <param name="postalCode">The postal code of the profile</param>
     * <param name="country">The country of the profile</param>
     * <returns>The profile id</returns>
     * 
     */
    public async Task<int> CreateCitizen(string firstName, string lastName, string email, string street, string number, string city, string postalCode, string country)
    {
        var createCitizenCommand = new CreateCitizenAccountCommand(firstName, lastName, email, street, number, city, postalCode, country);
        var citizen = await citizenCommandService.Handle(createCitizenCommand);
        return citizen?.Id ?? 0;
    }
    
    /**
     * Fetch a profile id by email.
     *
     * <param name="email">The email of the profile</param>
     * <returns>The profile id</returns>
     * 
     */
    public async Task<int> FetchCitizenIdByEmail(string email)
    {
        var getCitizenByEmailQuery = new GetCitizenByEmailQuery(new EmailAddress(email));
        var citizen = await citizenQueryService.Handle(getCitizenByEmailQuery);
        return citizen?.Id ?? 0;
    }
}