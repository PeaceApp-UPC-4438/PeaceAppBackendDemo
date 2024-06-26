using PeaceApp.API.Report.Domain.Model.Commands;

namespace PeaceApp.API.Report.Domain.Model.Aggregates;

public partial class ReportManagement
{
    public int Id { get; private set; }
    public string KindOfReport { get; private set; }
    
    // This should be date type

    
    // Location should be a value object  with district and location
    public string District { get; private set; }
    public string Location { get; private set; }
    public string Description { get; private set; }

    protected ReportManagement()
    {
        this.KindOfReport = string.Empty;
        this.District = string.Empty;
        this.Location = string.Empty;
        this.Description = string.Empty;
    }

    public ReportManagement(CreateReportCommand command)
    {
        this.KindOfReport = command.KindOfReport;
        this.District = command.District;
        this.Location = command.Location;
        this.Description = command.Description;
    }
    
}