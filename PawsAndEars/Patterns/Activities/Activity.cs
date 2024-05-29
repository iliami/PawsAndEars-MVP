using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Patterns.Activities
{
    [Serializable]
    public abstract class Activity : IGetInfo
    {
        protected string Id;
        protected string Name;
        protected string Description;

        public string GetId() { return Id; }

        public string GetString() { return $"{Name} - {Description}"; }
    }
}