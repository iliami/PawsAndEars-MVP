using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.EF.Models;

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
            return await db.Dogs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Dog>> GetAll()
        {
            return await db.Dogs.Include(d => d.User).Include(d => d.Breed).Include(d => d.Diseases).ToListAsync();
        }

        public void Save(Dog entity)
        {
            db.Dogs.Add(entity);
            db.SaveChanges();
        }

        public void Update(int id, Dog entity)
        {
            var entityToUpdate = db.Dogs.FirstOrDefault(x => x.Id == id);
            entityToUpdate.Age = entity.Age;
            entityToUpdate.User = entity.User;
            entityToUpdate.Id = id;
            entityToUpdate.Breed = entity.Breed;
            entityToUpdate.Length = entity.Length;
            // TODO
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