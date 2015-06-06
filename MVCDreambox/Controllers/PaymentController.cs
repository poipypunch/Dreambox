using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using Newtonsoft.Json;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class PaymentController : Controller
    {
        private DreamboxContext db = new DreamboxContext();
        //
        // GET: /Payment/

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }

        public JsonResult GetPaymentsList()
        {
            try
            {
                var paymentList = (List<Payment>)db.Payments.OrderByDescending(a => a.CreateDate).ToList();
                return Json(paymentList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "PaymentController", ex.ToString());
                string str = ex.Message.ToString();
            }
            return null;
        }        

        public string Add(PaymentDummy paymentdummy)
        {
            try
            {
                Payment payment;
                if (paymentdummy != null)
                {
                    //int NewID = GetMaxPaymentID() + 1;
                    for (int i = 0; i < paymentdummy.Quantity; i++)
                    {
                        payment = new Payment();
                        payment.PaymentID = DateTime.Now.ToString("ssyyyymmddHHMMf"); 
                        payment.PaymentName = paymentdummy.PaymentName;
                        payment.PaymentStatus = paymentdummy.PaymentStatus;
                        payment.PaymentCost = paymentdummy.PaymentCost;
                        payment.PaymentExpiryDate = paymentdummy.PaymentExpiryDate;
                        payment.PaymentTotalDay = paymentdummy.PaymentTotalDay;
                        payment.PaymentStatus = CommonConstant.CardStatus.New;
                        payment.CreateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        payment.CreateDate = DateTime.Now;
                        payment.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        payment.UpdateDate = DateTime.Now;
                        db.Payments.Add(payment);
                    }
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "PaymentController", ex.ToString());
            }
            return "Add payment failed.";
        }

        public string Delete(string id)
        {
            try
            {
                if (id != string.Empty)
                {
                    Payment payment = db.Payments.Find(id);
                    db.Payments.Remove(payment);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "PaymentController", ex.ToString());
            }
            return "Delete failed.";
        }        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private int GetMaxPaymentID()
        {
            int NewPaymentID = 0;
            try
            {
                var maxTopID = (from max in db.Payments
                                where !String.IsNullOrEmpty(max.PaymentID)
                                select max.PaymentID).Max();
                NewPaymentID = Convert.ToInt32(maxTopID);
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "PaymentController", ex.ToString());
                NewPaymentID = 0;
            }
            return NewPaymentID;
        }
    }
}