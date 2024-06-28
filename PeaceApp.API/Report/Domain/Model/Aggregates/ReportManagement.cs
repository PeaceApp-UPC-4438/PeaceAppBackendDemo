using PeaceApp.API.Report.Domain.Model.Commands;

namespace PeaceApp.API.Report.Domain.Model.Aggregates
{
    public partial class ReportManagement
    {
        public int Id { get; }
        public string Type { get; private set; }
        public string Date { get; private set; }
        public string Time { get; private set; }
        public string District { get; private set; }
        public string Location { get; private set; }
        public string Description { get; private set; }
        public string UrlEvidence { get; private set; }
        public Citizen.Domain.Model.Aggregates.Citizen Citizen { get; internal set; }
        public int CitizenId { get; private set; }

        protected ReportManagement()
        {
            Type = string.Empty;
            Date = string.Empty;
            Time = string.Empty;
            District = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            UrlEvidence = string.Empty;
            Citizen = new Citizen.Domain.Model.Aggregates.Citizen();
            CitizenId = int.MinValue;
        }

        public ReportManagement(string type, string date, string time, string district, string location, string description, string urlEvidence, int citizenId)
        {
            Type = type;
            Date = date;
            Time = time;
            District = district;
            Location = location;
            Description = description;
            UrlEvidence = urlEvidence;
            CitizenId = citizenId;
        }

        public ReportManagement(CreateReportCommand command)
        {
            Type = command.Type;
            Date = command.Date;
            Time = command.Time;
            District = command.District;
            Location = command.Location;
            Description = command.Description;
            UrlEvidence = command.UrlEvidence;
            CitizenId = command.CitizenId;
        }
    }
}