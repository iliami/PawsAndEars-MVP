using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PawsAndEars.EF;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.Models;
using PawsAndEars.EF.Entities;

namespace PawsAndEars.Controllers
{
    public class DogsController : Controller
    {
        private readonly IRepository<EF.Entities.Dog> repo;

        public DogsController(IRepository<EF.Entities.Dog> dogRepository)
        {
            repo = dogRepository;
        }

        // GET: Dogs
        public async Task<ActionResult> GetAll()
        {
            var repoDogs = await repo.GetAll();
            IEnumerable<Models.Dog> dogs = repoDogs.Select(
                d => new Models.Dog() 
                { 
                    Name = d.Name, 
                    BreedName = d.Breed.Name, 
                    Age = d.Age, 
                    Length = d.Length, 
                    Weight = d.Weight, 
                    UserId = d.UserId, 
                    Diseases = d.Diseases.Select(dis => dis.Name) 
                });
            return View("All", dogs);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(EF.Entities.Dog dog)
        {
            repo.Save(dog);
            return RedirectToAction("GetAll");
        }
    }
}