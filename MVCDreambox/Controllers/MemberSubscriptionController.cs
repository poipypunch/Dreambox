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

                string str = ex.Message.ToString();
            }
            return null;
        }

        public JsonResult GetActiveMemberList()
        {
            try
            {
                string strUserID = Session[CommonConstant.SessionUserID].ToString();
                var memberList = db.Members.Where(m => m.DealerID == strUserID).ToList();
                return Json(memberList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

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
                return "Payment not found";
            }
        }

        public string Add(string mid, string pid)
        {
            try
            {
                if (mid != string.Empty && pid != string.Empty)
                {
                    string UserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                    MemberSubscription mems = new MemberSubscription();
                    mems.MemberID = mid;
                    mems.PaymentID = pid;
                    mems.SubScribeDate = DateTime.Now;
                    mems.DealerID = UserID;
                    mems.UpdateBy = UserID;
                    mems.UpdateDate = DateTime.Now;
                    mems.CreateBy = UserID;
                    mems.CreateDate = DateTime.Now;
                    db.MemberSubscriptions.Add(mems);

                    Payment pay = db.Payments.Find(pid);
                    Payment paysave = pay;
                    paysave.PaymentStatus = CommonConstant.CardStatus.InUsed;
                    paysave.UpdateBy = UserID;
                    paysave.UpdateDate = DateTime.Now;
                    db.Entry(paysave).State = EntityState.Modified;
                    db.Payments.Add(paysave);

                    Member mem = db.Members.Find(mid);
                    Member memsave = mem;
                    memsave.ExpiryDate = mem.ExpiryDate.Value.AddDays(pay.PaymentTotalDay);
                    memsave.UpdateBy = UserID;
                    memsave.UpdateDate = DateTime.Now;
                    db.Entry(memsave).State = EntityState.Modified;
                    db.Members.Add(memsave);
                    
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {

            }
            return "Subscribe failed";
        }
        //
        // GET: /MemberSubscription/Details/5

        public ActionResult Details(string id = null)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            if (membersubscription == null)
            {
                return HttpNotFound();
            }
            return View(membersubscription);
        }

        //
        // GET: /MemberSubscription/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MemberSubscription/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberSubscription membersubscription)
        {
            if (ModelState.IsValid)
            {
                db.MemberSubscriptions.Add(membersubscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membersubscription);
        }

        //
        // GET: /MemberSubscription/Edit/5

        public ActionResult Edit(string id = null)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            if (membersubscription == null)
            {
                return HttpNotFound();
            }
            return View(membersubscription);
        }

        //
        // POST: /MemberSubscription/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberSubscription membersubscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membersubscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membersubscription);
        }

        //
        // GET: /MemberSubscription/Delete/5

        public ActionResult Delete(string id = null)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            if (membersubscription == null)
            {
                return HttpNotFound();
            }
            return View(membersubscription);
        }

        //
        // POST: /MemberSubscription/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MemberSubscription membersubscription = db.MemberSubscriptions.Find(id);
            db.MemberSubscriptions.Remove(membersubscription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}