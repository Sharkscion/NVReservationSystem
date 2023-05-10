using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.UI.Common
{
    internal class ReservationEventArgs : EventArgs
    {
        #region Properties
        public IFlight FlightInfo { get; set; }
        public DateTime FlightDate { get; set; }
        public IEnumerable<PassengerEventArgs> Passengers { get; set; }
        #endregion
    }
}
