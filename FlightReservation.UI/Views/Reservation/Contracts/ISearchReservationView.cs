using FlightReservation.Common.Contracts.Models;
using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views.Reservation.Contracts
{
    internal interface ISearchReservationView : IFormView
    {
        event EventHandler PNRChanged;
        event EventHandler Submitted;

        public string PNR { get; set; }
        void DisplayReservation(IReservation reservation);
        void DisplayNoReservation();
    }
}
