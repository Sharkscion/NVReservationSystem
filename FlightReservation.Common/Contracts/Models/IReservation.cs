namespace FlightReservation.Common.Contracts.Models
{
    public interface IReservation
    {
        #region Properties
        public IFlight FlightInfo { get; set; }
        public DateTime FlightDate { get; set; }
        public string PNR { get; }
        public IEnumerable<IPassenger> Passengers { get; set; }
        #endregion

        #region Functions
        IReservation CreateWith(string bookingReference);

        IReservation CreateFrom(
            DateTime flightDate,
            IFlight flightInfo,
            IEnumerable<IPassenger> passengers
        );
        #endregion
    }
}
