﻿// <auto-generated />
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DataLayer.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180513055345_questionUpdateMigration")]
    partial class questionUpdateMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerName");

                    b.Property<DateTime>("AnswerTime");

                    b.Property<Guid?>("QuestionId");

                    b.Property<Guid?>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("StudentId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("DataLayer.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<DateTime>("PostTime");

                    b.Property<Guid?>("TimetableId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TimetableId");

                    b.HasIndex("UserId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("DataLayer.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerTime");

                    b.Property<Guid?>("CourseId");

                    b.Property<bool>("IsLaunched");

                    b.Property<DateTime>("LaunchTime");

                    b.Property<string>("QuestionName");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("DataLayer.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Group");

                    b.Property<Guid?>("UserId");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DataLayer.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Function");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("DataLayer.Entities.TeacherCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("TeacherId");

                    b.Property<Guid?>("TimetableId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.HasIndex("TimetableId");

                    b.ToTable("TeacherCourses");
                });

            modelBuilder.Entity("DataLayer.Entities.Timetable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Day");

                    b.Property<string>("From");

                    b.Property<string>("Group");

                    b.Property<string>("Hall");

                    b.Property<string>("Name");

                    b.Property<string>("Pack");

                    b.Property<string>("Teacher");

                    b.Property<string>("To");

                    b.Property<string>("Week");

                    b.HasKey("Id");

                    b.ToTable("Timetables");
                });

            modelBuilder.Entity("DataLayer.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Entities.Answer", b =>
                {
                    b.HasOne("DataLayer.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("DataLayer.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("DataLayer.Entities.Chat", b =>
                {
                    b.HasOne("DataLayer.Entities.Timetable", "Timetable")
                        .WithMany()
                        .HasForeignKey("TimetableId");

                    b.HasOne("DataLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DataLayer.Entities.Question", b =>
                {
                    b.HasOne("DataLayer.Entities.TeacherCourse", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("DataLayer.Entities.Student", b =>
                {
                    b.HasOne("DataLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DataLayer.Entities.Teacher", b =>
                {
                    b.HasOne("DataLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DataLayer.Entities.TeacherCourse", b =>
                {
                    b.HasOne("DataLayer.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.HasOne("DataLayer.Entities.Timetable", "Timetable")
                        .WithMany()
                        .HasForeignKey("TimetableId");
                });
#pragma warning restore 612, 618
        }
    }
}
