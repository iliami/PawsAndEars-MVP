using PawsAndEars.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Models
{
    public class Food : IActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CaloriesPer100g { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }

        public string getDescription() => Description;
        public string getName() => Name;
    }
}