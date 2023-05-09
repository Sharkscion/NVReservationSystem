using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Models.Reservation;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.Reservation;
using FlightReservation.UI.Test.Fakes;
using FlightReservation.UI.Views.Reservation.Contracts;
using Moq;

namespace FlightReservation.UI.Test.Reservation
{
    public class CreateReservationPresenterShould : IDisposable
    {
        #region Declarations
        private readonly Mock<ICreateReservationView> _mockView;
        private readonly Mock<IReservationService> _mockReservationService;
        private readonly Mock<IFlightService> _mockFlightService;
        #endregion

        #region Constructors
        public CreateReservationPresenterShould()
        {
            _mockView = new Mock<ICreateReservationView>();
            _mockReservationService = new Mock<IReservationService>();
            _mockFlightService = new Mock<IFlightService>();

            new CreateReservationPresenter(
                view: _mockView.Object,
                reservationService: _mockReservationService.Object,
                flightService: _mockFlightService.Object,
                reservationModel: new FakeReservationModel(),
                flightModel: new FakeFlightModel(),
                passengerModel: new FakePassengerModel()
            );
        }
        #endregion

        #region Test Methods
        [Fact]
        private void SetAirlineCodeError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.AirlineCode, "Invalid")
                .Raise(v => v.AirlineCodeChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.AirlineCode),
                        "Airline code must be 2-3 uppercased-alphanumeric characters."
                    )
            );
        }

        [Fact]
        private void SetFlightNumberError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.FlightNumber, 0)
                .Raise(v => v.FlightNumberChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.FlightNumber),
                        "Flight number cannot be an integer between 1 and 9999."
                    )
            );
        }

        [Fact]
        private void SetFlightDateError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.FlightDate, DateTime.Now.AddDays(-1))
                .Raise(v => v.FlightDateChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.SetFieldError(nameof(v.FlightDate), "Flight date cannot not be past-dated.")
            );
        }

        [Fact]
        private void SetFirstNameError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.FirstName, "1nv@lid")
                .Raise(v => v.FirstNameChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.SetFieldError(nameof(v.FirstName), "Name should at least be 20 letters.")
            );
        }

        [Fact]
        private void SetLastNameError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.LastName, "1nv@lid")
                .Raise(v => v.LastNameChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.SetFieldError(nameof(v.LastName), "Name should at least be 20 letters.")
            );
        }

        [Fact]
        private void SetBirthDateError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.BirthDate, DateTime.Now)
                .Raise(v => v.BirthDateChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.BirthDate),
                        "Passenger should at least be 16 days old."
                    )
            );
        }

        [Fact]
        private void DislpayAvailableFlights_WhenSearched()
        {
            DateTime flightDate = DateTime.Now;
            string airlineCode = "NV";
            int flightNumber = 1;

            // Arrange flights returned by the mocked service
            int repeatCount = 3;
            var availableFlights = Enumerable.Repeat(new FakeFlightModel(), repeatCount);
            _mockFlightService
                .Setup(s => s.FindAvailableFlightsOn(flightDate, airlineCode, flightNumber, null))
                .Returns(availableFlights);

            // Arrange view properties
            _mockView.SetupProperty(v => v.FlightDate, flightDate);
            _mockView.SetupProperty(v => v.FlightNumber, flightNumber);
            _mockView.SetupProperty(v => v.AirlineCode, airlineCode);

            // Act
            _mockView.Raise(v => v.FlightSearched += null, this, EventArgs.Empty);

            // Assert
            _mockView.Verify(
                v =>
                    v.DisplayAvailableFlights(
                        It.Is<IEnumerable<IFlight>>(l => l.Count() == repeatCount)
                    )
            );
            _mockView.Verify(
                v =>
                    v.ShowFlightSelection(
                        It.Is<IEnumerable<IFlight>>(l => l.Count() == repeatCount)
                    )
            );
        }

        [Fact]
        private void DislpayNoFlights_WhenSearched()
        {
            DateTime flightDate = DateTime.Now;
            string airlineCode = "NV";
            int flightNumber = 1;

            // Arrange flights returned by the mocked service
            var noFlights = new List<IFlight>();

            _mockFlightService
                .Setup(s => s.FindAvailableFlightsOn(flightDate, airlineCode, flightNumber, null))
                .Returns(noFlights);

            // Arrange view properties
            _mockView.SetupProperty(v => v.FlightDate, flightDate);
            _mockView.SetupProperty(v => v.FlightNumber, flightNumber);
            _mockView.SetupProperty(v => v.AirlineCode, airlineCode);

            // Act
            _mockView.Raise(v => v.FlightSearched += null, this, EventArgs.Empty);

            // Assert
            _mockView.Verify(v => v.DisplayNoFlights());
        }

        [Fact]
        private void ReturnReservationPNR_WhenCreated()
        {
            // Arrange mocked service to create reservation
            _mockReservationService
                .Setup(s => s.Create(It.IsAny<IReservation>()))
                .Returns("ABC123");

            ReservationEventArgs eventArgs = generateReservationEventArgs();

            // Act
            _mockView.Raise(v => v.Submitted += null, null, eventArgs);

            // Assert
            _mockView.Verify(v => v.DisplayBookingConfirmation("ABC123"));
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        private void AlertError_WhenBookedFlightDoesNotExists()
        {
            // Arrange mocked service to create reservation
            _mockReservationService
                .Setup(s => s.Create(It.IsAny<IReservation>()))
                .Throws(new FlightDoesNotExistException());

            ReservationEventArgs eventArgs = generateReservationEventArgs();

            // Act
            _mockView.Raise(v => v.Submitted += null, null, eventArgs);

            // Assert
            _mockView.Verify(v => v.AlertError(It.IsAny<string>(), "Flight does not exists."));
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        private void AlertOtherOccuredException_WhenSubmitted()
        {
            // Arrange mocked service to create reservation
            _mockReservationService
                .Setup(s => s.Create(It.IsAny<IReservation>()))
                .Throws(new InvalidPNRException());

            ReservationEventArgs eventArgs = generateReservationEventArgs();

            // Act
            _mockView.Raise(v => v.Submitted += null, null, eventArgs);

            // Assert
            _mockView.Verify(v => v.AlertError(It.IsAny<string>(), It.IsAny<string>()));
            _mockView.Verify(v => v.Reset());
        }
        #endregion

        #region Implementations of IDisposable
        public void Dispose()
        {
            _mockFlightService.Reset();
            _mockReservationService.Reset();
            _mockView.Reset();
        }
        #endregion

        #region Private Methods
        private ReservationEventArgs generateReservationEventArgs()
        {
            // Arrange reservation event args to be raised
            var eventArgs = new ReservationEventArgs();
            eventArgs.FlightDate = DateTime.Now;
            eventArgs.FlightInfo = new FlightEventArgs
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
                DepartureScheduledTime = new TimeOnly(hour: 2, minute: 0),
                ArrivalScheduledTime = new TimeOnly(hour: 3, minute: 0)
            };

            eventArgs.Passengers = new List<PassengerEventArgs>
            {
                new PassengerEventArgs
                {
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = DateTime.Now.AddYears(-18)
                }
            };

            return eventArgs;
        }
        #endregion
    }
}
