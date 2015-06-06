using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class MemberTypeController : Controller
    {
        private DreamboxContext db = new DreamboxContext();
        //
        // GET: /MemberType/

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
            //return View(db.MemberTypes.ToList());
        }

        public JsonResult GetMemberTypesList()
        {
            try
            {
                string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                var memberTypelist = (from memtype in db.MemberTypes.ToList()
                                      join tbuser in db.tbUsers on memtype.DealerID equals tbuser.DealerID
                                      where memtype.DealerID == strUserID
                                      select new { memtype.MemberTypeID, memtype.MemberTypeName, memtype.DealerID, memtype.CreateDate, memtype.UpdateDate, tbuser.RealName }).OrderBy(m => m.MemberTypeName).ToList();

                //var memberTypeList = (List<MemberType>)db.MemberTypes.OrderBy(a => a.MemberTypeDesc).ToList();
                return Json(memberTypelist, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberTypeController", ex.ToString());
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
                    if (!IsDuplicate(string.Empty, memtype.MemberTypeName))
                    {
                        memtype.MemberTypeID = Guid.NewGuid().ToString();
                        memtype.DealerID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        memtype.UpdateDate = DateTime.Now;
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
            catch(Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberTypeController", ex.ToString());
            }
            return "Add member type failed.";
        }
        public string Update(MemberType memtype)
        {
            try
            {
                if (memtype != null)
                {
                    if (!IsDuplicate(memtype.MemberTypeID, memtype.MemberTypeName))
                    {
                        var mem = db.MemberTypes.Find(memtype.MemberTypeID);
                        mem.MemberTypeName = memtype.MemberTypeName;
                        mem.UpdateDate = DateTime.Now;
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
            catch (Exception ex) { LogFile.writeLogFile(DateTime.Now, "MemberTypeController", ex.ToString()); }
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
                LogFile.writeLogFile(DateTime.Now, "MemberTypeController", ex.ToString());
            }
            return "Delete failed.";
        }        

        private bool IsDuplicate(string id, string TypeDesc)
        {
            try
            {
                string UserID =CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                MemberType objMemberType;
                objMemberType = id != string.Empty ? db.MemberTypes.Where(x => x.MemberTypeName == TypeDesc && x.DealerID == UserID && x.MemberTypeID != id).First() : db.MemberTypes.Where(x => x.MemberTypeName == TypeDesc && x.DealerID == UserID).First();
                return objMemberType != null ? true : false;
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberTypeController", ex.ToString());
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