namespace FlightReservation.Models.Reservation
{
    public class MoreThanMaxPassengerCountException : ArgumentOutOfRangeException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Max passenger count per booking has been reached.";
        #endregion

        #region Constructors
        public MoreThanMaxPassengerCountException()
            : base(DEFAULT_MESSAGE) { }

        public MoreThanMaxPassengerCountException(string? message)
            : base(message) { }

        public MoreThanMaxPassengerCountException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }

    public class NoPassengersException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Reservation does not have any passengers.";
        #endregion

        #region Constructors
        public NoPassengersException()
            : base(DEFAULT_MESSAGE) { }

        public NoPassengersException(string? message)
            : base(message) { }

        public NoPassengersException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }

    public class InvalidFlightDateException : ArgumentOutOfRangeException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Flight date is invalid.";
        #endregion

        #region Constructors
        public InvalidFlightDateException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidFlightDateException(string? message)
            : base(message) { }

        public InvalidFlightDateException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }

    public class FlightDoesNotExistException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Flight does not exists.";
        #endregion

        #region Constructors
        public FlightDoesNotExistException()
            : base(DEFAULT_MESSAGE) { }

        public FlightDoesNotExistException(string? message)
            : base(message) { }

        public FlightDoesNotExistException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }

    public class InvalidPNRException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "PNR is invalid.";
        #endregion

        #region Constructors
        public InvalidPNRException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidPNRException(string? message)
            : base(message) { }

        public InvalidPNRException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }
}
