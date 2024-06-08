using PeaceApp.API.Communication.Domain.Model.Commands;

namespace PeaceApp.API.Communication.Domain.Model.Aggregates;

public partial class Notification
{
    public int Id { get; private set; }
    public string Message { get; private set; }
    public string Priority { get; private set; }

    protected Notification()
    {
        this.Message = string.Empty;
        this.Priority = string.Empty;
    }

    public Notification(CreateNotificationCommand command)
    {
        this.Message = command.Message;
        this.Priority = command.Priority;
    }
}