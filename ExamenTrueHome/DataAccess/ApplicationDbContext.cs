using ExamenTrueHome.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>()
                .HasData(
                    new Status
                    {
                        Id = 1,
                        Description = "Active"
                    },
                    new Status
                    {
                        Id = 2,
                        Description = "Done"
                    },
                    new Status
                    {
                        Id = 3,
                        Description = "Cancel"
                    }
                );
        }
    }
}
