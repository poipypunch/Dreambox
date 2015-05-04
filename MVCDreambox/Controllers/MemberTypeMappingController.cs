using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class MemberTypeMappingController : Controller
    {
        DreamboxContext db = new DreamboxContext();
        //
        // GET: /MemberTypeMapping/       
        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }
        public JsonResult GetActiveMemberTypeList()
        {
            try
            {
                string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                var MemberTypeList = db.MemberTypes.Where(m => m.DealerID == strUserID).ToList();
                return Json(MemberTypeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public JsonResult GetMappingCategoryList(string MemberTypeID)
        {
            try
            {                
                var Categorylist = (from p in db.MemberTypeMappings.ToList()
                                    join memtype in db.MemberTypes on p.MemberTypeID equals memtype.MemberTypeID
                                    join cate in db.Categories on p.CategoryID equals cate.CategoryID
                                    where p.MemberTypeID == MemberTypeID 
                                    select new { p.MemberTypeID, p.CategoryID, memtype.MemberTypeDesc, cate.CategoryDesc, p.CreateDate }).OrderByDescending(m => m.CreateDate).ToList();
                return Json(Categorylist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public JsonResult GetActiveCategorysList(string MemberTypeID)
        {
            try
            {
                string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                var categorys = (from s in db.Categories
                                 where s.DealerID==strUserID &&  !db.MemberTypeMappings.Any(p => (p.CategoryID == s.CategoryID) && (p.MemberTypeID == MemberTypeID))
                                 select s).ToList();
                return Json(categorys, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public string Delete(string mid, string cid)
        {
            try
            {
                if (mid != string.Empty && cid != string.Empty)
                {
                    MemberTypeMapping memtype = db.MemberTypeMappings.Where(m => m.MemberTypeID == mid && m.CategoryID == cid).FirstOrDefault();
                    db.MemberTypeMappings.Remove(memtype);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed.";
        }
        public string Add(string mid, string[] categoryids)
        {
            try
            {
                if (mid != string.Empty && categoryids != null && categoryids.Length > 0)
                {
                    MemberTypeMapping memtype;
                    for (int i = 0; i < categoryids.Length; i++)
                    {
                        memtype = new MemberTypeMapping();
                        memtype.MemberTypeID = mid;
                        memtype.CategoryID = categoryids[i];
                        memtype.DealerID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        memtype.CreateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        memtype.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        memtype.CreateDate = DateTime.Now;
                        memtype.UpdateDate = DateTime.Now;
                        db.MemberTypeMappings.Add(memtype);
                    }
                    db.SaveChanges();

                    return "Success";
                }
            }
            catch (Exception ex)
            {

            }
            return "Add failed.";
        }
    }
}
