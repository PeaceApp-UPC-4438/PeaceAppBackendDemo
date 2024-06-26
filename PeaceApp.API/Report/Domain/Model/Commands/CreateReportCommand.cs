namespace PeaceApp.API.Report.Domain.Model.Commands;

public record CreateReportCommand(string KindOfReport, string District, string Location, string Description);