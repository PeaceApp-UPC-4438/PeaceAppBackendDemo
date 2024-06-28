namespace PeaceApp.API.Report.Domain.Model.Commands
{
    public record CreateReportCommand
    {
        public string Type { get; }
        public string Date { get; }
        public string Time { get; }
        public string District { get; }
        public string Location { get; }
        public string UrlEvidence { get; }
        public string Description { get; }
        public int CitizenId { get; }

        public CreateReportCommand(
            string type, 
            string date, 
            string time, 
            string district, 
            string location, 
            string urlEvidence, 
            string description, 
            int citizenId)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "Type cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(date))
                throw new ArgumentNullException(nameof(date), "Date cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(time))
                throw new ArgumentNullException(nameof(time), "Time cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(district))
                throw new ArgumentNullException(nameof(district), "District cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentNullException(nameof(location), "Location cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(urlEvidence))
                throw new ArgumentNullException(nameof(urlEvidence), "UrlEvidence cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
            if (citizenId <= 0)
                throw new ArgumentException("CitizenId must be a positive integer.", nameof(citizenId));

            Type = type;
            Date = date;
            Time = time;
            District = district;
            Location = location;
            UrlEvidence = urlEvidence;
            Description = description;
            CitizenId = citizenId;
        }
    }
}
