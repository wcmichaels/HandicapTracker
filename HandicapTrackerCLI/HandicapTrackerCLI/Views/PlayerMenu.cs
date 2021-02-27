using HandicapTrackerCLI.Models;
using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Views
{
    public class PlayerMenu : ConsoleMenu
    {
        private Player player;

        public PlayerMenu(Player player)
        {
            this.player = player;

            AddOption("Log Round", LogRound)
                .AddOption("View Round History", ViewRoundHistory)
                .AddOption("Exit", Exit);
        }

        private MenuOptionResult ViewRoundHistory()
        {
            foreach (GolfRound round in player.GolfRounds)
            {
                Console.WriteLine($"{round.Tee.Course.CourseName} - {round.Tee.Name} Tees, Score: {round.Score}, Date Played: {round.DatePlayed:d}");
            }


            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult LogRound()
        {
            throw new NotImplementedException();
        }
    }
}
