using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Communication.Interfaces.REST.Resources;

namespace PeaceApp.API.Communication.Interfaces.REST.Transform;

public static class NotificationResourceFromEntityAssembler
{
    public static NotificationResource ToResourceFromEntity(Notification entity)
    {
        return new NotificationResource(entity.Id, entity.Message,entity.Priority);
    }
}