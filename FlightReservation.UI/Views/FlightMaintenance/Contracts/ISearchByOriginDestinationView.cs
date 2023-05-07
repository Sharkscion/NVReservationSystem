using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByOriginDestinationView : IFormView
    {
        event InputChangedEventHandler<string> DepartureStationChanged;
        event InputChangedEventHandler<string> ArrivalStationChanged;
        event SubmitEventHandler<SubmitEventArgs<Tuple<string, string>>> Submitted;

        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }

        void Display(IEnumerable<IFlight> flights);
    }
}
