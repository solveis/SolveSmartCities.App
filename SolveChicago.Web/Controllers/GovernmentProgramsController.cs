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
    public class GovernmentProgramsController : BaseController, IDisposable
    {
        public GovernmentProgramsController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        
        public new void Dispose()
        {
            base.Dispose();
        }

        // GET: GovernmentPrograms
        public ActionResult Index()
        {
            return View(db.GovernmentPrograms.ToList());
        }

        // GET: GovernmentPrograms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GovernmentProgram governmentProgram = db.GovernmentPrograms.Find(id);
            if (governmentProgram == null)
            {
                return HttpNotFound();
            }
            return View(governmentProgram);
        }

        // GET: GovernmentPrograms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GovernmentPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] GovernmentProgram governmentProgram)
        {
            if (ModelState.IsValid)
            {
                db.GovernmentPrograms.Add(governmentProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(governmentProgram);
        }

        // GET: GovernmentPrograms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GovernmentProgram governmentProgram = db.GovernmentPrograms.Find(id);
            if (governmentProgram == null)
            {
                return HttpNotFound();
            }
            return View(governmentProgram);
        }

        // POST: GovernmentPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] GovernmentProgram governmentProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(governmentProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(governmentProgram);
        }

        // GET: GovernmentPrograms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GovernmentProgram governmentProgram = db.GovernmentPrograms.Find(id);
            if (governmentProgram == null)
            {
                return HttpNotFound();
            }
            return View(governmentProgram);
        }

        // POST: GovernmentPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GovernmentProgram governmentProgram = db.GovernmentPrograms.Find(id);
            db.GovernmentPrograms.Remove(governmentProgram);
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
