using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectCrux.Models
{
    public class Hashtag
    {
        public int hashtagId { get; set; }

        [Display(Name = "Tag")]
        [Required(ErrorMessage = "Please provide a tag!")]
        public string hashtagName { get; set; }

        [Display(Name="Tag Description")]
        public string hashtagDescription { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}