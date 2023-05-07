using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.FlightMaintenance.Contracts
{
    internal interface ISearchByAirlineCodeView : IFormView
    {
        event InputChangedEventHandler<string> AirlineCodeChanged;
        event SubmitEventHandler<SubmitEventArgs<string>> Submitted;
        void Display(IEnumerable<IFlight> flights);
        void SetAirlineCodeError(string message);
    }
}
