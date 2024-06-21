namespace PeaceApp.API.Citizen.Domain.Model.ValueObjects
{
    public record StreetAddress(string Street, string Number, string City, string PostalCode, string Country)
    {
        public StreetAddress() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public string Street { get; init; } = Street;
        public string Number { get; init; } = Number;
        public string City { get; init; } = City;
        public string PostalCode { get; init; } = PostalCode;
        public string Country { get; init; } = Country;

        public string FullAddress => $"{Street} {Number}, {City}, {PostalCode}, {Country}";
    }
}