namespace PeaceApp.API.Report.Interfaces.REST.Resources;

public record CreateReportResource(string KindOfReport, string District, string Location, string Description);
