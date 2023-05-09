namespace FlightReservation.UI.Views.Contracts
{
    internal interface IView
    {
        #region Properties
        public string Title { get; set; }
        #endregion

        #region Functions
        /// <summary>
        /// Executes the view when called.
        /// </summary>
        void Execute();

        /// <summary>
        /// Clears the screen of the view.
        /// </summary>
        void ClearScreen();
        #endregion
    }
}
