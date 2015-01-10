using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCrux.Models;
using System.Web.Security;
using System.Web.Helpers;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net.Mail;

namespace ProjectCrux.Controllers
{
    public class StudentController : Controller
    {
        private ProjectCruxContext db = new ProjectCruxContext();

        // GET: Student


        // GET: Student/Details/5
        public ActionResult Details()
        {
            return View(db.Students.ToList());
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            // Country Dropdown
            var countryList = db.Countrys.ToList().OrderBy(a => a.countryName);

            List<SelectListItem> DropdownCountry = new List<SelectListItem>();

            foreach (var item in countryList)
            {
                DropdownCountry.Add(new SelectListItem { Text = item.countryName, Value = item.countryID.ToString() });
            }

            ViewBag.countryID = DropdownCountry;

            // School Dropdown
            var schoolList = db.Schools.ToList().OrderBy(a => a.schoolName);

            List<SelectListItem> DropdownSchool = new List<SelectListItem>();

            foreach (var item in schoolList)
            {
                DropdownSchool.Add(new SelectListItem { Text = item.schoolName, Value = item.schoolID.ToString() });
            }

            ViewBag.schoolID = DropdownSchool;
            
            
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentId,firstName,middleName,lastName,dateOfBirth,countryID,emailAddress,userName,userPassword,confirmPassword,schoolID")] Student student)
        {

            if (ModelState.IsValid)
            {


                var salt = Crypto.GenerateSalt();
                var saltedPassword = student.userPassword + salt;
                var hashedPassword = Crypto.HashPassword(saltedPassword);
                student.userPassword = hashedPassword;
                student.confirmPassword = hashedPassword;
                student.salt = salt;
                student.verified = false;
                student.verificationToken = Guid.NewGuid().ToString();

                db.Students.Add(student);

                db.SaveChanges();

                this.SendConfirmationMail(student.emailAddress, student.verificationToken);

                HttpContext.Session.Add("Student", student);

                return RedirectToAction("UploadImage");
            }

            // Country Dropdown
            var countryList = db.Countrys.ToList().OrderBy(a => a.countryName);

            List<SelectListItem> DropdownCountry = new List<SelectListItem>();

            foreach (var item in countryList)
            {
                DropdownCountry.Add(new SelectListItem { Text = item.countryName, Value = item.countryID.ToString() });
            }

            ViewBag.countryID = DropdownCountry;

            // School Dropdown
            var schoolList = db.Schools.ToList().OrderBy(a => a.schoolName);

            List<SelectListItem> DropdownSchool = new List<SelectListItem>();

            foreach (var item in schoolList)
            {
                DropdownSchool.Add(new SelectListItem { Text = item.schoolName, Value = item.schoolID.ToString() });
            }

            ViewBag.schoolID = DropdownSchool;

            return View(student);
        }

        // Send email confirmation
        public void SendConfirmationMail(String emailAddress, String verificationToken)
        {
            string verifyUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/student/verify?id="
                + verificationToken;

           
            var message = new System.Net.Mail.MailMessage("projectcruxfall14@gmail.com", emailAddress)
            {

                Subject = " Welcome to CRUX!!! Please verify your Crux account: ",
                Body = "Team => The Usual Suspects Welcomes you!!! To complete the registration, Please click the following link for verification purpose " + verifyUrl ,
                
            };

            var client = new System.Net.Mail.SmtpClient();

            client.Send(message);
           
        }

        [AllowAnonymous]
        public ActionResult Verify(string ID)
        {
            if (string.IsNullOrEmpty(ID) || (!System.Text.RegularExpressions.Regex.IsMatch(ID,
                           @"[0-9a-f]{8}\-([0-9a-f]{4}\-){3}[0-9a-f]{12}")))
            {
                TempData["tempMessage"] =
                        "The user account is not valid. Please try clicking the link in your email again.";
                return RedirectToAction("Login");
            }

            else
            {
                var Student = db.Students.FirstOrDefault(m => m.verificationToken == new Guid(ID).ToString());
                if (Student != null)
                {
                    if (!Student.verified)
                    {
                        Student.verified = true;
                        db.Entry(Student).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Login", "Student");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                return View();
            }
        }

        [AuthorizeUser]
        // upload image
        public ActionResult UploadImage()
        {

            return View();
        }
        // Check if the user name already exists
        public virtual JsonResult CheckUserNameExists(string userName)
        {
            bool chkUser = false;
            //Check in database that particular user name is exist or not
            chkUser = db.Students.Any(s => s.userName == userName);
            if (chkUser)    // if already exists    
            {
                return Json("User name is already registered! Try another one Brah!!! ", JsonRequestBehavior.AllowGet);
            }
            else
            {
                // if not
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

        // Check if the user name already exists
        public virtual JsonResult CheckEmailAddressAlreadyExists(string emailAddress)
        {
            bool chkUser = false;
            //Check in database that particular email address exist or not
            chkUser = db.Students.Any(s => s.emailAddress == emailAddress);
            if (chkUser)    // if already exists
            {
                return Json("Email Address Already Regesiterd! Did you forget about your account? ", JsonRequestBehavior.AllowGet);
            }
            else
            {   // if not
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase photo)
        {
            //string path = @"../Content/profileImage";

            if (photo != null && photo.ContentLength > 0)
            {
                var FileName = Guid.NewGuid();

                var path = System.IO.Path.Combine(Server.MapPath("~/Content/profileImage/"),
                                        FileName.ToString() + ".pdf");
                photo.SaveAs(path);
                photo.SaveAs(path + photo.FileName);

                var student = (Student)HttpContext.Session["Student"];

                student.image = path;

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("UploadImage");

            }
            else if(photo==null)
            {
                TempData["tempMessage"] =
                        "Please add a picture before uploading";
                return RedirectToAction("UploadImage");
            }

            return RedirectToAction("Index");
        }

   


      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //login function

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountLoginViewModel userTryingToLogin)
        {
            string salt, saltedPassword, hashedPassword;
            if (userTryingToLogin != null & ModelState.IsValid)
            {
                Student doesUserExist = db.Students.FirstOrDefault(s => s.userName.Equals(userTryingToLogin.UserName));


                try
                {
                    salt = doesUserExist.salt;
                }
                catch
                {
                    ModelState.AddModelError("UserDoesNotExist", "Username or Password is Incorrect! Please try Again!!");
                    return View(userTryingToLogin);
                }

                if (!doesUserExist.verified)
                {
                    ModelState.AddModelError("UserDoesNotExist", "Email Not verified. Please check your email confirmation");
                    return View(userTryingToLogin);
                }

                saltedPassword = userTryingToLogin.Password;

                hashedPassword = Crypto.HashPassword(saltedPassword);

                if (Crypto.VerifyHashedPassword(hashedPassword, userTryingToLogin.Password))
                {
                    //If everything for user checks out, store the user's userId and 
                    //firstName in the currrent session.
                    Session["userId"] = doesUserExist.studentId;
                    Session["firstName"] = doesUserExist.firstName;

                    FormsAuthentication.SetAuthCookie(userTryingToLogin.UserName, userTryingToLogin.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(userTryingToLogin);
            }

            ModelState.AddModelError("UserDoesNotExist", "Username or Password is Incorrect! Please try Again!!");
            return View(userTryingToLogin);
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");

        }
    }
}
