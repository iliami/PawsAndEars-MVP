using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CaloriesPer100g { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
    }
}
