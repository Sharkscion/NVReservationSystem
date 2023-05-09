namespace FlightReservation.Models.Passenger
{
    public class InvalidNameException : ArgumentException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Name is invalid.";
        #endregion

        #region Constructors
        public InvalidNameException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidNameException(string? message)
            : base(message) { }

        public InvalidNameException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidNameException(string? message, string? paramName)
            : base(message, paramName) { }
        #endregion
    }

    public class AgeLimitException : ArgumentOutOfRangeException
    {
        #region Declarations
        private const string DEFAULT_MESSAGE = "Age is not allowed.";

        #endregion

        #region Constructors
        public AgeLimitException()
            : base(DEFAULT_MESSAGE) { }

        public AgeLimitException(string? message)
            : base(message) { }

        public AgeLimitException(string? message, Exception? innerException)
            : base(message, innerException) { }
        #endregion
    }
}
