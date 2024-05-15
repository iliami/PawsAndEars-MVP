using PawsAndEars.EF.Interfaces;
using PawsAndEars.EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PawsAndEars.EF.Repositories
{
    public class TrainingRepository : IRepository<Training>
    {
        private AppDbContext db;

        public TrainingRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Training> Get(int id)
        {
            return await db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Training>> GetAll()
        {
            return await db.Trainings.ToListAsync();
        }

        public void Save(Training entity)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Training entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}