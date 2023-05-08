using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Models.Flight;
using FlightReservation.UI.Presenters.FlightMaintenance;
using FlightReservation.UI.Test.Fakes;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using Moq;

namespace FlightReservation.UI.Test.FlightMaintenance
{
    public class AddFlightPresenterShould : IDisposable
    {
        private readonly Mock<IAddFlightView> _mockView;
        private readonly Mock<IFlightService> _mockService;

        public AddFlightPresenterShould()
        {
            _mockService = new Mock<IFlightService>();
            _mockView = new Mock<IAddFlightView>();

            new AddFlightPresenter(_mockView.Object, _mockService.Object, new FakeFlightModel());
        }

        [Fact]
        public void SetAirlineCodeError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.AirlineCode, "Invalid")
                .Raise(v => v.AirlineCodeChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.AirlineCode),
                        "Airline code must be 2-3 uppercased-alphanumeric characters "
                            + "where a numeric character could appear at most once."
                    )
            );
        }

        [Fact]
        public void SetFlightNumberError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.FlightNumber, -1)
                .Raise(v => v.FlightNumberChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.FlightNumber),
                        "Flight number must be an integer between 1 and 9999."
                    )
            );
        }

        [Fact]
        public void SetDepartureStationError_WhenInvalidFormat()
        {
            _mockView
                .SetupProperty(v => v.DepartureStation, "Invalid")
                .Raise(v => v.DepartureStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.DepartureStation),
                        "Departure station must be exactly 3 uppercased-alphanumeric characters "
                            + "where the first character is a letter."
                    )
            );
        }

        [Fact]
        public void SetDepartureStationError_WhenMatchingWithArrivalStation()
        {
            _mockView.SetupProperty(v => v.ArrivalStation, "MNL");
            _mockView
                .SetupProperty(v => v.DepartureStation, "MNL")
                .Raise(v => v.DepartureStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.DepartureStation),
                        "Departure station must not be the same as the arrival station."
                    )
            );
        }

        [Fact]
        public void SetArrivalStationError_WhenInvalidFormat()
        {
            _mockView
                .SetupProperty(v => v.ArrivalStation, "Invalid")
                .Raise(v => v.ArrivalStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.ArrivalStation),
                        "Arrival station must be exactly 3 uppercased-alphanumeric characters "
                            + "where the first character is a letter."
                    )
            );
        }

        [Fact]
        public void SetArrivalStationError_WhenMatchingWithDepartureStation()
        {
            _mockView.SetupProperty(v => v.DepartureStation, "MNL");
            _mockView
                .SetupProperty(v => v.ArrivalStation, "MNL")
                .Raise(v => v.ArrivalStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.ArrivalStation),
                        "Arrival station must not be the same as the departure station."
                    )
            );
        }

        [Fact]
        public void SetDepartureScheduledTimeError_WhenInvalid()
        {
            _mockView.SetupProperty(v => v.ArrivalScheduledTime, new TimeOnly(hour: 3, minute: 0));

            _mockView
                .SetupProperty(v => v.DepartureScheduledTime, new TimeOnly(hour: 4, minute: 0))
                .Raise(v => v.DepartureScheduledTimeChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.DepartureScheduledTime),
                        "Departure scheduled time must be before the arrival scheduled time."
                    )
            );
        }

        [Fact]
        public void SetArrivalScheduledTimeError_WhenInvalid()
        {
            _mockView.SetupProperty(
                v => v.DepartureScheduledTime,
                new TimeOnly(hour: 3, minute: 0)
            );

            _mockView
                .SetupProperty(v => v.ArrivalScheduledTime, new TimeOnly(hour: 2, minute: 0))
                .Raise(v => v.ArrivalScheduledTimeChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.ArrivalScheduledTime),
                        "Arrival scheduled time must be after the departure scheduled time."
                    )
            );
        }

        [Fact]
        public void CreateFlight_WhenSubmitted()
        {
            _mockView.SetupProperty(v => v.AirlineCode, "NV");
            _mockView.SetupProperty(v => v.FlightNumber, 1);
            _mockView.SetupProperty(v => v.DepartureStation, "MNL");
            _mockView.SetupProperty(v => v.ArrivalStation, "CEB");
            _mockView.SetupProperty(
                v => v.DepartureScheduledTime,
                new TimeOnly(hour: 2, minute: 0)
            );
            _mockView.SetupProperty(v => v.ArrivalScheduledTime, new TimeOnly(hour: 3, minute: 0));

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockService.Verify(s => s.Create(It.IsAny<IFlight>()));
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        public void DisplayError_WhenFlightDuplicate_Submitted()
        {
            _mockView.SetupProperty(v => v.AirlineCode, "NV");
            _mockView.SetupProperty(v => v.FlightNumber, 1);
            _mockView.SetupProperty(v => v.DepartureStation, "MNL");
            _mockView.SetupProperty(v => v.ArrivalStation, "CEB");
            _mockView.SetupProperty(
                v => v.DepartureScheduledTime,
                new TimeOnly(hour: 2, minute: 0)
            );
            _mockView.SetupProperty(v => v.ArrivalScheduledTime, new TimeOnly(hour: 3, minute: 0));

            _mockService
                .Setup(s => s.Create(It.IsAny<IFlight>()))
                .Throws(new DuplicateFlightException());

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockService.Verify(s => s.Create(It.IsAny<IFlight>()));
            _mockView.Verify(v => v.AlertError(It.IsAny<string>(), "A flight already exists."));
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        public void AlertOtherOccuredException_WhenSubmitted()
        {
            _mockView.SetupProperty(v => v.AirlineCode, "NV");
            _mockView.SetupProperty(v => v.FlightNumber, 1);
            _mockView.SetupProperty(v => v.DepartureStation, "MNL");
            _mockView.SetupProperty(v => v.ArrivalStation, "CEB");
            _mockView.SetupProperty(
                v => v.DepartureScheduledTime,
                new TimeOnly(hour: 2, minute: 0)
            );
            _mockView.SetupProperty(v => v.ArrivalScheduledTime, new TimeOnly(hour: 3, minute: 0));

            _mockService
                .Setup(s => s.Create(It.IsAny<IFlight>()))
                .Throws(new InvalidDataException());

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockService.Verify(s => s.Create(It.IsAny<IFlight>()));
            _mockView.Verify(v => v.AlertError(It.IsAny<string>(), It.IsAny<string>()));
            _mockView.Verify(v => v.Reset());
        }

        public void Dispose()
        {
            _mockView.Reset();
            _mockService.Reset();
        }
    }
}
