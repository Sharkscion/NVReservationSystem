using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views
{
    internal class ExitView : IView
    {
        #region Properties
        public string Title { get; set; }

        #endregion

        #region Constructors
        public ExitView()
        {
            Title = "Quit";
        }

        public ExitView(string title)
        {
            Title = title;
        }
        #endregion

        #region Implementations of IView
        public void Execute()
        {
            Console.WriteLine("Good bye!");
            Environment.Exit(0);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
        #endregion
    }
}
