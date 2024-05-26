using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ninject.Infrastructure.Language;
using PawsAndEars.Services.Interfaces;


namespace PawsAndEars.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private IService<Models.ScheduleTimeInterval, IEnumerable<Models.ScheduleTimeInterval>> scheduleService;
        public ScheduleController(IService<Models.ScheduleTimeInterval, IEnumerable<Models.ScheduleTimeInterval>> scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        // GET: Schedule/ByDate{date?}
        public ActionResult ByDate(string date)
        {
            if (date == null) date = DateTime.Today.ToShortDateString();
            var scheduleFromDb = scheduleService.Get(User.Identity.GetUserId());
            var schedule = scheduleFromDb.Where(s => s.StartTime.ToShortDateString() == date);
            return View("Schedule", (schedule.ToEnumerable(), date));
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        [HttpPost]
        public ActionResult Create(Models.ScheduleTimeInterval scheduleTimeInterval)
        {
            scheduleService.Create(User.Identity.GetUserId(), scheduleTimeInterval);
            scheduleService.Save();
            return RedirectToAction("ByDate");
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Schedule/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Models.ScheduleTimeInterval scheduleTimeInterval)
        {
            scheduleService.Update(id, scheduleTimeInterval);
            scheduleService.Save();
            return RedirectToAction("ByDate");
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Schedule/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            scheduleService.Delete(id);
            scheduleService.Save();
            return RedirectToAction("ByDate");
        }
    }
}
