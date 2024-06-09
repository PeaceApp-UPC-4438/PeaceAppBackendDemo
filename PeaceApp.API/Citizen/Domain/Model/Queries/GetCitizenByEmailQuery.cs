using PeaceApp.API.Citizen.Domain.Model.ValueObjects;

namespace PeaceApp.API.Citizen.Domain.Model.Queries;

public record GetCitizenByEmailQuery(EmailAddress Email);