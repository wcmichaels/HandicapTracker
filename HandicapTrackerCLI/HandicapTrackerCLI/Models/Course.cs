using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Par { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public List<Hole> Holes { get; set; } = new List<Hole>();

    }
}
