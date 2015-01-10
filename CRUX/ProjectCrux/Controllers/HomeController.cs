using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCrux.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overview()
        {
            return View();
        }

        public ActionResult Institution()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
       
        }
        public ActionResult Login() {
            ViewBag.Message = "Your Login page.";
            
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Message = "Your Registration page.";

            return View();
        }
        public ActionResult ResetPassword()
        {
            ViewBag.Message = "Reset password.";

            return View();
        }
        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "Your Firgot Password page.";

            return View();
        }
        public ActionResult Subscribe()
        {
            ViewBag.Message = "Your Subscribe page.";

            return View();
        }
        public ActionResult Privacy()
        {
            ViewBag.Message = "Your Privacy page.";

            return View();
        }
    }
}