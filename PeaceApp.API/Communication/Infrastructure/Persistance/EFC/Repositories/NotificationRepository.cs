using Microsoft.EntityFrameworkCore;
using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Communication.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PeaceApp.API.Communication.Infrastructure.Persistance.EFC.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notification>> FindAllByPriorityAsync(string priority)
    {
        return await Context.Set<Notification>().Where(n => n.Priority == priority)
            .ToListAsync();
    }
}