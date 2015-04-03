using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using System.Data.Objects.SqlClient;

namespace MVCDreambox.Controllers
{
    public class SysUserController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /SysUser/

        public ActionResult Index()
        {
            return View(db.SysUsers.ToList());
        }

        //
        // GET: /SysUser/Details/5

        public ActionResult Details(string id = null)
        {
            SysUser sysuser = db.SysUsers.Find(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        //
        // GET: /SysUser/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SysUser/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                sysuser.UserID = Guid.NewGuid().ToString();
                sysuser.CreateBy = Role.Admin;
                sysuser.CreateDate = DateTime.Now;
                sysuser.UpdateBy = Role.Admin;
                sysuser.UpdateDate = DateTime.Now;
                db.SysUsers.Add(sysuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysuser);
        }

        //
        // GET: /SysUser/Edit/5

        public ActionResult Edit(string id = null)
        {
            SysUser sysuser = db.SysUsers.Find(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        //
        // POST: /SysUser/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                sysuser.UpdateBy = Role.Admin;
                sysuser.UpdateDate = DateTime.Now;
                db.Entry(sysuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysuser);
        }

        //
        // GET: /SysUser/Delete/5

        public ActionResult Delete(string id = null)
        {
            SysUser sysuser = db.SysUsers.Find(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        //
        // POST: /SysUser/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SysUser sysuser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private string GetNewUserID()
        {
            string UserID = string.Empty;
            try
            {
                var maxTopID = (from max in db.SysUsers
                                where !String.IsNullOrEmpty(max.UserID)
                                select max.UserID).Max();
                maxTopID = (Convert.ToInt32(maxTopID) + 1).ToString();
                UserID = String.Format("{0:0000}", maxTopID);
            }
            catch (Exception ex)
            {
                UserID = "0001";
            }
            return UserID;
        }
    }
}