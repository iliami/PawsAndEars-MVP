using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartWorkingTime { get; set; }
        public DateTime EndWorkingTime { get; set; }
        public virtual ICollection<Dog> Dogs { get; set; }
    }
}
