namespace ProjectCrux.Migrations
{
    using ProjectCrux.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectCrux.Models.ProjectCruxContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProjectCrux.Models.ProjectCruxContext context)
        {
            var schools = new List<School>
            {
                new School { schoolName = "Southesatern Louisiana University"},
                new School { schoolName = "Stanford University"},
                new School { schoolName = "Louisiana State University"},
                new School { schoolName = "Harvard University"}
             
            };

            schools.ForEach(s => context.Schools.AddOrUpdate(p => p.schoolName, s));
            context.SaveChanges();

            var computerCourses = new List<ComputerCourse>
            {
                new ComputerCourse { computerCourseName = "Intro To Programming", computerCourseDescription="Introduction to the intellectual enterprises of computer science and the art of programming for majors and non-majors alike, with or without prior programming experience."},
                new ComputerCourse { computerCourseName = "Data Structures", computerCourseDescription="This course is a survey of fundamental data structures for information processing, including lists, stacks, queues, trees, and graphs. It explores the implementation of these data structures (both array-based and linked representations) and examines classic algorithms that use these structures for tasks such as sorting, searching, and text compression."},
                new ComputerCourse { computerCourseName="Numerical Analysis" , computerCourseDescription =" The course covers root finding, solving systems of linear equations, interpolation, least squares, numerical integration and differentiation, and solving systems of differential equations."},
                new ComputerCourse { computerCourseName="Formal Systems and Computations" , computerCourseDescription="Rigorous introduction to formal systems and the theory of computation. Elementary treatment of automata, formal languages, computability, uncomputability, computational complexity, NP-completeness, and mathematical logic. "},
                new ComputerCourse { computerCourseName="Misc", computerCourseDescription="All Random questions for Testing Phase and Beta test!"}
            };

            computerCourses.ForEach(s => context.ComputerCourses.AddOrUpdate(p => p.computerCourseName, s));
            context.SaveChanges();
        }
    }
}
