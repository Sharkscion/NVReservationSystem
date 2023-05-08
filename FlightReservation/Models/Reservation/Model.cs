using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Validators;

namespace FlightReservation.Models.Reservation
{
    public class ReservationModel : IReservation
    {
        private const int MAX_PASSENGER_COUNT = 5;

        private IEnumerable<IPassenger> _passengers;
        private DateTime _flightDate;

        public IFlight FlightInfo { get; set; }
        public DateTime FlightDate
        {
            get { return _flightDate; }
            set
            {
                if (!ReservationValidator.IsFlightDateValid(value))
                {
                    throw new InvalidFlightDateException("Flight date must not be past-dated.");
                }

                _flightDate = value;
            }
        }

        public string PNR { get; }

        public IEnumerable<IPassenger> Passengers
        {
            get { return _passengers; }
            set
            {
                if (value.Count() == 0)
                {
                    throw new NoPassengersException("Reservation must have at least 1 passenger.");
                }

                if (value.Count() > MAX_PASSENGER_COUNT)
                {
                    throw new MoreThanMaxPassengerCountException(
                        $"Up to {MAX_PASSENGER_COUNT} passengers are allowed per booking."
                    );
                }

                _passengers = value;
            }
        }

        public ReservationModel() { }

        public ReservationModel(
            IFlight flightInfo,
            DateTime flightDate,
            IEnumerable<IPassenger> passengers
        )
        {
            FlightInfo = flightInfo;
            FlightDate = flightDate;
            Passengers = passengers;
        }

        public ReservationModel(
            string bookingReference,
            IFlight flightInfo,
            DateTime flightDate,
            IEnumerable<IPassenger> passengers
        )
            : this(flightInfo, flightDate, passengers)
        {
            if (!ReservationValidator.IsBookingReferenceFormatValid(bookingReference))
            {
                throw new InvalidPNRException(
                    "PNR must be exactly 6 uppercased-alphanumeric characters "
                        + "with the first character being a letter."
                );
            }

            PNR = bookingReference;
        }

        public IReservation CreateWith(string bookingReference)
        {
            return new ReservationModel(bookingReference, FlightInfo, FlightDate, Passengers);
        }

        public IReservation CreateFrom(
            DateTime flightDate,
            IFlight flightInfo,
            IEnumerable<IPassenger> passengers
        )
        {
            return new ReservationModel(flightInfo, flightDate, passengers);
        }
    }
}
