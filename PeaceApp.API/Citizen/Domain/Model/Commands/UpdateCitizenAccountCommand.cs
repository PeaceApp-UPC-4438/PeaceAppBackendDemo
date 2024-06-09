namespace PeaceApp.API.Citizen.Domain.Model.Commands;

public record UpdateCitizenAccountCommand(int Id,string FirstName,string LastName,string Email,string Street,string Number,string City,string PostalCode,string Country);