using System;
using PeaceApp.API.Citizen.Domain.Model.ValueObjects;

namespace PeaceApp.API.Citizen.Domain.Model.Queries
{
    public record GetCitizenByEmailQuery
    {
        public EmailAddress Email { get; }

        public GetCitizenByEmailQuery(EmailAddress email)
        {
            if (email == null || string.IsNullOrWhiteSpace(email.Address))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            }

            Email = email;
        }
    }
}