using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PawsAndEars.EF;
using PawsAndEars.Services;
using PawsAndEars.Services.Interfaces;

namespace PawsAndEars.NinjectModules
{
    public class DogsServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGettingService<IEnumerable<Models.Dog>>>().To<DogsService>();
            Bind<ICUDService<Models.Dog>>().To<DogsService>();
            Bind<IService<Models.Dog, IEnumerable<Models.Dog>>>().To<DogsService>();
        }
    }
}