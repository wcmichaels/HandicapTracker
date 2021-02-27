using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Handicap { get; set; }
        public DateTime DOB { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<GolfRound> GolfRounds { get; set; } = new List<GolfRound>();
    }
}
