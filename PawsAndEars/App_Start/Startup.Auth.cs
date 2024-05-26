using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PawsAndEars.EF;
using PawsAndEars.Services;
using PawsAndEars.EF.Entities;

[assembly: OwinStartup(typeof(PawsAndEars.App_Start.Startup))]

namespace PawsAndEars.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext(() => new AppDbContext("DefaultConnection"));
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            //app.CreatePerOwinContext<RoleManager<Role>>((options, context) =>
            //    new RoleManager<Role>(
            //        new RoleStore<Role>(context.Get<AppDbContext>("DefaultConnection"))));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}
