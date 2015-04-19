//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using MVCDreambox.Models;

//namespace MVCDreambox.Controllers
//{
//    public class ChannelController : Controller
//    {
//        private DreamboxContext db = new DreamboxContext();

//        //
//        // GET: /Channel/

//        public ActionResult Index()
//        {
//            return View(db.Channels.ToList());
//        }

//        //
//        // GET: /Channel/Details/5

//        public ActionResult Details(string id = null)
//        {
//            Channel channel = db.Channels.Find(id);
//            if (channel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(channel);
//        }

//        //
//        // GET: /Channel/Create

//        public ActionResult Create()
//        {
//            return View();
//        }

//        //
//        // POST: /Channel/Create

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(Channel channel)
//        {
//            if (ModelState.IsValid)
//            {
//                if (!CheckDuplicate(string.Empty, channel.ChannelDesc))
//                {
//                    channel.ChannelID = Guid.NewGuid().ToString();
//                    channel.UpdateBy = Session["UserID"].ToString();
//                    channel.UpdateDate = DateTime.Now;
//                    channel.CreateBy = Session["UserID"].ToString();
//                    channel.CreateDate = DateTime.Now;
//                    db.Channels.Add(channel);
//                    db.SaveChanges();
//                    return RedirectToAction("Index");
//                }
//            }

//            return View(channel);
//        }

//        //
//        // GET: /Channel/Edit/5

//        public ActionResult Edit(string id = null)
//        {
//            Channel channel = db.Channels.Find(id);
//            if (channel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(channel);
//        }

//        //
//        // POST: /Channel/Edit/5

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(Channel channel)
//        {
//            if (ModelState.IsValid)
//            {
//                if (!CheckDuplicate(channel.ChannelID, channel.ChannelDesc))
//                {
//                    channel.UpdateBy = Session["UserID"].ToString();
//                    channel.UpdateDate = DateTime.Now;
//                    db.Entry(channel).State = EntityState.Modified;
//                    db.SaveChanges();
//                    return RedirectToAction("Index");
//                }
//            }
//            return View(channel);
//        }

//        //
//        // GET: /Channel/Delete/5

//        public ActionResult Delete(string id = null)
//        {
//            Channel channel = db.Channels.Find(id);
//            if (channel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(channel);
//        }

//        //
//        // POST: /Channel/Delete/5

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(string id)
//        {
//            Channel channel = db.Channels.Find(id);
//            db.Channels.Remove(channel);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        private bool CheckDuplicate(string id, string TypeDesc)
//        {
//            try
//            {
//                Channel channel;
//                channel = id == string.Empty ? db.Channels.Where(x => x.ChannelDesc == TypeDesc && x.ChannelID != id).First() : db.Channels.Where(x => x.ChannelDesc == TypeDesc).First();
//                return channel != null ? true : false;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        protected override void Dispose(bool disposing)
//        {
//            db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}
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
           // return View(db.Channels.ToList());
            return View();
        }

        public JsonResult GetAllChannels()
        {
            try
            {
                var channelList = (List<Channel>)db.Channels.OrderBy(a=>a.ChannelDesc).ToList();
                return Json(channelList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                string str = ex.Message.ToString();
            }
            return null;
        }

        public string Update(Channel channel)
        {
            if (channel != null)
            {
                var chan = db.Channels.Find(channel.ChannelID);
                chan.ChannelDesc = channel.ChannelDesc;
                chan.ChannelPath = channel.ChannelPath;
                chan.ChannelStatus = channel.ChannelStatus;
                chan.UpdateDate = DateTime.Now;
                chan.UpdateBy = "Admin";
                db.Entry(chan).State = EntityState.Modified;
                db.SaveChanges();
                return "Record has been Updated";
            }
            else
            {
                return "Record has Not been Updated";
            }
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
                    return "Channel Has Been Deleted";
                }
                else
                {
                    return "Channel Hasnot Been Deleted";
                }
            }
            catch
            {
                return "Channel Hasnot Been Deleted";
            }
        }

        public string Add(Channel channel)
        {
            try
            {
                if (channel != null)
                {
                    channel.ChannelID = Guid.NewGuid().ToString();
                    channel.UpdateBy = Session["UserID"].ToString();
                    channel.UpdateDate = DateTime.Now;
                    channel.CreateBy = Session["UserID"].ToString();
                    channel.CreateDate = DateTime.Now;
                    db.Channels.Add(channel);
                    db.SaveChanges();
                    return "Record has been Added";
                }
                else
                {
                    return "Record has Not been Verified";
                }
            }

            catch
            {
                return "Record has Not been Added";
            }
        } 

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}