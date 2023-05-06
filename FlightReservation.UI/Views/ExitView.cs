using FlightReservation.UI.Views.Contracts;

namespace FlightReservation.UI.Views
{
    internal class ExitView : IView
    {
        public string Title { get; set; }

        public ExitView()
        {
            Title = "Quit";
        }

        public ExitView(string title)
        {
            Title = title;
        }

        public void Execute()
        {
            Console.WriteLine("Good bye!");
            Environment.Exit(0);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}
