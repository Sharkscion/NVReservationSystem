using System.Text.RegularExpressions;

namespace FlightReservation.Common.Validators
{
    public static class FlightValidator
    {
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

        public static bool IsFlightNumberValid(int value)
        {
            bool isWithinRange = value > 0 && value < 10000;

            if (!isWithinRange)
            {
                return false;
            }

            return true;
        }

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
    }
}
