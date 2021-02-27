
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandicapTrackerAPI.Models
{
    public class Hole
    {
        public int HoleId { get; set; }
        public int HoleNumber { get; set; }
        public int CourseId { get; set; }
        public int ParScore { get; set; }
        public int HoleIndex { get; set; }
    }
}
