using Microsoft.EntityFrameworkCore;
using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Repositories;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PeaceApp.API.Report.Infrastructure.Persistance.EFC.Repositories;

public class ReportManagementRepository : BaseRepository<ReportManagement>, IReportManagementRepository
{
    public ReportManagementRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ReportManagement>> FindAllByDistrictAsync(string district)
    {
        return await Context.Set<ReportManagement>().Where(f => f.District == district)
            .ToListAsync();
    }

    public async Task<IEnumerable<ReportManagement>> FindAllByKindOfReportAsync(string kindOfReport)
    {
        return await Context.Set<ReportManagement>().Where(f => f.KindOfReport == kindOfReport)
            .ToListAsync();
    }

    // OJITO CON LOS DOS QUE VIENEN
    public async Task<IEnumerable<ReportManagement>> FindAllByDateAsync(DateTimeOffset date)
    {
        return await Context.Set<ReportManagement>()
            .Where(f => f.CreatedDate.HasValue && f.CreatedDate.Value.Date == date.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<ReportManagement>> FindAllByDistrictAndDateAsync(string district, DateTimeOffset date)
    {
        return await Context.Set<ReportManagement>().Where(f => f.District == district && f.CreatedDate == date)
            .ToListAsync();
    }
}