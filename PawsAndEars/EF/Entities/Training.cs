using System.Collections.Generic;

namespace PawsAndEars.EF.Entities
{
    public class Training
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ScheduleTimeInterval> ScheduleTimeIntervals { get; set; }
    }
}
