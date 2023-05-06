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

        void SetAirlineCodeError(string message);
        void SetFlightNumberError(string message);
        void SetArrivalStationError(string message);
        void SetDepartureStationError(string message);
        void SetArrivalScheduledTimeError(string message);
        void SetDepartureScheduledTimeError(string message);
        void AlertError(string header, string message);
    }
}
