using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectCrux.Models
{
    public class ProjectCruxContext : DbContext
    {
        public ProjectCruxContext()
            : base("name=DefaultConnection")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<ComputerCourse> ComputerCourses { get; set; }
        


    }
}