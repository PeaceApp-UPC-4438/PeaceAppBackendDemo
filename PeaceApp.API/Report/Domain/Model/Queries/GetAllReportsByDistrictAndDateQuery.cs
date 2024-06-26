namespace PeaceApp.API.Report.Domain.Model.Queries;

public record GetAllReportsByDistrictAndDateQuery(string District, DateTimeOffset Date);