namespace PeaceApp.API.Citizen.Domain.Model.Commands;

public record CreateCitizenAccountCommand(string FirstName, string LastName, string Email, string Street, string Number, string City, string PostalCode, string Country);