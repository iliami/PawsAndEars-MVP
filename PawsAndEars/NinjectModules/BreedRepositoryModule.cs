using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PawsAndEars.EF.Entities;
using PawsAndEars.EF.Interfaces;
using PawsAndEars.EF.Repositories;

namespace PawsAndEars.NinjectModules
{
    public class BreedRepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Breed>>().To<BreedRepository>();
        }
    }
}