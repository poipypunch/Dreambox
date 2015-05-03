using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class ContentManagementController : Controller
    {
        //
        // GET: /ContentManagement/

        DreamboxContext db = new DreamboxContext();        
        //
        // GET: /PackageMapping/

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }
        public JsonResult GetCategoryList()
        {
            try
            {
                string strUserID =CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                var CategoryList = db.Categories.Where(m => m.DealerID == strUserID).ToList();
                Category cate = new Category();
                cate.CategoryID = "0";
                cate.CategoryDesc = "Root";
                cate.DealerID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                cate.ParentID = null;
                CategoryList.Add(cate);
                return Json(CategoryList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public JsonResult GetMappingChannelList(string CategoryID)
        {
            try
            {
                var Channellist = (from content in db.ContentMangements.ToList()
                                   join cate in db.Categories on content.CategoryID equals cate.CategoryID
                                   join chan in db.Channels on content.ChannelID equals chan.ChannelID
                                   where content.CategoryID == CategoryID
                                   select new { content.CategoryID, content.ChannelID, cate.CategoryDesc, chan.ChannelDesc, content.CreateDate }).OrderByDescending(m => m.CreateDate).ToList();
                return Json(Channellist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public JsonResult GetActiveChannelList(string CategoryID)
        {
            try
            {
                var channels = (from s in db.Channels
                                where s.ChannelStatus == CommonConstant.Status.Active  && !db.ContentMangements.Any(p => (p.ChannelID == s.ChannelID) && (p.CategoryID == CategoryID))
                                select s).ToList();
                return Json(channels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public string Delete(string cid, string chid)
        {
            try
            {
                if (cid != string.Empty && chid != string.Empty)
                {
                    ContentManagement content = db.ContentMangements.Where(m => m.CategoryID == cid && m.ChannelID == chid).FirstOrDefault();
                    db.ContentMangements.Remove(content);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed.";
        }
        public string Add(string cid, string[] channelids)
        {
            try
            {
                if (cid != string.Empty && channelids != null && channelids.Length > 0)
                {
                    ContentManagement content;
                    for (int i = 0; i < channelids.Length; i++)
                    {
                        content = new ContentManagement();
                        content.CategoryID = cid;
                        content.ChannelID = channelids[i];
                        content.CreateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        content.CreateDate = DateTime.Now;
                        db.ContentMangements.Add(content);
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
