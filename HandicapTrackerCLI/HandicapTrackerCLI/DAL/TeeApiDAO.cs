using HandicapTrackerCLI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.DAL
{
    public class TeeApiDAO : ITeeDAO
    {
        private RestClient client;
        public TeeApiDAO(string apiUrl)
        {
            client = new RestClient(apiUrl);
        }

        public List<Tee> GetAllTees()
        {
            RestRequest request = new RestRequest("tees");

            IRestResponse<List<Tee>> response = client.Get<List<Tee>>(request);
            CheckResponse(response);
            return response.Data;

        }

        public Tee GetTeeWithHoles(int teeId)
        {
            RestRequest request = new RestRequest($"tees/{teeId}");

            IRestResponse<Tee> response = client.Get<Tee>(request);

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
