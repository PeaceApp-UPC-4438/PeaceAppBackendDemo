using PeaceApp.API.Report.Domain.Model.Commands;
using PeaceApp.API.Report.Interfaces.REST.Resources;

namespace PeaceApp.API.Report.Interfaces.REST.Transform;

public static class CreateReportCommandFromResourceAssembler
{
    public static CreateReportCommand ToCommandFromResource(CreateReportResource resource)
    {
        return new CreateReportCommand(resource.KindOfReport, resource.Date, resource.District, resource.Location,
            resource.Description);
        
    }
}