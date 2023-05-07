using FlightReservation.Models.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface ISearchReservationView : IFormView
    {
        event InputChangedEventHandler<string> PNRChanged;
        event SubmitEventHandler<SubmitEventArgs<string>> Submitted;

        public string PNR { get; set; }
        void DisplayReservation(IReservation? reservation);
    }
}
