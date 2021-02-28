using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public interface IGolfRoundDAO
    {
        List<GolfRound> GetGolfRoundsByPlayerId(int playerId);
        GolfRound GetGolfRoundById(int id);

        GolfRound CreateGolfRound(GolfRound round);
    }
}
