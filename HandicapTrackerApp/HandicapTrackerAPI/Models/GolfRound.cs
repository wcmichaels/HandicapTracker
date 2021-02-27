using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.Models
{
    public class GolfRound
    {
        public int GolfRoundId { get; set; }
        public int PlayerId { get; set; }
        public int TeeId { get; set; }
        public DateTime DatePlayed { get; set; }
        public int Score { get; set; }
    }
}
