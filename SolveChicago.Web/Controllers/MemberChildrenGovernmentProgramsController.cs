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
    public class MemberChildrenGovernmentProgramsController : BaseController
    {
        public MemberChildrenGovernmentProgramsController(SolveChicagoEntities db) : base(db) { }
        public MemberChildrenGovernmentProgramsController() : base() { }

        // GET: MemberChildrenGovernmentPrograms
        public ActionResult Index()
        {
            var memberChildrenGovernmentPrograms = db.MemberChildrenGovernmentPrograms.Include(m => m.GovernmentProgram).Include(m => m.MemberChildren).Include(m => m.Member);
            return View(memberChildrenGovernmentPrograms.ToList());
        }

        // GET: MemberChildrenGovernmentPrograms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberChildrenGovernmentProgram memberChildrenGovernmentProgram = db.MemberChildrenGovernmentPrograms.Find(id);
            if (memberChildrenGovernmentProgram == null)
            {
                return HttpNotFound();
            }
            return View(memberChildrenGovernmentProgram);
        }

        // GET: MemberChildrenGovernmentPrograms/Create
        public ActionResult Create()
        {
            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name");
            ViewBag.MemberChildrenId = new SelectList(db.MemberChildrens, "Id", "Name");
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email");
            return View();
        }

        // POST: MemberChildrenGovernmentPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,MemberChildrenId,GovernmentProgramId,Start,End")] MemberChildrenGovernmentProgram memberChildrenGovernmentProgram)
        {
            if (ModelState.IsValid)
            {
                db.MemberChildrenGovernmentPrograms.Add(memberChildrenGovernmentProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name", memberChildrenGovernmentProgram.GovernmentProgramId);
            ViewBag.MemberChildrenId = new SelectList(db.MemberChildrens, "Id", "Name", memberChildrenGovernmentProgram.MemberChildrenId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberChildrenGovernmentProgram.MemberId);
            return View(memberChildrenGovernmentProgram);
        }

        // GET: MemberChildrenGovernmentPrograms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberChildrenGovernmentProgram memberChildrenGovernmentProgram = db.MemberChildrenGovernmentPrograms.Find(id);
            if (memberChildrenGovernmentProgram == null)
            {
                return HttpNotFound();
            }
            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name", memberChildrenGovernmentProgram.GovernmentProgramId);
            ViewBag.MemberChildrenId = new SelectList(db.MemberChildrens, "Id", "Name", memberChildrenGovernmentProgram.MemberChildrenId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberChildrenGovernmentProgram.MemberId);
            return View(memberChildrenGovernmentProgram);
        }

        // POST: MemberChildrenGovernmentPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,MemberChildrenId,GovernmentProgramId,Start,End")] MemberChildrenGovernmentProgram memberChildrenGovernmentProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberChildrenGovernmentProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name", memberChildrenGovernmentProgram.GovernmentProgramId);
            ViewBag.MemberChildrenId = new SelectList(db.MemberChildrens, "Id", "Name", memberChildrenGovernmentProgram.MemberChildrenId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberChildrenGovernmentProgram.MemberId);
            return View(memberChildrenGovernmentProgram);
        }

        // GET: MemberChildrenGovernmentPrograms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberChildrenGovernmentProgram memberChildrenGovernmentProgram = db.MemberChildrenGovernmentPrograms.Find(id);
            if (memberChildrenGovernmentProgram == null)
            {
                return HttpNotFound();
            }
            return View(memberChildrenGovernmentProgram);
        }

        // POST: MemberChildrenGovernmentPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberChildrenGovernmentProgram memberChildrenGovernmentProgram = db.MemberChildrenGovernmentPrograms.Find(id);
            db.MemberChildrenGovernmentPrograms.Remove(memberChildrenGovernmentProgram);
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
