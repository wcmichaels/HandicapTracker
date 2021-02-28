using HandicapTrackerCLI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.DAL
{
    public interface IGolfRoundDAO
    {
        public GolfRound CreateGolfRound(GolfRound round);

    }
}
