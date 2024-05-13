using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PawsAndEars.Models;

namespace PawsAndEars.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DBSet<Breed> Breeds { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<ScheduleTimeInterval> ScheduleTimeIntervals { get; set; }

        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new DbInitializer());
        }

        public AppDbContext(string connectionString)
            : base(connectionString)
        { }
    }
}