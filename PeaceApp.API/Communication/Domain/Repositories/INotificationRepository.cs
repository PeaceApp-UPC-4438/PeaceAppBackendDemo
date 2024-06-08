using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Communication.Domain.Repositories;

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<IEnumerable<Notification>> FindAllByPriorityAsync(string priority);
}