

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WudFilmApp.Areas.Identity.Data;
using WudFilmApp.Models;

[assembly: HostingStartup(typeof(WudFilmApp.Areas.Identity.IdentityHostingStartup))]
namespace WudFilmApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddIdentity<WudFilmAppUser, IdentityRole>()
                    // services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders();


                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddRazorPagesOptions(options =>
                    {
                        options.AllowAreas = true;
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    });

                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Identity/Account/Login";
                    options.LogoutPath = $"/Identity/Account/Logout";
                    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                });

                // using Microsoft.AspNetCore.Identity.UI.Services;
                services.AddSingleton<IEmailSender, EmailSender>();

            });
        }
    }
}