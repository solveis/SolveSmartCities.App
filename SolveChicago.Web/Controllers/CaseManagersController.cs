using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Entities;
using SolveChicago.Service;
using SolveChicago.Common.Models.Profile.Member;

namespace SolveChicago.Web.Controllers
{
    [Authorize(Roles = "Admin, Nonprofit, CaseManager")]
    public class CaseManagersController : BaseController, IDisposable
    {
        public CaseManagersController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public CaseManagersController() : base() { }
        public new void Dispose()
        {
            base.Dispose();
        }
        
        // GET: CaseManagers
        public ActionResult Index(int? caseManagerId)
        {
            ImpersonateCaseManager(caseManagerId);
            CaseManagerService service = new CaseManagerService(this.db);
            
                FamilyEntity[] caseManagers = service.GetFamiliesForCaseManager(State.CaseManagerId);
                return View(caseManagers.ToList());
        }

        // GET: CaseManagers/AddCaseNote
        public ActionResult AddCaseNote(int? caseManagerId, int memberId)
        {
            ImpersonateCaseManager(caseManagerId);
            CaseNote model = new CaseNote() { CaseManagerId = State.CaseManagerId, MemberId = memberId };
            return View(model);
        }

        // POST: CaseManagers/AddCaseNote
        [HttpPost]
        public ActionResult AddCaseNote(CaseNote caseNote)
        {
            ImpersonateCaseManager(caseNote.CaseManagerId);
            if(ModelState.IsValid)
            {
                caseNote.CreatedDate = DateTime.UtcNow;
                db.CaseNotes.Add(caseNote);
                db.SaveChanges();
               return CaseManagerRedirect(State.CaseManagerId);
            }
            return View(caseNote);
        }
        
        // GET: CaseManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseManager caseManager = db.CaseManagers.Find(id);
            if (caseManager == null)
            {
                return HttpNotFound();
            }
            return View(caseManager);
        }

        // GET: CaseManagers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CaseManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,FirstName,LastName")] CaseManager caseManager)
        {
            if (ModelState.IsValid)
            {
                caseManager.CreatedDate = DateTime.UtcNow;
                db.Nonprofits.Single(x => x.Id == State.NonprofitId).CaseManagers.Add(caseManager);
                db.SaveChanges();
                return UserRedirect();
            }

            return View(caseManager);
        }

        // GET: CaseManagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("CaseManager", "Profile", new { id = id });
        }

        // GET: CaseManagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseManager caseManager = db.CaseManagers.Find(id);
            if (caseManager == null)
            {
                return HttpNotFound();
            }
            return View(caseManager);
        }

        // POST: CaseManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaseManager caseManager = db.CaseManagers.Find(id);
            db.CaseManagers.Remove(caseManager);
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
