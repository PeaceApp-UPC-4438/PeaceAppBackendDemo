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



    public int Id { get; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public StreetAddress Address { get; private set; }

    public string FullName => Name.FullName;

    public string EmailAddress => Email.Address;

    public string StreetAddress => Address.FullAddress;
}