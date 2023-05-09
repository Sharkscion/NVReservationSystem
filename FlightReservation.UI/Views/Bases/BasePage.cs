namespace FlightReservation.UI.Views.Contracts
{
    internal abstract class BasePage : IView, IViewWithNavigation
    {
        #region Declarations
        protected const string REPEAT_COMMAND = "R";
        protected IDictionary<string, IView> _navigationMenus;
        #endregion

        #region Properties
        public IDictionary<string, IView> NavigationMenus
        {
            get { return _navigationMenus; }
            private set { _navigationMenus = value; }
        }

        public string Title { get; set; }
        #endregion

        #region Constructors
        public BasePage()
        {
            _navigationMenus = new Dictionary<string, IView>();
        }

        public BasePage(string title)
            : this()
        {
            Title = title;
        }
        #endregion


        #region Implementations of IViewWithNavigation
        public void AddNavigationMenu(string command, IView menu)
        {
            if (_navigationMenus.Any((menu) => menu.Key == command))
            {
                throw new InvalidOperationException($"Command [{command}] already exists.");
            }

            if (command == REPEAT_COMMAND)
            {
                throw new InvalidOperationException("Command conflicts with Repeat Command [R].");
            }

            _navigationMenus.Add(key: command.ToUpper(), value: menu);
        }

        public void DisplayNavigationMenus()
        {
            Console.WriteLine();
            Console.WriteLine($"[{REPEAT_COMMAND}] {Title}");

            foreach (var menu in NavigationMenus)
            {
                Console.WriteLine($"[{menu.Key}] {menu.Value.Title}");
            }
        }
        #endregion

        #region Implementations of IView
        public void ClearScreen()
        {
            Console.Clear();
        }

        public virtual void Execute()
        {
            var option = REPEAT_COMMAND;

            do
            {
                ClearScreen();

                displayTitle();

                ShowContent();
                DisplayNavigationMenus();
                option = getSelectedOption();
            } while (option == REPEAT_COMMAND);

            NavigationMenus[option].Execute();
        }

        #endregion

        #region Abstract Methods
        public abstract void ShowContent();

        #endregion

        #region Private & Protected Methods

        protected char getYesOrNoInput(string question)
        {
            while (true)
            {
                Console.Write($"{question} [Y|N]? ");
                string? input = Console.ReadLine()?.Trim().ToUpper();

                switch (input)
                {
                    case "Y":
                        return 'Y';
                    case "N":
                        return 'N';
                    default:
                        Console.WriteLine("Please enter a valid option...\n");
                        break;
                }
            }
        }

        private void displayTitle()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"    {Title}");
            Console.WriteLine("-------------------------------------");
        }

        private string getSelectedOption()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Choose an Option: ");
                string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                var isInputValid = validateInput(input);

                if (isInputValid)
                {
                    return input;
                }

                Console.WriteLine("Please enter a valid option...");
            }
        }

        private bool validateInput(string? input)
        {
            bool hasCommand = NavigationMenus.Any((item) => item.Key == input);
            return hasCommand || input == REPEAT_COMMAND;
        }
        #endregion
    }
}
