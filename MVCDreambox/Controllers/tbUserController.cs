using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using System.Data.Objects.SqlClient;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class tbUserController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("tbUser", "Login"); } else { return View(); }
        }
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        public JsonResult GetUsersList()
        {
            try
            {
                var userList = (List<tbUser>)db.tbUsers.OrderBy(a => a.UserName).ToList();
                return Json(userList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public string Add(tbUser tbuser)
        {
            try
            {
                if (tbuser != null)
                {
                    if (!IsDuplicate(string.Empty, tbuser.UserName))
                    {
                        tbuser.DealerID = Guid.NewGuid().ToString();
                        tbuser.UpdateBy = Session["UserID"].ToString();
                        tbuser.UpdateDate = DateTime.Now;
                        tbuser.CreateBy = Session["UserID"].ToString();
                        tbuser.CreateDate = DateTime.Now;
                        RSACrypto crypto = new RSACrypto();
                        tbuser.Password = crypto.Encrypt(CommonConstant.DefaultPassword);
                        db.tbUsers.Add(tbuser);
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This username already in used.";
                    }
                }
            }
            catch
            {

            }
            return "Add user failed.";
        }
        public string Update(tbUser tbuser)
        {
            try
            {
                if (tbuser != null)
                {
                    if (!IsDuplicate(tbuser.DealerID, tbuser.UserName))
                    {
                        var user = db.tbUsers.Find(tbuser.DealerID);
                        user.UserName = tbuser.UserName;
                        user.RealName = tbuser.RealName;
                        user.Email = tbuser.RealName;
                        user.Phone = tbuser.Phone;
                        user.Address = tbuser.Address;
                        user.Status = tbuser.Status;
                        user.Role = tbuser.Role;
                        user.UpdateDate = DateTime.Now;
                        user.UpdateBy = Session["UserID"].ToString();
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This username already in used.";
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
                    tbUser tbuser = db.tbUsers.Find(id);
                    db.tbUsers.Remove(tbuser);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed.";
        }

        public string CheckUser(string UserID, string Password)
        {
            try
            {
                if (UserID != string.Empty && Password != string.Empty)
                {

                    tbUser tbuser = db.tbUsers.Where(m => m.UserName == UserID && m.Status==CommonConstant.Status.Active).FirstOrDefault();
                    if (tbuser != null)
                    {
                        RSACrypto crypto = new RSACrypto();
                        if (Password == crypto.Decrypt(tbuser.Password))
                        {
                            Session[CommonConstant.SessionUserID] = tbuser.DealerID;
                            Session[CommonConstant.SessionRole] = tbuser.Role;
                            Session["RealName"] = tbuser.RealName;
                            return "Success";
                        }
                        else {
                            return "Password is incorrect.";
                        }                        
                    }
                    else
                    {
                        return "This username is not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Login failed.";
            }
            return "Login failed.";
        }
        //
        // GET: /SysUser/

        //public ActionResult Index()
        //{
        //    return View(db.tbUsers.ToList());
        //}

        ////
        //// GET: /SysUser/Details/5

        //public ActionResult Details(string id = null)
        //{
        //    tbUser tbuser = db.tbUsers.Find(id);
        //    if (tbuser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbuser);
        //}

        ////
        //// GET: /SysUser/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /SysUser/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(tbUser tbuser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!CheckDuplicate(string.Empty, tbuser.UserName))
        //        {
        //            tbuser.DealerID = Guid.NewGuid().ToString();
        //            tbuser.CreateBy = Session["UserID"].ToString();
        //            tbuser.CreateDate = DateTime.Now;
        //            tbuser.UpdateBy = Session["UserID"].ToString();
        //            db.tbUsers.Add(tbuser);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(tbuser);
        //}

        ////
        //// GET: /SysUser/Edit/5

        //public ActionResult Edit(string id = null)
        //{
        //    tbUser tbuser = db.tbUsers.Find(id);
        //    if (tbuser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbuser);
        //}

        ////
        //// POST: /SysUser/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(tbUser tbuser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!CheckDuplicate(tbuser.DealerID, tbuser.UserName))
        //        {
        //            tbuser.UpdateBy = Session["UserID"].ToString();
        //            tbuser.UpdateDate = DateTime.Now;
        //            db.Entry(tbuser).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(tbuser);
        //}

        ////
        //// GET: /SysUser/Delete/5

        //public ActionResult Delete(string id = null)
        //{
        //    tbUser tbuser = db.tbUsers.Find(id);
        //    if (tbuser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbuser);
        //}

        ////
        //// POST: /SysUser/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    tbUser tbuser = db.tbUsers.Find(id);
        //    db.tbUsers.Remove(tbuser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool IsDuplicate(string id, string strUserName)
        {
            try
            {
                tbUser tbuser;
                tbuser = id != string.Empty ? db.tbUsers.Where(x => x.UserName == strUserName && x.DealerID != id).First() : db.tbUsers.Where(x => x.UserName == strUserName).First();
                return tbuser != null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}