using labtaskweek3.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace labtaskweek3.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        [HttpGet]
        public ActionResult Index()
        {
            Lab3Entities db = new Lab3Entities();
            var data = db.News.ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(News nn)
        {
            var s = Request["Search"];
            Lab3Entities db = new Lab3Entities();
            var data = (from n in db.News where n.Category == s || n.PublishDate == s select n).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(News n)
        {
            if(ModelState.IsValid)
            {
                Lab3Entities db = new Lab3Entities();
                db.News.Add(n);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Lab3Entities db = new Lab3Entities();
            var news = (from n in db.News where n.Id == id select n).FirstOrDefault();
            return View(news);
        }
        [HttpPost]
        public ActionResult Update(News nn)
        {
            if (ModelState.IsValid)
            {
                Lab3Entities db = new Lab3Entities();
                var news = (from n in db.News where n.Id == nn.Id select n).FirstOrDefault();
                db.Entry(news).CurrentValues.SetValues(nn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Lab3Entities db = new Lab3Entities();
            var news = (from n in db.News where n.Id == id select n).FirstOrDefault();
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}