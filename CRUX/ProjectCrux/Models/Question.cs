using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectCrux.Models
{
    public class Question
    {
        public int questionId { get; set; }
       
        [Required]
        [Display(Name = "Question")]
        public string question { get; set; }

        public int studentID { get; set; }  // FK

        
        
    

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Hashtag> Hashtags { get; set; }

        public virtual Student Student { get; set; }

        public string nulled { get; set; }


        //Autohide + ratings

        public Boolean questionVisibility { get; set; }

        [Display(Name="Rating")]
        public int questionRating { get; set; }


        
        public int computerCourseID { get; set; } // FK

        [Display(Name="Course:")]
        public virtual ComputerCourse computerCourse { get; set; }

    }
}