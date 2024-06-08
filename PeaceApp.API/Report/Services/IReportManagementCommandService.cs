using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Model.Commands;

namespace PeaceApp.API.Report.Services;

public interface IReportManagementCommandService
{
    Task<ReportManagement> Handle(CreateReportCommand command);
     
}