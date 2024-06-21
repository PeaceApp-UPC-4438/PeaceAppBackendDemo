using System;

namespace PeaceApp.API.Citizen.Domain.Model.Commands
{
    public record UpdateCitizenAccountCommand
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Street { get; }
        public string Number { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public UpdateCitizenAccountCommand(int id, string firstName, string lastName, string email, string street, string number, string city, string postalCode, string country)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName), "First name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName), "Last name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            if (!email.Contains("@"))
                throw new ArgumentException("Email must contain an '@' symbol.", nameof(email));
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException(nameof(street), "Street cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number), "Number cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city), "City cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentNullException(nameof(postalCode), "Postal code cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country), "Country cannot be null or empty.");

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            Number = number;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
