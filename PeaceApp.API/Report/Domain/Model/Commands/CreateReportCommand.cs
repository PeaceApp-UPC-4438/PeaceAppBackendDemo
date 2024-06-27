namespace PeaceApp.API.Report.Domain.Model.Commands;

public record CreateReportCommand(string Type, string Date, string Time, string District, string Location, string UrlEvidence, string Description, int CitizenId);