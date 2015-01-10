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

namespace ProjectCrux.Controllers
{
    public class QuestionController : Controller
    {
        private ProjectCruxContext db = new ProjectCruxContext();

        // GET: Question
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: Question/Details/5

        public ActionResult Recent_1()
        {
            var question = from h in db.Questions
                           select h;
            question = question.Where(s => s.question.Contains("Gaussian elimination (with no pivoting) in CUDA?"));
            return View(question);
        }

        public ActionResult Recent_2()
        {
            var question = from h in db.Questions
                           select h;
            question = question.Where(s => s.question.Contains("Finding the depth of a Tree?"));
            return View(question);
        }

        public ActionResult Recent_3()
        {
            var question = from h in db.Questions
                           select h;
            question = question.Where(s => s.question.Contains("Boyer-Moore Algorithm shift rules?"));
            return View(question);
        }
        [AuthorizeUser]
        public ActionResult Details(int id)
        {
            Question askedQuestion = db.Questions.FirstOrDefault(q => q.questionId == id);

            if (askedQuestion.questionVisibility)
            {
                SortingPagingInfo info = new SortingPagingInfo();


                var answers = db.Answers.Where(a => a.questionID == id);

                info.SortField = "dateAndTime";
                info.SortDirection = "descending";
                info.PageSize = 4;
                info.PageCount = Convert.ToInt32(Math.Ceiling((double)(answers.Count()
                         / info.PageSize))) + 1;
                info.CurrentPageIndex = 0;
                info.PageId = id;

                var query = answers.OrderByDescending(c => c.postDate).Take(info.PageSize);
                ViewBag.SortingPagingInfo = info;
                List<Answer> model = query.ToList();

                ViewBag.Answers = model;
                ViewBag.question = id;

                return View(askedQuestion);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Details(SortingPagingInfo info)
        {
            IQueryable<Answer> query = null;
            var answers = db.Answers.Where(a => a.questionID == info.PageId);
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(answers.Count()
                         / info.PageSize))) + 1;
            query = answers.OrderByDescending(c => c.postDate);

            query = query.Skip(info.CurrentPageIndex * info.PageSize).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;
            ViewBag.question = info.PageId;
            List<Answer> model = query.ToList();

            ViewBag.Answers = model;
            return View(db.Questions.FirstOrDefault(q => q.questionId == info.PageId));
        }

        // GET: Question/Create
        [AuthorizeUser]
        public ActionResult Create()
        {
            // Computer Course Dropdown
            var computerCourseList = db.ComputerCourses.ToList().OrderBy(a => a.computerCourseName);

            List<SelectListItem> DropdownComputerCourse = new List<SelectListItem>();

            foreach (var item in computerCourseList)
            {
                DropdownComputerCourse.Add(new SelectListItem { Text = item.computerCourseName, Value = item.computerCourseId.ToString() });
            }

            ViewBag.computerCourseID = DropdownComputerCourse;
            return View();
        }

        // POST: Question/Create
        [HttpPost]

        public ActionResult Create(FormCollection form)
        {
            // Computer Course Dropdown
            var computerCourseList = db.ComputerCourses.ToList().OrderBy(a => a.computerCourseName);

            List<SelectListItem> DropdownComputerCourse = new List<SelectListItem>();

            foreach (var item in computerCourseList)
            {
                DropdownComputerCourse.Add(new SelectListItem { Text = item.computerCourseName, Value = item.computerCourseId.ToString() });
            }

            ViewBag.computerCourseID = DropdownComputerCourse;

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {

                Question question = new Question();
                Student poster = db.Students.FirstOrDefault(s => s.userName.Equals(User.Identity.Name));

                question.question = form["question"];
                question.studentID = poster.studentId;

                question.computerCourseID = Convert.ToInt32(form["computerCourseID"]);
              
                var tags = form["hashtags"];

                if (true)
                {
                    string[] Tags = tags.Split(',');


                    foreach (var hash in Tags)
                    {
                        if (db.Hashtags.FirstOrDefault(a => a.hashtagName.ToUpper().Equals(hash.ToUpper())) == null)
                        {
                            db.Hashtags.Add(new Hashtag { hashtagName = hash });
                            db.SaveChanges();
                        }
                    }

                    var Hashes = db.Hashtags.Where(a => Tags.Contains(a.hashtagName));
                    question.Hashtags = Hashes.ToList();

                    question.questionVisibility = true;

                    db.Questions.Add(question);
                    
                   
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                    }

                    return RedirectToAction("Index");
                }
                
            }

           
            return View();

        }

      

        public ActionResult Rate(Question voted)
        {
            Question question = db.Questions.FirstOrDefault(q => q.questionId == voted.questionId);
            question.questionRating += voted.questionRating;
            db.SaveChanges();
            return View(db.Questions.FirstOrDefault(q => q.questionId == question.questionId));
        }
    

    }
}
