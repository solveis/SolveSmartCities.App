using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Web.Data;

namespace SolveChicago.Web.Controllers
{
    public class MemberStatusController : Controller
    {
        private SolveChicagoEntities db = new SolveChicagoEntities();

        // GET: MemberStatus
        public ActionResult Index()
        {
            var memberStatuses = db.MemberStatuses.Include(m => m.Member);
            return View(memberStatuses.ToList());
        }

        // GET: MemberStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberStatus memberStatus = db.MemberStatuses.Find(id);
            if (memberStatus == null)
            {
                return HttpNotFound();
            }
            return View(memberStatus);
        }

        // GET: MemberStatus/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email");
            return View();
        }

        // POST: MemberStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,Status,CreatedDate")] MemberStatus memberStatus)
        {
            if (ModelState.IsValid)
            {
                db.MemberStatuses.Add(memberStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberStatus.MemberId);
            return View(memberStatus);
        }

        // GET: MemberStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberStatus memberStatus = db.MemberStatuses.Find(id);
            if (memberStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberStatus.MemberId);
            return View(memberStatus);
        }

        // POST: MemberStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,Status,CreatedDate")] MemberStatus memberStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberStatus.MemberId);
            return View(memberStatus);
        }

        // GET: MemberStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberStatus memberStatus = db.MemberStatuses.Find(id);
            if (memberStatus == null)
            {
                return HttpNotFound();
            }
            return View(memberStatus);
        }

        // POST: MemberStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberStatus memberStatus = db.MemberStatuses.Find(id);
            db.MemberStatuses.Remove(memberStatus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
