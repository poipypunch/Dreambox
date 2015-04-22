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
    public class MemberTypeController : Controller
    {
        private DreamboxContext db = new DreamboxContext();
        //
        // GET: /MemberType/

        public ActionResult Index()
        {
            return View();
            //return View(db.MemberTypes.ToList());
        }

        public JsonResult GetMemberTypesList()
        {
            try
            {
                var memberTypelist = (from memtype in db.MemberTypes.ToList()
                                      join tbuser in db.tbUsers on memtype.DealerID equals tbuser.DealerID
                                      select new { memtype.MemberTypeID, memtype.MemberTypeDesc, memtype.DealerID, memtype.CreateDate, memtype.CreateBy, memtype.UpdateDate, memtype.UpdateBy, tbuser.RealName }).OrderBy(m=>m.MemberTypeDesc).ToList();

                //var memberTypeList = (List<MemberType>)db.MemberTypes.OrderBy(a => a.MemberTypeDesc).ToList();
                return Json(memberTypelist, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public string Add(MemberType memtype)
        {
            try
            {
                if (memtype != null)
                {
                    if (!IsDuplicate(string.Empty, memtype.MemberTypeDesc))
                    {
                        memtype.MemberTypeID = Guid.NewGuid().ToString();
                        memtype.DealerID = Session["UserID"].ToString();
                        memtype.UpdateBy = Session["UserID"].ToString();
                        memtype.UpdateDate = DateTime.Now;
                        memtype.CreateBy = Session["UserID"].ToString();
                        memtype.CreateDate = DateTime.Now;
                        db.MemberTypes.Add(memtype);
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This member type name already in used.";
                    }
                }
            }
            catch
            {

            }
            return "Add member type failed.";
        }
        public string Update(MemberType memtype)
        {
            try
            {
                if (memtype != null)
                {
                    if (!IsDuplicate(memtype.MemberTypeID, memtype.MemberTypeDesc))
                    {
                        var mem = db.MemberTypes.Find(memtype.MemberTypeID);
                        mem.MemberTypeDesc = memtype.MemberTypeDesc;
                        mem.UpdateDate = DateTime.Now;
                        mem.UpdateBy = Session["UserID"].ToString();
                        db.Entry(mem).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This member type name already in used.";
                    }
                }
            }
            catch (Exception ex) { }
            return "Update failed";
        }


        public string Delete(string id)
        {
            try
            {
                if (id != string.Empty)
                {
                    MemberType memtype = db.MemberTypes.Find(id);
                    db.MemberTypes.Remove(memtype);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed.";
        }
        //
        // GET: /MemberType/Details/5

        //public ActionResult Details(string id = null)
        //{
        //    MemberType membertype = db.MemberTypes.Find(id);
        //    if (membertype == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(membertype);
        //}

        ////
        //// GET: /MemberType/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}
        //
        // POST: /MemberType/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(MemberType membertype)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!CheckDuplicate(string.Empty, membertype.MemberTypeDesc))
        //        {
        //            membertype.MemberTypeID = Guid.NewGuid().ToString();
        //            membertype.CreateBy = Session["UserID"].ToString();
        //            membertype.UpdateBy = Session["UserID"].ToString();
        //            membertype.CreateDate = DateTime.Now;
        //            membertype.UpdateDate = DateTime.Now;
        //            membertype.DealerID = Session["UserID"].ToString();
        //            db.MemberTypes.Add(membertype);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(membertype);
        //}

        ////
        //// GET: /MemberType/Edit/5

        //public ActionResult Edit(string id = null)
        //{
        //    MemberType membertype = db.MemberTypes.Find(id);
        //    if (membertype == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(membertype);
        //}

        ////
        //// POST: /MemberType/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(MemberType membertype)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (CheckDuplicate(membertype.MemberTypeID, membertype.MemberTypeDesc))
        //        {
        //            membertype.UpdateBy = Session["UserID"].ToString();
        //            membertype.UpdateDate = DateTime.Now;
        //            db.Entry(membertype).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(membertype);
        //}

        ////
        //// GET: /MemberType/Delete/5

        //public ActionResult Delete(string id = null)
        //{
        //    MemberType membertype = db.MemberTypes.Find(id);
        //    if (membertype == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(membertype);
        //}

        ////
        //// POST: /MemberType/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    MemberType membertype = db.MemberTypes.Find(id);
        //    db.MemberTypes.Remove(membertype);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        private bool IsDuplicate(string id, string TypeDesc)
        {
            try
            {
                string UserID = Session["UserID"].ToString();
                MemberType objMemberType;
                objMemberType = id != string.Empty ? db.MemberTypes.Where(x => x.MemberTypeDesc == TypeDesc && x.DealerID == UserID && x.MemberTypeID != id).First() : db.MemberTypes.Where(x => x.MemberTypeDesc == TypeDesc && x.DealerID == UserID).First();
                return objMemberType != null ? true : false;
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