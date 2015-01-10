using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace ProjectCrux.Models
{
    public class Student
    {
        public int studentId { get; set; }
        
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name="First Name*")]
        public string firstName { get; set; }
        
        [Display(Name = "Middle Name")]
        public string middleName { get; set; }

        [Display(Name = "Last Name*")]
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DateOfBirth(MinAge = 15, MaxAge = 90, ErrorMessage = "Too young or too old???")]
        public DateTime dateOfBirth { get; set; }

        [Display(Name = "Country*")]
        [Required(ErrorMessage = "What's your country?")]
        public int countryID { get; set; }

        [RegularExpression(@".*@(harvard|stanford|southeastern|selu|lsu|southeastern)\.edu", ErrorMessage = "Beta Version. Only open to limited schools!")]
        [System.Web.Mvc.Remote("CheckEmailAddressAlreadyExists", "Student")]
        [Display(Name = "Email Address*")]
        [Required(ErrorMessage = "Enter a valid email-address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string emailAddress { get; set; }

        [System.Web.Mvc.Remote("CheckUserNameExists", "Student")]
        [Display(Name = "User Name*")]
        [Required(ErrorMessage = "User Name is required", AllowEmptyStrings = false)]
        public string userName { get; set; }

        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(500, MinimumLength = 8, ErrorMessage = "Atleast 8 characters long.")]
        public string userPassword { get; set; }

        [Display(Name = "Confirm Password*")]
        [Compare("userPassword", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        [Display(Name = "School*")]
        [Required(ErrorMessage = "Please provide your university name.")]
        public int schoolID { get; set; }

        //[FileSize(10240)]
        //[FileTypes("jpg,jpeg,png")]
        public string image { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public Boolean verified { get; set; }
        public String verificationToken { get; set; }

        public string salt { get; set; }

        [Display(Name="Country")]
        public virtual Country country { get; set; }


        [Display(Name = "School*")]
        public virtual School school { get; set; }
    } 

   
    /* 
     * Attribute to validate date of birth within a range
     */
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var val = (DateTime)value;

            if (val.AddYears(MinAge) > DateTime.Now)
                return false;

            return (val.AddYears(MaxAge) > DateTime.Now);
        }
    }

    // Validate image size
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return _maxSize > (value as HttpPostedFile).ContentLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The image file size should not exceed {0}", _maxSize);
        }
    }

    // Validate image type
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFile).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid image file type. Only the following types {0} are supported.", String.Join(", ", _types));
        }
    }

}