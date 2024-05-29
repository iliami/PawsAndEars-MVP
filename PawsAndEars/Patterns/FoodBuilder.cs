using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.EnterpriseServices;
using PawsAndEars.EF.Entities;

namespace PawsAndEars.Patterns
{
    public class FoodBuilder
    {
        private string Id = Guid.NewGuid().ToString();
        private string Name = "Food";
        private string Description = "Food";
        private double CaloriesPer100g { get; set; }
        private double Weight { get; set; }
        private decimal? Price { get; set; }
        
        public FoodBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        public FoodBuilder WithDescription(string description) 
        {
            this.Description = description; 
            return this;
        }

        public FoodBuilder WithWeight(double weight)
        {
            this.Weight = weight;
            return this;
        }

        public FoodBuilder WithCalories(double caloriesPer100g)
        {
            this.CaloriesPer100g = caloriesPer100g;
            return this;
        }

        public FoodBuilder WithPrice(decimal price)
        {
            this.Price = price;
            return this;
        }

        public Food Build()
        {
            var food = new Food
            {
                Id = Id,
                Name = Name,
                Description = Description,
                CaloriesPer100g = CaloriesPer100g,
                Weight = Weight,
                Price = Price
            };

            return food;
        }
        
    }
}