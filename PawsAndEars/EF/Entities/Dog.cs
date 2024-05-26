using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars.EF.Entities
{
    public class Dog
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BreedId { get; set; }
        public virtual Breed Breed { get; set; }
        public string FoodId { get; set; }
        public virtual Food Food { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; } = null;
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ScheduleTimeInterval> ScheduleTimeIntervals { get; set; }
    }
}
