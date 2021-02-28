using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Models
{
    public class HoleResult
    {
        public int HoleResultId { get; set; }
        //public int GolfRound { get; set; }
        public Hole Hole { get; set; }
        public int Score { get; set; }
        public bool HitFairway { get; set; }
        public int Putts { get; set; }
        public bool InGreensideBunker { get; set; }
        public bool OutOfBounds { get; set; }
        public bool InWater { get; set; }
        public bool DropOrOther { get; set; }

    }
}
