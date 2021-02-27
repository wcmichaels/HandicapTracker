using HandicapTrackerCLI.DAL;
using HandicapTrackerCLI.Models;
using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Views
{
    class MainMenu : ConsoleMenu
    {
        private IPlayerDAO playerDAO;
        private IGolfRoundDAO golfRoundDAO;
        public MainMenu(IPlayerDAO playerDAO, IGolfRoundDAO golfRoundDAO)
        {
            this.playerDAO = playerDAO;
            this.golfRoundDAO = golfRoundDAO;

            AddOption("Login", Login)
                .AddOption("CreatePlayer", CreatePlayer)
                .AddOption("Exit", Exit);
        }

        private MenuOptionResult CreatePlayer()
        {
            throw new NotImplementedException();
        }

        private MenuOptionResult Login()
        {
            Console.Write("Please enter your username: ");
            string userName = Console.ReadLine();

            Console.Write("Please enter your password: ");
            string passWord = Console.ReadLine();

            try
            {
                Player player = playerDAO.GetPlayerByUsernamePassword(userName, passWord);

                if (player != null)
                {
                    Console.WriteLine("Succesful login!");
                    Console.ReadLine();
                    PlayerMenu playerMenu = new PlayerMenu(player);
                    playerMenu.Show();
                }
                else
                {
                    Console.WriteLine("Invalid login credentials");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return MenuOptionResult.WaitAfterMenuSelection;

        }
    }
}
