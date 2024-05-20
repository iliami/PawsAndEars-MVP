using PawsAndEars.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Models
{
    public class ScheduleTimeInterval
    {
        public string Id { get; set; }
        public string DogName { get; set; }
        public DateTime StartActivityTime { get; set; }
        public DateTime EndActivityTime { get; set; }
        public string ActivityName { get; set; }
        public string ActivityId { get; set; }
        public string ActivityNameDescription { get; set; }
    }
}