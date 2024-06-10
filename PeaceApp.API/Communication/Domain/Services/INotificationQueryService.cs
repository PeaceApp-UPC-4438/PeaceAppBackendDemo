using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Communication.Domain.Model.Queries;

namespace PeaceApp.API.Communication.Domain.Services;

public interface INotificationQueryService
{
    Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query);
    Task<Notification> Handle(GetNotificationByIdQuery query);
    Task<IEnumerable<Notification>> Handle(GetAllNotificationsByPriorityQuery query);
}