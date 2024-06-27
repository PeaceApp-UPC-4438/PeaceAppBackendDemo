using PeaceApp.API.Citizen.Domain.Repositories;
using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Model.Commands;
using PeaceApp.API.Report.Domain.Repositories;
using PeaceApp.API.Report.Domain.Services;
using PeaceApp.API.Shared.Domain.Repositories;

namespace PeaceApp.API.Report.Application.Internal.CommandServices;

public class ReportManagementCommandService(IReportManagementRepository reportManagementRepository, ICitizenRepository citizenRepository, IUnitOfWork unitOfWork)
    : IReportManagementCommandService
{


    public async Task<ReportManagement> Handle(CreateReportCommand command)
    {
        var reportManagement = new ReportManagement(command.Type, command.Date, command.Time, command.District, command.Location,command.Description,command.UrlEvidence,command.CitizenId);
        await reportManagementRepository.AddAsync(reportManagement);
        await unitOfWork.CompleteAsync();
        var citizen = await citizenRepository.FindByIdAsync(command.CitizenId);
        reportManagement.Citizen = citizen;
        return reportManagement;
    }
}