using PawsAndEars.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Models
{
    public class ScheduleTimeInterval
    {
        public int DogId { get; set; }
        public virtual Dog Dog { get; set; }
        public DateTime StartActivityTime { get; set; }
        public DateTime EndActivityTime { get; set; }
        public string ActivityName { get; set; }
        public int ActivityId { get; set; }
        public IActivity Activity { get; set; }
    }
}