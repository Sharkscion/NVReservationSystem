namespace FlightReservation.UI.Views.Contracts
{
    internal abstract class BasePage : IView, IViewWithNavigation
    {
        protected const string REPEAT_COMMAND = "R";
        protected IDictionary<string, IView> _navigationMenus;

        public IDictionary<string, IView> NavigationMenus
        {
            get { return _navigationMenus; }
            private set { _navigationMenus = value; }
        }

        public string Title { get; set; }

        public BasePage()
        {
            _navigationMenus = new Dictionary<string, IView>();
        }

        public BasePage(string title)
            : this()
        {
            Title = title;
        }

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

        public virtual void Execute()
        {
            var option = REPEAT_COMMAND;

            do
            {
                ClearScreen();
                Console.WriteLine(Title);
                ShowContent();
                DisplayNavigationMenus();
                option = getSelectedOption();
            } while (option == REPEAT_COMMAND);

            NavigationMenus[option].Execute();
        }

        private string getSelectedOption()
        {
            while (true)
            {
                Console.Write("Choose an Option: ");
                string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                var isInputValid = validateInput(rawInput: input);

                if (isInputValid)
                {
                    return input;
                }

                Console.WriteLine("Please enter a valid option...");
            }
        }

        private bool validateInput(string? rawInput)
        {
            bool hasCommand = NavigationMenus.Any((item) => item.Key == rawInput);
            return hasCommand || rawInput == REPEAT_COMMAND;
        }

        public abstract void ShowContent();

        public void ClearScreen()
        {
            Console.Clear();
        }

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
    }
}
