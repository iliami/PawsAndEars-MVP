using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PawsAndEars.EF;

namespace PawsAndEars.EF.Entities
{
    public class User : IdentityUser
    {
        public DateTime StartWorkingTime { get; set; } = DateTime.Now;
        public DateTime EndWorkingTime { get; set; } = DateTime.Now.AddHours(5);
        public virtual ICollection<Dog> Dogs { get; set; }
    }
}
