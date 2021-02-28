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
        private IPlayerDAO playerDAO;
        private ITeeDAO teeDAO;
        private IGolfRoundDAO golfRoundDAO;


        public PlayerMenu(Player player, IPlayerDAO playerDAO, ITeeDAO teeDAO, IGolfRoundDAO golfRoundDAO)
        {
            this.player = player;
            this.playerDAO = playerDAO;
            this.teeDAO = teeDAO;
            this.golfRoundDAO = golfRoundDAO;


            AddOption("Log Round", LogRound)
                .AddOption("View Round History", ViewRoundHistory)
                .AddOption("Exit", Exit);
        }

        private MenuOptionResult ViewRoundHistory()
        {
            player = playerDAO.GetPlayerById(player.PlayerId);

            Console.WriteLine("********************************************************");
            Console.WriteLine($"              Current Handicap Index: {player.Handicap:F1}               ");
            Console.WriteLine("********************************************************");
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

            foreach (Tee t in tees)
            {
                Console.WriteLine($"{t.TeeId}: {t.Course.CourseName}: Tees: {t.Name}");
            }

            int teeId = GetInteger("Please enter the number course and tee combination you would like to log your round for: ");
            DateTime datePlayed = GetDate("Please enter the date the round was played: ");
            int score = GetInteger("Please enter your score for this round: ");
            string answerToStats = GetString("Do you want to enter hole-by-hole stats for this round? (Y/N): ");

            Tee tee = GetTeeByTeeId(tees, teeId);

            GolfRound round = new GolfRound() { PlayerId = player.PlayerId, DatePlayed = datePlayed, Score = score, Tee = tee };

            if (answerToStats.ToLower() == "y")
            {
 
                tee = teeDAO.GetTeeWithHoles(teeId);

                round.Tee = tee;


                for (int i = 0; i < 18; i++)
                {
                    Console.Clear();

                    Console.WriteLine($"*********{tee.Course.CourseName} - {tee.Name} - {tee.Yardage}**********");
                    Console.WriteLine($"\nHole #:{tee.Course.Holes[i].HoleNumber} Par {tee.Course.Holes[i].ParScore} Handicap: {tee.Course.Holes[i].HoleIndex}\n\n");
                    int holeScore = GetInteger("Score: ");
                    int putts = GetInteger("Putts: ");
                    string hitFairwayString = GetString("Hit Fairway (Y/N): ");
                    string inGreensideBunkerString = GetString("In Greenside Bunker (Y/N): ");
                    string outOfBoundsString = GetString("Out of Bounds (Y/N): ");
                    string inWaterString = GetString("Water (Y/N): ");
                    string dropOrOtherString = GetString("Other Penalty (Y/N): ");

                    HoleResult holeResult = new HoleResult();

                    holeResult.Hole = tee.Course.Holes[i];
                    holeResult.Score = holeScore;
                    holeResult.Putts = putts;
                    holeResult.HitFairway = GetBoolFromStringAnswer(hitFairwayString);
                    holeResult.InGreensideBunker = GetBoolFromStringAnswer(inGreensideBunkerString);
                    holeResult.OutOfBounds = GetBoolFromStringAnswer(outOfBoundsString);
                    holeResult.InWater = GetBoolFromStringAnswer(inWaterString);
                    holeResult.DropOrOther = GetBoolFromStringAnswer(dropOrOtherString);

                    round.HoleResults.Add(holeResult);

                }

            }

            GolfRound createdRound = golfRoundDAO.CreateGolfRound(round);
            player.GolfRounds.Add(createdRound);

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

        private static bool GetBoolFromStringAnswer(string stringResponse)
        {
            if (stringResponse.ToLower() == "y")
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
