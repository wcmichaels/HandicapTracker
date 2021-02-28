using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Models
{
    public class GolfRound
    {
        public int GolfRoundId { get; set; }
        public int PlayerId { get; set; }
        // got rid of TeeId for a Tee object
        //public int TeeId { get; set; }
        public DateTime DatePlayed { get; set; }
        public int Score { get; set; }
        public Tee Tee { get; set; }
        public List<HoleResult> HoleResults { get; set; } = new List<HoleResult>();

    }
}
