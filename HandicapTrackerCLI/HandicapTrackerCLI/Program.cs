using HandicapTrackerCLI.DAL;
using HandicapTrackerCLI.Views;
using System;

namespace HandicapTrackerCLI
{
    class Program
    {
        private static readonly string apiUrl = "https://localhost:44354/api/";
        static void Main(string[] args)
        {
            IPlayerDAO playerDAO = new PlayerApiDAO(apiUrl);
            IGolfRoundDAO golfRoundDAO = new GolfRoundApiDAO(apiUrl);
            MainMenu mainMenu = new MainMenu(playerDAO, golfRoundDAO);
            mainMenu.Show();
        }
    }
}
