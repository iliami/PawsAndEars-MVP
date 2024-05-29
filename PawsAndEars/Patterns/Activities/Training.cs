using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.Patterns.Activities
{
    [Serializable]
    public class Training : Activity
    {
        public Training()
        {
            Id = Guid.NewGuid().ToString();
            Name = "Training";
            Description = "Training is the process of teaching the animal to follow commands and perform various skills: sit, lie down, stand, come to me, give a paw. It is important that the dog behaves correctly and unquestioningly obeys the commands.";
        }
    }
}