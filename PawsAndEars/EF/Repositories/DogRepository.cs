using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.Models;

namespace PawsAndEars.EF.Repositories
{
    public class DogRepository : IRepository<Dog>
    {
        private AppDbContext db;
        
        public DogRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Dog> Get(int id)
        {
            return await db.Dogs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Dog>> GetAll()
        {
            return await db.Dogs.Include(d => d.User).ToListAsync();
        }

        public void Save(Dog entity)
        {
            db.Dogs.Add(entity);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
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