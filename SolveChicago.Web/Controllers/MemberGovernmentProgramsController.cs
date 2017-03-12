using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Entities;

namespace SolveChicago.Web.Controllers
{
    public class MemberGovernmentProgramsController : BaseController, IDisposable
    {
        public MemberGovernmentProgramsController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public MemberGovernmentProgramsController() : base() { }

        public new void Dispose()
        {
            base.Dispose();
        }

        // GET: MemberGovernmentPrograms
        public ActionResult Index()
        {
            var memberGovernmentPrograms = db.MemberGovernmentPrograms.Include(m => m.GovernmentProgram).Include(m => m.Member);
            return View(memberGovernmentPrograms.ToList());
        }

        // GET: MemberGovernmentPrograms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberGovernmentProgram memberGovernmentProgram = db.MemberGovernmentPrograms.Find(id);
            if (memberGovernmentProgram == null)
            {
                return HttpNotFound();
            }
            return View(memberGovernmentProgram);
        }

        // GET: MemberGovernmentPrograms/Create
        public ActionResult Create()
        {
            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name");
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email");
            return View();
        }

        // POST: MemberGovernmentPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,GovernmentProgramId,Start,End")] MemberGovernmentProgram memberGovernmentProgram)
        {
            if (ModelState.IsValid)
            {
                db.MemberGovernmentPrograms.Add(memberGovernmentProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name", memberGovernmentProgram.GovernmentProgramId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberGovernmentProgram.MemberId);
            return View(memberGovernmentProgram);
        }

        // GET: MemberGovernmentPrograms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberGovernmentProgram memberGovernmentProgram = db.MemberGovernmentPrograms.Find(id);
            if (memberGovernmentProgram == null)
            {
                return HttpNotFound();
            }
            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name", memberGovernmentProgram.GovernmentProgramId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberGovernmentProgram.MemberId);
            return View(memberGovernmentProgram);
        }

        // POST: MemberGovernmentPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,GovernmentProgramId,Start,End")] MemberGovernmentProgram memberGovernmentProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberGovernmentProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GovernmentProgramId = new SelectList(db.GovernmentPrograms, "Id", "Name", memberGovernmentProgram.GovernmentProgramId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberGovernmentProgram.MemberId);
            return View(memberGovernmentProgram);
        }

        // GET: MemberGovernmentPrograms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberGovernmentProgram memberGovernmentProgram = db.MemberGovernmentPrograms.Find(id);
            if (memberGovernmentProgram == null)
            {
                return HttpNotFound();
            }
            return View(memberGovernmentProgram);
        }

        // POST: MemberGovernmentPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberGovernmentProgram memberGovernmentProgram = db.MemberGovernmentPrograms.Find(id);
            db.MemberGovernmentPrograms.Remove(memberGovernmentProgram);
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
