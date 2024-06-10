namespace PeaceApp.API.Communication.Domain.Model.Commands;

public record CreateNotificationCommand(string Message, string Priority);