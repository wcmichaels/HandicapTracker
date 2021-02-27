using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.Models
{
    public class Tee
    {
        public int TeeId { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public double RatingFull { get; set; }
        public int SlopeFull { get; set; }
        public double RatingFront { get; set; }
        public int SlopeFront { get; set; }
        public double RatingBack { get; set; }
        public int SlopeBack { get; set; }
        public int Yardage { get; set; }
    }
}
