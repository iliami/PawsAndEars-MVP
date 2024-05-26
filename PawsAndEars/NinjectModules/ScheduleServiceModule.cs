using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PawsAndEars.Services.Interfaces;
using PawsAndEars.Services;

namespace PawsAndEars.NinjectModules
{
    public class ScheduleServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGettingService<IEnumerable<Models.ScheduleTimeInterval>>>().To<ScheduleService>();
            Bind<ICUDService<Models.ScheduleTimeInterval>>().To<ScheduleService>();
            Bind<IService<Models.ScheduleTimeInterval, IEnumerable<Models.ScheduleTimeInterval>>>().To<ScheduleService>();
        }
    }
}