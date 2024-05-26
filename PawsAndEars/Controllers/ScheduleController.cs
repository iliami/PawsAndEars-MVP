using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PawsAndEars.EF.Repositories;
using PawsAndEars.Models;
using PawsAndEars.EF.Entities;
using Ninject.Infrastructure.Language;
using Microsoft.AspNet.Identity;


namespace PawsAndEars.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private DogRepository dogRepo;
        private ScheduleRepository repo;
        private FoodRepository foodRepo;
        private TrainingRepository trainingRepo;
        public ScheduleController(DogRepository dogRepository, ScheduleRepository scheduleRepository, FoodRepository foodRepository, TrainingRepository trainingRepository)
        {
            dogRepo = dogRepository;
            repo = scheduleRepository;
            foodRepo = foodRepository;
            trainingRepo = trainingRepository;
        }

        // GET: Schedule/ByDate{date?}
        public async Task<ActionResult> ByDate(string date)
        {
            //var dogs = await dogRepo.GetAll();
            //if (!(dogs.Any(d => d.UserId.ToString() == User.Identity.GetUserId()))) return View("");
            if (date == null) date = DateTime.Today.ToShortDateString();
            var repoSchedule = repo.GetByDate(date).ToList();
            List<Models.ScheduleTimeInterval> schedule = new List<Models.ScheduleTimeInterval>();
            foreach (var i in repoSchedule)
            {
                var item = new Models.ScheduleTimeInterval()
                {
                    Id = i.Id,
                    DogName = i.Dog.Name,
                    StartActivityTime = i.StartActivityTime,
                    EndActivityTime = i.EndActivityTime,
                    ActivityName = i.ActivityName,
                    ActivityId = (i.FoodId ?? i.TrainingId),
                    ActivityNameDescription = (i.FoodId != null) ? (await foodRepo.Get(i.FoodId)).Name + "\n" + (await foodRepo.Get(i.FoodId)).Description : (await trainingRepo.Get(i.TrainingId)).Name + "\n" + (await trainingRepo.Get(i.TrainingId)).Description
                };
                schedule.Add(item);
            }
            return View("Schedule", (schedule.ToEnumerable(), date));
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        [HttpPost]
        public ActionResult Create(EF.Entities.ScheduleTimeInterval scheduleTimeInterval)
        {
            try
            {
                // TODO: Add insert logic here
                repo.Save(scheduleTimeInterval);
                return RedirectToAction("Schedule");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Schedule/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, EF.Entities.ScheduleTimeInterval scheduleTimeInterval)
        {
            try
            {
                // TODO: Add update logic here
                repo.Update(id, scheduleTimeInterval);
                return RedirectToAction("Schedule");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schedule/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Schedule");
            }
            catch
            {
                return View();
            }
        }
    }
}
