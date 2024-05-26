using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PawsAndEars.EF;
using PawsAndEars.EF.Entities;
using PawsAndEars.Services.Interfaces;

namespace PawsAndEars.Services
{
    public class DogsService : IService<Models.Dog, IEnumerable<Models.Dog>>
    {
        private AppDbContext context;

        public DogsService()
        {
            context = new AppDbContext("DefaultConnection"); 
        }

        public void Create(string userId, Models.Dog model)
        {
            Breed breed = context.Breeds.FirstOrDefault(b => b.Name == model.BreedName);
            Food food = context.Foods.FirstOrDefault(f => f.Id == model.FoodId);
            ICollection<Disease> diseases = null;
            if (model.Diseases != null) 
                diseases = context.Diseases
                    .Where(d => model.Diseases.Contains(d.Name))
                    .ToList();

            Dog dog = new Dog
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                BreedId = breed.Id,
                Breed = breed,
                FoodId = model.FoodId,
                Food = food,
                Age = model.Age,
                Weight = model.Weight,
                Length = model.Length,
                Diseases = diseases,
                UserId = userId
            };

            context.Dogs.Add(dog);
            context.SaveChanges();
        }

        public void Create(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Dog> Get(string userId)
        {
            List<Dog> _dogs = context.Dogs
                .Where(d => d.UserId == userId)
                .ToList();
            IEnumerable<Models.Dog> dogs = _dogs.Select(d => new Models.Dog
            {
                Id = d.Id,
                Name = d.Name,
                BreedName = d.Breed.Name,
                FoodId = d.FoodId,
                FoodString = d.Food.Name + "\n" + d.Food.Description + "\nВес: " + d.Food.Weight + " Калорийность на 100 грамм: " + d.Food.CaloriesPer100g,
                Age = d.Age,
                Weight = d.Weight,
                Length = d.Length,
                Diseases = d.Diseases.Select(disease => disease.Name),
                UserId = userId
            });
            return dogs;
        }
    }
}