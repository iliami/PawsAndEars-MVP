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
    public class FoodRepository : IRepository<Food>
    {
        private AppDbContext db;

        public FoodRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Food> Get(int id)
        {
            return await db.Foods.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            return await db.Foods.ToListAsync();
        }

        public void Save(Food entity)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Food entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}