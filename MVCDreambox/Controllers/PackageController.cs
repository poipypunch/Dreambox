using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using MVCDreambox.ViewModels;
using System.Data.Entity.Validation;
using System.Net;
namespace MVCDreambox.Controllers
{
    public class PackageController : Controller
    {
        private DreamboxContext db = new DreamboxContext();


        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPackagesList()
        {
            try
            {
                var packageList = (List<Package>)db.Packages.OrderBy(a => a.PackageDesc).ToList();
                return Json(packageList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public string Add(Package package)
        {
            try
            {
                if (package != null)
                {
                    if (!IsDuplicate(string.Empty, package.PackageDesc))
                    {
                        package.PackageID = Guid.NewGuid().ToString();
                        package.UpdateBy = Session["UserID"].ToString();
                        package.UpdateDate = DateTime.Now;
                        package.CreateBy = Session["UserID"].ToString();
                        package.CreateDate = DateTime.Now;
                        db.Packages.Add(package);
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This package name already in used.";
                    }
                }
            }
            catch
            {

            }
            return "Add user failed.";
        }
        public string Update(Package package)
        {
            try
            {
                if (package != null)
                {
                    if (!IsDuplicate(package.PackageID, package.PackageDesc))
                    {
                        var pack = db.Packages.Find(package.PackageID);
                        pack.PackageDesc = package.PackageDesc;
                        pack.PackageStatus = package.PackageStatus;                       
                        pack.UpdateDate = DateTime.Now;
                        pack.UpdateBy = Session["UserID"].ToString();
                        db.Entry(pack).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This package name already in used.";
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
                    Package package = db.Packages.Find(id);
                    db.Packages.Remove(package);
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
        // GET: /Package/

        //public ActionResult Index()
        //{
        //    return View(db.Packages.ToList());
        //}

        //public ActionResult ChannelList(string id, string PackageDesc)
        //{
        //    ViewBag.PackageName = PackageDesc;
        //    ViewBag.PackageID = id;
        //    return View(db.PackageMappings.Where(p => p.PackageID == id).OrderByDescending(p => p.channel.CreateDate).Include(p => p.package).Include(p => p.channel).ToList());
        //}
        //[HttpPost]
        //public ActionResult ChannelList(string[] DeleteIds)
        //{
        //    List<PackageMapping> objPackage = new List<PackageMapping>();
        //    string packageID = string.Empty;
        //    //string[] Pid = null;
        //    //string[] Cid = null;
        //    //if (DeleteIds != null)
        //    //{
        //    //    //Pid = new string[DeleteIds.Length];
        //    //    //Cid = new string[DeleteIds.Length];
        //    //    //int j = 0;
        //    //    //foreach (string i in DeleteIds)
        //    //    //{
        //    //    //    string[] str = i.Split('|').ToArray();
        //    //    //    Pid[j] = str[0];
        //    //    //    Cid[j] = str[1];
        //    //    //    j++;
        //    //    //}
        //    //}
        //    if (DeleteIds != null && DeleteIds.Length > 0)
        //    {
        //        List<PackageMapping> selectedIds = new List<PackageMapping>();
        //        packageID = DeleteIds[0].Split('|').ToArray().First();
        //        Package objpackage = new Package();
        //        objpackage = db.Packages.Where(a => a.PackageID == packageID).FirstOrDefault();
        //        // packageID = DeleteIds[0].Split('|').ToArray().First();
        //        //selectedIds = db.PackageMappings.Where(a => Pid.Contains(a.PackageID) && Cid.Contains(a.ChannelID)).ToList();
        //        selectedIds = db.PackageMappings.Where(a => DeleteIds.Contains(a.PackageID + "|" + a.ChannelID)).ToList();
        //        foreach (var i in selectedIds)
        //        {
        //            db.PackageMappings.Remove(i);
        //        }
        //        db.SaveChanges();
        //        //objPackage = db.PackageMappings.ToList();
        //        objPackage = db.PackageMappings.Where(p => p.PackageID == objpackage.PackageID).OrderByDescending(p => p.channel.CreateDate).Include(p => p.package).Include(p => p.channel).ToList();
        //        ViewBag.PackageName = objpackage.PackageDesc;
        //        ViewBag.message = "Selected Records are Deleted Successfully";
        //    }
        //    return View(objPackage);
        //}

        //public ActionResult AddChannel(string id)
        //{
        //    List<Channel> channel = new List<Channel>();

        //    channel = (from s in db.Channels
        //               where !db.PackageMappings.Any(p => (p.ChannelID == s.ChannelID) && (p.PackageID == id))
        //               select s).ToList();
        //    Package objpackage = new Package();
        //    objpackage = db.Packages.Where(a => a.PackageID == id).FirstOrDefault();
        //    ViewBag.PackageName = objpackage.PackageID;
        //    ViewBag.PackageID = id;
        //    return View(channel);
        //}

        //[HttpPost]
        //public ActionResult AddChannel(string[] AddIds)
        //{
        //    List<PackageMapping> objPackage = new List<PackageMapping>();
        //    string packageID = string.Empty;
        //    if (AddIds != null && AddIds.Length > 0)
        //    {
        //        PackageMapping pack;
        //        packageID = AddIds[0].Split('|').ToArray().First();
        //        string[] svalue = null;
        //        for (int i = 0; i < AddIds.Length; i++)
        //        {
        //            svalue = AddIds[i].Split('|').ToArray();
        //            pack = new PackageMapping();
        //            pack.PackageID = svalue[0];
        //            pack.ChannelID = svalue[1];
        //            pack.CreateBy = Session["UserID"].ToString();
        //            pack.UpdateBy = Session["UserID"].ToString();
        //            pack.CreateDate = DateTime.Now;
        //            pack.UpdateDate = DateTime.Now;
        //            db.PackageMappings.Add(pack);
        //        }

        //        db.SaveChanges();
        //        Package objpackage = new Package();
        //        objpackage = db.Packages.Where(a => a.PackageID == packageID).FirstOrDefault();
        //        ViewBag.PackageName = objpackage.PackageDesc;
        //        ViewBag.PackageID = packageID;
        //        return RedirectToAction("ChannelList", new { id = objpackage.PackageID, PackageDesc = objpackage.PackageDesc });
        //    }
        //    List<Channel> channel = new List<Channel>();
        //    channel = (from s in db.Channels
        //               where !db.PackageMappings.Any(p => (p.ChannelID == s.ChannelID) && (p.PackageID == packageID))
        //               select s).ToList();
        //    return View(channel);            
        //}

        ////
        //// GET: /Package/Details/5

        //public ActionResult Details(string id = null)
        //{
        //    Package package = db.Packages.Find(id);
        //    if (package == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(package);
        //}

        ////
        //// GET: /Package/Create

        //public ActionResult Create()
        //{
        //    var package = new Package();
        //    // package.Channels = new List<Channel>();
        //    //PopulateMappingChannelData(package);
        //    //PopulateAssignedCourseData(instructor);
        //    return View();
        //}

        ////
        //// POST: /Package/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Package package, string[] selectedChannels)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    if (!CheckDuplicate(string.Empty, package.PackageDesc))
        //    //    {
        //    //        package.PackageID = Guid.NewGuid().ToString();
        //    //        package.UpdateBy = Session["UserID"].ToString();
        //    //        package.UpdateDate = DateTime.Now;
        //    //        package.CreateBy = Session["UserID"].ToString();
        //    //        package.CreateDate = DateTime.Now;
        //    //        db.Packages.Add(package);
        //    //        db.SaveChanges();
        //    //        return RedirectToAction("Index");
        //    //    }
        //    //}
        //    try
        //    {
        //        if (selectedChannels != null)
        //        {
        //            //package.Channels = new List<Channel>();
        //            //foreach (var channel in selectedChannels)
        //            //{
        //            //    var ChannelToAdd = db.Channels.Find(channel);
        //            //    package.Channels.Add(ChannelToAdd);
        //            //}
        //        }
        //        if (!CheckDuplicate(string.Empty, package.PackageDesc))
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                package.PackageID = Guid.NewGuid().ToString();
        //                package.UpdateBy = Session["UserID"].ToString();
        //                package.UpdateDate = DateTime.Now;
        //                package.CreateBy = Session["UserID"].ToString();
        //                package.CreateDate = DateTime.Now;


        //                db.Packages.Add(package);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        Exception raise = ex;
        //        foreach (var validationErrors in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                string message = string.Format("{0}:{1}",
        //                    validationErrors.Entry.Entity.ToString(),
        //                    validationError.ErrorMessage);
        //                raise = new InvalidOperationException(message, raise);
        //            }
        //        }
        //        throw raise;

        //    }
        //    //PopulateMappingChannelData(package);

        //    return View(package);
        //}

        //private void PopulateMappingChannelData(Package package)
        //{
        //    var viewModel = new List<MappingChannelToPackage>();
        //    try
        //    {
        //        var allChannel = db.Channels.OrderBy(c => c.ChannelDesc);
        //        //var packageChannel = new HashSet<string>(package.Channels.Select(c => c.ChannelID));
        //        foreach (var channel in allChannel)
        //        {
        //            viewModel.Add(new MappingChannelToPackage
        //            {
        //                ChannelID = channel.ChannelID,
        //                ChannelDesc = channel.ChannelDesc,
        //               // Mapping = packageChannel.Contains(channel.ChannelID)
        //            });
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        Exception raise = ex;
        //        foreach (var validationErrors in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                string message = string.Format("{0}:{1}",
        //                    validationErrors.Entry.Entity.ToString(),
        //                    validationError.ErrorMessage);
        //                raise = new InvalidOperationException(message, raise);
        //            }
        //        }
        //        throw raise;

        //    }
        //    ViewBag.Channels = viewModel;
        //}


        ////
        //// GET: /Package/Edit/5

        //public ActionResult Edit(string id = null)
        //{
        //    //Package package = db.Packages.Find(id);
        //    //if (package == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //return View(package);

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Package package = db.Packages
        //        //.Include(i => i.Channels)
        //        .Where(i => i.PackageID == id)
        //        .Single();
        //    // PopulateMappingChannelData(package);
        //    if (package == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(package);
        //}

        ////
        //// POST: /Package/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Package package, string[] selectedChannels)
        //{
        //    try
        //    {
        //        var PackageToUpdate = db.Packages
        //            //.Include(i => i.Channels)
        //       .Where(i => i.PackageID == package.PackageID)
        //       .Single();
        //        if (ModelState.IsValid)
        //        {
        //            PackageToUpdate.PackageDesc = package.PackageDesc;
        //            PackageToUpdate.PackageStatus = package.PackageStatus;
        //            //UpdatePackageChannels(selectedChannels, PackageToUpdate);
        //            if (!CheckDuplicate(PackageToUpdate.PackageID, PackageToUpdate.PackageDesc))
        //            {
        //                PackageToUpdate.UpdateBy = Session["UserID"].ToString();
        //                PackageToUpdate.UpdateDate = DateTime.Now;
        //                db.Entry(PackageToUpdate).State = EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //        }
        //    }
        //    catch (Exception ex /* dex */)
        //    {
        //        //Log the error (uncomment dex variable name and add a line here to write a log.
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        //    }

        //    //PopulateMappingChannelData(package);
        //    return View(package);
        //    //if (ModelState.IsValid)
        //    //{
        //    //    if (!CheckDuplicate(package.PackageID, package.PackageDesc))
        //    //    {
        //    //        db.Entry(package).State = EntityState.Modified;
        //    //        package.UpdateBy = Session["UserID"].ToString();
        //    //        package.UpdateDate = DateTime.Now;
        //    //        db.SaveChanges();
        //    //        return RedirectToAction("Index");
        //    //    }
        //    //}
        //    //return View(package);
        //}

        ////private void UpdatePackageChannels(string[] selectedChannels, Package packageToUpdate)
        ////{
        ////    if (selectedChannels == null)
        ////    {
        ////        packageToUpdate.Channels = new List<Channel>();
        ////        return;
        ////    }
        ////    var selectedChannelsHS = new HashSet<string>(selectedChannels);
        ////    var PackageChannels = new HashSet<string>
        ////        (packageToUpdate.Channels.Select(c => c.ChannelID));
        ////    foreach (var course in db.Channels)
        ////    {
        ////        if (selectedChannelsHS.Contains(course.ChannelID))
        ////        {
        ////            if (!PackageChannels.Contains(course.ChannelID))
        ////            {
        ////                packageToUpdate.Channels.Add(course);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            if (PackageChannels.Contains(course.ChannelID))
        ////            {
        ////                packageToUpdate.Channels.Remove(course);
        ////            }
        ////        }
        ////    }
        ////}
        ////
        //// GET: /Package/Delete/5

        //public ActionResult Delete(string id = null)
        //{
        //    Package package = db.Packages.Find(id);
        //    if (package == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(package);
        //}

        ////
        //// POST: /Package/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Package package = db.Packages.Find(id);
        //    db.Packages.Remove(package);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        private bool IsDuplicate(string id, string packageDesc)
        {
            try
            {
                Package package;
                package = id != string.Empty ? db.Packages.Where(x => x.PackageDesc == packageDesc && x.PackageID != id).First() : db.Packages.Where(x => x.PackageDesc == packageDesc).First();
                return package != null ? true : false;
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