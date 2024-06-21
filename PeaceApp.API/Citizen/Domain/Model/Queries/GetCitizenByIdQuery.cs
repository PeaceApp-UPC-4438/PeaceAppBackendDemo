using System;

namespace PeaceApp.API.Citizen.Domain.Model.Queries
{
    public record GetCitizenByIdQuery
    {
        public int ProfileId { get; }

        public GetCitizenByIdQuery(int profileId)
        {
            if (profileId <= 0)
            {
                throw new ArgumentException("ProfileId must be a positive integer.", nameof(profileId));
            }

            ProfileId = profileId;
        }
    }
}