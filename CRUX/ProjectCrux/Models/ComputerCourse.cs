using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectCrux.Models
{
    public class ComputerCourse
    {
        public int computerCourseId { get; set; }

         [Display(Name = "Course")]
        public string computerCourseName { get; set; }

        public string computerCourseDescription { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}