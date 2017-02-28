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
    public class CaseNotesController : BaseController
    {
        public CaseNotesController(SolveChicagoEntities db) : base(db) { }
        public CaseNotesController() : base() { }

        // GET: CaseNotes
        public ActionResult Index()
        {
            var caseNotes = db.CaseNotes.Include(c => c.CaseManager).Include(c => c.Member).Include(c => c.Outcome);
            return View(caseNotes.ToList());
        }

        // GET: CaseNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseNote caseNote = db.CaseNotes.Find(id);
            if (caseNote == null)
            {
                return HttpNotFound();
            }
            return View(caseNote);
        }

        // GET: CaseNotes/Create
        public ActionResult Create()
        {
            ViewBag.CaseManagerId = new SelectList(db.CaseManagers, "Id", "Email");
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email");
            ViewBag.OutcomeId = new SelectList(db.Outcomes, "Id", "Name");
            return View();
        }

        // POST: CaseNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,CaseManagerId,CreatedDate,Note,OutcomeId,OutcomeWeight")] CaseNote caseNote)
        {
            if (ModelState.IsValid)
            {
                db.CaseNotes.Add(caseNote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CaseManagerId = new SelectList(db.CaseManagers, "Id", "Email", caseNote.CaseManagerId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", caseNote.MemberId);
            ViewBag.OutcomeId = new SelectList(db.Outcomes, "Id", "Name", caseNote.OutcomeId);
            return View(caseNote);
        }

        // GET: CaseNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseNote caseNote = db.CaseNotes.Find(id);
            if (caseNote == null)
            {
                return HttpNotFound();
            }
            ViewBag.CaseManagerId = new SelectList(db.CaseManagers, "Id", "Email", caseNote.CaseManagerId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", caseNote.MemberId);
            ViewBag.OutcomeId = new SelectList(db.Outcomes, "Id", "Name", caseNote.OutcomeId);
            return View(caseNote);
        }

        // POST: CaseNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,CaseManagerId,CreatedDate,Note,OutcomeId,OutcomeWeight")] CaseNote caseNote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caseNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CaseManagerId = new SelectList(db.CaseManagers, "Id", "Email", caseNote.CaseManagerId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", caseNote.MemberId);
            ViewBag.OutcomeId = new SelectList(db.Outcomes, "Id", "Name", caseNote.OutcomeId);
            return View(caseNote);
        }

        // GET: CaseNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseNote caseNote = db.CaseNotes.Find(id);
            if (caseNote == null)
            {
                return HttpNotFound();
            }
            return View(caseNote);
        }

        // POST: CaseNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseNote caseNote = db.CaseNotes.Find(id);
            db.CaseNotes.Remove(caseNote);
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
