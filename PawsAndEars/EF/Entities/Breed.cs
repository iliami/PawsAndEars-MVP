using System.Collections.Generic;

namespace PawsAndEars.EF.Entities
{
    public class Breed
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MealsPerDay { get; set;}
        public int WalkingMinutesPerDay { get; set;}
        public virtual ICollection<Dog> Dogs {  get; set; } 
    }
}