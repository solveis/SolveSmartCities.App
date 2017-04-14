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
    public class Members1Controller : Controller
    {
        private SolveChicagoEntities db = new SolveChicagoEntities();

        // GET: Members1
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.Family);
            return View(members.ToList());
        }

        // GET: Members1/Details/5
        public ActionResult Details(int? id)
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
            return View(member);
        }

        // GET: Members1/Create
        public ActionResult Create()
        {
            ViewBag.FamilyId = new SelectList(db.Families, "Id", "FamilyName");
            return View();
        }

        // POST: Members1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,FirstName,LastName,ProfilePicturePath,CreatedDate,Gender,Birthday,FamilyId,IsHeadOfHousehold,Income,IsDisabled,SurveyStep")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FamilyId = new SelectList(db.Families, "Id", "FamilyName", member.FamilyId);
            return View(member);
        }

        // GET: Members1/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.FamilyId = new SelectList(db.Families, "Id", "FamilyName", member.FamilyId);
            return View(member);
        }

        // POST: Members1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,LastName,ProfilePicturePath,CreatedDate,Gender,Birthday,FamilyId,IsHeadOfHousehold,Income,IsDisabled,SurveyStep")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamilyId = new SelectList(db.Families, "Id", "FamilyName", member.FamilyId);
            return View(member);
        }

        // GET: Members1/Delete/5
        public ActionResult Delete(int? id)
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
            return View(member);
        }

        // POST: Members1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
