namespace FlightReservation.UI.Views.Contracts
{
    internal interface IView
    {
        public string Title { get; set; }
        void Execute();

        void ClearScreen();
    }
}
