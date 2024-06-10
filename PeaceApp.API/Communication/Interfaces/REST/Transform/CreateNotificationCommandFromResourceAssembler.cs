using PeaceApp.API.Communication.Domain.Model.Commands;
using PeaceApp.API.Communication.Interfaces.REST.Resources;

namespace PeaceApp.API.Communication.Interfaces.REST.Transform;

public static class CreateNotificationCommandFromResourceAssembler
{
    public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
    {
        return new CreateNotificationCommand(resource.Message, resource.Priority);
        
    }
}