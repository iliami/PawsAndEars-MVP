using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using PawsAndEars.EF.Entities;

namespace PawsAndEars.EF
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<ScheduleTimeInterval> ScheduleTimeIntervals { get; set; }

        //static AppDbContext()
        //{
        //    Database.SetInitializer<AppDbContext>(new DbInitializer());
        //}

        public AppDbContext(string connectionString)
            : base(connectionString)
        { }
    }
}