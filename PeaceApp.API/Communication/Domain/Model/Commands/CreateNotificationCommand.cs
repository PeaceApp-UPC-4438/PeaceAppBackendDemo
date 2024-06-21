using System;

namespace PeaceApp.API.Communication.Domain.Model.Commands
{
    public record CreateNotificationCommand
    {
        public string Message { get; }
        public string Priority { get; }

        public CreateNotificationCommand(string message, string priority)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Message cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(priority))
            {
                throw new ArgumentNullException(nameof(priority), "Priority cannot be null or empty.");
            }

            Message = message;
            Priority = priority;
        }
    }
}