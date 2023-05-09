namespace FlightReservation.UI.Views.Contracts
{
    enum ViewType
    {
        Page,
        Navigation
    }

    internal abstract class BaseScreen : IView, IViewWithNavigation
    {
        #region Declarations
        private IDictionary<string, IView> _navigationMenus;
        private ISet<IView> _pages;
        #endregion

        #region Properties
        public string Title { get; set; }

        public ISet<IView> Pages
        {
            get { return _pages; }
            private set { _pages = value; }
        }

        public IDictionary<string, IView> NavigationMenus
        {
            get { return _navigationMenus; }
            private set { _navigationMenus = value; }
        }
        #endregion

        #region Constructors
        public BaseScreen()
        {
            _pages = new HashSet<IView>();
            _navigationMenus = new Dictionary<string, IView>();
        }

        public BaseScreen(string title)
            : this()
        {
            Title = title;
        }
        #endregion

        public void AddPage(IView page) => _pages.Add(page);

        public virtual void DisplayPages()
        {
            foreach (var page in Pages.Select((value, index) => new { value, index }))
            {
                Console.WriteLine($"[{page.index}] {page.value.Title}");
            }
            Console.WriteLine();
        }

        public virtual void AddNavigationMenu(string command, IView menu)
        {
            if (_navigationMenus.Any((menu) => menu.Key == command))
            {
                throw new InvalidOperationException($"Command [{command}] already exists.");
            }
            _navigationMenus.Add(key: command.ToUpper(), value: menu);
        }

        public virtual void DisplayNavigationMenus()
        {
            foreach (var menu in NavigationMenus)
            {
                Console.WriteLine($"[{menu.Key}] {menu.Value.Title}");
            }
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public virtual void Execute()
        {
            ClearScreen();
            Console.WriteLine(Title);
            DisplayPages();
            DisplayNavigationMenus();
            runSelectedOption();
        }

        private void runSelectedOption()
        {
            IView selectedOption;

            var (input, selectedViewType) = getSelectedOption();

            switch (selectedViewType)
            {
                case ViewType.Page:
                    int pageIndex = int.Parse(input);
                    selectedOption = Pages.ElementAt(pageIndex);
                    break;

                default:
                    selectedOption = NavigationMenus[input];
                    break;
            }

            selectedOption.Execute();
        }

        private (string, ViewType) getSelectedOption()
        {
            while (true)
            {
                Console.Write("Choose an Option: ");
                string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                var (isInputValid, viewType) = validateInput(rawInput: input);

                if (isInputValid)
                {
                    return (input, viewType);
                }

                Console.WriteLine("Please enter a valid option...");
            }
        }

        private (bool, ViewType) validateInput(string? rawInput)
        {
            int selectedPageIndex = -1;
            bool isNumeric = int.TryParse(rawInput, out selectedPageIndex);
            bool isWithinRange = selectedPageIndex >= 0 && selectedPageIndex < Pages.Count();

            if (isNumeric)
            {
                return (isWithinRange, ViewType.Page);
            }

            bool hasCommand = NavigationMenus.Any((item) => item.Key == rawInput);
            return (hasCommand, ViewType.Navigation);
        }
    }
}
