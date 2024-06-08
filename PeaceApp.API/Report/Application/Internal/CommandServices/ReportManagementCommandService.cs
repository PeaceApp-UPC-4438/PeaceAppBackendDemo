using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Model.Commands;
using PeaceApp.API.Report.Domain.Repositories;
using PeaceApp.API.Report.Domain.Services;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Report.Application.Internal.CommandServices;

public class ReportManagementCommandService(IReportManagementRepository reportManagementRepository, IUnitOfWork unitOfWork)
    : IReportManagementCommandService
{


    public async Task<ReportManagement> Handle(CreateReportCommand command)
    {
        var reportManagement = new ReportManagement(command);
        await reportManagementRepository.AddAsync(reportManagement);
        await unitOfWork.CompleteAsync();
        return reportManagement;
    }
}