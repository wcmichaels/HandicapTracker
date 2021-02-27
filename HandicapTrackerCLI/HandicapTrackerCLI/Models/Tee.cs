using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Models
{
    public class Tee
    {
        public int TeeId { get; set; }
        public string Name { get; set; }
        // Got rid of CourseID for a course object
        //public int CourseId { get; set; }
        public Course Course { get; set; }
        public double RatingFull { get; set; }
        public int SlopeFull { get; set; }
        public double RatingFront { get; set; }
        public int SlopeFront { get; set; }
        public double RatingBack { get; set; }
        public int SlopeBack { get; set; }
        public int Yardage { get; set; }
    }
}
