namespace FlightReservation.Models.Flight
{
    public class InvalidAirlineCodeException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Invalid Airline Code.";
        #endregion

        #region Constructors
        public InvalidAirlineCodeException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidAirlineCodeException(string? message)
            : base(message) { }

        public InvalidAirlineCodeException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }

    public class InvalidFlightNumberException : ArgumentOutOfRangeException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Invalid Flight Number.";
        #endregion

        #region Constructors
        public InvalidFlightNumberException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidFlightNumberException(string? message)
            : base(message) { }

        public InvalidFlightNumberException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }

    public class InvalidStationFormatException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Station contains an invalid format.";
        #endregion

        #region Constructors
        public InvalidStationFormatException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidStationFormatException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidStationFormatException(string? message, string? paramName)
            : base(message, paramName) { }
        #endregion
    }

    public class InvalidMarketPairException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Market pair is invalid.";
        #endregion

        #region Constructors
        public InvalidMarketPairException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidMarketPairException(string? message)
            : base(message) { }

        public InvalidMarketPairException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidMarketPairException(string? message, string? paramName)
            : base(message, paramName) { }
        #endregion
    }

    public class DuplicateFlightException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "A flight already exists.";
        #endregion

        #region Constructors
        public DuplicateFlightException()
            : base(DEFAULT_MESSAGE) { }

        public DuplicateFlightException(string? message)
            : base(message) { }

        public DuplicateFlightException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }
}
