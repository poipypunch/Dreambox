using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class PackagePermissionController : Controller
    {
        DreamboxContext db = new DreamboxContext();
        //
        // GET: /PackageMapping/

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("tbUser", "Login"); } else { return View(); }
        }
        public JsonResult GetActiveUserList()
        {
            try
            {
                var UserList = db.tbUsers.Where(m => m.Status == CommonConstant.Status.Active && m.Role != CommonConstant.Role.Admin).ToList();
                return Json(UserList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public JsonResult GetMappingPackageList(string DealerID)
        {
            try
            {
                var Packagelist = (from pack in db.PackagePermissions.ToList()
                                   join user in db.tbUsers on pack.DealerID equals user.DealerID
                                   join p in db.Packages on pack.PackageID equals p.PackageID
                                   where pack.DealerID == DealerID
                                   select new { pack.DealerID, pack.PackageID, user.RealName, p.PackageDesc, pack.CreateDate }).OrderByDescending(m => m.CreateDate).ToList();
                return Json(Packagelist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public JsonResult GetActivePackageList(string DealerID)
        {
            try
            {
                var packages = (from s in db.Packages
                                where s.PackageStatus == CommonConstant.Status.Active && !db.PackagePermissions.Any(p => (p.PackageID == s.PackageID) && (p.DealerID == DealerID))
                                select s).ToList();
                return Json(packages, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }
        public string Delete(string uid, string pid)
        {
            try
            {
                if (uid != string.Empty && pid != string.Empty)
                {
                    PackagePermission package = db.PackagePermissions.Where(m => m.PackageID == pid && m.DealerID == uid).FirstOrDefault();
                    db.PackagePermissions.Remove(package);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed.";
        }
        public string Add(string uid, string[] packids)
        {
            try
            {
                if (uid != string.Empty && packids != null && packids.Length > 0)
                {
                    PackagePermission pack;
                    for (int i = 0; i < packids.Length; i++)
                    {
                        pack = new PackagePermission();
                        pack.DealerID = uid;
                        pack.PackageID = packids[i];
                        pack.CreateBy = Session[CommonConstant.SessionUserID].ToString();
                        pack.UpdateBy = Session[CommonConstant.SessionUserID].ToString();
                        pack.CreateDate = DateTime.Now;
                        pack.UpdateDate = DateTime.Now;
                        db.PackagePermissions.Add(pack);
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

