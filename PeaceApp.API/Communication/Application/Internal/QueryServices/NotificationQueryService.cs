using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Communication.Domain.Model.Queries;
using PeaceApp.API.Communication.Domain.Repositories;
using PeaceApp.API.Communication.Domain.Services;

namespace PeaceApp.API.Communication.Application.Internal.QueryServices;

public class NotificationQueryService(INotificationRepository notificationRepository)
    : INotificationQueryService
{


    public async Task<Notification> Handle(GetNotificationByIdQuery query)
    {
        return await notificationRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsByPriorityQuery query)
    {
        return await notificationRepository.FindAllByPriorityAsync(query.Priority);
    }
    public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query)
    {
        return await notificationRepository.ListAsync();
    }
}