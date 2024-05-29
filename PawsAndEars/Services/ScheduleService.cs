using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using PawsAndEars.EF;
using PawsAndEars.EF.Entities;
using PawsAndEars.Patterns;
using PawsAndEars.Services.Interfaces;

namespace PawsAndEars.Services
{
    public class ScheduleService : IService<Models.ScheduleTimeInterval, IEnumerable<Models.ScheduleTimeInterval>>
    {
        private AppDbContext context;
        public ScheduleService(AppDbContext context) { this.context = context; }
        
        
        
        public void Create(string userId, Models.ScheduleTimeInterval model)
        {
            ScheduleTimeInterval sti = new ScheduleTimeInterval();
            Dog dog = context.Dogs.FirstOrDefault(d => d.UserId == userId && d.Name == model.DogName);
            switch (model.ActivityType)
            {
                case "Food":
                    {
                        if (model.IsPurchased)
                            sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithPurchasedFood(model.Weight, model.CaloriesPer100g, model.Price);
                        else
                            sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithHomemadeFood(model.Weight, model.CaloriesPer100g);

                        var builder = new FoodBuilder();

                        var food = builder
                            .WithName(model.ActivityName)
                            .WithDescription(model.ActivityDescription)
                            .WithWeight(model.Weight)
                            .WithCalories(model.CaloriesPer100g)
                            .WithPrice(model.Price).Build();

                        sti.StartTime = model.StartTime;
                        sti.EndTime = model.EndTime;
                        sti.Dog = dog;
                        sti.DogId = dog.Id;
                        sti.Food = food;
                        sti.FoodId = food.Id;
                        
                        break;
                    }
                case "Training":
                    {
                        sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithWalking();
                        sti.StartTime = model.StartTime;
                        sti.EndTime = model.EndTime;
                        sti.Dog = dog;
                        sti.DogId = dog.Id;
                        sti.Training.Name = model.ActivityName;
                        sti.Training.Description = model.ActivityDescription;

                        break;
                    }
            }

            context.ScheduleTimeIntervals.Add(sti);
        }

        public void Create(string userId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            var dogs = context.Dogs.Where(d => d.UserId == userId);

            foreach (var dog in dogs)
            {
                if (context.ScheduleTimeIntervals.ToList().Where(s => s.Dog == dog).Any()) continue;

                var builder = new ScheduleBuilder();
                builder
                    .WithMealsPerDay(dog.Breed.MealsPerDay)
                    .WithWalkingMinutesPerDay(dog.Breed.WalkingMinutesPerDay)
                    .WithWorkingTime(user.StartWorkingTime, user.EndWorkingTime);

                var scheduleTimeIntervals = builder.Build(dog);

                context.ScheduleTimeIntervals.AddRange(scheduleTimeIntervals);

                for (int i = 1; i < 7; i++)
                {
                    foreach (var _sti in scheduleTimeIntervals)
                    {
                        var sti = new ScheduleTimeInterval
                        {
                            Id = Guid.NewGuid().ToString(),
                            DogId = dog.Id,
                            Dog = dog,
                            StartTime = _sti.StartTime.AddDays(i),
                            EndTime = _sti.EndTime.AddDays(i),
                            ActivityType = _sti.ActivityType,
                            FoodId = _sti.FoodId,
                            Food = _sti.Food,
                            TrainingId = _sti.TrainingId,
                            Training = _sti.Training
                        };
                        context.ScheduleTimeIntervals.Add(sti);
                    }
                }
            }
        }

        public void Delete(string stiId)
        {
            var sti = context.ScheduleTimeIntervals.Include(t => t.Food).Include(t => t.Training).FirstOrDefault(t => t.Id == stiId);
            context.ScheduleTimeIntervals.Remove(sti);
            context.SaveChanges();
        }

        public IEnumerable<Models.ScheduleTimeInterval> Get(string userId)
        {
            var dogs = context.Dogs.Where(d => d.UserId == userId);
            var _sti = context.ScheduleTimeIntervals.Include(s => s.Dog).Where(s => dogs.Contains(s.Dog)).ToList();
            var sti = _sti.Select(s =>
                new Models.ScheduleTimeInterval
                {
                    Id = s.Id,
                    DogId = s.DogId,
                    DogName = s.Dog.Name,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    ActivityType = s.ActivityType,
                    ActivityId = s.FoodId ?? s.TrainingId,
                    ActivityString = (s.ActivityType == "Food") ? (
                        (s.Food.Price == null) ?
                          $"{s.Food.Name} - {s.Food.Description} - Weight: {s.Food.Weight} - Calories 100g: {s.Food.CaloriesPer100g}" :
                          $"{s.Food.Name} - {s.Food.Description} - Price: {s.Food.Price} - Weight: {s.Food.Weight} - Calories 100g: {s.Food.CaloriesPer100g}"
                          )
                        : s.Training.Name + " - " + s.Training.Description
                }
            );
            sti = sti.OrderBy(s => s.StartTime).OrderBy(s => s.EndTime).ToList();
            return sti;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string stiId, Models.ScheduleTimeInterval model)
        {
            var _sti = context.ScheduleTimeIntervals.FirstOrDefault(t => t.Id == stiId);
            var dog = context.Dogs.FirstOrDefault(d => d.Id == model.DogId && d.Name == model.DogName);

            ScheduleTimeInterval sti = new ScheduleTimeInterval();
            switch (model.ActivityType)
            {
                case "Food":
                    {
                        if (model.IsPurchased)
                            sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithPurchasedFood(model.Weight, model.CaloriesPer100g, model.Price);
                        else
                            sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithHomemadeFood(model.Weight, model.CaloriesPer100g);

                        var builder = new FoodBuilder();

                        var food = builder
                            .WithName(model.ActivityName)
                            .WithDescription(model.ActivityDescription)
                            .WithWeight(model.Weight)
                            .WithCalories(model.CaloriesPer100g)
                            .WithPrice(model.Price).Build();

                        sti.StartTime = model.StartTime;
                        sti.EndTime = model.EndTime;
                        sti.Dog = dog;
                        sti.DogId = dog.Id;
                        sti.Food = food;
                        sti.FoodId = food.Id;

                        break;
                    }
                case "Training":
                    {
                        sti = ScheduleTimeIntervalFactory.CreateTimeIntervalWithWalking();
                        sti.StartTime = model.StartTime;
                        sti.EndTime = model.EndTime;
                        sti.Dog = dog;
                        sti.DogId = dog.Id;
                        sti.Training.Name = model.ActivityName;
                        sti.Training.Description = model.ActivityDescription;

                        break;
                    }
            }

            _sti.StartTime = sti.StartTime;
            _sti.EndTime = sti.EndTime;
            _sti.Dog = sti.Dog;
            _sti.DogId = sti.DogId;
            _sti.Food = sti.Food;
            _sti.FoodId = sti.FoodId;
            _sti.Training = sti.Training;
            _sti.TrainingId = sti.TrainingId;
        }
    }
}