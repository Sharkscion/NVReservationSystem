namespace FlightReservation.UI.Views.Contracts
{
    internal interface IViewWithNavigation
    {
        #region Properties
        public IDictionary<string, IView> NavigationMenus { get; }

        #endregion

        #region Functions
        /// <summary>
        /// Adds a navigation menu to the view and its corresponding command key.
        /// </summary>
        void AddNavigationMenu(string command, IView menu);

        /// <summary>
        /// Displays the navigation menus of the view.
        /// </summary>
        void DisplayNavigationMenus();
        #endregion
    }
}
