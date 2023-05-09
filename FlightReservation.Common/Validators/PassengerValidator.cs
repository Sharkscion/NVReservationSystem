using System.Text.RegularExpressions;

namespace FlightReservation.Common.Validators
{
    /// <summary>
    /// Utility class for validating passenger details that are common
    /// or invariant across the system.
    /// </summary>
    public static class PassengerValidator
    {
        #region Public Methods
        /// <summary>
        /// Validates the name if it is consists of 1 to 20 letters.
        public static bool IsNameValid(string value)
        {
            if (string.IsNullOrEmpty(value?.Trim()))
            {
                return false;
            }

            string namePattern = @"^[A-Za-z]{1,20}$";
            if (!Regex.IsMatch(value, namePattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the birth date if it within the allowed age limit which is 16 days old.
        /// </summary>
        public static bool IsBirthDateValid(DateTime value)
        {
            bool isAtleast16DaysOld = DateTime.Now.Subtract(value).TotalDays >= 16;

            if (!isAtleast16DaysOld)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
