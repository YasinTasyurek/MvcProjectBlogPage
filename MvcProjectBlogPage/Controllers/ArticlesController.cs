using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcProjectBlogPage.Models;
using PagedList;

namespace MvcProjectBlogPage.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.articles.ToList());
        }

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            return View(db.articles.OrderBy(x => x.PhotoUrl).ToPagedList(page, 10));
        }

        // GET: Articles/Details/5
        public ActionResult Details()
        {
            List<Article> list = db.articles.SqlQuery("select * from articles;").ToList();
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Body,PhotoUrl")] Article article, HttpPostedFileBase file)
        {
            Article articleTemp = new Article();
            if (ModelState.IsValid)
            {
                articleTemp.ID = article.ID;
                articleTemp.Title = article.Title;
                articleTemp.Body = article.Body;
                articleTemp.PhotoUrl = file.FileName;

                db.articles.Add(articleTemp);
                db.SaveChanges();

                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/" + file.FileName));
                }

                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = db.articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAuthenticated)
            {
                return View(article);
                //return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Body,PhotoUrl")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAuthenticated)
            {
                return View(article);
                //return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.articles.Find(id);
            db.articles.Remove(article);
            db.SaveChanges();
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
    }
}