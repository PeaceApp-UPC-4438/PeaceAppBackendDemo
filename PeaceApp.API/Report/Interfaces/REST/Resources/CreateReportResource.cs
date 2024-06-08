namespace PeaceApp.API.Report.Interfaces.REST.Resources;

public record CreateReportResource(string KindOfReport, string Date, string District, string Location, string Description);
