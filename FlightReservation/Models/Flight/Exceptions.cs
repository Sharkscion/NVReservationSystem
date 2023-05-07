﻿namespace FlightReservation.Models.Flight
{
    public class InvalidAirlineCodeException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "Invalid Airline Code.";

        public InvalidAirlineCodeException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidAirlineCodeException(string? message)
            : base(message) { }

        public InvalidAirlineCodeException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

    public class InvalidFlightNumberException : ArgumentOutOfRangeException
    {
        private const string DEFAULT_MESSAGE = "Invalid Flight Number.";

        public InvalidFlightNumberException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidFlightNumberException(string? message)
            : base(message) { }

        public InvalidFlightNumberException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

    public class InvalidStationFormatException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "Station contains an invalid format.";

        public InvalidStationFormatException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidStationFormatException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidStationFormatException(string? message, string? paramName)
            : base(message, paramName) { }
    }

    public class InvalidMarketPairException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "Market pair is invalid.";

        public InvalidMarketPairException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidMarketPairException(string? message)
            : base(message) { }

        public InvalidMarketPairException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidMarketPairException(string? message, string? paramName)
            : base(message, paramName) { }
    }

    public class DuplicateFlightException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "A flight already exists.";

        public DuplicateFlightException()
            : base(DEFAULT_MESSAGE) { }

        public DuplicateFlightException(string? message)
            : base(message) { }

        public DuplicateFlightException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }

    public class InvalidFlightTimeException : ArgumentException
    {
        private const string DEFAULT_MESSAGE = "Flight time is invalid.";

        public InvalidFlightTimeException()
            : base(DEFAULT_MESSAGE) { }

        public InvalidFlightTimeException(string? message, Exception? innerException)
            : base(message, innerException) { }

        public InvalidFlightTimeException(string? message, string? paramName)
            : base(message, paramName) { }
    }
}