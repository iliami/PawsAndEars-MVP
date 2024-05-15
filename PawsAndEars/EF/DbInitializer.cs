using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PawsAndEars.EF.Models;

namespace PawsAndEars.EF
{
    public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            Disease disease = new Disease() { Id = 1, Name = "Puppy-poe syndrom", Description = "Abashapotyq" };

            List<Breed> breeds = new List<Breed>()
            {
                new Breed() { Id = 1, Name = "A", MealsPerDay = 2, WalkingMinutesPerDay=120},
                new Breed() { Id = 2, Name = "AA", MealsPerDay = 3, WalkingMinutesPerDay=150},
                new Breed() { Id = 3, Name = "AAA", MealsPerDay = 4, WalkingMinutesPerDay=180}
            };

            User user = new User() { Id = 1, Name = "Joe", StartWorkingTime = new DateTime(2024, 1, 1, 8, 0, 0), EndWorkingTime = new DateTime(2024, 1, 1, 17, 0, 0) };

            List<Dog> dogs = new List<Dog>() 
            {
                new Dog() { Id = 1, Name = "A", User = user, UserId = 1, Age = 12, Breed = breeds[0], Length = 100, Weight = 10 }
            ,   new Dog() { Id = 2, Name = "AA", User = user, UserId = 1, Age = 24, Breed = breeds[1], Length = 150, Weight = 15, Diseases = new List<Disease>() { disease } }
            };

            Food food = new Food() { Id = 1, Name = "Cracker", Description = "Tasty crackers for your dog", Weight = 1000, CaloriesPer100g = 250, Price = 5.87m };
            
            Training training = new Training() { Id = 1, Name = "Walking", Description = "Walking is the best training exercise for dogs" };
            
            List<ScheduleTimeInterval> schedule = new List<ScheduleTimeInterval>
            {
                new ScheduleTimeInterval() { Id = 1, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + 8*60*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 15)*60*1000*10000L) }
            ,   new ScheduleTimeInterval() { Id = 2, Dog = dogs[1], DogId = dogs[1].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 5)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 15)*60*1000*10000L) }
            ,   new ScheduleTimeInterval() { Id = 3, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Training", TrainingId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 30)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + (10*60 + 30)*60*1000*10000L) }
            ,   new ScheduleTimeInterval() { Id = 4, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + (24+8)*60*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 15)*60*1000*10000L) }
            ,   new ScheduleTimeInterval() { Id = 5, Dog = dogs[1], DogId = dogs[1].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 5)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 15)*60*1000*10000L) }
            ,   new ScheduleTimeInterval() { Id = 6, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Training", TrainingId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 30)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + ((24+10)*60 + 30)*60*1000*10000L) }
            };

            context.Diseases.Add(disease);
            context.Breeds.AddRange(breeds);
            context.Foods.Add(food);
            context.Trainings.Add(training);
            context.Users.Add(user);
            context.Dogs.AddRange(dogs);
            context.ScheduleTimeIntervals.AddRange(schedule);
            
            context.SaveChanges();
        }
    }
}