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
using SolveChicago.Web.Models.Nonprofit;
using SolveChicago.Web.Models.Member;

namespace SolveChicago.Web.Controllers
{
    public class NonprofitsController : BaseController, IDisposable
    {
        public NonprofitsController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public NonprofitsController() : base() { }

        public new void Dispose()
        {
            base.Dispose();
        }

        // GET: Nonprofits
        public ActionResult Index(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            NonprofitService service = new NonprofitService(this.db);
            CaseManager[] caseManagers = service.GetCaseManagers(State.NonprofitId);
            return View(caseManagers.ToList());
        }

        // GET : Nonprofits/Members
        public ActionResult Members(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            NonprofitService service = new NonprofitService(this.db);
            Member[] members = service.GetMembers(State.NonprofitId);
            return View(members.ToList());
        }

        // GET : Nonprofits/AssignCaseManager
        public ActionResult AssignCaseManager(int? nonprofitId, int memberId)
        {
            ImpersonateNonprofit(nonprofitId);
            NonprofitService service = new NonprofitService(this.db);
            AssignCaseManagerViewModel model = new AssignCaseManagerViewModel
            {
                NonprofitId = State.NonprofitId,
                MemberId = memberId,
                CaseManagers = service.GetCaseManagers(State.NonprofitId)
            };
            return View(model);
        }

        // POST : Nonprofits/AssignCaseManager
        [HttpPost]
        public ActionResult AssignCaseManager(AssignCaseManagerViewModel model)
        {
            if(ModelState.IsValid)
            {
                ImpersonateNonprofit(model.NonprofitId);
                NonprofitService service = new NonprofitService(this.db);
                service.AssignCaseManager(model.NonprofitId, model.MemberId, model.CaseManagerId.Value);
                return NonprofitRedirect(State.NonprofitId);
            }
            return View(model);
        }

        // GET: Nonprofits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nonprofit nonprofit = db.Nonprofits.Find(id);
            if (nonprofit == null)
            {
                return HttpNotFound();
            }
            return View(nonprofit);
        }

        // GET: Nonprofits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nonprofits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Name,Phone,Address1,Address2,City,Province,Country,CreatedDate")] Nonprofit nonprofit)
        {
            if (ModelState.IsValid)
            {
                db.Nonprofits.Add(nonprofit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nonprofit);
        }

        // GET: Nonprofits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Nonprofit", "Profile", new { id = id });
        }

        // GET: Nonprofits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nonprofit nonprofit = db.Nonprofits.Find(id);
            if (nonprofit == null)
            {
                return HttpNotFound();
            }
            return View(nonprofit);
        }

        // POST: Nonprofits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nonprofit nonprofit = db.Nonprofits.Find(id);
            db.Nonprofits.Remove(nonprofit);
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
