using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using System.Data.SqlClient;
using MVCDreambox.App_Code;
namespace MVCDreambox.Controllers
{
    public class tbUserController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
            //string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
            //var user = db.tbUsers.Where(a => a.DealerID == strUserID).FirstOrDefault();
            //return Json(user, JsonRequestBehavior.AllowGet);
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
                LogFile.writeLogFile(DateTime.Now,"tbUserController", ex.ToString());
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
                        tbuser.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        tbuser.UpdateDate = DateTime.Now;
                        tbuser.CreateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
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
            catch(Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "tbUserController", ex.ToString());
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
                        user.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
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
            catch (Exception ex) { LogFile.writeLogFile(DateTime.Now, "tbUserController", ex.ToString()); }
            return "Update failed";
        }

        public string UpdatePassword(string Password)
        {
            try
            {
                if (Password != string.Empty)
                {
                    string strUserID = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                    var user = db.tbUsers.Find(strUserID);
                    RSACrypto crypto = new RSACrypto();
                    user.Password = crypto.Encrypt(Password);
                    user.UpdateDate = DateTime.Now;
                    user.UpdateBy = strUserID;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex) { LogFile.writeLogFile(DateTime.Now, "tbUserController", ex.ToString()); }
            return "Change password failed";
        }

        public string ResetPassword(string id)
        {
            try
            {
                if (id != string.Empty)
                {
                    RSACrypto crypto = new RSACrypto();
                    tbUser user = db.tbUsers.Find(id);
                    user.Password = crypto.Encrypt(CommonConstant.DefaultPassword);
                    user.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                    user.UpdateDate = DateTime.Now;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "tbUserController", ex.ToString());
            }
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
                LogFile.writeLogFile(DateTime.Now, "tbUserController", ex.ToString());
            }
            return "Delete failed.";
        }

        public string CheckUser(string UserID, string Password)
        {
            try
            {
                if (UserID != string.Empty && Password != string.Empty)
                {

                    tbUser tbuser = db.tbUsers.Where(m => m.UserName == UserID && m.Status == CommonConstant.Status.Active).FirstOrDefault();
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
                        else
                        {
                            return "Password is incorrect.";
                        }
                    }
                    else
                    {
                        LogFile.writeLogFile(DateTime.Now, "tbUserController", "This username is not exist.");
                        return "This username is not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.writeLogFile(DateTime.Now, "tbUserController", ex.ToString());
                //return ex.Message.ToString();
            }
            return "Login failed.";
        }
        
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
                LogFile.writeLogFile(DateTime.Now, "tbUserController", ex.ToString());
                return false;
            }
        }
    }
}