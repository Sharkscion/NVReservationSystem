using FlightReservation.Data.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByFlightNumberView : IFormView
    {
        event InputChangedEventHandler<int> FlightNumberChanged;
        event SubmitEventHandler<SubmitEventArgs<int>> Submitted;
        void Display(IEnumerable<IFlight> flights);

        void SetFlightNumberError(string message);
    }
}
