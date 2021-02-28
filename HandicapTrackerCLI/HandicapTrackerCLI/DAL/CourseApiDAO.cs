using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.DAL
{
    public class CourseApiDAO
    {
        RestClient client;
        public CourseApiDAO(string apiUrl)
        {
            this.client = new RestClient(apiUrl);
        }

        
    }
}
