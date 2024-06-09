using PeaceApp.API.Citizen.Domain.Model.Commands;
using PeaceApp.API.Citizen.Interfaces.REST.Resources;

namespace PeaceApp.API.Citizen.Interfaces.REST.Transform;

public static class UpdateCitizenCommandFromResourceAssembler
{
    public static UpdateCitizenAccountCommand ToCommandFromResource(int id, UpdateCitizenResource resource)
    {
        return new UpdateCitizenAccountCommand(id,resource.FirstName,resource.LastName,resource.Email,resource.Street,resource.Number,resource.City,resource.PostalCode,resource.Country);
    }
}