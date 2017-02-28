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
    public class MemberChildrensController : BaseController
    {
        public MemberChildrensController(SolveChicagoEntities db) : base(db) { }
        public MemberChildrensController() : base() { }

        // GET: MemberChildrens
        public ActionResult Index()
        {
            var memberChildrens = db.MemberChildrens.Include(m => m.Member);
            return View(memberChildrens.ToList());
        }

        // GET: MemberChildrens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberChildren memberChildren = db.MemberChildrens.Find(id);
            if (memberChildren == null)
            {
                return HttpNotFound();
            }
            return View(memberChildren);
        }

        // GET: MemberChildrens/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email");
            return View();
        }

        // POST: MemberChildrens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MemberId,BirthDate")] MemberChildren memberChildren)
        {
            if (ModelState.IsValid)
            {
                db.MemberChildrens.Add(memberChildren);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberChildren.MemberId);
            return View(memberChildren);
        }

        // GET: MemberChildrens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberChildren memberChildren = db.MemberChildrens.Find(id);
            if (memberChildren == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberChildren.MemberId);
            return View(memberChildren);
        }

        // POST: MemberChildrens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MemberId,BirthDate")] MemberChildren memberChildren)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberChildren).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberChildren.MemberId);
            return View(memberChildren);
        }

        // GET: MemberChildrens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberChildren memberChildren = db.MemberChildrens.Find(id);
            if (memberChildren == null)
            {
                return HttpNotFound();
            }
            return View(memberChildren);
        }

        // POST: MemberChildrens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberChildren memberChildren = db.MemberChildrens.Find(id);
            db.MemberChildrens.Remove(memberChildren);
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
