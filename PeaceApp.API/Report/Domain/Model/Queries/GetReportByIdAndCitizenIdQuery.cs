namespace PeaceApp.API.Report.Domain.Model.Queries;

public record GetReportByIdAndCitizenIdQuery(int CitizenId, int Id);