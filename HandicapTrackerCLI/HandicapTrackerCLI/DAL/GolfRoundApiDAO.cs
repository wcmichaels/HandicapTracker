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
            RestClient client = new RestClient(apiUrl);
        }
    }
}
