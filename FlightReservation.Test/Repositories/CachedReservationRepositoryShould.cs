using FlightReservation.Models.Contracts;
using FlightReservation.Models.Flight;
using FlightReservation.Models.Passenger;
using FlightReservation.Models.Reservation;
using FlightReservation.Repositories;

namespace FlightReservation.Test.Repositories
{
    public class CachedReservationRepositoryShould
    {
        [Fact]
        public void Save_CreatedReservation()
        {
            // Arrange
            var flight = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
            };

            var passengers = new List<IPassenger>
            {
                new PassengerModel("FirstName", "LastName", new DateTime(1996, 2, 29))
            };

            var reservation = new ReservationModel(
                bookingReference: "ABC123",
                flightDate: DateTime.Now,
                flightInfo: flight,
                passengers: passengers
            );

            var repository = new CachedReservationRepository();

            // Act
            bool successful = repository.Save(reservation);

            // Assert
            Assert.True(successful);
        }

        [Fact]
        public void Return_AllReservationsInMemory()
        {
            // Arrange
            var flight = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
            };

            var passengers = new List<IPassenger>
            {
                new PassengerModel("FirstName", "LastName", new DateTime(1996, 2, 29))
            };

            var repository = new CachedReservationRepository();

            int count = 4;
            while (count > 0)
            {
                var reservation = new ReservationModel(
                    bookingReference: $"ABC12{count}",
                    flightDate: DateTime.Now,
                    flightInfo: flight,
                    passengers: passengers
                );

                repository.Save(reservation);

                count--;
            }

            // Act
            IEnumerable<IReservation> reservations = repository.List();

            // Assert
            Assert.Equal(4, reservations.Count());
            Assert.Contains(reservations, item => item.PNR == "ABC123");
        }
    }
}
