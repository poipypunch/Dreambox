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
            try
            {
                string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                var results = db.Categories.Where(m => m.DealerID == strUserID).OrderBy(m => m.CategoryName).ToList();

                Category cate = new Category();
                cate.CategoryID = "0";
                cate.CategoryName = "Root";
                cate.DealerID = strUserID;
                cate.ParentID = null;
                results.Add(cate);
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "CategoryController", ex.ToString());
            }
            return Json(null, JsonRequestBehavior.AllowGet);
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
                    if (!IsDuplicate(string.Empty, category.CategoryName))
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
                        cate.CategoryName = category.CategoryName;
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
                LogFile.writeLogFile(DateTime.Now, "CategoryController", ex.ToString());
            }
            return "Failed|Add new category failed";
        }

        public string Update(Category category)
        {
            try
            {
                if (category != null)
                {
                    if (!IsDuplicate(category.CategoryID, category.CategoryName))
                    {
                        Category cate = db.Categories.Find(category.CategoryID);
                        cate.CategoryName = category.CategoryName;
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
                LogFile.writeLogFile(DateTime.Now, "CategoryController", ex.ToString());
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
                LogFile.writeLogFile(DateTime.Now, "CategoryController", ex.ToString());

            }
            return "Delete failed";
        }

        private bool IsDuplicate(string id, string strCategoryDesc)
        {
            try
            {
                string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                Category cate;
                cate = id != string.Empty ? db.Categories.Where(x => x.CategoryName == strCategoryDesc && x.DealerID == strUserID && x.CategoryID != id).First() : db.Categories.Where(x => x.CategoryName == strCategoryDesc && x.DealerID == strUserID).First();
                return cate != null ? true : false;
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "CategoryController", ex.ToString());
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