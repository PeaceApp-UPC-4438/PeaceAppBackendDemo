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
    [HttpGet]
    public async Task<ActionResult> GetAllReports()
    {
        var getAllReportsQuery = new GetAllReportsQuery();
        var result = await reportManagementQueryService.Handle(getAllReportsQuery);
        var resources = result.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetReportById(int id)
    {
        var getReportByIdQuery = new GetReportByIdQuery(id);
        var result = await reportManagementQueryService.Handle(getReportByIdQuery);
        var resource = ReportResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
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

}