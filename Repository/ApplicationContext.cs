using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;

namespace Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CategoryTasks> CategoryTasks { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<ResultTest> Results { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Test> Tests { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestsModel;Username=postgres;Password=1710");
        }
    }
}
