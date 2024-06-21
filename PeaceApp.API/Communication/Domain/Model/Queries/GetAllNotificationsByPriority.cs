using System;

namespace PeaceApp.API.Communication.Domain.Model.Queries
{
    public record GetAllNotificationsByPriorityQuery
    {
        public string Priority { get; }

        public GetAllNotificationsByPriorityQuery(string priority)
        {
            if (string.IsNullOrWhiteSpace(priority))
            {
                throw new ArgumentNullException(nameof(priority), "Priority cannot be null or empty.");
            }

            Priority = priority;
        }
    }
}