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
    public class ChannelController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /Channel/

        public ActionResult Index()
        {
            if (Session[CommonConstant.SessionUserID] == null) { return RedirectToAction("Login", "tbUser"); } else { return View(); }
        }

        public JsonResult GetChannelsList()
        {
            try
            {
                string strUserID=CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                var channelList = (List<Channel>)db.Channels.Where(m => m.ChannelStatus == CommonConstant.Status.Active || m.CreateBy == strUserID).OrderBy(a => a.ChannelName).ToList();
                return Json(channelList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public string Add(Channel channel)
        {
            try
            {
                if (channel != null)
                {
                    if (!IsDuplicate(string.Empty, channel.ChannelName))
                    {
                        channel.ChannelID = Guid.NewGuid().ToString();
                        channel.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        channel.UpdateDate = DateTime.Now;
                        channel.CreateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        channel.CreateDate = DateTime.Now;
                        db.Channels.Add(channel);
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This channel name already in used.";
                    }
                }
            }
            catch
            {

            }
            return "Add channel failed.";
        }
        public string Update(Channel channel)
        {
            try
            {
                if (channel != null)
                {
                    if (!IsDuplicate(channel.ChannelID, channel.ChannelName))
                    {
                        var chan = db.Channels.Find(channel.ChannelID);
                        chan.ChannelName = channel.ChannelName;
                        chan.iOSUrl = channel.iOSUrl;
                        chan.BrowserUrl = channel.BrowserUrl;
                        chan.AndroidUrl = channel.AndroidUrl;
                        chan.ChannelStatus = channel.ChannelStatus;
                        chan.UpdateDate = DateTime.Now;
                        chan.UpdateBy = CommonConstant.GetFieldValueString(Session[CommonConstant.SessionUserID]);
                        db.Entry(chan).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "This channel name already in used.";
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
                    Channel channel = db.Channels.Find(id);
                    db.Channels.Remove(channel);
                    db.SaveChanges();
                    return "Delete success.";
                }
            }
            catch (Exception ex)
            {

            }
            return "Delete failed.";
        }

        private bool IsDuplicate(string id, string strChannel)
        {
            try
            {
                Channel channel;
                channel = id != string.Empty ? db.Channels.Where(x => x.ChannelName == strChannel && x.ChannelID != id).First() : db.Channels.Where(x => x.ChannelName == strChannel).First();
                return channel != null ? true : false;
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