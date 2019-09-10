using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
    /*
namespace WudFilmApp.Models
{ 
    public class ApplicationUser : IdentityUser
        {
            public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }

            public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
            {
                return Task.FromResult(GenerateUserIdentity(manager));
            }
        }

        public class ApplicationDbContext : IdentityDbContext<WudFilmAppUser>
        {
            public ApplicationDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }
        }
    }
}*/
