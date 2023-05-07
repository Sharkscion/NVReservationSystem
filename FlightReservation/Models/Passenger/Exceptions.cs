namespace FlightReservation.Models.Passenger
{
    public class InvalidNameException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "Name is invalid.";

        public InvalidNameException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidNameException(string? message)
            : base(message) { }

        public InvalidNameException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidNameException(string? message, string? paramName)
            : base(message, paramName) { }
    }

    public class AgeLimitException : ArgumentOutOfRangeException
    {
        private const string DEFAULT_MESSAGE = "Age is not allowed.";

        public AgeLimitException()
            : base(DEFAULT_MESSAGE) { }

        public AgeLimitException(string? message)
            : base(message) { }

        public AgeLimitException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}
