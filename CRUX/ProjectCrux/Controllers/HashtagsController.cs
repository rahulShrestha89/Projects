using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectCrux.Models;

namespace ProjectCrux.Controllers
{
    public class HashtagsController : Controller
    {
        private ProjectCruxContext db = new ProjectCruxContext();

        // GET: Hashtags
        public ActionResult Index(string searchString)
        {
            var hashtags = from h in db.Hashtags
                           select h;

            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("NotFound");
            }
            else
            {
                hashtags = hashtags.Where(s => s.hashtagName.Contains(searchString));

                if (!hashtags.Any())
                {
                    return RedirectToAction("NotFound");
                }
                else
                {
                    var query = db.Questions.Where(Q => Q.questionId < 0);
                    List<Question> questions = new List<Question> { };
                    foreach (var item in hashtags)
                    {
                        query = item.Questions.Where(Q => Q.questionId >= 0).AsQueryable();

                        foreach (var thing in query)
                        {
                            questions.Add(thing);
                        }
                    }

                    ViewBag.Question = questions;
                    return View(hashtags);
                }
            }


        }

        public ActionResult NotFound()
        {
            return View();
        }

        // GET: Hashtags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hashtag hashtag = db.Hashtags.Find(id);
            if (hashtag == null)
            {
                return HttpNotFound();
            }
            return View(hashtag);
        }

        // GET: Hashtags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hashtags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hashtagId,hashtagName,hashtagDescription")] Hashtag hashtag)
        {
            if (ModelState.IsValid)
            {
                db.Hashtags.Add(hashtag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hashtag);
        }

        // GET: Hashtags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hashtag hashtag = db.Hashtags.Find(id);
            if (hashtag == null)
            {
                return HttpNotFound();
            }
            return View(hashtag);
        }

        // POST: Hashtags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hashtagId,hashtagName,hashtagDescription")] Hashtag hashtag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hashtag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hashtag);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // for the tags
        public ActionResult find1()
        {
            return RedirectToAction("Look_1");
        }

        public ActionResult Look_1()
        {
            var hashtags = from h in db.Hashtags
                           select h;

            if (String.IsNullOrEmpty("java"))
            {
                return RedirectToAction("NotFound");
            }
            else
            {
                hashtags = hashtags.Where(s => s.hashtagName.Contains("java"));

                if (!hashtags.Any())
                {
                    return RedirectToAction("NotFound");
                }
                else
                {
                    var query = db.Questions.Where(Q => Q.questionId < 0);
                    List<Question> questions = new List<Question> { };
                    foreach (var item in hashtags)
                    {
                        query = item.Questions.Where(Q => Q.questionId >= 0).AsQueryable();

                        foreach (var thing in query)
                        {
                            questions.Add(thing);
                        }
                    }

                    ViewBag.Question = questions;
                    return View(hashtags);
                }
            }

            
        }

        public ActionResult find2()
        {
            return RedirectToAction("Look_2");
        }

        public ActionResult Look_2()
        {
            var hashtags = from h in db.Hashtags
                           select h;

            if (String.IsNullOrEmpty("drBurris"))
            {
                return RedirectToAction("NotFound");
            }
            else
            {
                hashtags = hashtags.Where(s => s.hashtagName.Contains("drBurris"));

                if (!hashtags.Any())
                {
                    return RedirectToAction("NotFound");
                }
                else
                {
                    var query = db.Questions.Where(Q => Q.questionId < 0);
                    List<Question> questions = new List<Question> { };
                    foreach (var item in hashtags)
                    {
                        query = item.Questions.Where(Q => Q.questionId >= 0).AsQueryable();

                        foreach (var thing in query)
                        {
                            questions.Add(thing);
                        }
                    }

                    ViewBag.Question = questions;
                    return View(hashtags);
                }
            }


        }

        public ActionResult find3()
        {
            return RedirectToAction("Look_3");
        }

        public ActionResult Look_3()
        {
            var hashtags = from h in db.Hashtags
                           select h;

            if (String.IsNullOrEmpty("interpolation"))
            {
                return RedirectToAction("NotFound");
            }
            else
            {
                hashtags = hashtags.Where(s => s.hashtagName.Contains("interpolation"));

                if (!hashtags.Any())
                {
                    return RedirectToAction("NotFound");
                }
                else
                {
                    var query = db.Questions.Where(Q => Q.questionId < 0);
                    List<Question> questions = new List<Question> { };
                    foreach (var item in hashtags)
                    {
                        query = item.Questions.Where(Q => Q.questionId >= 0).AsQueryable();

                        foreach (var thing in query)
                        {
                            questions.Add(thing);
                        }
                    }

                    ViewBag.Question = questions;
                    return View(hashtags);
                }
            }


        }

        public ActionResult find4()
        {
            return RedirectToAction("Look_4");
        }

        public ActionResult Look_4()
        {
            var hashtags = from h in db.Hashtags
                           select h;

            if (String.IsNullOrEmpty("python"))
            {
                return RedirectToAction("NotFound");
            }
            else
            {
                hashtags = hashtags.Where(s => s.hashtagName.Contains("python"));

                if (!hashtags.Any())
                {
                    return RedirectToAction("NotFound");
                }
                else
                {
                    var query = db.Questions.Where(Q => Q.questionId < 0);
                    List<Question> questions = new List<Question> { };
                    foreach (var item in hashtags)
                    {
                        query = item.Questions.Where(Q => Q.questionId >= 0).AsQueryable();

                        foreach (var thing in query)
                        {
                            questions.Add(thing);
                        }
                    }

                    ViewBag.Question = questions;
                    return View(hashtags);
                }
            }


        }

        public ActionResult find5()
        {
            return RedirectToAction("Look_5");
        }
        public ActionResult Look_5()
        {
            var hashtags = from h in db.Hashtags
                           select h;

            if (String.IsNullOrEmpty("stanford"))
            {
                return RedirectToAction("NotFound");
            }
            else
            {
                hashtags = hashtags.Where(s => s.hashtagName.Contains("stanford"));

                if (!hashtags.Any())
                {
                    return RedirectToAction("NotFound");
                }
                else
                {
                    var query = db.Questions.Where(Q => Q.questionId < 0);
                    List<Question> questions = new List<Question> { };
                    foreach (var item in hashtags)
                    {
                        query = item.Questions.Where(Q => Q.questionId >= 0).AsQueryable();

                        foreach (var thing in query)
                        {
                            questions.Add(thing);
                        }
                    }

                    ViewBag.Question = questions;
                    return View(hashtags);
                }
            }


        }
    }
}
