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
        public string DogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ActivityType { get; set; }
        public string ActivityId { get; set; }
        public string ActivityString { get; set; }
    }
}