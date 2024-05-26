using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using PawsAndEars.EF;
using PawsAndEars.EF.Entities;

namespace PawsAndEars.Services
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store) : base(store)
        {
        }

        // this method is called by Owin therefore this is the best place to configure your User Manager
        public static UserManager Create(
            IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            var manager = new UserManager(
                new UserStore<User>(context.Get<AppDbContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}