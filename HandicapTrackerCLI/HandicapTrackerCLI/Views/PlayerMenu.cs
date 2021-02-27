using HandicapTrackerCLI.DAL;
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
        private ITeeDAO teeDAO;

        public PlayerMenu(Player player, ITeeDAO teeDAO)
        {
            this.player = player;
            this.teeDAO = teeDAO;

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
            // TODO Make this UI in this method more user friendly

            List<Tee> tees = teeDAO.GetAllTees();

            foreach (Tee tee in tees)
            {
                Console.WriteLine($"{tee.TeeId}: {tee.Course.CourseName}: Tees: {tee.Name}");
            }

            int teeId = GetInteger("Please enter the number course and tee combination you would like to log your round for: ");
            DateTime datePlayed = GetDate("Please enter the date the round was played: ");
            int score = GetInteger("Please enter your score for this round: ");

            Tee tee = GetTeeByTeeId(tees, teeId);

            GolfRound round = new GolfRound() { PlayerId = player.PlayerId, Tee = tee };



            return MenuOptionResult.WaitAfterMenuSelection;
        }

        // TODO move this method out of UI
        private static Tee GetTeeByTeeId(List<Tee> tees, int teeId)
        {
            Tee output = null;
            foreach (Tee tee in tees)
            {
                if (tee.TeeId == teeId)
                {
                    output = tee;
                    return output;
                }
            }

            return null;
        }
    }
}
