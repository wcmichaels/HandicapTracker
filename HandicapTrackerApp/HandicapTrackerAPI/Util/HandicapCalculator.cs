using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.Util
{
    public static class HandicapCalculator
    {
        public static double HandicapCalculation(List<GolfRound> orderedRounds)
        {
            List<GolfRound> orderedRounds = orderedRounds.OrderByDescending(x => x.DatePlayed).ToList();

            double runningTotalScore = 0;
            double output;

            if (orderedRounds.Count == 3)
            {
                output = orderedRounds[0];
                output = output - 2;
            }
            else if (orderedRounds.Count == 4)
            {
                output = orderedRounds[0];
                output = output - 1;
            }
            else if (orderedRounds.Count == 5)
            {
                output = orderedRounds[0];
            }
            else if (orderedRounds.Count == 6)
            {
                output = ((orderedRounds[0] + orderedRounds[1]) / 2);
                output = output - 1;
            }
            else if (orderedRounds.Count == 7 || orderedRounds.Count == 8)
            {
                output = ((orderedRounds[0] + orderedRounds[1]) / 2);
            }
            else if (orderedRounds.Count >= 9 || orderedRounds.Count <= 11)
            {
                for (int i = 0; i < 3; i++)
                {
                    runningTotalScore += orderedRounds[i];
                }
                output = runningTotalScore / 3;
            }
            else if (orderedRounds.Count >= 12 || orderedRounds.Count <= 14)
            {
                for (int i = 0; i < 4; i++)
                {
                    runningTotalScore += orderedRounds[i];
                }
                output = runningTotalScore / 4;
            }
            else if (orderedRounds.Count >= 15 || orderedRounds.Count <= 16)
            {
                for (int i = 0; i < 4; i++)
                {
                    runningTotalScore += orderedRounds[i];
                }
                output = runningTotalScore / 5;
            }
            else if (orderedRounds.Count >= 17 || orderedRounds.Count <= 18)
            {
                for (int i = 0; i < 5; i++)
                {
                    runningTotalScore += orderedRounds[i];
                }
                output = runningTotalScore / 6;
            }
            else if (orderedRounds.Count == 19)
            {
                for (int i = 0; i < 6; i++)
                {
                    runningTotalScore += orderedRounds[i];
                }
                output = runningTotalScore / 7;
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    runningTotalScore += orderedRounds[i];
                }
                output = runningTotalScore / 8;
            }
            return output;

        }
    }
}
