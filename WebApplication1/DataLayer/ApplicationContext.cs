using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace DataLayer
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }
        public ApplicationContext()
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Licenta2018DB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }

}
