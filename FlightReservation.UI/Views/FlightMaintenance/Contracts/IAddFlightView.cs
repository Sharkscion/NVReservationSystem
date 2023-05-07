using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface IAddFlightView : IFormView
    {
        event InputChangedEventHandler<string> AirlineCodeChanged;
        event InputChangedEventHandler<int> FlightNumberChanged;
        event InputChangedEventHandler<string> ArrivalStationChanged;
        event InputChangedEventHandler<string> DepartureStationChanged;
        event InputChangedEventHandler<TimeOnly> ArrivalScheduledTimeChanged;
        event InputChangedEventHandler<TimeOnly> DepartureScheduledTimeChanged;
        event SubmitEventHandler<FlightEventArgs> Submitted;

        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public TimeOnly DepartureScheduledTime { get; set; }
        public TimeOnly ArrivalScheduledTime { get; set; }
    }
}
