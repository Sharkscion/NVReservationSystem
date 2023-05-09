using FlightReservation.Models.Flight;
using FlightReservation.Models.Passenger;
using FlightReservation.Models.Reservation;

namespace FlightReservation.Test.Models
{
    public class ReservationModelShould
    {
        #region Data Generators
        public static IEnumerable<object[]> GetValidFlightDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddDays(1) };
        }
        #endregion

        #region Test Methods
        [Fact]
        public void RaiseError_WhenInvalidFlightDate()
        {
            var model = new ReservationModel();

            Action action = () => model.FlightDate = DateTime.Now.AddDays(-1);

            Assert.Throws<InvalidFlightDateException>(action);
        }

        [Theory]
        [MemberData(nameof(GetValidFlightDates))]
        public void SetFlightDate_WhenValid(DateTime value)
        {
            var model = new ReservationModel();

            model.FlightDate = value;

            Assert.Equal(value, model.FlightDate);
        }

        [Fact]
        public void RaiseError_WhenMoreThanMaxPassengerCount()
        {
            var passengers = generatePassengers(count: 6);
            var model = new ReservationModel();

            Action action = () => model.Passengers = passengers;

            Assert.Throws<MoreThanMaxPassengerCountException>(action);
        }

        [Fact]
        public void RaiseError_WhenInvalidBookingReference()
        {
            var passengers = generatePassengers(count: 1);
            var flight = new FlightModel(
                airlineCode: "NV",
                flightNumber: 1234,
                departureStation: "MNL",
                arrivalStation: "CEB",
                departureScheduledTime: new TimeOnly(hour: 1, minute: 0),
                arrivalScheduledTime: new TimeOnly(hour: 2, minute: 0)
            );

            var flightDate = DateTime.Now;

            var invalidBookingReference = "1AB234";

            Action action = () =>
                new ReservationModel(
                    bookingReference: invalidBookingReference,
                    flightInfo: flight,
                    flightDate: flightDate,
                    passengers: passengers
                );

            Assert.Throws<InvalidPNRException>(action);
        }

        [Fact]
        public void CreateNewInstance_WhenBookingReferenceProvided()
        {
            var passengers = generatePassengers(count: 1);

            var flight = new FlightModel(
                airlineCode: "NV",
                flightNumber: 1234,
                departureStation: "MNL",
                arrivalStation: "CEB",
                departureScheduledTime: new TimeOnly(hour: 1, minute: 0),
                arrivalScheduledTime: new TimeOnly(hour: 2, minute: 0)
            );

            var flightDate = DateTime.Now;

            var initialBooking = new ReservationModel(
                flightInfo: flight,
                flightDate: flightDate,
                passengers: passengers
            );

            var validBookingReference = "ABC123";

            // Action
            var confirmedBooking = initialBooking.CreateWith(validBookingReference);

            // Assert
            Assert.Equal(validBookingReference, confirmedBooking.PNR);
            Assert.Equal(initialBooking.FlightInfo, confirmedBooking.FlightInfo);
            Assert.Equal(initialBooking.FlightDate, confirmedBooking.FlightDate);
            Assert.Equal(initialBooking.Passengers.Count(), confirmedBooking.Passengers.Count());
        }

        [Fact]
        public void CreateNewInstance_WhenDataProvided()
        {
            var passengers = generatePassengers(count: 1);

            var flight = new FlightModel(
                airlineCode: "NV",
                flightNumber: 1234,
                departureStation: "MNL",
                arrivalStation: "CEB",
                departureScheduledTime: new TimeOnly(hour: 1, minute: 0),
                arrivalScheduledTime: new TimeOnly(hour: 2, minute: 0)
            );

            var flightDate = DateTime.Now;

            var initModel = new ReservationModel();

            // Action
            var otherModel = initModel.CreateFrom(flightDate, flight, passengers);

            // Assert
            Assert.Null(otherModel.PNR);
            Assert.Equal(flight, otherModel.FlightInfo);
            Assert.Equal(flightDate, otherModel.FlightDate);
            Assert.Equal(passengers.Count(), otherModel.Passengers.Count());
        }

        [Fact]
        public void RaiseError_WhenNoPassengers()
        {
            var flight = new FlightModel(
                airlineCode: "NV",
                flightNumber: 1234,
                departureStation: "MNL",
                arrivalStation: "CEB",
                departureScheduledTime: new TimeOnly(hour: 1, minute: 0),
                arrivalScheduledTime: new TimeOnly(hour: 2, minute: 0)
            );

            var flightDate = DateTime.Now;

            Action action = () =>
                new ReservationModel(
                    flightInfo: flight,
                    flightDate: flightDate,
                    passengers: new List<PassengerModel>()
                );

            Assert.Throws<NoPassengersException>(action);
        }
        #endregion

        #region Utility Methods
        private List<PassengerModel> generatePassengers(int count)
        {
            var passengers = new List<PassengerModel>();

            while (count > 0)
            {
                var passenger = new PassengerModel(
                    "Firstname",
                    "Lastname",
                    new DateTime(1996, 2, 28)
                );

                passengers.Add(passenger);
                count--;
            }

            return passengers;
        }
        #endregion
    }
}
