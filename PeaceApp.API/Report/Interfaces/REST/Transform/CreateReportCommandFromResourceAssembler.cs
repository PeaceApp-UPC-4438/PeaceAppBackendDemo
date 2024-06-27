using PeaceApp.API.Report.Domain.Model.Commands;
using PeaceApp.API.Report.Interfaces.REST.Resources;

namespace PeaceApp.API.Report.Interfaces.REST.Transform;

public static class CreateReportCommandFromResourceAssembler
{
    public static CreateReportCommand ToCommandFromResource(CreateReportResource resource)
    {
        return new CreateReportCommand(
            resource.Type, 
            resource.Date, 
            resource.Time,
            resource.District, 
            resource.Location,
            resource.UrlEvidence,
            resource.Description,
            resource.CitizenId);
    }
}