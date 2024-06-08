using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Communication.Domain.Model.Commands;
using PeaceApp.API.Communication.Domain.Repositories;
using PeaceApp.API.Communication.Domain.Services;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Communication.Application.Internal.CommandServices;

public class NotificationCommandService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    : INotificationCommandService
{
    public async Task<Notification> Handle(CreateNotificationCommand command)
    {
        var notification = new Notification(command);
        await notificationRepository.AddAsync(notification);
        await unitOfWork.CompleteAsync();
        return notification;
    }
}