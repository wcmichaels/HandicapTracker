using HandicapTrackerCLI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.DAL
{
    public interface IPlayerDAO
    {
        Player GetPlayerByUsernamePassword(string username, string password);
        Player GetPlayerById(int playerId);

    }
}
