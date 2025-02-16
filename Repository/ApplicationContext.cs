using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Database;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Reflection.Emit;


namespace Repository
{
    public class ApplicationContext : DbContext
    {
      
        public DbSet<Direction> Directions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CategoryTasks> CategoryTasks { get; set; }
       // public DbSet<Course> Courses { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<ResultTest> Results { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
       // public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserResponses> UserResponses { get; set; }
       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestModel;Username=postgres;Password=1710");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
           builder.Entity<Schedule>().HasKey(u => new { u.Cours, u.DirectionId  });
            builder.Entity<Role>().HasData(new Role { Id = 1, Name = "admin" });
            builder.Entity<Role>().HasData(
                new Role { Id = 2, Name = "teacher" }
                
                );
            builder.Entity<Role>().HasData(
            new Role { Id = 3, Name = "student" }

            );


            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "admin",
                    Password = Convert.ToHexString(
                    MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes("123456As"))),
                    Disciplines = null,
                    Group = null,
                    Tests = null,
                    RoleId = 1,

                });
         
            builder.Entity<User>().HasData(
               new User
               {
                   Id = 2,
                   FullName = "teacher",
                   Password = Convert.ToHexString(
                   MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes("123456As"))),
                   Disciplines = null,
                   Group = null,
                   Tests = null,
                   RoleId = 2

               });
            builder.Entity<User>().HasData(new User
            {
                Id = 3,
                FullName = "student",
                Password = Convert.ToHexString(
                   MD5.Create().ComputeHash(System.Text.Encoding.ASCII.GetBytes("123456As"))),
                Disciplines = null,
                Group = null,
                Tests = null,
                RoleId = 3,

            });
            builder.Entity<CategoryTasks>().HasData(new CategoryTasks
            {
                Id = 1,
                Name = "Обычный вопрос",

            });
            builder.Entity<CategoryTasks>().HasData(new CategoryTasks
            {
                Id = 2,
                Name = "Вопрос с множеством ответов",

            });
            builder.Entity<CategoryTasks>().HasData(new CategoryTasks
            {
                Id = 3,
                Name = "Вопрос с ответом пользователя",
            });

            base.OnModelCreating(builder);
        }
    }
}
