using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MealsPerDay { get; set;}
        public int WalkingMinutesPerDay { get; set;}
        public virtual ICollection<Dog> Dogs {  get; set; } 
    }
}