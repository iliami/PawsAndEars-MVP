using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PawsAndEars.EF;

namespace PawsAndEars.NinjectModules
{
    public class AppDbContextModule : NinjectModule
    {
        private string connectionString;
        public AppDbContextModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<AppDbContext>().To<AppDbContext>().WithConstructorArgument(connectionString);
        }
    }
}