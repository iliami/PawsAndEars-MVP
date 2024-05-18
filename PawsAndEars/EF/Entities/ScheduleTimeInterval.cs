using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars.EF.Entities
{
    public class ScheduleTimeInterval
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public virtual Dog Dog { get; set; }
        public DateTime StartActivityTime { get; set; }
        public DateTime EndActivityTime { get; set; }
        public string ActivityName { get; set; }
        public int? FoodId { get; set; }
        public virtual Food Food { get; set; }
        public int? TrainingId { get; set; }
        public virtual Training Training { get; set; }
    }
}