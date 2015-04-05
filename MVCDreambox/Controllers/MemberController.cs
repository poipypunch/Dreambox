using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDreambox.Models;
using System.Net;

namespace MVCDreambox.Controllers
{
    public class MemberController : Controller
    {
        private DreamboxContext db = new DreamboxContext();

        public MemberController()
        {
            db = new DreamboxContext();
        }
        //
        // GET: /Member/

        public ActionResult Index(string SelectedDepartment)
        {
            var memberType = db.MemberTypes.OrderBy(q => q.MemberTypeDesc).ToList();
            ViewBag.SelectedDepartment = new SelectList(memberType, "MemberTypeID", "MemberTypeDesc", SelectedDepartment);
            string memberTypeID = SelectedDepartment;

            IQueryable<Member> members = db.Members
                .Where(c => SelectedDepartment == string.Empty || c.MemberTypeID == memberTypeID)
                .OrderBy(d => d.MemberName)
                .Include(d => d.memberType);
            var sql = members.ToString();
            return View(members.ToList());
        }

        //public ActionResult Index()
        //{
        //    var viewModel =
        //   (from m in db.Members
        //    join mt in db.MemberTypes on m.MemberTypeID equals mt.MemberTypeID
        //    orderby m.MemberName, mt.MemberTypeDesc
        //    select new MemberViewModel { member = m, membertype = mt });
        //    return View(viewModel);
        //}

        //
        // GET: /Member/Details/5

        public ActionResult Details(string id = null)
        {

            // Member member = (Member)db.Members.Include(p => p.memberType);
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // GET: /Member/Create

        public ActionResult Create()
        {
            // ViewBag.MemberTypeList = GetMemberTypeList();
            PopulateMembersDropDownList();
            return View();
        }

        //
        // POST: /Member/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                if (!CheckDuplicate(string.Empty, member.UserName))
                {
                    PopulateMembersDropDownList();
                    member.MemberID = Guid.NewGuid().ToString();
                    member.DealerID = Session["UserID"].ToString();
                    member.UpdateBy = Session["UserID"].ToString();
                    member.UpdateDate = DateTime.Now;
                    member.CreateBy = Session["UserID"].ToString();
                    member.CreateDate = DateTime.Now;
                    db.Members.Add(member);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(member);
        }

        //
        // GET: /Member/Edit/5

        public ActionResult Edit(string id = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            PopulateMembersDropDownList(member.MemberTypeID);
            return View(member);
            //Member member = db.Members.Find(id);
            //if (member == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.MemberTypeList = GetMemberTypeList();
            //return View(member);
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            PopulateMembersDropDownList();
            if (ModelState.IsValid)
            {
                if (!CheckDuplicate(member.MemberID, member.UserName))
                {
                                       
                    member.UpdateBy = Session["UserID"].ToString();
                    member.UpdateDate = DateTime.Now;
                    db.Entry(member).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(member);
        }

        //
        // GET: /Member/Delete/5

        public ActionResult Delete(string id = null)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // POST: /Member/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public SelectListItem[] GetMemberTypeList()
        {
            SelectListItem[] MemberTypeList = null;
            try
            {
                MemberTypeList = (from m in db.MemberTypes.OrderBy(m => m.MemberTypeDesc).ToArray()
                                  select new SelectListItem
                                  {
                                      Text = m.MemberTypeDesc,
                                      Value = m.MemberTypeID
                                  }).ToArray();

            }
            catch (Exception ex)
            {

            }
            return MemberTypeList;
        }

        private bool CheckDuplicate(string id, string TypeDesc)
        {
            try
            {
                string UserID = Session["UserID"].ToString();
                Member member;
                member = id != string.Empty ? db.Members.Where(x => x.UserName == TypeDesc && x.DealerID == UserID && x.MemberID != id).First() : db.Members.Where(x => x.UserName == TypeDesc && x.DealerID == UserID).First();
                return member != null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void PopulateMembersDropDownList(object selectedMember = null)
        {
            var membersQuery = from d in db.MemberTypes
                               orderby d.MemberTypeDesc
                               select d;
            ViewBag.MemberTypeID = new SelectList(membersQuery, "MemberTypeID", "MemberTypeDesc", selectedMember);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}