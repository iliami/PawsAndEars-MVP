using Ninject.Modules;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PawsAndEars.NinjectModules
{
    public class TrainingRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<EF.Models.Training>>().To<TrainingRepository>();
        }
    }
}