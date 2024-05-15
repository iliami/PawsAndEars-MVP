using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawsAndEars.EF;

namespace PawsAndEars.EF.Models
{
    public class User
    {
        private readonly AppDbContext context;

        public User()
        {
        }

        public User(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartWorkingTime { get; set; }
        public DateTime EndWorkingTime { get; set; }
        public virtual ICollection<Dog> Dogs { get; set; }

        public IEnumerable<ScheduleTimeInterval> GetSchedule()
        {
            return context.ScheduleTimeIntervals.Where(sti => sti.Dog.UserId == Id).ToList();
        }
    }
}
