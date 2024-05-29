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
            var startWorkingTime = user.StartWorkingTime.Hour * 60 + user.StartWorkingTime.Minute;
            var endWorkingTime = user.EndWorkingTime.Hour * 60 + user.EndWorkingTime.Minute;

            DateTime startTime;
            int morningTrainingMins = 0;
            DateTime endTime = DateTime.Today.AddHours(23);
            if (startWorkingTime <= 8 * 60)
            {
                startTime = DateTime.Today.AddHours(startWorkingTime / 60 - 1).AddMinutes(startWorkingTime % 60);
            }
            else if (8 * 60 < startWorkingTime && startWorkingTime <= 11 * 60)
            {
                startTime = DateTime.Today.AddHours(startWorkingTime / 60 - 2).AddMinutes(startWorkingTime % 60);
                morningTrainingMins = 60;
            }
            else if (11 * 60 < startWorkingTime && startWorkingTime <= 14 * 60)
            {
                startTime = DateTime.Today.AddHours(startWorkingTime / 60 - 5).AddMinutes(startWorkingTime % 60);
                morningTrainingMins = 120;
            }
            else
            {
                startTime = DateTime.Today.AddHours(startWorkingTime / 60 - 8).AddMinutes(startWorkingTime % 60);
                morningTrainingMins = 240;
            }

            var timeDiff = (endTime - startTime).TotalMinutes;

            var dogs = context.Dogs.Where(d => d.UserId == userId);
            foreach (var dog in dogs)
            {
                if (context.ScheduleTimeIntervals.ToList().Where(s => s.Dog == dog).Any()) continue;

                var meals = dog.Breed.MealsPerDay;
                var walkingMinutes = dog.Breed.WalkingMinutesPerDay;
                List<ScheduleTimeInterval> scheduleTimeIntervals = new List<ScheduleTimeInterval>(meals + walkingMinutes % 60 == 0 ? walkingMinutes / 60 : walkingMinutes / 60 + 1);

                var intervalBetweenMeals = (int)((timeDiff - meals * 15) / meals) / 60;


                var food = context.Foods.FirstOrDefault(f => f.Id == dog.FoodId);
                for (int i = 0, interval = 0; i < meals; i++)
                {
                    scheduleTimeIntervals.Add(new ScheduleTimeInterval
                    {
                        Id = Guid.NewGuid().ToString(),
                        DogId = dog.Id,
                        Dog = dog,
                        StartTime = startTime,
                        EndTime = startTime.AddMinutes(15),
                        ActivityType = "Food",
                        FoodId = dog.FoodId,
                        Food = food
                    });

                    if (user.StartWorkingTime <= startTime.AddHours(intervalBetweenMeals) && startTime.AddHours(intervalBetweenMeals) <= startTime.AddHours(intervalBetweenMeals))
                    {
                        interval = (int)(user.EndWorkingTime - startTime.AddHours(intervalBetweenMeals)).TotalMinutes;
                        startTime = user.EndWorkingTime;
                    }
                    else
                    {
                        if (interval > 0)
                        {
                            intervalBetweenMeals -= (int)(1.0 * interval / (meals - i));
                            interval = 0;
                        }
                        startTime = startTime.AddHours(intervalBetweenMeals);
                    }
                }

                int morningTrainingMinsForDog = Math.Min(morningTrainingMins, walkingMinutes);
                var training = context.Trainings.FirstOrDefault(t => t.Name == "Walking");
                startTime = scheduleTimeIntervals[0].EndTime.AddMinutes(15);
                scheduleTimeIntervals.Add(new ScheduleTimeInterval
                {
                    Id = Guid.NewGuid().ToString(),
                    DogId = dog.Id,
                    Dog = dog,
                    StartTime = startTime,
                    EndTime = startTime.AddMinutes(morningTrainingMins),
                    ActivityType = "Training",
                    TrainingId = training.Id,
                    Training = training
                });
                walkingMinutes -= morningTrainingMins;
                if (walkingMinutes > 0)
                    scheduleTimeIntervals.Add(new ScheduleTimeInterval
                    {
                        Id = Guid.NewGuid().ToString(),
                        DogId = dog.Id,
                        Dog = dog,
                        StartTime = scheduleTimeIntervals.First(f => f.ActivityType == "Food" && f.StartTime > user.EndWorkingTime).EndTime.AddMinutes(15),
                        EndTime = scheduleTimeIntervals.First(f => f.ActivityType == "Food" && f.StartTime > user.EndWorkingTime).EndTime.AddMinutes(15 + walkingMinutes),
                        ActivityType = "Training",
                        TrainingId = training.Id,
                        Training = training
                    });

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