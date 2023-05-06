namespace FlightReservation.Data.Passenger
{
    public class InvalidNameException : ArgumentOutOfRangeException
    {
        private const string DEFAULT_MESSAGE = "Name is invalid.";

        public InvalidNameException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidNameException(string? message)
            : base(message) { }

        public InvalidNameException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidNameException(string? paramName, string? message)
            : base(paramName, message) { }
    }

    public class AgeLimitException : ArgumentOutOfRangeException
    {
        private const string DEFAULT_MESSAGE = "Age is invalid.";

        public AgeLimitException()
            : base(DEFAULT_MESSAGE) { }

        public AgeLimitException(string? message)
            : base(message) { }

        public AgeLimitException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
