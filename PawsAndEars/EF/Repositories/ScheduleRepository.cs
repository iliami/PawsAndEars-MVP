using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.EF.Entities;

namespace PawsAndEars.EF.Repositories
{
    public class ScheduleRepository : IRepository<ScheduleTimeInterval>
    {
        private AppDbContext db;
        public ScheduleRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<ScheduleTimeInterval> Get(int id)
        {
            return await db.ScheduleTimeIntervals.AsNoTracking().FirstOrDefaultAsync(interval => interval.Id == id);
        }

        public async Task<IEnumerable<ScheduleTimeInterval>> GetAll()
        {
            return await db.ScheduleTimeIntervals
                .OrderBy(interval => interval.StartActivityTime)
                .OrderBy(interval => interval.EndActivityTime)
                .Include(interval => interval.Dog)
                .ToListAsync();
        }

        public IEnumerable<ScheduleTimeInterval> GetByDate(string date)
        {
            return db.ScheduleTimeIntervals
                .Include(interval => interval.Dog)
                .ToList()
                .Where(interval => interval.StartActivityTime.ToShortDateString() == date)
                .OrderBy(interval => interval.StartActivityTime)
                .OrderBy(interval => interval.EndActivityTime)
                .ToList();
        }
        public void Update(int id, ScheduleTimeInterval entity)
        {
            var entityToUpdate = db.ScheduleTimeIntervals.FirstOrDefault(x => x.Id == id);
            entityToUpdate.StartActivityTime = entity.StartActivityTime;
            entityToUpdate.EndActivityTime = entity.EndActivityTime;
            entityToUpdate.ActivityName = entity.ActivityName;
            entityToUpdate.FoodId = entity.FoodId;
            entityToUpdate.TrainingId = entity.TrainingId;
            entityToUpdate.DogId = entity.DogId;

            db.SaveChanges();
        }
        public void Save(ScheduleTimeInterval entity)
        {
            db.ScheduleTimeIntervals.Add(entity);
            db.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}