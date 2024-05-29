using System.Collections.Generic;

namespace PawsAndEars.EF.Entities
{
    public class Food
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CaloriesPer100g { get; set; }
        public double Weight { get; set; }
        public decimal? Price { get; set; }
        public virtual ICollection<ScheduleTimeInterval> ScheduleTimeIntervals { get; set; }
        public virtual ICollection<Dog> Dogs { get; set; }
    }
}
