using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PawsAndEars.EF;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.Models;

namespace PawsAndEars.Controllers
{
    public class DogsController : Controller
    {
        private readonly IRepository<Dog> repo;

        public DogsController(IRepository<Dog> dogRepository)
        {
            repo = dogRepository;
        }

        // GET: Dogs
        public async Task<ActionResult> GetAll()
        {
            var dogs = await repo.GetAll();
            return View("All", dogs);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(Dog dog)
        {
            repo.Save(dog);
            var dogs = await repo.GetAll();
            return View("All", dogs);
        }
    }
}