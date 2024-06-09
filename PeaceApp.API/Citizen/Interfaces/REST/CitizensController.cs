using System.Net.Mime;
using PeaceApp.API.Citizen.Domain.Model.Queries;
using PeaceApp.API.Citizen.Domain.Services;
using PeaceApp.API.Citizen.Interfaces.REST.Resources;
using PeaceApp.API.Citizen.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using PeaceApp.API.Citizen.Domain.Model.Commands;

namespace PeaceApp.API.Citizen.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CitizensController(ICitizenCommandService citizenCommandService, ICitizenQueryService citizenQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCitizen(CreateCitizenResource resource)
    {
        var createCitizenCommand = CreateCitizenCommandFromResourceAssembler.ToCommandFromResource(resource);
        var citizen = await citizenCommandService.Handle(createCitizenCommand);
        if (citizen is null) return BadRequest();
        var citizenResource = CitizenResourceFromEntityAssembler.ToResourceFromEntity(citizen);
        return CreatedAtAction(nameof(GetCitizenById), new { citizenId = citizenResource.Id }, citizenResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCitizens()
    {
        var getAllCitizensQuery = new GetAllCitizensQuery();
        var citizens = await citizenQueryService.Handle(getAllCitizensQuery);
        var citizenResources = citizens.Select(CitizenResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(citizenResources);
    }

    [HttpGet("{citizenId:int}")]
    public async Task<IActionResult> GetCitizenById(int citizenId)
    {
        var getCitizenByIdQuery = new GetCitizenByIdQuery(citizenId);
        var citizen = await citizenQueryService.Handle(getCitizenByIdQuery);
        if (citizen == null) return NotFound();
        var citizenResource = CitizenResourceFromEntityAssembler.ToResourceFromEntity(citizen);
        return Ok(citizenResource);
    }
    [HttpPut("{citizenId:int}")]
    public async Task<IActionResult> UpdateCitizen(int citizenId, UpdateCitizenResource resource)
    {
        var updateCitizenCommand = UpdateCitizenCommandFromResourceAssembler.ToCommandFromResource(citizenId, resource);
        var updatedCitizen = await citizenCommandService.Handle(updateCitizenCommand);
        if (updatedCitizen == null) return NotFound();
        var citizenResource = CitizenResourceFromEntityAssembler.ToResourceFromEntity(updatedCitizen);
        return Ok(citizenResource);
    }

    [HttpDelete("{citizenId:int}")]
    public async Task<IActionResult> DeleteCitizen(int citizenId)
    {
        var deleteCitizenCommand = new DeleteCitizenAccountCommand(citizenId);
        var result = await citizenCommandService.Handle(deleteCitizenCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}