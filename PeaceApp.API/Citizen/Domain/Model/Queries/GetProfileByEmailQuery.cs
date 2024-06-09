using PeaceApp.API.Citizen.Domain.Model.ValueObjects;

namespace PeaceApp.API.Citizen.Domain.Model.Queries;

public record GetProfileByEmailQuery(EmailAddress Email);