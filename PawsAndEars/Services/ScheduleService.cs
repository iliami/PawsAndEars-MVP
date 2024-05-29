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
            Dog dog = context.Dogs.FirstOrDefault(d => d.Name == model.DogName);
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
                DogId = dog.Id,
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
                          s.Food.Name + " " + s.Food.Description + " Калорийность на 100 грамм: " + s.Food.CaloriesPer100g
                        : s.Training.Name + "\n" + s.Training.Description
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