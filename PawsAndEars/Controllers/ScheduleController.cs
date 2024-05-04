using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PawsAndEars.EF.Repositories;
using PawsAndEars.Models;

namespace PawsAndEars.Controllers
{
    public class ScheduleController : Controller
    {
        private ScheduleRepository repo;
        public ScheduleController(ScheduleRepository scheduleRepository)
        {
            repo = scheduleRepository;
        }

        // GET: Schedule/{date?}
        public ActionResult Schedule(string date)
        {
            if (date == null) date = DateTime.Today.ToShortDateString();
            var schedule = repo.GetByDate(date);
            return View((schedule, date));
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        [HttpPost]
        public ActionResult Create(ScheduleTimeInterval scheduleTimeInterval)
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
        public ActionResult Edit(int id, ScheduleTimeInterval scheduleTimeInterval)
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
