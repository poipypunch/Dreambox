using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class CategoryController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }

        public JsonResult GetCategoryTrees()
        {
            string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
            var results = db.Categories.Where(m => m.DealerID == strUserID).OrderBy(m => m.CategoryDesc).ToList();

            Category cate = new Category();
            cate.CategoryID = "0";
            cate.CategoryDesc = "Root";
            cate.DealerID = strUserID;
            cate.ParentID = null;
            results.Add(cate);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //public string Add(CategoryDummay category)
        //{
        //    try
        //    {
        //        if (category != null)
        //        {
        //            if (!IsDuplicate(string.Empty, category.CategoryDesc))
        //            {

        //                string fname = HttpContext.Server.MapPath("Uploads\\" + category.Attachment.FileName);
        //                category.Attachment.SaveAs(fname);
        //                category.CategoryID = Guid.NewGuid().ToString();
        //                category.DealerID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
        //                category.UpdateDate = DateTime.Now;
        //                category.CreateDate = DateTime.Now;
        //                //db.Categories.Add(category);
        //                db.SaveChanges();
        //                return "Success|" + category.CategoryID.ToString();
        //            }
        //            else
        //            {
        //                return "Failed|This category name is already in used.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return "Failed|Add new category failed";
        //}
        public string Add(CategoryDummay category)
        {
            try
            {
                if (category != null)
                {
                    if (!IsDuplicate(string.Empty, category.CategoryDesc))
                    {
                        Category cate = new Category();
                        if (category.Attachment != null)
                        {
                            cate.ImgPath = cate.CategoryID + "\\" + category.Attachment.FileName;
                            try
                            {
                                //string fname = HttpContext.Server.MapPath("Uploads\\" + cate.CategoryID + "\\" + category.Attachment.FileName);
                                string fname = HttpContext.Server.MapPath("Uploads\\" + category.Attachment.FileName);
                                //string fname = CommonConstant.GetSiteRoot() + "Uploads\\" + cate.CategoryID + "\\" + category.Attachment.FileName;
                                category.Attachment.SaveAs(fname);
                            }
                            catch (Exception err)
                            {

                            }
                        }
                        
                        cate.CategoryID = Guid.NewGuid().ToString();
                        cate.CategoryDesc = category.CategoryDesc;   
                        cate.DealerID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        cate.UpdateDate = DateTime.Now;
                        cate.CreateDate = DateTime.Now;
                        cate.ParentID = category.ParentID;

                        db.Categories.Add(cate);
                        db.SaveChanges();
                        return "Success|" + cate.CategoryID.ToString();
                    }
                    else
                    {
                        return "Failed|This category name is already in used.";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "Failed|Add new category failed";
        }

        public string Update(Category category)
        {
            try
            {
                if (category != null)
                {
                    if (!IsDuplicate(category.CategoryID, category.CategoryDesc))
                    {
                        Category cate = db.Categories.Find(category.CategoryID);
                        cate.CategoryDesc = category.CategoryDesc;
                        cate.ImgPath = category.ImgPath;
                        cate.UpdateDate = DateTime.Now;
                        db.Entry(cate).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This category name is already in used.";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return "Update failed";
        }

        public string Delete(string id)
        {
            try
            {
                if (id != string.Empty)
                {
                    var results = from c in db.Categories
                                  where (c.ParentID == id || c.CategoryID == id)
                                  select c;

                    foreach (var cate in results)
                    {
                        db.Categories.Remove(cate);
                    }
                    db.SaveChanges();
                    return "Delete success";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed";
        }

        private bool IsDuplicate(string id, string strCategoryDesc)
        {
            try
            {
                string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                Category cate;
                cate = id != string.Empty ? db.Categories.Where(x => x.CategoryDesc == strCategoryDesc && x.DealerID == strUserID && x.CategoryID != id).First() : db.Categories.Where(x => x.CategoryDesc == strCategoryDesc && x.DealerID == strUserID).First();
                return cate != null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public ActionResult PatialCreate(string ParentID, string viewName)
        //{
        //    Category model = new Category();
        //    Category cate = new Category();
        //    viewName = "Create";
        //    cate = db.Categories.Find(ParentID);
        //    model.ParentID = ParentID;
        //    ViewBag.ParentName = "Parent : " + cate.CategoryDesc;
        //    return PartialView(viewName, model);
        //}
        //[HttpPost]
        //public ActionResult Create(Category cate)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            cate.CategoryID = Guid.NewGuid().ToString();
        //            cate.CreateDate = DateTime.Now;
        //            cate.UpdateDate = DateTime.Now;
        //            cate.DealerID = Session["UserID"].ToString();
        //            db.Categories.Add(cate);
        //            db.SaveChanges();
        //            return RedirectToAction("Index", "Category");
        //        }
        //    }
        //    catch (Exception ex) { }
        //    return PartialView("Create", cate);
        //}

        ////
        //// GET: /Category/Details/5

        //public ActionResult Details(string id = null)
        //{
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        ////
        //// GET: /Category/Create

        ////public ActionResult Create()
        ////{
        ////    return View();
        ////}

        ////
        //// POST: /Category/Create

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Create(Category category)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        db.Categories.Add(category);
        ////        db.SaveChanges();
        ////        return RedirectToAction("Index");
        ////    }

        ////    return View(category);
        ////}

        ////
        //// GET: /Category/Edit/5

        //public ActionResult Edit(string id = null)
        //{
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        ////
        //// POST: /Category/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(category).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        ////
        //// GET: /Category/Delete/5

        //public ActionResult Delete(string id = null)
        //{
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        ////
        //// POST: /Category/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Category category = db.Categories.Find(id);
        //    db.Categories.Remove(category);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}