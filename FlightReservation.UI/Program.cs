using FlightReservation.Repositories;
using FlightReservation.Services;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Presenters.FlightMaintenance;
using FlightReservation.UI.Presenters.Reservation;
using FlightReservation.UI.Views.FlightMaintenance;
using FlightReservation.UI.Views.Reservation;

namespace FlightReservation.UI.Views
{
    internal class Program
    {
        const string EXIT_COMMAND = "X";
        const string BACK_COMMAND = "B";

        private readonly IReservationService _reservationService;
        private readonly IFlightService _flightService;

        private ExitView _exitView;

        private MainScreen _mainScreen;
        private ReservationScreen _reservationScreen;
        private FlightMaintenanceScreen _flightMaintenanceScreen;

        private AddFlightPage _addFlightPage;
        private SearchFlightScreen _searchFlightScreen;
        private SearchByAirlineCodePage _searchByAirlineCodePage;
        private SearchByFlightNumberPage _searchByFlightNumberPage;
        private SearchByOriginDestinationPage _searchByOriginDestinationPage;

        private CreateReservationPage _createReservationPage;
        private DisplayAllReservationsPage _viewReservationsPage;
        private SearchReservationPage _searchReservationPage;

        public Program()
        {
            var cachedFlightRepository = new CachedFlightRepository();
            var cachedReservationRepository = new CachedReservationRepository();

            _flightService = new FlightService(cachedFlightRepository);
            _reservationService = new ReservationService(
                cachedReservationRepository,
                flightService: _flightService
            );

            _exitView = new ExitView();
            _mainScreen = new MainScreen("Main Screen");

            _reservationScreen = new ReservationScreen("Reservation Screen");
            _flightMaintenanceScreen = new FlightMaintenanceScreen("Flight Maintenance Screen");
            _searchFlightScreen = new SearchFlightScreen("Search a Flight");

            initAddFlightPage();
            initSearchByAirlineCodePage();
            initSearchByFlightNumberPage();
            initSearchByOriginDestinationPage();

            initCreateReservationPage();
            initSearchReservationPage();
            initDisplayAllReservationsPage();
        }

        static void Main(string[] args)
        {
            var program = new Program();

            program.constructMainScreen();
            program.constructFlightMaintenanceScreen();
            program.constructReservationScreen();
            program.constructSearchFlightScreen();

            program.constructAddFlightPage();
            program.constructSearchByAirlineCodePage();
            program.constructSearchByFlightNumberPage();
            program.constructSearchByOriginDestinationPage();

            program.constructCreateReservationPage();
            program.constructViewReservationPage();
            program.constructSearchReservationPage();

            program.run();
        }

        private void initAddFlightPage()
        {
            _addFlightPage = new AddFlightPage("Add a Flight");

            var presenter = new AddFlightPresenter(view: _addFlightPage, service: _flightService);

            _addFlightPage.AirlineCodeChanged += presenter.OnAirlineCodeChanged;
            _addFlightPage.FlightNumberChanged += presenter.OnFlightNumberChanged;
            _addFlightPage.DepartureStationChanged += presenter.OnDepartureStationChanged;
            _addFlightPage.ArrivalStationChanged += presenter.OnArrivalStationChanged;
            _addFlightPage.DepartureScheduledTimeChanged +=
                presenter.OnDepartureScheduledTimeChanged;
            _addFlightPage.ArrivalScheduledTimeChanged += presenter.OnArrivalScheduledTimeChanged;
            _addFlightPage.Submitted += presenter.OnSubmitted;
        }

        private void initSearchByFlightNumberPage()
        {
            _searchByFlightNumberPage = new SearchByFlightNumberPage("Search by Flight Number");

            var presenter = new SearchByFlightNumberPresenter(
                view: _searchByFlightNumberPage,
                service: _flightService
            );

            _searchByFlightNumberPage.FlightNumberChanged += presenter.OnFlightNumberChanged;
            _searchByFlightNumberPage.Submitted += presenter.OnSubmitted;
        }

        private void initSearchByAirlineCodePage()
        {
            _searchByAirlineCodePage = new SearchByAirlineCodePage("Search by Airline Code");

            var presenter = new SearchByAirlineCodePresenter(
                view: _searchByAirlineCodePage,
                service: _flightService
            );

            _searchByAirlineCodePage.AirlineCodeChanged += presenter.OnAirlineCodeChanged;
            _searchByAirlineCodePage.Submitted += presenter.OnSubmitted;
        }

        private void initSearchByOriginDestinationPage()
        {
            _searchByOriginDestinationPage = new SearchByOriginDestinationPage(
                "Search by Origin & Destination"
            );

            var presenter = new SearchByOriginDestinationPresenter(
                view: _searchByOriginDestinationPage,
                service: _flightService
            );

            _searchByOriginDestinationPage.DepartureStationChanged +=
                presenter.OnDepartureStationChanged;
            _searchByOriginDestinationPage.ArrivalStationChanged +=
                presenter.OnArrivalStationChanged;
            _searchByOriginDestinationPage.Submitted += presenter.OnSubmitted;
        }

        private void initCreateReservationPage()
        {
            _createReservationPage = new CreateReservationPage("Create a Reservation");

            var presenter = new CreateReservationPresenter(
                view: _createReservationPage,
                reservationService: _reservationService,
                flightService: _flightService
            );

            _createReservationPage.AirlineCodeChanged += presenter.OnAirlineCodeChanged;
            _createReservationPage.FlightNumberChanged += presenter.OnFlightNumberChanged;
            _createReservationPage.FlightDateChanged += presenter.OnFlightDateChanged;

            _createReservationPage.FirstNameChanged += presenter.OnFirstNameChanged;
            _createReservationPage.LastNameChanged += presenter.OnLastNameChanged;
            _createReservationPage.BirthDateChanged += presenter.OnBirthDateChanged;

            _createReservationPage.FlightSearched += presenter.OnFlightSearched;
            _createReservationPage.Submitted += presenter.OnSubmitted;
        }

        private void initSearchReservationPage()
        {
            _searchReservationPage = new SearchReservationPage("Search a Reservation");

            var presenter = new SearchReservationPresenter(
                view: _searchReservationPage,
                service: _reservationService
            );

            _searchReservationPage.PNRChanged += presenter.OnPNRChanged;
            _searchReservationPage.Submitted += presenter.OnSubmitted;
        }

        private void initDisplayAllReservationsPage()
        {
            _viewReservationsPage = new DisplayAllReservationsPage("View Reservations");

            var presenter = new DisplayAllReservationsPresenter(
                view: _viewReservationsPage,
                service: _reservationService
            );

            _viewReservationsPage.Submitted += presenter.OnSubmitted;
        }

        private void constructMainScreen()
        {
            _mainScreen.AddPage(_flightMaintenanceScreen);
            _mainScreen.AddPage(_reservationScreen);
            _mainScreen.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructReservationScreen()
        {
            _reservationScreen.AddPage(_createReservationPage);
            _reservationScreen.AddPage(_viewReservationsPage);
            _reservationScreen.AddPage(_searchReservationPage);
            _reservationScreen.AddNavigationMenu(command: BACK_COMMAND, menu: _mainScreen);
            _reservationScreen.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructFlightMaintenanceScreen()
        {
            _flightMaintenanceScreen.AddPage(_addFlightPage);
            _flightMaintenanceScreen.AddPage(_searchFlightScreen);
            _flightMaintenanceScreen.AddNavigationMenu(command: BACK_COMMAND, menu: _mainScreen);
            _flightMaintenanceScreen.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructSearchFlightScreen()
        {
            _searchFlightScreen.AddPage(_searchByAirlineCodePage);
            _searchFlightScreen.AddPage(_searchByFlightNumberPage);
            _searchFlightScreen.AddPage(_searchByOriginDestinationPage);

            _searchFlightScreen.AddNavigationMenu(
                command: BACK_COMMAND,
                menu: _flightMaintenanceScreen
            );
            _searchFlightScreen.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructAddFlightPage()
        {
            _addFlightPage.AddNavigationMenu(command: BACK_COMMAND, menu: _flightMaintenanceScreen);
            _addFlightPage.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructSearchByFlightNumberPage()
        {
            _searchByFlightNumberPage.AddNavigationMenu(
                command: BACK_COMMAND,
                menu: _searchFlightScreen
            );
            _searchByFlightNumberPage.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructSearchByAirlineCodePage()
        {
            _searchByAirlineCodePage.AddNavigationMenu(
                command: BACK_COMMAND,
                menu: _searchFlightScreen
            );
            _searchByAirlineCodePage.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructSearchByOriginDestinationPage()
        {
            _searchByOriginDestinationPage.AddNavigationMenu(
                command: BACK_COMMAND,
                menu: _searchFlightScreen
            );
            _searchByOriginDestinationPage.AddNavigationMenu(
                command: EXIT_COMMAND,
                menu: _exitView
            );
        }

        private void constructCreateReservationPage()
        {
            _createReservationPage.AddNavigationMenu(
                command: BACK_COMMAND,
                menu: _reservationScreen
            );
            _createReservationPage.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructViewReservationPage()
        {
            _viewReservationsPage.AddNavigationMenu(
                command: BACK_COMMAND,
                menu: _reservationScreen
            );
            _viewReservationsPage.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void constructSearchReservationPage()
        {
            _searchReservationPage.AddNavigationMenu(
                command: BACK_COMMAND,
                menu: _reservationScreen
            );
            _searchReservationPage.AddNavigationMenu(command: EXIT_COMMAND, menu: _exitView);
        }

        private void run()
        {
            _mainScreen.Execute();
        }
    }
}
