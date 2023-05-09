using FlightReservation.Common.Contracts.Services;
using FlightReservation.Models.Flight;
using FlightReservation.Models.Passenger;
using FlightReservation.Models.Reservation;
using FlightReservation.Repositories;
using FlightReservation.Services;
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

        private readonly ExitView _exitView;

        private readonly MainScreen _mainScreen;
        private readonly ReservationScreen _reservationScreen;
        private readonly FlightMaintenanceScreen _flightMaintenanceScreen;

        private readonly AddFlightPage _addFlightPage;
        private readonly SearchFlightScreen _searchFlightScreen;
        private readonly SearchByAirlineCodePage _searchByAirlineCodePage;
        private readonly SearchByFlightNumberPage _searchByFlightNumberPage;
        private readonly SearchByOriginDestinationPage _searchByOriginDestinationPage;

        private readonly CreateReservationPage _createReservationPage;
        private readonly DisplayAllReservationsPage _viewReservationsPage;
        private readonly SearchReservationPage _searchReservationPage;

        public Program()
        {
            // Initiate Program Repositories
            var cachedFlightRepository = new CachedFlightRepository();
            var cachedReservationRepository = new CachedReservationRepository();

            // Initiate Program Services
            _flightService = new FlightService(cachedFlightRepository);
            _reservationService = new ReservationService(
                cachedReservationRepository,
                flightService: _flightService
            );

            // Initiate Main Views
            _exitView = new ExitView();
            _mainScreen = new MainScreen("Main Screen");

            _reservationScreen = new ReservationScreen("Reservation Screen");
            _flightMaintenanceScreen = new FlightMaintenanceScreen("Flight Maintenance Screen");
            _searchFlightScreen = new SearchFlightScreen("Search a Flight");

            // Initiate Flight Maintenance Views and Its Presenters
            _addFlightPage = new AddFlightPage("Add a Flight");
            new AddFlightPresenter(
                view: _addFlightPage,
                service: _flightService,
                model: new FlightModel()
            );

            _searchByFlightNumberPage = new SearchByFlightNumberPage("Search by Flight Number");
            new SearchByFlightNumberPresenter(
                view: _searchByFlightNumberPage,
                service: _flightService
            );

            _searchByAirlineCodePage = new SearchByAirlineCodePage("Search by Airline Code");
            new SearchByAirlineCodePresenter(
                view: _searchByAirlineCodePage,
                service: _flightService
            );

            _searchByOriginDestinationPage = new SearchByOriginDestinationPage(
                "Search by Origin & Destination"
            );
            new SearchByOriginDestinationPresenter(
                view: _searchByOriginDestinationPage,
                service: _flightService
            );

            // Initiate Reservation Views and Its Presenters
            _createReservationPage = new CreateReservationPage("Create a Reservation");
            new CreateReservationPresenter(
                view: _createReservationPage,
                reservationService: _reservationService,
                flightService: _flightService,
                reservationModel: new ReservationModel(),
                flightModel: new FlightModel(),
                passengerModel: new PassengerModel()
            );

            _searchReservationPage = new SearchReservationPage("Search a Reservation");
            new SearchReservationPresenter(
                view: _searchReservationPage,
                service: _reservationService
            );

            _viewReservationsPage = new DisplayAllReservationsPage("View Reservations");
            new DisplayAllReservationsPresenter(
                view: _viewReservationsPage,
                service: _reservationService
            );
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
