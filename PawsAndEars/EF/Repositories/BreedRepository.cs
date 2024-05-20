using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PawsAndEars.EF.Entities;
using PawsAndEars.EF.Interfaces;

namespace PawsAndEars.EF.Repositories
{
    public class BreedRepository : IRepository<Breed>
    {
        private AppDbContext db;

        public BreedRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Breed> Get(string id)
        {
            return await db.Breeds.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Breed>> GetAll()
        {
            return await db.Breeds.ToListAsync();
        }

        public void Save(Breed entity)
        {
            db.Breeds.Add(entity);
            db.SaveChanges();
        }

        public void Update(string id, Breed entity)
        {
            throw new NotImplementedException();
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