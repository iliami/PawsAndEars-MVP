using PawsAndEars.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Models
{
    public class Training : IActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string getDescription() => Description;
        public string getName() => Name;
    }
}