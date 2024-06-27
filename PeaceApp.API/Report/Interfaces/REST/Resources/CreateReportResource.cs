namespace PeaceApp.API.Report.Interfaces.REST.Resources;

public record CreateReportResource(string Type, string Date, string Time, string District, string Location, string Description, string UrlEvidence, int CitizenId);
