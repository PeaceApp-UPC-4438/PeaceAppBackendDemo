namespace PeaceApp.API.Report.Interfaces.REST.Resources;

public record ReportResource(int Id, string KindOfReport, string District, string Location, string Description, DateTimeOffset? CreatedDate);