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
        Player GetPlayerByUsernamePassword(string username, string password);
        List<Player> ListPlayers();
        Player CreatePlayer(Player player);
        Player UpdatePlayer(Player player);
    }
}
