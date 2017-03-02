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
    public class CorporationsController : BaseController
    {
        // GET: Corporations
        public ActionResult Index()
        {
            return View(db.Corporations.ToList());
        }

        // GET: Corporations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corporation corporation = db.Corporations.Find(id);
            if (corporation == null)
            {
                return HttpNotFound();
            }
            return View(corporation);
        }

        // GET: Corporations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Corporations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Name,CreatedDate")] Corporation corporation)
        {
            if (ModelState.IsValid)
            {
                db.Corporations.Add(corporation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(corporation);
        }

        // GET: Corporations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corporation corporation = db.Corporations.Find(id);
            if (corporation == null)
            {
                return HttpNotFound();
            }
            return View(corporation);
        }

        // POST: Corporations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Name,CreatedDate")] Corporation corporation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corporation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corporation);
        }

        // GET: Corporations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corporation corporation = db.Corporations.Find(id);
            if (corporation == null)
            {
                return HttpNotFound();
            }
            return View(corporation);
        }

        // POST: Corporations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Corporation corporation = db.Corporations.Find(id);
            db.Corporations.Remove(corporation);
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
