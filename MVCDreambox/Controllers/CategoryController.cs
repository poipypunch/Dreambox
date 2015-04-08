using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;

namespace MVCDreambox.Controllers
{
    public class CategoryController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            List<Category> all = new List<Category>();
            all = db.Categories.OrderBy(a => a.ParentID).ToList();
            return View(all);
        }
       
        public ActionResult PatialCreate(string ParentID, string viewName)
        {
            Category model = new Category();
            Category cate = new Category();
            viewName = "Create";
            cate = db.Categories.Find(ParentID);
            model.ParentID = ParentID;
            ViewBag.ParentName ="Parent : "+ cate.CategoryDesc;           
            return PartialView(viewName, model);
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cate.CategoryID = Guid.NewGuid().ToString();
                    cate.CreateDate = DateTime.Now;
                    cate.UpdateDate = DateTime.Now;
                    cate.DealerID = Session["UserID"].ToString();
                    db.Categories.Add(cate);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Category");
                }
            }
            catch (Exception ex) { }
            return PartialView("Create", cate);
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(string id = null)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //
        // POST: /Category/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Categories.Add(category);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(string id = null)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(string id = null)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}