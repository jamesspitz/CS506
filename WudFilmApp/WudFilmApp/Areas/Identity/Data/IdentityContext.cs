﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WudFilmApp.Areas.Identity.Data;

namespace WudFilmApp.Models
{
    public class IdentityContext : IdentityDbContext<WudFilmAppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
            

        }
        
        public string NetID { get; set; }

        public DbSet<WudFilmApp.Areas.Identity.Data.WudFilmAppUser> WudFilmAppUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
