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
using PawsAndEars.Services.Interfaces;

namespace PawsAndEars.Services
{
    public class ScheduleService : IService<Models.ScheduleTimeInterval, IEnumerable<Models.ScheduleTimeInterval>>
    {
        private AppDbContext context;
        public ScheduleService(AppDbContext context) { this.context = context; }
        public void Create(string dogId, Models.ScheduleTimeInterval model)
        {
            Dog dog = context.Dogs.FirstOrDefault(d => d.Id == dogId);
            
            (Food food, string foodId) = (null, null);
            (Training training, string trainingId) = (null, null);
            switch (model.ActivityType)
            {
                case "Food": 
                    {
                        food = context.Foods.FirstOrDefault(f => f.Id == model.ActivityId);
                        foodId = model.ActivityId;
                        break; 
                    }
                case "Training":
                    {
                        training = context.Trainings.FirstOrDefault(t => t.Id == model.ActivityId);
                        trainingId = model.ActivityId;
                        break;
                    }
            }

            ScheduleTimeInterval scheduleTimeInterval = new ScheduleTimeInterval
            {
                Id = Guid.NewGuid().ToString(),
                DogId = dogId,
                Dog = dog,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                ActivityType = model.ActivityType,
                FoodId = foodId,
                Food = food,
                TrainingId = trainingId,
                Training = training
            };

            context.ScheduleTimeIntervals.Add(scheduleTimeInterval);
        }

        public void Create(string userId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            var startWorkingTime = user.StartWorkingTime;
            var endWorkingTime = user.EndWorkingTime;

            var dogs = context.Dogs.Where(d => d.UserId == userId);
            foreach (var dog in dogs)
            {
                if (context.ScheduleTimeIntervals.ToList().Where(s => s.Dog == dog).Any()) continue;

                var meals = dog.Breed.MealsPerDay;
                var walkingMinutes = dog.Breed.WalkingMinutesPerDay;
                List<ScheduleTimeInterval> scheduleTimeIntervals = new List<ScheduleTimeInterval>(meals + walkingMinutes % 60==0? walkingMinutes / 60 : walkingMinutes / 60 + 1);

                var food = context.Foods.FirstOrDefault(f => f.Id == dog.FoodId);
                for (int i = 0; i < meals; i++)
                {
                    scheduleTimeIntervals.Add(new ScheduleTimeInterval
                        {
                            Id = Guid.NewGuid().ToString(),
                            DogId = dog.Id,
                            Dog = dog,
                            StartTime = DateTime.Now, // -----------------------------------------------------------------------------------------------------------------------------
                            EndTime = DateTime.Now,   // -----------------------------------------------------------------------------------------------------------------------------
                            ActivityType = "Food",
                            FoodId = dog.FoodId,
                            Food = food
                        });
                }

                var training = context.Trainings.FirstOrDefault(t => t.Name == "Walking");
                for (int i = 0; i < walkingMinutes / 60; i++)
                {
                    scheduleTimeIntervals.Add(new ScheduleTimeInterval
                        {
                            Id = Guid.NewGuid().ToString(),
                            DogId = dog.Id,
                            Dog = dog,
                            StartTime = DateTime.Now, // -----------------------------------------------------------------------------------------------------------------------------
                            EndTime = DateTime.Now,   // -----------------------------------------------------------------------------------------------------------------------------
                            ActivityType = "Training",
                            TrainingId = training.Id,
                            Training = training
                        });
                }

                context.ScheduleTimeIntervals.AddRange(scheduleTimeIntervals);
            }
        }

        public void Delete(string stiId)
        {
            var sti = context.ScheduleTimeIntervals.FirstOrDefault(t => t.Id == stiId);
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
                    ActivityString = (s.ActivityType == "Food") ?
                          s.Food.Name + "\n" + s.Food.Description + "\nВес: " + s.Food.Weight + " Калорийность на 100 грамм: " + s.Food.CaloriesPer100g
                        : s.Training.Name + "\n" + s.Training.Description
                }
            );
            return sti;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string stiId, Models.ScheduleTimeInterval model)
        {
            var sti = context.ScheduleTimeIntervals.FirstOrDefault(t => t.Id == stiId);
            var dog = context.Dogs.FirstOrDefault(d => d.Id == model.DogId);

            (Food food, string foodId) = (null, null);
            (Training training, string trainingId) = (null, null);
            switch (model.ActivityType)
            {
                case "Food":
                    {
                        food = context.Foods.FirstOrDefault(f => f.Id == model.ActivityId);
                        foodId = model.ActivityId;
                        break;
                    }
                case "Training":
                    {
                        training = context.Trainings.FirstOrDefault(t => t.Id == model.ActivityId);
                        trainingId = model.ActivityId;
                        break;
                    }
            }

            sti.DogId = model.DogId;
            sti.Dog = dog;
            sti.StartTime = model.StartTime;
            sti.EndTime = model.EndTime;
            sti.ActivityType = model.ActivityType;
            sti.FoodId = foodId;
            sti.Food = food;
            sti.TrainingId = trainingId;
            sti.Training = training;
        }
    }
}