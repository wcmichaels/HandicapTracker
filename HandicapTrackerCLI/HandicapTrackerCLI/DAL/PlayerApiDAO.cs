using HandicapTrackerCLI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.DAL
{
    public class PlayerApiDAO : IPlayerDAO
    {
        private RestClient client;

        public PlayerApiDAO(string apiUrl)
        {
            client = new RestClient(apiUrl);
        }

        public Player GetPlayerByUsernamePassword(string username, string password)
        {
            RestRequest request = new RestRequest($"players/login?username={username}&password={password}");

            IRestResponse<Player> response = client.Get<Player>(request);

            CheckResponse(response);

            return response.Data;
        }

        private static void CheckResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error - unable to reach server");
            }
            if ((int)response.StatusCode == 404)
            {
                throw new Exception("Invalid login credentials");
            }
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error - server returned error response {response.StatusCode} {(int)response.StatusCode}");
            }
        }

    }
}
