using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Model.Queries;

namespace PeaceApp.API.Report.Domain.Services;

public interface IReportManagementQueryService
{
    Task<IEnumerable<ReportManagement>> Handle(GetAllReportsQuery query);
    Task<ReportManagement> Handle(GetReportByIdQuery query);
    Task<ReportManagement> Handle(GetReportByIdAndCitizenIdQuery query);
    Task<IEnumerable<ReportManagement>> Handle(GetAllReportsByCitizenIdQuery query);
    Task<IEnumerable<ReportManagement>> Handle(GetAllReportsByDateQuery query);
    Task<IEnumerable<ReportManagement>> Handle(GetAllReportsByDistrictAndDateQuery query);
    Task<IEnumerable<ReportManagement>> Handle(GetAllReportsByDistrictQuery query);
    Task<IEnumerable<ReportManagement>> Handle(GetAllReportsByKindOfReportQuery query);
}

