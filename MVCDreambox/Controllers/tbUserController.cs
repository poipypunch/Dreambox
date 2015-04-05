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
    public class tbUserController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /SysUser/

        public ActionResult Index()
        {
            return View(db.tbUsers.ToList());
        }

        //
        // GET: /SysUser/Details/5

        public ActionResult Details(string id = null)
        {
            tbUser tbuser = db.tbUsers.Find(id);
            if (tbuser == null)
            {
                return HttpNotFound();
            }
            return View(tbuser);
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
        public ActionResult Create(tbUser tbuser)
        {
            if (ModelState.IsValid)
            {
                if (!CheckDuplicate(string.Empty, tbuser.UserName))
                {
                    tbuser.DealerID = Guid.NewGuid().ToString();
                    tbuser.CreateBy = Session["UserID"].ToString();
                    tbuser.CreateDate = DateTime.Now;
                    tbuser.UpdateBy = Session["UserID"].ToString();
                    db.tbUsers.Add(tbuser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(tbuser);
        }

        //
        // GET: /SysUser/Edit/5

        public ActionResult Edit(string id = null)
        {
            tbUser tbuser = db.tbUsers.Find(id);
            if (tbuser == null)
            {
                return HttpNotFound();
            }
            return View(tbuser);
        }

        //
        // POST: /SysUser/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbUser tbuser)
        {
            if (ModelState.IsValid)
            {
                if (!CheckDuplicate(tbuser.DealerID, tbuser.UserName))
                {
                    tbuser.UpdateBy = Session["UserID"].ToString();
                    tbuser.UpdateDate = DateTime.Now;
                    db.Entry(tbuser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(tbuser);
        }

        //
        // GET: /SysUser/Delete/5

        public ActionResult Delete(string id = null)
        {
            tbUser tbuser = db.tbUsers.Find(id);
            if (tbuser == null)
            {
                return HttpNotFound();
            }
            return View(tbuser);
        }

        //
        // POST: /SysUser/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbUser tbuser = db.tbUsers.Find(id);
            db.tbUsers.Remove(tbuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //private string GetNewUserID()
        //{
        //    string UserID = string.Empty;
        //    try
        //    {
        //        var maxTopID = (from max in db.SysUsers
        //                        where !String.IsNullOrEmpty(max.UserID)
        //                        select max.UserID).Max();
        //        maxTopID = (Convert.ToInt32(maxTopID) + 1).ToString();
        //        UserID = String.Format("{0:0000}", maxTopID);
        //    }
        //    catch (Exception ex)
        //    {
        //        UserID = "0001";
        //    }
        //    return UserID;
        //}

        private bool CheckDuplicate(string id, string TypeDesc)
        {
            try
            {
                tbUser tbuser;
                tbuser = id == string.Empty ? db.tbUsers.Where(x => x.UserName == TypeDesc && x.DealerID != id).First() : db.tbUsers.Where(x => x.UserName == TypeDesc).First();
                return tbuser != null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}