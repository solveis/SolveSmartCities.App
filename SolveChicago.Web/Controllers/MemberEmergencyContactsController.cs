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
    public class MemberEmergencyContactsController : Controller
    {
        private SolveChicagoEntities db = new SolveChicagoEntities();

        // GET: MemberEmergencyContacts
        public ActionResult Index()
        {
            var memberEmergencyContacts = db.MemberEmergencyContacts.Include(m => m.Member);
            return View(memberEmergencyContacts.ToList());
        }

        // GET: MemberEmergencyContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberEmergencyContact memberEmergencyContact = db.MemberEmergencyContacts.Find(id);
            if (memberEmergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(memberEmergencyContact);
        }

        // GET: MemberEmergencyContacts/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email");
            return View();
        }

        // POST: MemberEmergencyContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,Name,Phone,Email")] MemberEmergencyContact memberEmergencyContact)
        {
            if (ModelState.IsValid)
            {
                db.MemberEmergencyContacts.Add(memberEmergencyContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberEmergencyContact.MemberId);
            return View(memberEmergencyContact);
        }

        // GET: MemberEmergencyContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberEmergencyContact memberEmergencyContact = db.MemberEmergencyContacts.Find(id);
            if (memberEmergencyContact == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberEmergencyContact.MemberId);
            return View(memberEmergencyContact);
        }

        // POST: MemberEmergencyContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,Name,Phone,Email")] MemberEmergencyContact memberEmergencyContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberEmergencyContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Email", memberEmergencyContact.MemberId);
            return View(memberEmergencyContact);
        }

        // GET: MemberEmergencyContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberEmergencyContact memberEmergencyContact = db.MemberEmergencyContacts.Find(id);
            if (memberEmergencyContact == null)
            {
                return HttpNotFound();
            }
            return View(memberEmergencyContact);
        }

        // POST: MemberEmergencyContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberEmergencyContact memberEmergencyContact = db.MemberEmergencyContacts.Find(id);
            db.MemberEmergencyContacts.Remove(memberEmergencyContact);
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
