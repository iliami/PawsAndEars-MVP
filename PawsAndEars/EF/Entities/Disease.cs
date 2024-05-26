using System.Collections.Generic;

namespace PawsAndEars.EF.Entities
{
    public class Disease
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Dog> Dogs { get; set; }
    }
}
