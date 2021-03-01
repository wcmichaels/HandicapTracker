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

            Console.WriteLine("************************************************************************************************************************");
            Console.WriteLine($"                                    Current Handicap Index: {player.Handicap:F1}                                                  ");
            Console.WriteLine("************************************************************************************************************************");

            Console.WriteLine($"| {"Course",-37} | {"Tees",-20} | {"Score",-8} | {"Par",-5} | {"Course Rating",-14} |");
            foreach (GolfRound round in player.GolfRounds)
            {
                Console.WriteLine($"| { round.Tee.Course.CourseName,-37} | { round.Tee.Name,-20} | { round.Score,-8} | {round.Tee.Course.Par,-5} | {round.Tee.RatingFull,-14} |");
            }
        

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult LogRound()
        {
            // TODO Make this UI in this method more user friendly

            List<Tee> tees = teeDAO.GetAllTees();

            Console.WriteLine($"| {"Id",-4} | {"Course",-37} | {"Tees",-20} | {"Yardage",-10} | {"City",-20} | {"State", -3} ");
            Console.WriteLine("****************************************************************************************************************************\n");

            foreach (Tee t in tees)
            {
                Console.WriteLine($"| {t.TeeId,-4} | {t.Course.CourseName,-37} | {t.Name,-20} | {t.Yardage,-10} | {t.Course.City,-20} | {t.Course.State,-3}");
            }

            Console.WriteLine("\n\n************************************************************************************************************************");

            int teeId = GetInteger("Enter the Id to select a course: ");
            DateTime datePlayed = GetDate("Date played (MM-DD-YYYY): ");
            string answerToStats = GetString("Do you want to enter hole-by-hole stats for this round? (Y/N): ");

            Tee tee = GetTeeByTeeId(tees, teeId);

            GolfRound round = new GolfRound() { PlayerId = player.PlayerId, DatePlayed = datePlayed, Tee = tee };

            if (answerToStats.ToLower() == "y")
            {
 
                tee = teeDAO.GetTeeWithHoles(teeId);

                round.Tee = tee;


                for (int i = 0; i < 18; i++)
                {
                    Console.Clear();

                    Console.WriteLine($"{tee.Course.CourseName} - {tee.Name} - {tee.Yardage} Yards");
                    Console.WriteLine($"\nHole #:{tee.Course.Holes[i].HoleNumber} Par {tee.Course.Holes[i].ParScore} Handicap: {tee.Course.Holes[i].HoleIndex}\n\n");
                    int holeScore = GetInteger("Score: ");
                    int putts = GetInteger("Putts: ");

                    bool? hitFairway = null;
                    if (tee.Course.Holes[i].ParScore != 3)
                    {
                        string hitFairwayString = GetString("Hit Fairway (Y/N): ");
                        hitFairway = GetBoolFromStringAnswer(hitFairwayString);
                    }

                    string inGreensideBunkerString = GetString("In Greenside Bunker (Y/N): ");
                    string outOfBoundsString = GetString("Out of Bounds (Y/N): ");
                    string inWaterString = GetString("Water (Y/N): ");
                    string dropOrOtherString = GetString("Other Penalty (Y/N): ");

                    HoleResult holeResult = new HoleResult();

                    holeResult.Hole = tee.Course.Holes[i];
                    holeResult.Score = holeScore;
                    holeResult.Putts = putts;
                    holeResult.HitFairway = hitFairway;
                    holeResult.InGreensideBunker = GetBoolFromStringAnswer(inGreensideBunkerString);
                    holeResult.OutOfBounds = GetBoolFromStringAnswer(outOfBoundsString);
                    holeResult.InWater = GetBoolFromStringAnswer(inWaterString);
                    holeResult.DropOrOther = GetBoolFromStringAnswer(dropOrOtherString);

                    round.HoleResults.Add(holeResult);
                }

                round.Score = CalculateRoundScoreByHoleResults(round);
            }
            else
            {
                round.Score = GetInteger("Round Score: ");
            }

            GolfRound createdRound = golfRoundDAO.CreateGolfRound(round);
            player.GolfRounds.Add(createdRound);

            Console.WriteLine("\n\nSuccessfully logged round!");

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private static int CalculateRoundScoreByHoleResults(GolfRound round)
        {
            int totalScore = 0;

            foreach (HoleResult hr in round.HoleResults)
            {
                totalScore += hr.Score;
            }

            return totalScore;
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
