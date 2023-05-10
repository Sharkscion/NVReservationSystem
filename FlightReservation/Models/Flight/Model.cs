using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Validators;

namespace FlightReservation.Models.Flight
{
    public class FlightModel : IFlight
    {
        #region Declarations
        private const string TIME_FORMAT = "HH:mm";

        private string _airlineCode;
        private int _flightNumber;
        private string _arrivalStation;
        private string _departureStation;
        private TimeOnly _arrivalScheduledTime;
        private TimeOnly _departureScheduledTime;
        #endregion

        #region Properties
        public string AirlineCode
        {
            get { return _airlineCode; }
            set
            {
                if (!FlightValidator.IsAirlineCodeValid(value))
                {
                    throw new InvalidAirlineCodeException(
                        "Airline code must be 2-3 uppercased-alphanumeric characters "
                            + "where a numeric character could appear at most once."
                    );
                }

                _airlineCode = value;
            }
        }

        public int FlightNumber
        {
            get { return _flightNumber; }
            set
            {
                if (!FlightValidator.IsFlightNumberValid(value))
                {
                    throw new InvalidFlightNumberException(
                        "Flight number must be an integer between 1 and 9999."
                    );
                }

                _flightNumber = value;
            }
        }

        public string ArrivalStation
        {
            get { return _arrivalStation; }
            set
            {
                if (!FlightValidator.IsStationFormatValid(value))
                {
                    throw new InvalidStationFormatException(
                        paramName: nameof(ArrivalStation),
                        message: "Station must be exactly 3 alphanumeric characters "
                            + "where the first character is a letter."
                    );
                }

                bool hasDepartureStation = DepartureStation != string.Empty;
                bool areBothSame = hasDepartureStation && value.Trim() == DepartureStation;

                if (areBothSame)
                {
                    throw new InvalidMarketPairException(
                        paramName: nameof(ArrivalStation),
                        message: "Arrival station cannot be the same as the Departure Station."
                    );
                }

                _arrivalStation = value;
            }
        }
        public string DepartureStation
        {
            get { return _departureStation; }
            set
            {
                if (!FlightValidator.IsStationFormatValid(value))
                {
                    throw new InvalidStationFormatException(
                        paramName: nameof(DepartureStation),
                        message: "Station must be exactly 3 alphanumeric characters "
                            + "where the first character is a letter."
                    );
                }

                bool hasArrivalStation = ArrivalStation != string.Empty;
                bool areBothSame = hasArrivalStation && value.Trim() == ArrivalStation;

                if (areBothSame)
                {
                    throw new InvalidMarketPairException(
                        paramName: nameof(DepartureStation),
                        message: "Departure station cannot be the same as the Arrival Station."
                    );
                }

                _departureStation = value;
            }
        }

        public TimeOnly ArrivalScheduledTime
        {
            get { return _arrivalScheduledTime; }
            set { _arrivalScheduledTime = value; }
        }
        public TimeOnly DepartureScheduledTime
        {
            get { return _departureScheduledTime; }
            set { _departureScheduledTime = value; }
        }

        public string ArrivalScheduledTimeString
        {
            get
            {
                var value = ArrivalScheduledTime.ToString(TIME_FORMAT);
                if (DepartureScheduledTime >= ArrivalScheduledTime)
                {
                    value += " +1";
                }

                return value;
            }
        }

        public string DepartureScheduledTimeString
        {
            get { return DepartureScheduledTime.ToString(TIME_FORMAT); }
        }
        #endregion

        #region Constructors
        public FlightModel()
        {
            ArrivalScheduledTime = new TimeOnly(hour: 23, minute: 59);
            DepartureScheduledTime = new TimeOnly(hour: 0, minute: 0);
        }

        public FlightModel(
            string airlineCode,
            int flightNumber,
            string arrivalStation,
            string departureStation,
            TimeOnly arrivalScheduledTime,
            TimeOnly departureScheduledTime
        )
        {
            AirlineCode = airlineCode;
            FlightNumber = flightNumber;
            ArrivalStation = arrivalStation;
            DepartureStation = departureStation;
            ArrivalScheduledTime = arrivalScheduledTime;
            DepartureScheduledTime = departureScheduledTime;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"{AirlineCode} {FlightNumber} {DepartureStation}->{ArrivalStation} {DepartureScheduledTimeString}-{ArrivalScheduledTimeString}";
        }

        /// <summary>
        /// Create a new flight instance from the given details.
        /// </summary>
        public IFlight CreateFrom(
            string airlineCode,
            int flightNumber,
            string departureStation,
            string arrivalStation,
            TimeOnly departureScheduledTime,
            TimeOnly arrivalScheduledTime
        )
        {
            return new FlightModel(
                airlineCode,
                flightNumber,
                arrivalStation,
                departureStation,
                arrivalScheduledTime,
                departureScheduledTime
            );
        }
        #endregion
    }
}
