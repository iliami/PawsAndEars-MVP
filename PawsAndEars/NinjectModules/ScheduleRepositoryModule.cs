using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.EF.Repositories;
using PawsAndEars.Models;

namespace PawsAndEars.NinjectModules
{
    public class ScheduleRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<ScheduleTimeInterval>>().To<ScheduleRepository>();
        }
    }
}