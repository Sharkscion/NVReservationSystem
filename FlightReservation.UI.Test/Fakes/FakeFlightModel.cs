using FlightReservation.Models.Contracts;

namespace FlightReservation.UI.Test.Fakes
{
    internal class FakeFlightModel : IFlight
    {
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureStation { get; set; }
        public TimeOnly ArrivalScheduledTime { get; set; }
        public TimeOnly DepartureScheduledTime { get; set; }
    }
}
