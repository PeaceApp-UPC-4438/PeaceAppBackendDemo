using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PeaceApp.API.Report.Domain.Model.Queries;
using PeaceApp.API.Report.Domain.Services;
using PeaceApp.API.Report.Interfaces.REST.Resources;
using PeaceApp.API.Report.Interfaces.REST.Transform;

namespace PeaceApp.API.Report.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ReportsManagementController(
    IReportManagementCommandService favoriteSourceCommandService,
    IReportManagementQueryService reportManagementQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateReport([FromBody] CreateReportResource resource)
    {
        var createReportCommand = CreateReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await favoriteSourceCommandService.Handle(createReportCommand);
        return CreatedAtAction(nameof(GetReportById), new { id = result.Id },
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
    
    [HttpGet("Kind/{kindOfReport}")]
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
    
    
    // Esta, pero se puede cambiar
    
    [HttpGet]
    public async Task<ActionResult> GetAllReportsFromQuery([FromQuery]string district, [FromQuery] string date)
    {
        return string.IsNullOrEmpty(date)
            ? await GetAllReportsByDistrict(district)
            : await GetAllReportsByDistrictAndDate(district, date);
    }
}