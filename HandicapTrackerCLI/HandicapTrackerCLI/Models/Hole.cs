using System;
using System.Collections.Generic;
using System.Text;

namespace HandicapTrackerCLI.Models
{
    public class Hole
    {
        public int HoleId { get; set; }
        public int HoleNumber { get; set; }
        public Course Course { get; set; }
        public int ParScore { get; set; }
        public int HoleIndex { get; set; }

    }
}
