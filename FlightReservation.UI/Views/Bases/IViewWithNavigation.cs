namespace FlightReservation.UI.Views.Contracts
{
    internal interface IViewWithNavigation
    {
        public IDictionary<string, IView> NavigationMenus { get; }
        void AddNavigationMenu(string command, IView menu);
        void DisplayNavigationMenus();
    }
}
