using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectCrux.Models
{
    public class Country
    {
        
        public int countryID { get; set; }
        
        public string countryName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}