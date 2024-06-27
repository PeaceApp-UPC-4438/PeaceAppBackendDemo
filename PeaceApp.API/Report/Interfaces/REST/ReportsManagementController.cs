using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PeaceApp.API.Report.Domain.Model.Queries;
using PeaceApp.API.Report.Domain.Services;
using PeaceApp.API.Report.Interfaces.REST.Resources;
using PeaceApp.API.Report.Interfaces.REST.Transform;

namespace PeaceApp.API.Report.Interfaces.REST;


[ApiController]
[Route("api/v1/reports")]
[Produces(MediaTypeNames.Application.Json)]
public class ReportsManagementController(
    IReportManagementCommandService favoriteSourceCommandService,
    IReportManagementQueryService reportManagementQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportResource resource)
    {
        var createReportCommand = CreateReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await favoriteSourceCommandService.Handle(createReportCommand);
        var citizen = ReportResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetReportById), new { id = citizen.Id },
            ReportResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetReportById(int id)
    {
        var getReportByIdQuery = new GetReportByIdQuery(id);
        var result = await reportManagementQueryService.Handle(getReportByIdQuery);
        var resource = ReportResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet("{kindOfReport}")]
    private async Task<ActionResult> GetAllReportsByKindOfReport(string kindOfReport)
    {
        var getAllReportsByKindOfReportQuery = new GetAllReportsByKindOfReportQuery(kindOfReport);
        var result = await reportManagementQueryService.Handle(getAllReportsByKindOfReportQuery);
        var resources = result.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    [HttpGet("Date/{date}")]
    public  async Task<ActionResult> GetAllReportsByDate(string date)
    {
        var getAllReportsByDateQuery = new GetAllReportsByDateQuery(date);
        var result = await reportManagementQueryService.Handle(getAllReportsByDateQuery);
        var resources = result.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    [HttpGet("District/{district}")]
    public async Task<ActionResult> GetAllReportsByDistrict(string district)
    {
        var getAllReportsByDistrictQuery = new GetAllReportsByDistrictQuery(district);
        var result = await reportManagementQueryService.Handle(getAllReportsByDistrictQuery);
        var resources = result.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    
    
    
// Original
    private async Task<ActionResult> GetAllReportsByDistrictAndDate(string district, string date)
    {
        var getAllReportsByDistrictAndDateQuery = new GetAllReportsByDistrictAndDateQuery(district, date);
        var result = await reportManagementQueryService.Handle(getAllReportsByDistrictAndDateQuery);
        var resources = result.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    [HttpGet("Citizen/{citizenId}")]
    public async Task<ActionResult> GetAllReportsByCitizenId(int citizenId)
    {
        var getAllReportsByCitizenIdQuery = new GetAllReportsByCitizenIdQuery(citizenId);
        var result = await reportManagementQueryService.Handle(getAllReportsByCitizenIdQuery);
        var resources = result.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    [HttpGet("{citizenId},{id}")]
    public async Task<ActionResult> GetReportByIdAndCitizenId(int id, int citizenId)
    {
        var getReportByIdAndCitizenId = new GetReportByIdAndCitizenIdQuery(citizenId, id);
        var result = await reportManagementQueryService.Handle(getReportByIdAndCitizenId);
        var resource = ReportResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    [HttpGet]
    public async Task<ActionResult> GetAllReportsFromQuery([FromQuery]string district, [FromQuery] string date)
    {
        return string.IsNullOrEmpty(date)
            ? await GetAllReportsByDistrict(district)
            : await GetAllReportsByDistrictAndDate(district, date);
    }
}