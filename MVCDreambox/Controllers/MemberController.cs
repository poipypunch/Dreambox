﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using System.Net;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class MemberController : Controller
    {
        private DreamboxContext db = new DreamboxContext();


        //
        // GET: /Member/

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }

        public JsonResult GetAllMember()
        {
            try
            {
                string strUserID = Session[CommonConstant.SessionUserID].ToString();
                var memberlist = (from mem in db.Members
                                  join memtype in db.MemberTypes on mem.MemberTypeID equals memtype.MemberTypeID
                                  join tbuser in db.tbUsers on mem.DealerID equals tbuser.DealerID
                                  where mem.DealerID == strUserID
                                  select new { mem.MemberID, mem.UserName, mem.Password, mem.MemberName, mem.Email, mem.Address, mem.Phone, mem.MemberTypeID, mem.DealerID, memtype.MemberTypeName, tbuser.RealName }).ToList();
                return Json(memberlist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberController", ex.ToString());
                string str = ex.Message.ToString();
            }
            return null;
        }

        public JsonResult GetAllMemberTypes()
        {
            try
            {
                string strUserID = Session[CommonConstant.SessionUserID].ToString();
                var memberTypeList = db.MemberTypes.Where(m => m.DealerID == strUserID).ToList();
                return Json(memberTypeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberController", ex.ToString());
                string str = ex.Message.ToString();
            }
            return null;
        }
        public string Add(Member member)
        {
            try
            {
                if (member != null)
                {
                    if (!IsDuplicate(string.Empty, member.UserName))
                    {
                        string UserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        RSACrypto crypto = new RSACrypto();
                        member.MemberID = Guid.NewGuid().ToString();
                        member.Password = crypto.Encrypt(CommonConstant.DefaultPassword);
                        member.DealerID = UserID;
                        member.UpdateBy = UserID;
                        member.UpdateDate = DateTime.Now;
                        member.CreateBy = UserID;
                        member.CreateDate = DateTime.Now;
                        db.Members.Add(member);
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This username is already in used.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberController", ex.ToString());
            }
            return "Add new member failed";
        }
        public string Update(Member member)
        {
            try
            {
                if (member != null)
                {
                    if (!IsDuplicate(member.MemberID, member.UserName))
                    {

                        Member mem = db.Members.Find(member.MemberID);
                        mem.UserName = member.UserName;
                        mem.MemberName = member.MemberName;
                        mem.MemberTypeID = member.MemberTypeID;
                        //mem.Password = member.Password;
                        mem.Email = member.Email;
                        mem.Address = member.Address;
                        mem.Phone = member.Phone;
                        mem.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        mem.UpdateDate = DateTime.Now;
                        db.Entry(mem).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This username is already in used.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberController", ex.ToString());

            }
            return "Update failed";
        }

        public string ResetPassword(string id)
        {
            try
            {
                if (id != string.Empty)
                {
                    RSACrypto crypto = new RSACrypto();
                    Member mem = db.Members.Find(id);
                    mem.Password = crypto.Encrypt(CommonConstant.DefaultPassword);
                    mem.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                    mem.UpdateDate = DateTime.Now;
                    db.Entry(mem).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberController", ex.ToString());
            }
            return "Update failed";
        }

        public string Delete(string id)
        {
            try
            {
                if (id != string.Empty)
                {
                    Member member = db.Members.Find(id);
                    db.Members.Remove(member);
                    db.SaveChanges();
                    return "Delete success";
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberController", ex.ToString());
            }
            return "Delete failed";
        }

        private bool IsDuplicate(string id, string strUserName)
        {
            try
            {
                Member member;
                member = id != string.Empty ? db.Members.Where(x => x.UserName == strUserName && x.MemberID != id).First() : db.Members.Where(x => x.UserName == strUserName).First();
                return member != null ? true : false;
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberController", ex.ToString());
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