using System;

namespace PeaceApp.API.Communication.Domain.Model.Queries
{
    public record GetNotificationByIdQuery
    {
        public int Id { get; }

        public GetNotificationByIdQuery(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be a positive integer.", nameof(id));
            }

            Id = id;
        }
    }
}