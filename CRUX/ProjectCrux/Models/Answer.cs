using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectCrux.Models
{
    public class Answer
    {
        public int answerId { get; set; }


        [Required]
        public string answer { get; set; }
        public DateTime postDate { get; set; }

        public int questionID { get; set; } // FK
        public virtual Question Question { get; set; }
    }
}