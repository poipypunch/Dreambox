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
    public class PackageController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /Package/

        public ActionResult Index()
        {
            return View(db.Packages.ToList());
        }

        //
        // GET: /Package/Details/5

        public ActionResult Details(string id = null)
        {
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        //
        // GET: /Package/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Package/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Package package)
        {
            if (ModelState.IsValid)
            {
                if (!CheckDuplicate(string.Empty, package.PackageDesc))
                {
                    package.PackageID = Guid.NewGuid().ToString();
                    package.UpdateBy = Session["UserID"].ToString();
                    package.UpdateDate = DateTime.Now;
                    package.CreateBy = Session["UserID"].ToString();
                    package.CreateDate = DateTime.Now;
                    db.Packages.Add(package);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(package);
        }

        //
        // GET: /Package/Edit/5

        public ActionResult Edit(string id = null)
        {
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        //
        // POST: /Package/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Package package)
        {
            if (ModelState.IsValid)
            {
                if (!CheckDuplicate(package.PackageID, package.PackageDesc))
                {
                    db.Entry(package).State = EntityState.Modified;
                    package.UpdateBy = Session["UserID"].ToString();
                    package.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(package);
        }

        //
        // GET: /Package/Delete/5

        public ActionResult Delete(string id = null)
        {
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        //
        // POST: /Package/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool CheckDuplicate(string id, string TypeDesc)
        {
            try
            {
                Package package;
                package = id == string.Empty ? db.Packages.Where(x => x.PackageDesc == TypeDesc && x.PackageID != id).First() : db.Packages.Where(x => x.PackageDesc == TypeDesc).First();
                return package != null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}