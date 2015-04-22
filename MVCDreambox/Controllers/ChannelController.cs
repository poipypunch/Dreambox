using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;

namespace MVCDreambox.Controllers
{
    public class ChannelController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        //
        // GET: /Channel/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetChannelsList()
        {
            try
            {
                var channelList = (List<Channel>)db.Channels.OrderBy(a => a.ChannelDesc).ToList();
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
                    if (!IsDuplicate(string.Empty, channel.ChannelDesc))
                    {
                        channel.ChannelID = Guid.NewGuid().ToString();
                        channel.UpdateBy = Session["UserID"].ToString();
                        channel.UpdateDate = DateTime.Now;
                        channel.CreateBy = Session["UserID"].ToString();
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
                    if (!IsDuplicate(channel.ChannelID, channel.ChannelDesc))
                    {
                        var chan = db.Channels.Find(channel.ChannelID);
                        chan.ChannelDesc = channel.ChannelDesc;
                        chan.ChannelPath = channel.ChannelPath;
                        chan.ChannelStatus = channel.ChannelStatus;
                        chan.UpdateDate = DateTime.Now;
                        chan.UpdateBy = Session["UserID"].ToString();
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
                channel = id != string.Empty ? db.Channels.Where(x => x.ChannelDesc == strChannel && x.ChannelID != id).First() : db.Channels.Where(x => x.ChannelDesc == strChannel).First();
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