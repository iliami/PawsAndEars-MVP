using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using PawsAndEars.EF.Entities;
using PawsAndEars.Patterns.Activities;

namespace PawsAndEars.Patterns
{
    public static class ScheduleTimeIntervalFactory
    {
        private static Dictionary<string, Activity> ActivitiesPrototypes = new Dictionary<string, Activity> { { "Walking", new Walking() }, { "Training", new Activities.Training() } };

        public static ScheduleTimeInterval CreateTimeIntervalWithHomemadeFood(double weight, double caloriesPer100g)
        {
            var builder = new FoodBuilder();
           
            var food = builder
                .WithName("Food")
                .WithDescription("Homemade food")
                .WithWeight(weight)
                .WithCalories(caloriesPer100g)
                .Build();

            var sti = new ScheduleTimeInterval()
            {
                Id = Guid.NewGuid().ToString(),
                ActivityType = "Food",
                Food = food,
                FoodId = food.Id
            };

            return sti;
        }

        public static ScheduleTimeInterval CreateTimeIntervalWithPurchasedFood(double weight, double caloriesPer100g, decimal price)
        {
            var builder = new FoodBuilder();

            var food = builder
                .WithName("Food")
                .WithDescription("Purchased food")
                .WithWeight(weight)
                .WithCalories(caloriesPer100g)
                .WithPrice(price)
                .Build();

            var sti = new ScheduleTimeInterval()
            { 
                Id = Guid.NewGuid().ToString(),
                ActivityType = "Food",
                Food = food,
                FoodId = food.Id
            };

            return sti;
        }

        public static ScheduleTimeInterval CreateTimeIntervalWithWalking()
        {
            var walking = ActivitiesPrototypes["Walking"].DeepCopy();

            var sti = new ScheduleTimeInterval 
            {
                Id = Guid.NewGuid().ToString(),
                ActivityType = "Training",
                Training = new EF.Entities.Training
                {
                    Id = walking.GetId(),
                    Name = walking.GetString().Split('-')[0],
                    Description = walking.GetString().Split('-')[1]
                },
                TrainingId = walking.GetId()
            };

            return sti;
        }

        public static ScheduleTimeInterval CreateTimeIntervalWithTraining()
        {
            var training = ActivitiesPrototypes["Training"].DeepCopy();

            var sti = new ScheduleTimeInterval
            {
                Id = Guid.NewGuid().ToString(),
                ActivityType = "Training",
                Training = new EF.Entities.Training
                {
                    Id = training.GetId(),
                    Name = training.GetString().Split('-')[0],
                    Description = training.GetString().Split('-')[1]
                },
                TrainingId = training.GetId()
            };

            return sti;
        }
    }
}