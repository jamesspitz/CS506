using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WudFilmApp.Models;

namespace WudFilmApp.Models
{
    public class WudFilmAppContext : DbContext
    {
        public WudFilmAppContext (DbContextOptions<WudFilmAppContext> options)
            : base(options)
        {
        }

        public DbSet<WudFilmApp.Models.Movie> Movie { get; set; }

        public DbSet<WudFilmApp.Models.Showtime> Showtime { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add Mapping to store CCAP as a comma separated list 
            modelBuilder.Entity<Movie>()
                .Property(p => p.CCAP)
                .HasConversion(
                    v => string.Join(",", v),
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries));

            // Add Mapping to store trailers as comma separated list 
            modelBuilder.Entity<Movie>()
                .Property(p => p.Trailers)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(",", StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
