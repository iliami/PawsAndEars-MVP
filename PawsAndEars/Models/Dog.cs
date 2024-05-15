using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string BreedName { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public virtual IEnumerable<string> Diseases { get; set; }
        public int UserId { get; set; }
    }
}