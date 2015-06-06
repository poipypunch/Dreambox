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
    public class MemberSubscriptionController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /MemberSubscription/
        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }

        public JsonResult GetSubScribeList()
        {
            try
            {
                string strUserID = Session[CommonConstant.SessionUserID].ToString();
                var memberlist = (from mems in db.MemberSubscriptions
                                  join mem in db.Members on mems.MemberID equals mem.MemberID
                                  join memtype in db.MemberTypes on mem.MemberTypeID equals memtype.MemberTypeID
                                  join pay in db.Payments on mems.PaymentID equals pay.PaymentID
                                  where mems.DealerID == strUserID
                                  select new { mems.PaymentID, pay.PaymentName, mems.SubScribeDate, mem.MemberID, mem.UserName, mem.Password, mem.MemberName, mem.Email, mem.Address, mem.Phone, memtype.MemberTypeName }).OrderByDescending(m => m.SubScribeDate).ToList();
                return Json(memberlist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberSubscriptionController", ex.ToString());
                string str = ex.Message.ToString();
            }
            return null;
        }

        public JsonResult GetActiveMemberList()
        {
            try
            {
                string strUserID = Session[CommonConstant.SessionUserID].ToString();
                var memberlist = (from mem in db.Members
                                  join memtype in db.MemberTypes on mem.MemberTypeID equals memtype.MemberTypeID
                                  where mem.DealerID == strUserID
                                  select new { mem.MemberID, mem.UserName, mem.Password, mem.MemberName, mem.Email, mem.Address, mem.Phone, memtype.MemberTypeName }).OrderBy(m => m.MemberName).ToList();
                return Json(memberlist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberSubscriptionController", ex.ToString());
                string str = ex.Message.ToString();
            }
            return null;
        }

        public string CheckPayment(string PaymentID)
        {
            try
            {
                var pay = db.Payments.Where(m => m.PaymentID == PaymentID).FirstOrDefault();
                if (pay != null)
                {
                    if (pay.PaymentStatus != CommonConstant.CardStatus.New)
                    {
                        return "This payment is already in used.";
                    }
                    else if (pay.PaymentExpiryDate < DateTime.Today)
                    {
                        return "This payment is already expired.";
                    }
                    else
                    {
                        return "Success";
                    }
                }
                else
                {
                    return "Payment not found";
                }
                //return Json(memberTypeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "MemberSubscriptionController", ex.ToString());
                return "Payment not found";
            }
        }

        public string Add(string mid, string pid)
        {
            if (mid != string.Empty && pid != string.Empty)
            {
                using (var context = new DreamboxContext())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            string UserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                            Payment pay = db.Payments.Find(pid);
                            context.Database.ExecuteSqlCommand(
                                @"INSERT INTO  MemberSubscription (PaymentID,MemberID,DealerID,SubScribeDate,CreateBy,CreateDate,UpdateBy,UpdateDate) VALUES ('" + pid + "','" + mid + "','" + UserID + "',GETDATE(),'" + UserID + "',GETDATE(),'" + UserID + "',GETDATE())");
                            context.Database.ExecuteSqlCommand(
                              @"UPDATE Payment SET PaymentStatus = '" + CommonConstant.CardStatus.InUsed + "',UpdateBy='" + UserID + "',UpdateDate=GETDATE()" +
                                  " WHERE PaymentID = '" + pid + "'"
                              );
                            context.Database.ExecuteSqlCommand(
                              @"UPDATE Member SET ExpiryDate = DATEADD(day," + pay.PaymentTotalDay + ",ExpiryDate),UpdateBy='"+UserID+"',UpdateDate=GETDATE() " +
                                  " WHERE MemberID ='" + mid + "'"
                              );                    
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            return "Success";
                        }
                        catch (Exception ex)
                        {
                            LogFile.writeLogFile(DateTime.Now, "MemberSubscriptionController", ex.ToString());
                            dbContextTransaction.Rollback();
                        }
                    }
                }
            }
            return "Subscribe failed";
        }
      

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}