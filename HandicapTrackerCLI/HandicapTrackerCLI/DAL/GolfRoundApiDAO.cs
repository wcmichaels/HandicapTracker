using HandicapTrackerCLI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.DAL
{
    public class GolfRoundApiDAO : IGolfRoundDAO
    {
        private RestClient client;

        public GolfRoundApiDAO(string apiUrl)
        {
            this.client = new RestClient(apiUrl);
        }

        public GolfRound CreateGolfRound(GolfRound round)
        {
            RestRequest request = new RestRequest("GolfRounds");
            request.AddJsonBody(round);
            IRestResponse<GolfRound> response = client.Post<GolfRound>(request);

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
