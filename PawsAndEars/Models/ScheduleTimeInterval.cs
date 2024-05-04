using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars.Models
{
    public class ScheduleTimeInterval
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public virtual Dog Dog { get; set; }
        public DateTime StartActivityTime { get; set; }
        public DateTime EndActivityTime { get; set; }
        public string ActivityName { get; set; }
        public int ActivityId { get; set; }

        public ScheduleTimeInterval()
        {
            
        }
    }
}
