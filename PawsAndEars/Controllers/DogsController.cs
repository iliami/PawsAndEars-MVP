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
using Microsoft.AspNet.Identity;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace PawsAndEars.Controllers
{
    
    public class DogsController : Controller
    {
        private readonly AppDbContext db;

        public DogsController(AppDbContext db)
        {
            this.db = db;
        }

        // GET: Dogs
        [Authorize]
        public ActionResult GetAll()
        {
            var repoDogs = db.Dogs.ToList().Where(d => d.UserId.ToString() == User.Identity.GetUserId());
            if (repoDogs.Count() > 0)
            {
                IEnumerable<Models.Dog> dogs = repoDogs.Select(
                    d => new Models.Dog()
                    {
                        Id = d.Id,
                        Name = d.Name,
                        BreedName = d.Breed.Name,
                        Age = d.Age,
                        Length = d.Length,
                        Weight = d.Weight,
                        UserId = d.UserId,
                        Diseases = (d.Diseases == null) ? null : d.Diseases.Select(dis => dis.Name)
                    });
                return View("All", dogs);
            }
            else
                return View("All", new List<Models.Dog>());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Add(Models.Dog dog)
        {
            var d = new EF.Entities.Dog()
            {
                Id = Guid.NewGuid().ToString(),
                Name = dog.Name,
                UserId = User.Identity.GetUserId(),
                Age = dog.Age,
                Length = dog.Length,
                Weight = dog.Weight,
                Breed = db.Breeds.FirstOrDefault(b => b.Name == dog.BreedName)
            };
            db.Dogs.Add(d);
            //db.SaveChanges();
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
            }
            return RedirectToAction("GetAll");
        }

        public ActionResult GetAllBreeds()
        {
            return View("AllBreeds");
        }
    }
}