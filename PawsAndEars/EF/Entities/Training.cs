﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars.EF.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ScheduleTimeInterval> ScheduleTimeIntervals { get; set; }
    }
}
