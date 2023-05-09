using System.Text.RegularExpressions;

namespace FlightReservation.Common.Validators
{
    /// <summary>
    /// Utility class for validating reservation details that are common
    /// or invariant across the system.
    /// </summary>
    public static class ReservationValidator
    {
        #region Public Methods
        /// <summary>
        /// Validates that the flight date is not past-dated.
        /// </summary>
        public static bool IsFlightDateValid(DateTime value)
        {
            bool isOnOrAfterToday = value.Date >= DateTime.Now.Date;
            if (!isOnOrAfterToday)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///  Validates the booking reference/PNR that is matches the required specifications:
        ///  - 6 characters in length
        ///  - Alphanumeric
        ///  - First character is a letter
        /// </summary>
        public static bool IsBookingReferenceFormatValid(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
            {
                return false;
            }

            string pattern = @"^[A-Z][A-Z0-9]{5}$";
            if (!Regex.IsMatch(value, pattern))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
