using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PawsAndEars.Services.Interfaces;

namespace PawsAndEars.Controllers
{

    public class DogsController : Controller
    {
        private IService<Models.Dog, IEnumerable<Models.Dog>> dogsService;
        private IService<Models.ScheduleTimeInterval, IEnumerable<Models.ScheduleTimeInterval>> scheduleCreatingService;

        public DogsController(
            IService<Models.Dog, IEnumerable<Models.Dog>> dogsService,
            IService<Models.ScheduleTimeInterval, IEnumerable<Models.ScheduleTimeInterval>> scheduleCreatingService)
        {
            this.dogsService = dogsService;
            this.scheduleCreatingService = scheduleCreatingService;
        }

        // GET: Dogs
        [Authorize]
        public ActionResult GetAll()
        {
            var dogs = dogsService.Get(User.Identity.GetUserId());
            return View("All", dogs);
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
            if ((0 >= dog.Age && dog.Age < 15) ||
                (0 >= dog.Weight && dog.Weight < 50) ||
                (0 >= dog.Length && dog.Length < 75))
            {
                ModelState.AddModelError("", "Неверно заполнено одно из полей: возраст, вес, длина");
                return View(dog);
            }
            dogsService.Create(User.Identity.GetUserId(), dog);
            dogsService.Save();
            scheduleCreatingService.Create(User.Identity.GetUserId());
            scheduleCreatingService.Save();
            return RedirectToAction("GetAll");
        }

        public ActionResult GetAllBreeds()
        {
            return View("AllBreeds");
        }

        // GET: Dogs/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Dogs/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Models.Dog dog)
        {
            dogsService.Update(id, dog);
            dogsService.Save();
            return RedirectToAction("GetAll");
        }

        // GET: Dogs/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Dogs/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            dogsService.Delete(id);
            dogsService.Save();
            return RedirectToAction("GetAll");
        }
    }
}