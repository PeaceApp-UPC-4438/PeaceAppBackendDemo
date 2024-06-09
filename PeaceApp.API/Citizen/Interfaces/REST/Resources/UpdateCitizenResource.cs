namespace PeaceApp.API.Citizen.Interfaces.REST.Resources;

public record UpdateCitizenResource(string FirstName, string LastName, string Email, string Street, string Number, string City, string PostalCode, string Country);