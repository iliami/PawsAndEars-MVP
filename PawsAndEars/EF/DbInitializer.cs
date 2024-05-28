using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PawsAndEars.EF.Entities;

namespace PawsAndEars.EF
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext> //DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            if (context.Diseases.Any()) return;

            //Disease disease = new Disease() { Id = Guid.NewGuid().ToString(), Name = "Puppy-poe syndrom", Description = "Abashapotyq" };
            List<Disease> diseases = new List<Disease>()
            {
                new Disease() { Id = Guid.NewGuid().ToString(), Name = "Сердечная недостаточность", Description = "Abashapotyq" },
                new Disease() { Id = Guid.NewGuid().ToString(), Name = "Проблемы с ЖКТ", Description = "Abashapotyq" },
                new Disease() { Id = Guid.NewGuid().ToString(), Name = "Почечная недостаточность", Description = "Renal RF14" },
                new Disease() { Id = Guid.NewGuid().ToString(), Name = "Струвидныe камни в почках", Description = "Veterinary Diets UR Urinary" }
            };

            List<Breed> breeds = new List<Breed>()
            {
                new Breed() { Id = Guid.NewGuid().ToString(), Name = "Немецкая овчарка", MealsPerDay = 2, WalkingMinutesPerDay=120},
                new Breed() { Id = Guid.NewGuid().ToString(), Name = "Золотой ретривер", MealsPerDay = 3, WalkingMinutesPerDay=180},
                new Breed() { Id = Guid.NewGuid().ToString(), Name = "Чихуахуа", MealsPerDay = 3, WalkingMinutesPerDay=30},
                new Breed() { Id = Guid.NewGuid().ToString(), Name = "Немецкий шпиц", MealsPerDay = 3, WalkingMinutesPerDay=40},
                new Breed() { Id = Guid.NewGuid().ToString(), Name = "Йоркширский терьер", MealsPerDay = 4, WalkingMinutesPerDay=60}
            };

            //User user = new User() { StartWorkingTime = new DateTime(2024, 1, 1, 8, 0, 0), EndWorkingTime = new DateTime(2024, 1, 1, 17, 0, 0) };

            //List<Dog> dogs = new List<Dog>()
            //{
            //    new Dog() { Id = 1, Name = "A", User = user, Age = 12, Breed = breeds[0], Length = 100, Weight = 10 }
            //,   new Dog() { Id = 2, Name = "AA", User = user, Age = 24, Breed = breeds[1], Length = 150, Weight = 15, Diseases = new List<Disease>() { disease } }
            //};

            //Food food = new Food() { Id = Guid.NewGuid().ToString(), Name = "Cracker", Description = "Tasty crackers for your dog", Weight = 1000, CaloriesPer100g = 250, Price = 5.87m };
            List<Food> food = new List<Food>()
            {
                new Food(){Id = Guid.NewGuid().ToString(), Name = "Veterinary Diets UR Urinary", Description = "Сухой корм для взрослых собак для растворения струвитных камней", Weight = 1500, CaloriesPer100g = 200, Price = 3315m },
                new Food(){Id = Guid.NewGuid().ToString(), Name = "Renal RF14", Description = "Сухой корм для собак при почечной недостаточности", Weight = 2000, CaloriesPer100g = 230, Price = 3139m },
                new Food(){Id = Guid.NewGuid().ToString(), Name = "Grandin Holistic", Description = "Сухой корм для собак средних и крупных пород", Weight = 2700, CaloriesPer100g = 250, Price = 3499m }
            };
            Training training = new Training() { Id = Guid.NewGuid().ToString(), Name = "Walking", Description = "Walking is the best training exercise for dogs" };

            //List<ScheduleTimeInterval> schedule = new List<ScheduleTimeInterval>
            //{
            //    new ScheduleTimeInterval() { Id = 1, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + 8*60*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 15)*60*1000*10000L) }
            //,   new ScheduleTimeInterval() { Id = 2, Dog = dogs[1], DogId = dogs[1].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 5)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 15)*60*1000*10000L) }
            //,   new ScheduleTimeInterval() { Id = 3, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Training", TrainingId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + (8*60 + 30)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + (10*60 + 30)*60*1000*10000L) }
            //,   new ScheduleTimeInterval() { Id = 4, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + (24+8)*60*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 15)*60*1000*10000L) }
            //,   new ScheduleTimeInterval() { Id = 5, Dog = dogs[1], DogId = dogs[1].Id, ActivityName = "Food", FoodId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 5)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 15)*60*1000*10000L) }
            //,   new ScheduleTimeInterval() { Id = 6, Dog = dogs[0], DogId = dogs[0].Id, ActivityName = "Training", TrainingId = 1, StartActivityTime = new DateTime(DateTime.Today.Ticks + ((24+8)*60 + 30)*60*1000*10000L), EndActivityTime = new DateTime(DateTime.Today.Ticks + ((24+10)*60 + 30)*60*1000*10000L) }
            //};

            context.Diseases.AddRange(diseases);
            context.Breeds.AddRange(breeds);
            context.Foods.AddRange(food);
            context.Trainings.Add(training);
            //context.Users.Add(user);
            //context.Dogs.AddRange(dogs);
            //context.ScheduleTimeIntervals.AddRange(schedule);

            context.SaveChanges();
        }
    }
}