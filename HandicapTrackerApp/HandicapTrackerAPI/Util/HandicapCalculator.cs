using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.Util
{
    public static class HandicapCalculator
    {
        public static List<double> CalculateScoreDifferentials(List<GolfRound> rounds)
        {
            List<double> output = new List<double>();

            List<GolfRound> orderedRounds = rounds.OrderByDescending(x => x.DatePlayed).ToList();

            foreach (GolfRound round in rounds)
            {
                double scoreDifferential = ((double)round.Score - round.Tee.RatingFull) * (113.0 / round.Tee.SlopeFull);
                output.Add(scoreDifferential);
            }

            return output;
        }

        public static double CalculateHandicap(List<double> scoreDifferentials)
        {
  

            double runningTotal = 0;
            double handicap;

            // TODO - might technically need 3 scores for a handicap
            if (scoreDifferentials.Count <= 3)
            {
                handicap = scoreDifferentials[0];
                handicap = handicap - 2;
            }
            else if (scoreDifferentials.Count == 4)
            {
                handicap = scoreDifferentials[0];
                handicap = handicap - 1;
            }
            else if (scoreDifferentials.Count == 5)
            {
                handicap = scoreDifferentials[0];
            }
            else if (scoreDifferentials.Count == 6)
            {
                handicap = ((scoreDifferentials[0] + scoreDifferentials[1]) / 2);
                handicap = handicap - 1;
            }
            else if (scoreDifferentials.Count == 7 || scoreDifferentials.Count == 8)
            {
                handicap = ((scoreDifferentials[0] + scoreDifferentials[1]) / 2);
            }
            else if (scoreDifferentials.Count >= 9 || scoreDifferentials.Count <= 11)
            {
                for (int i = 0; i < 3; i++)
                {
                    runningTotal += scoreDifferentials[i];
                }
                handicap = runningTotal / 3;
            }
            else if (scoreDifferentials.Count >= 12 || scoreDifferentials.Count <= 14)
            {
                for (int i = 0; i < 4; i++)
                {
                    runningTotal += scoreDifferentials[i];
                }
                handicap = runningTotal / 4;
            }
            else if (scoreDifferentials.Count >= 15 || scoreDifferentials.Count <= 16)
            {
                for (int i = 0; i < 4; i++)
                {
                    runningTotal += scoreDifferentials[i];
                }
                handicap = runningTotal / 5;
            }
            else if (scoreDifferentials.Count >= 17 || scoreDifferentials.Count <= 18)
            {
                for (int i = 0; i < 5; i++)
                {
                    runningTotal += scoreDifferentials[i];
                }
                handicap = runningTotal / 6;
            }
            else if (scoreDifferentials.Count == 19)
            {
                for (int i = 0; i < 6; i++)
                {
                    runningTotal += scoreDifferentials[i];
                }
                handicap = runningTotal / 7;
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    runningTotal += scoreDifferentials[i];
                }
                handicap = runningTotal / 8;
            }

            return handicap;

       }
    }
}
