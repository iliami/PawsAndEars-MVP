using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Patterns.Activities
{
    [Serializable]
    public class Walking : Activity
    {
        public Walking()
        {
            Id = Guid.NewGuid().ToString();
            Name = "Walking";
            Description = "Walking is good for your pet";
        }
    }
}