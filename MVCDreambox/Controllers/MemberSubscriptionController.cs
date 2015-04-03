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
    public class MemberSubscriptionController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /MemberSubscription/

        public ActionResult Index()
        {
            return View(db.MemberSubscriptions.ToList());
        }

        //
        // GET: /MemberSubscription/Details/5

        public ActionResult Details(string id = null)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            if (membersubscription == null)
            {
                return HttpNotFound();
            }
            return View(membersubscription);
        }

        //
        // GET: /MemberSubscription/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MemberSubscription/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberSubscription membersubscription)
        {
            if (ModelState.IsValid)
            {
                db.MemberSubscriptions.Add(membersubscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membersubscription);
        }

        //
        // GET: /MemberSubscription/Edit/5

        public ActionResult Edit(string id = null)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            if (membersubscription == null)
            {
                return HttpNotFound();
            }
            return View(membersubscription);
        }

        //
        // POST: /MemberSubscription/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberSubscription membersubscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membersubscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membersubscription);
        }

        //
        // GET: /MemberSubscription/Delete/5

        public ActionResult Delete(string id = null)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            if (membersubscription == null)
            {
                return HttpNotFound();
            }
            return View(membersubscription);
        }

        //
        // POST: /MemberSubscription/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            db.MemberSubscriptions.Remove(membersubscription);
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