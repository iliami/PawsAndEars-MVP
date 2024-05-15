using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PawsAndEars.EF.Repositories;
using PawsAndEars.Models;
using PawsAndEars.EF.Models;
using Ninject.Infrastructure.Language;


namespace PawsAndEars.Controllers
{
    public class ScheduleController : Controller
    {
        private ScheduleRepository repo;
        private FoodRepository foodRepo;
        private TrainingRepository trainingRepo;
        public ScheduleController(ScheduleRepository scheduleRepository, FoodRepository foodRepository, TrainingRepository trainingRepository)
        {
            repo = scheduleRepository;
            foodRepo = foodRepository;
            trainingRepo = trainingRepository;
        }

        // GET: Schedule/ByDate{date?}
        public async Task<ActionResult> ByDate(string date)
        {
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
                    ActivityId = (int)(i.FoodId ?? i.TrainingId),
                    ActivityNameDescription = (i.FoodId != null) ? (await foodRepo.Get((int)i.FoodId)).Name + "\n" + (await foodRepo.Get((int)i.FoodId)).Description : (await trainingRepo.Get((int)i.TrainingId)).Name + "\n" + (await trainingRepo.Get((int)i.TrainingId)).Description
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
        public ActionResult Create(EF.Models.ScheduleTimeInterval scheduleTimeInterval)
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schedule/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EF.Models.ScheduleTimeInterval scheduleTimeInterval)
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
