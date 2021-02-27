using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.Models
{
    public class HoleResult
    {
        public int HoleResultId { get; set; }
        public int GolfRoundId { get; set; }
        public int HoleId { get; set; }
        public int Score { get; set; }
        public bool? HitFairway { get; set; }
        public int? Putts { get; set; }
        public bool? InGreensideBunker { get; set; }
        public bool? OutOfBounds { get; set; }
        public bool? InWater { get; set; }
        public bool? DropOrOther { get; set; }

    }
}
