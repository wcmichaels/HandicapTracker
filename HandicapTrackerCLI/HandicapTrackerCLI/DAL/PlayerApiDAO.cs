﻿using HandicapTrackerCLI.Models;
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

            return response.Data;
        }

        public Player GetPlayerById(int playerId)
        {
            // TODO - this is just being used right now to get handicap back.  Too much data over the wire just for that?
            RestRequest request = new RestRequest($"players/{playerId}");

            IRestResponse<Player> response = client.Get<Player>(request);

            CheckResponse(response);

            return response.Data;
        }

        public Player CreatePlayer(Player player)
        {
            RestRequest request = new RestRequest("players");
            request.AddJsonBody(player);
            IRestResponse<Player> response = client.Post<Player>(request);



            CheckResponse(response);

            return response.Data;
        }

        public bool CheckIfUsernameAvailable(string username)
        {
            RestRequest request = new RestRequest($"players/create?username={username}");

            IRestResponse<bool> response = client.Get<bool>(request);

            CheckResponse(response);

            return response.Data;

        }


        private static void CheckResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error - unable to reach server");
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error - server returned error response {response.StatusCode} {(int)response.StatusCode}");
            }
        }

    }
}
