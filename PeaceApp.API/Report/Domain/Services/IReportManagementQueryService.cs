using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Model.Queries;

namespace PeaceApp.API.Report.Domain.Services;

public interface IReportManagementQueryService
{
    Task<ReportManagement> Handle(GetReportByIdQuery query);
    Task<IEnumerable<ReportManagement>> Handle(GetAllReportsQuery query);
}