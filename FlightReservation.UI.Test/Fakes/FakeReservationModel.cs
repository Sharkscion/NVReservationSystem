using FlightReservation.Models.Contracts;

namespace FlightReservation.UI.Test.Fakes
{
    internal class FakeReservationModel : IReservation
    {
        public IFlight FlightInfo { get; set; }
        public DateTime FlightDate { get; set; }

        public string PNR { get; }

        public IEnumerable<IPassenger> Passengers { get; set; }

        public FakeReservationModel() { }

        public FakeReservationModel(string bookingReference)
        {
            PNR = bookingReference;
        }

        public IReservation FromBookingReference(string bookingReference)
        {
            return new FakeReservationModel(bookingReference);
        }
    }
}
