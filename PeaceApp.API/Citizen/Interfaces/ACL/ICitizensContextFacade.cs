namespace PeaceApp.API.Citizen.Interfaces.ACL;

public interface ICitizensContextFacade
{
    Task<int> CreateCitizen(string firstName, string lastName, string email, string street, string number, string city,
        string postalCode, string country);

    Task<int> FetchCitizenIdByEmail(string email);

    Task<bool> UpdateCitizen(int id, string firstName, string lastName, string email, string street, string number, string city,
        string postalCode, string country);

    Task<bool> DeleteCitizen(int id);
}