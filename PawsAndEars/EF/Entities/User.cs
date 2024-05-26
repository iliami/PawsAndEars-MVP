using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PawsAndEars.EF.Entities
{
    public class User : IdentityUser
    {
        public DateTime StartWorkingTime { get; set; } = DateTime.Now;
        public DateTime EndWorkingTime { get; set; } = DateTime.Now.AddHours(5);
        public virtual ICollection<Dog> Dogs { get; set; }
    }
}
