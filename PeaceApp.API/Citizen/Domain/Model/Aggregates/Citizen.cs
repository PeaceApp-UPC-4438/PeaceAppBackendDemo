using PeaceApp.API.Citizen.Domain.Model.Commands;
using PeaceApp.API.Citizen.Domain.Model.ValueObjects;

namespace PeaceApp.API.Citizen.Domain.Model.Aggregates;

public partial class Citizen
{
    public Citizen()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Address = new StreetAddress();
    }

    public Citizen(string firstName, string lastName, string email, string street, string number, string city,
        string postalCode, string country)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Address = new StreetAddress(street, number, city, postalCode, country);
    }

    public Citizen(CreateCitizenAccountCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Address = new StreetAddress(command.Street, command.Number, command.City, command.PostalCode, command.Country);
    }

    public int Id { get; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public StreetAddress Address { get; private set; }

    public string FullName => Name.FullName;

    public string EmailAddress => Email.Address;

    public string StreetAddress => Address.FullAddress;
    public void UpdateName(string firstName, string lastName)
    {
        Name = new PersonName(firstName, lastName);
    }

    public void UpdateEmail(string email)
    {
        Email = new EmailAddress(email);
    }

    public void UpdateAddress(string street, string number, string city, string postalCode, string country)
    {
        Address = new StreetAddress(street, number, city, postalCode, country);
    }
}