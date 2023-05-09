namespace FlightReservation.UI.Views.Contracts
{
    internal interface IFormView
    {
        #region Properties
        public bool IsFormValid { get; }
        #endregion

        #region Functions
        /// <summary>
        /// Sets the error message pertaining to the specified field name.
        /// </summary>
        void SetFieldError(string paramName, string message);

        /// <summary>
        /// Alerts the error on the screen with no specific field association.
        /// </summary>
        void AlertError(string header, string message);

        /// <summary>
        /// Resets the fields to default values.
        /// </summary>
        void Reset();
        #endregion
    }
}
