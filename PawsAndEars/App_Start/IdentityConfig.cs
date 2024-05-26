using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Owin;
using PawsAndEars.EF;
using PawsAndEars.EF.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PawsAndEars.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new AppDbContext("DefaultConnection"));
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<RoleManager<Role>>((options, context) =>
                new RoleManager<Role>(
                    new RoleStore<Role>(context.Get<AppDbContext>("DefaultConnection"))));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}