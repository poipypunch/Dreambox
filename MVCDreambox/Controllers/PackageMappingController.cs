using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
namespace MVCDreambox.Controllers
{
    public class PackageMappingController : Controller
    {
        DreamboxContext db = new DreamboxContext();
        //
        // GET: /PackageMapping/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetActivePackageListTypes()
        {
            try
            {
                var PackageList = db.Packages.Where(m => m.PackageStatus == "Active").ToList();
                return Json(PackageList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public JsonResult GetMappingChannelList(string PackageID)
        {
            try
            {
                var Channellist = (from p in db.PackageMappings.ToList()
                                   join pack in db.Packages on p.PackageID equals pack.PackageID
                                   join chan in db.Channels on p.ChannelID equals chan.ChannelID
                                   where p.PackageID == PackageID
                                   select new { p.PackageID, p.ChannelID, pack.PackageDesc, chan.ChannelDesc, p.CreateDate }).OrderByDescending(m => m.CreateDate).ToList();
                return Json(Channellist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;

            try
            {
                var PackageList = db.Packages.Where(m => m.PackageStatus == "Active").ToList();
                return Json(PackageList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public JsonResult GetActiveChannelList(string PackageID)
        {
            try
            {
                var channels = (from s in db.Channels
                                where s.ChannelStatus == "Active" && !db.PackageMappings.Any(p => (p.ChannelID == s.ChannelID) && (p.PackageID == PackageID))
                                select s).ToList();
                // Package objpackage = new Package();

                //var PackageList= db.PackageMappings.Where(p => p.PackageID == id).OrderByDescending(p => p.channel.CreateDate).Include(p => p.package).Include(p => p.channel).ToList()
                //var PackageList = db.Packages.Where(m => m.PackageStatus == "Active").ToList();
                return Json(channels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public string Delete(string pid, string cid)
        {
            try
            {
                if (pid != string.Empty && cid != string.Empty)
                {
                    PackageMapping package = db.PackageMappings.Where(m => m.PackageID == pid && m.ChannelID == cid).FirstOrDefault();
                    db.PackageMappings.Remove(package);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed.";
        }
        public string Add(string pid, string[] channelids)
        {
            try
            {
                if (pid != string.Empty && channelids != null && channelids.Length > 0)
                {
                    PackageMapping pack;
                    for (int i = 0; i < channelids.Length; i++)
                    {
                        pack = new PackageMapping();
                        pack.PackageID = pid;
                        pack.ChannelID = channelids[i];
                        pack.CreateBy = Session["UserID"].ToString();
                        pack.UpdateBy = Session["UserID"].ToString();
                        pack.CreateDate = DateTime.Now;
                        pack.UpdateDate = DateTime.Now;
                        db.PackageMappings.Add(pack);
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
