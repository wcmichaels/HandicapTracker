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
        private ITeeDAO teeDAO;
        public MainMenu(IPlayerDAO playerDAO, IGolfRoundDAO golfRoundDAO, ITeeDAO teeDAO)
        {
            this.playerDAO = playerDAO;
            this.golfRoundDAO = golfRoundDAO;
            this.teeDAO = teeDAO;

            AddOption("Login", Login)
                .AddOption("CreatePlayer", CreatePlayer)
                .AddOption("Exit", Exit);
        }

        private MenuOptionResult CreatePlayer()
        {
            // TODO - have user type in password twice to confirm accurate
            Player player = new Player();
            Console.WriteLine("Please enter a username and we will check if it's available.");
            string username = GetString("Username: ");

            try
            {
                bool isAvailable = playerDAO.CheckIfUsernameAvailable(username);

                while(!isAvailable)
                {
                    Console.WriteLine("Sorry it looks like that username is not available! Please try a different username");
                    username = GetString("Username: ");
                    isAvailable = playerDAO.CheckIfUsernameAvailable(username);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("That username is available.  Lets now finish creating a profile. Press enter to continue");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Username: {username}");
            player.Username = username;
            player.FirstName = GetString("First name: ");
            player.LastName = GetString("Last name: ");
            // do a special type of return if username isnt available
            player.Password = GetString("Password: ");
            player.Handicap = 0;
            player.DOB = GetDate("Date of birth (MM-DD-YYYY): ");
            player.StreetAddress = GetString("Street address: ");
            player.City = GetString("City: ");
            player.State = GetString("State: ");
            player.CountryCode = GetString("Three letter country code (ex: 'USA'): ");
            player.PostalCode = GetString("PostalCode: ");
            player.Email = GetString("Email: ");
            player.Phone = GetString("Phone number: ");

            Player createdPlayer = playerDAO.CreatePlayer(player);

            if (createdPlayer != null)
            {
                Console.Clear();
                Console.WriteLine("Successfully created player! Login at main menu to log your first round!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("User not created.  Please try again.");
            }

            return MenuOptionResult.WaitAfterMenuSelection;
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
                    PlayerMenu playerMenu = new PlayerMenu(player, playerDAO, teeDAO, golfRoundDAO);
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
