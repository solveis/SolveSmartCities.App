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
    public class ReferrersController : Controller
    {
        private SolveChicagoEntities db = new SolveChicagoEntities();

        // GET: Referrers
        public ActionResult Index()
        {
            return View(db.Referrers.ToList());
        }

        // GET: Referrers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referrer Referrer = db.Referrers.Find(id);
            if (Referrer == null)
            {
                return HttpNotFound();
            }
            return View(Referrer);
        }

        // GET: Referrers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Referrers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Name,Phone,CreatedDate")] Referrer Referrer)
        {
            if (ModelState.IsValid)
            {
                db.Referrers.Add(Referrer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Referrer);
        }

        // GET: Referrers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referrer Referrer = db.Referrers.Find(id);
            if (Referrer == null)
            {
                return HttpNotFound();
            }
            return View(Referrer);
        }

        // POST: Referrers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Name,Phone,CreatedDate")] Referrer Referrer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Referrer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Referrer);
        }

        // GET: Referrers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referrer Referrer = db.Referrers.Find(id);
            if (Referrer == null)
            {
                return HttpNotFound();
            }
            return View(Referrer);
        }

        // POST: Referrers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Referrer Referrer = db.Referrers.Find(id);
            db.Referrers.Remove(Referrer);
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
