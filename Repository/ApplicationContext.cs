﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Database;
using Microsoft.Extensions.Options;

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
       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestsModel;Username=postgres;Password=1710");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            Role r = new Role { Id = 1, Name = "admin" };
            builder.Entity<Role>().HasData(
                new Role { Id = 2, Name = "user" },
                r
                );

                    

            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "admin",
                    Password = "123",
                    Disciplines = null,
                    Group = null,
                    RoleId = 1,
                });
            base.OnModelCreating(builder);
        }
    }
}
