using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PeaceApp.API.Communication.Domain.Model.Queries;
using PeaceApp.API.Communication.Domain.Services;
using PeaceApp.API.Communication.Interfaces.REST.Resources;
using PeaceApp.API.Communication.Interfaces.REST.Transform;

namespace PeaceApp.API.Communication.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class NotificationController(
    INotificationCommandService notificationCommandService,
    INotificationQueryService notificationQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateNotification([FromBody] CreateNotificationResource resource)
    {
        var createNotificationCommand = CreateNotificationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await notificationCommandService.Handle(createNotificationCommand);
        return CreatedAtAction(nameof(GetNotificationById), new { id = result.Id },
            NotificationResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetNotificationById(int id)
    {
        var getNotificationByIdQuery = new GetNotificationByIdQuery(id);
        var result = await notificationQueryService.Handle(getNotificationByIdQuery);
        var resource = NotificationResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet("Priority/{priority}")]
    public  async Task<ActionResult> GetAllNotificationsByPriority(string priority)
    {
        var getAllNotificationsByPriorityQuery = new GetAllNotificationsByPriorityQuery(priority);
        var result = await notificationQueryService.Handle(getAllNotificationsByPriorityQuery);
        var resources = result.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    [HttpGet]
    public async Task<ActionResult> GetAllNotifications()
    {
        var getAllNotificationsQuery = new GetAllNotificationsQuery();
        var notifications = await notificationQueryService.Handle(getAllNotificationsQuery);
        var notificationResources = notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(notificationResources);
    }
}