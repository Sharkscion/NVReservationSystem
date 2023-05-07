using FlightReservation.Models.Contracts;
using FlightReservation.Models.Flight;
using FlightReservation.Models.Passenger;
using FlightReservation.Models.Reservation;
using FlightReservation.Repositories.Contracts;

namespace FlightReservation.Test.Services.Fixtures
{
    internal class FakeReservationRepository : IReservationRepository, IDisposable
    {
        private readonly HashSet<IReservation> _reservations;

        public FakeReservationRepository()
        {
            var flight = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
                DepartureScheduledTime = new TimeOnly(hour: 8, minute: 0),
                ArrivalScheduledTime = new TimeOnly(hour: 9, minute: 0)
            };

            var passengers = new List<IPassenger>
            {
                new PassengerModel("Maria", "Cruz", new DateTime(1990, 1, 1)),
                new PassengerModel("Juan", "Cruz", new DateTime(1990, 1, 1))
            };

            _reservations = new HashSet<IReservation>
            {
                new ReservationModel(
                    bookingReference: "ABC123",
                    flightInfo: flight,
                    flightDate: DateTime.Now,
                    passengers
                ),
                new ReservationModel(
                    bookingReference: "DEF123",
                    flightInfo: flight,
                    flightDate: DateTime.Now.AddDays(5),
                    passengers
                )
            };
        }

        public void Dispose()
        {
            _reservations.Clear();
        }

        public IQueryable<IReservation> List()
        {
            return _reservations.AsQueryable();
        }

        public bool Save(IReservation item)
        {
            _reservations.Add(item);
            return true;
        }
    }
}
