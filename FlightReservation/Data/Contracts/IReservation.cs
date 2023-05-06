namespace FlightReservation.Data.Contracts
{
    public interface IReservation
    {
        public IFlight FlightInfo { get; set; }
        public DateTime FlightDate { get; set; }
        public string PNR { get; }

        public IEnumerable<IPassenger> Passengers { get; set; }

        IReservation FromBookingReference(string bookingReference);
    }
}
