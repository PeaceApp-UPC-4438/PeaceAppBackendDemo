namespace PeaceApp.API.Citizen.Domain.Model.ValueObjects
{
    public record PersonName(string FirstName, string LastName)
    {
        public PersonName() : this(string.Empty, string.Empty) { }

        public string FirstName { get; init; } = FirstName;
        public string LastName { get; init; } = LastName;

        public string FullName => $"{FirstName} {LastName}";
    }
}