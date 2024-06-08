using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Model.Commands;

namespace PeaceApp.API.Report.Domain.Services;

public interface IReportManagementCommandService
{
    Task<ReportManagement> Handle(CreateReportCommand command);
}