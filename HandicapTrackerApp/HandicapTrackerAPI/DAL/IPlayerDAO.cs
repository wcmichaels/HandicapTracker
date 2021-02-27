using HandicapTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.DAL
{
    public interface IPlayerDAO
    {
        Player GetPlayerById(int id);
        List<Player> ListPlayers();
        Player CreatePlayer(Player player);
        Player UpdatePlayer(Player player);
    }
}
