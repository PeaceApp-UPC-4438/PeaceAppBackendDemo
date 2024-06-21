public record EmailAddress
{
    public string Address { get; private set; }

    public EmailAddress(string address)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }

    // Default constructor for EF Core
    public EmailAddress() { }
}