namespace PeaceApp.API.Citizen.Domain.Model.Commands
{
    public record DeleteCitizenAccountCommand
    {
        public int Id { get; }

        public DeleteCitizenAccountCommand(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));
                
            Id = id;
        }
    }
}