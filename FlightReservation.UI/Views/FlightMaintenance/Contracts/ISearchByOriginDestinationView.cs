using FlightReservation.Data.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByOriginDestinationView : IFormView
    {
        event InputChangedEventHandler<string> DepartureStationChanged;
        event InputChangedEventHandler<string> ArrivalStationChanged;
        event SubmitEventHandler<SubmitEventArgs<Tuple<string, string>>> Submitted;
        void Display(IEnumerable<IFlight> flights);
        void SetDepartureStationError(string message);
        void SetArrivalStationError(string message);
    }
}
