using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WudFilmApp.Areas.Identity.Authorization;
using WudFilmApp.Areas.Identity;
using WudFilmApp.Areas.Identity.Data;
using WudFilmApp.Models;

namespace WudFilmApp.Areas.Identity.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new IdentityContext(serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "WUD@wisc.edu");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "WUD@wisc.edu");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                SeedDB(context, adminID);
            }
        }
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string NetID)
        {
            var userManager = serviceProvider.GetService<UserManager<WudFilmAppUser>>();

            var user = await userManager.FindByNameAsync(NetID);
            if (user == null)
            {
                user = new WudFilmAppUser { NetID = NetID };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }


        public static void SeedDB(IdentityContext context, string adminID)
        {
            if (context.WudFilmAppUser.Any())
            {
                return;   // DB  seeded
            }

        }
    }
}
