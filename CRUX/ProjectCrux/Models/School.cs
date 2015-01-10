using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCrux.Models
{
    public class School
    {
        public int schoolID { get; set; }

        public string schoolName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}