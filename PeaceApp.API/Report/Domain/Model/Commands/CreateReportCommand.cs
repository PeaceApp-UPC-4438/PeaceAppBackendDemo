namespace PeaceApp.API.Report.Domain.Model.Commands;

public record CreateReportCommand(string KindOfReport, string Date, string District, string Location, string Description);