using PeaceApp.API.Report.Domain.Model.Commands;

namespace PeaceApp.API.Report.Domain.Model.Aggregates;

public partial class ReportManagement
{
    public int Id { get;}
    public string Type { get; private set; }
    public string Date { get; private set; }
    public string Time { get; private set; }
    public string District { get; private set; }
    public string Location { get; private set; }
    public string Description { get; private set; }
    public string UrlEvidence { get; private set; }
    public Citizen.Domain.Model.Aggregates.Citizen Citizen { get; internal set; }
    public int CitizenId { get; private set; }

    protected ReportManagement()
    {
        Type = string.Empty;
        this.Date = string.Empty;
        this.Time = string.Empty;
        this.District = string.Empty;
        this.Location = string.Empty;
        this.Description = string.Empty;
        this.UrlEvidence = string.Empty;
        this.Citizen = new Citizen.Domain.Model.Aggregates.Citizen();
        this.CitizenId = int.MinValue;
        
    }

    public ReportManagement(CreateReportCommand command)
    {
        this.Type = command.Type;
        this.Date = command.Date;
        this.Time = command.Time;
        this.District = command.District;
        this.Location = command.Location;
        this.Description = command.Description;
        this.UrlEvidence = command.UrlEvidence;
        this.Citizen = new Citizen.Domain.Model.Aggregates.Citizen();
        this.CitizenId = command.CitizenId;
    }
}