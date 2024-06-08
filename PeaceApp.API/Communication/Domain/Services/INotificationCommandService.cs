using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Communication.Domain.Model.Commands;

namespace PeaceApp.API.Communication.Domain.Services;

public interface INotificationCommandService
{
    Task<Notification> Handle(CreateNotificationCommand command);
}