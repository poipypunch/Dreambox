﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;

namespace MVCDreambox.Controllers
{
    public class PaymentController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /Payment/

        public ActionResult Index()
        {
            return View(db.Payments.ToList());
        }

        //
        // GET: /Payment/Details/5

        public ActionResult Details(string id = null)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        //
        // GET: /Payment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Payment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentDummy paymentdummy)
        {
            Payment payment = new Payment();
            if (ModelState.IsValid)
            {
                
                int NewID = GetMaxPaymentID() + 1;
                for (int i = 0; i < paymentdummy.Quantity; i++)
                {
                    payment = new Payment();
                    payment.PaymentID = String.Format("{000000000000}", NewID + i);
                    payment.PaymentName = paymentdummy.PaymentName;
                    payment.PaymentStatus = paymentdummy.PaymentStatus;
                    payment.PaymentCost = paymentdummy.PaymentCost;
                    payment.ExpiryDate = DateTime.Now ;
                    payment.PaymentTotalDay = paymentdummy.PaymentTotalDay;
                    payment.CreateBy = Role.Admin;
                    payment.CreateDate = DateTime.Now;
                    payment.UpdateBy = Role.Admin;
                    payment.UpdateDate = DateTime.Now;
                    db.Payments.Add(payment);
                }
                // db.PaymentDummies.Add(paymentdummy);
                //  db.Payments.Add(paymentdummy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentdummy);
        }

        //
        // GET: /Payment/Edit/5

        public ActionResult Edit(string id = null)
        {
            PaymentDummy paymentdummy = db.PaymentDummies.Find(id);
            if (paymentdummy == null)
            {
                return HttpNotFound();
            }
            return View(paymentdummy);
        }

        //
        // POST: /Payment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        //
        // GET: /Payment/Delete/5

        public ActionResult Delete(string id = null)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        //
        // POST: /Payment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
                NewPaymentID = 0;
            }
            return NewPaymentID;
        }
    }
}