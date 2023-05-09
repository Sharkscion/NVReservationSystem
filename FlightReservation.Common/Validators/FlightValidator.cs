using System.Text.RegularExpressions;

namespace FlightReservation.Common.Validators
{
    /// <summary>
    /// Utility class for validating flight details that are common
    /// or invariant across the system.
    /// </summary>
    public static class FlightValidator
    {
        #region Public Methods
        /// <summary>
        /// Validates the airline code format if it matches the required specifications:
        /// - 2-3 characters in length
        /// - Alphanumeric but numeric characters are optional
        /// - Numeric character only appears once
        /// </summary>
        public static bool IsAirlineCodeValid(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
            {
                return false;
            }

            bool isLengthValid = value.Length > 1 && value.Length < 4;
            if (!isLengthValid)
            {
                return false;
            }

            string airlineCodePattern = @"^[A-Z]*[0-9]?[A-Z]*$";
            if (!Regex.IsMatch(value, airlineCodePattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the flight number if it is from 1 - 9999.
        /// </summary>
        public static bool IsFlightNumberValid(int value)
        {
            bool isWithinRange = value > 0 && value < 10000;

            if (!isWithinRange)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the station if it matches the required specifications:
        /// - 3 alphanumeric characters, numeric characters are optional
        /// - First character must be a letter
        /// </summary>
        public static bool IsStationFormatValid(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
            {
                return false;
            }

            string stationPattern = @"^[A-Z]{1}[0-9A-Z]{2}$";
            if (!Regex.IsMatch(value, stationPattern))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
