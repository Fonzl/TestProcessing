﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Database.Answer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrectAnswer")
                        .HasColumnType("boolean");

                    b.Property<long>("QuestId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("QuestId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Database.CategoryTasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CategoryTasks");
                });

            modelBuilder.Entity("Database.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("Database.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<short>("Cours")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("EndOfTraining")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDateOfTraining")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Database.Quest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("CategoryTasksId")
                        .HasColumnType("integer");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryTasksId");

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("Database.ResultTest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Result")
                        .HasColumnType("numeric");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Database.Role", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = (short)2,
                            Name = "teacher"
                        },
                        new
                        {
                            Id = (short)3,
                            Name = "student"
                        });
                });

            modelBuilder.Entity("Database.Test", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("DisciplineId")
                        .HasColumnType("integer");

                    b.Property<string>("InfoTest")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Database.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("RoleId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FullName = "admin",
                            Password = "202CB962AC59075B964B07152D234B70",
                            RoleId = (short)1
                        });
                });

            modelBuilder.Entity("DisciplineGroup", b =>
                {
                    b.Property<int>("DisciplinesId")
                        .HasColumnType("integer");

                    b.Property<long>("GroupsId")
                        .HasColumnType("bigint");

                    b.HasKey("DisciplinesId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("DisciplineGroup");
                });

            modelBuilder.Entity("DisciplineUser", b =>
                {
                    b.Property<int>("DisciplinesId")
                        .HasColumnType("integer");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("DisciplinesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("DisciplineUser");
                });

            modelBuilder.Entity("QuestTest", b =>
                {
                    b.Property<long>("QuestsId")
                        .HasColumnType("bigint");

                    b.Property<long>("TestsId")
                        .HasColumnType("bigint");

                    b.HasKey("QuestsId", "TestsId");

                    b.HasIndex("TestsId");

                    b.ToTable("QuestTest");
                });

            modelBuilder.Entity("TestUser", b =>
                {
                    b.Property<long>("TestsId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("TestsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TestUser");
                });

            modelBuilder.Entity("Database.Answer", b =>
                {
                    b.HasOne("Database.Quest", "Quest")
                        .WithMany("Answers")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quest");
                });

            modelBuilder.Entity("Database.Quest", b =>
                {
                    b.HasOne("Database.CategoryTasks", "CategoryTasks")
                        .WithMany()
                        .HasForeignKey("CategoryTasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryTasks");
                });

            modelBuilder.Entity("Database.ResultTest", b =>
                {
                    b.HasOne("Database.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Test", b =>
                {
                    b.HasOne("Database.Discipline", "Discipline")
                        .WithMany("Tests")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");
                });

            modelBuilder.Entity("Database.User", b =>
                {
                    b.HasOne("Database.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId");

                    b.HasOne("Database.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DisciplineGroup", b =>
                {
                    b.HasOne("Database.Discipline", null)
                        .WithMany()
                        .HasForeignKey("DisciplinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DisciplineUser", b =>
                {
                    b.HasOne("Database.Discipline", null)
                        .WithMany()
                        .HasForeignKey("DisciplinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuestTest", b =>
                {
                    b.HasOne("Database.Quest", null)
                        .WithMany()
                        .HasForeignKey("QuestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Test", null)
                        .WithMany()
                        .HasForeignKey("TestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestUser", b =>
                {
                    b.HasOne("Database.Test", null)
                        .WithMany()
                        .HasForeignKey("TestsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Discipline", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("Database.Group", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Database.Quest", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
