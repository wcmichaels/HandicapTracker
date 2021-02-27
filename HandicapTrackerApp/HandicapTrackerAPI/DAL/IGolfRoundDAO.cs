using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public interface IGolfRoundDAO
    {
        GolfRound CreateGolfRound(GolfRound round);
        GolfRound GetGolfRoundById(int id);
    }
}
