using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PeaceApp.API.Organization.Domain.Model.Queries;
using PeaceApp.API.Organization.Domain.Services;
using PeaceApp.API.Organization.Interfaces.REST.Resources;
using PeaceApp.API.Organization.Interfaces.REST.Transform;

namespace PeaceApp.API.Organization.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class OrganizationsAccountController(
    IOrganizationAccountCommandService organizationAccountCommandService,
    IOrganizationAccountQueryService organizationAccountQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateOrganizationAccount(
        [FromBody] CreateOrganizationAccountResource resource)
    {
        var createOrganizationAccountCommand =
            CreateOrganizationAccountCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await organizationAccountCommandService.Handle(createOrganizationAccountCommand);
        return CreatedAtAction(nameof(GetOrganizationAccountByOrganizationName),
            new { organizationName = result.OrganizationName },
            OrganizationAccountResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{organizationName}")]
    public async Task<ActionResult> GetOrganizationAccountByOrganizationName(string organizationName)
    {
        var getOrganizationAccountByOrganizationName = new GetOrganizationAccountByOrganizationNameQuery(organizationName);
        var result =await organizationAccountQueryService.Handle(getOrganizationAccountByOrganizationName);
        var resource = OrganizationAccountResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
} 