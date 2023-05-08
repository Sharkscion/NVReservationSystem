using FlightReservation.Common.Validators;
using FlightReservation.Models.Contracts;

namespace FlightReservation.Models.Flight
{
    public class FlightModel : IFlight
    {
        private string _airlineCode;
        private int _flightNumber;
        private string _arrivalStation;
        private string _departureStation;
        private TimeOnly _arrivalScheduledTime;
        private TimeOnly _departureScheduledTime;

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
            set
            {
                bool isAfterDepartureTime = DepartureScheduledTime < value;
                if (!isAfterDepartureTime)
                {
                    throw new InvalidFlightTimeException(
                        paramName: nameof(ArrivalScheduledTime),
                        message: "Scheduled time of arrival must be after scheduled time of departure."
                    );
                }

                _arrivalScheduledTime = value;
            }
        }
        public TimeOnly DepartureScheduledTime
        {
            get { return _departureScheduledTime; }
            set
            {
                bool isBeforeArrivalTime = value < ArrivalScheduledTime;
                if (!isBeforeArrivalTime)
                {
                    throw new InvalidFlightTimeException(
                        paramName: nameof(DepartureScheduledTime),
                        message: "Scheduled time of departure must be before scheduled time of arrival."
                    );
                }
                _departureScheduledTime = value;
            }
        }

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

        public override string ToString()
        {
            return $"{AirlineCode} {FlightNumber} {DepartureStation}->{ArrivalStation}";
        }

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
    }
}
