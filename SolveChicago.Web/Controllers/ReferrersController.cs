using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Member;
using SolveChicago.Service;
using SolveChicago.Common;

namespace SolveChicago.Web.Controllers
{
    [Authorize(Roles = "Admin, Referrer")]
    public class ReferrersController : BaseController, IDisposable
    {
        public ReferrersController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public ReferrersController() : base() { }

        public new void Dispose()
        {
            base.Dispose();
        }

        // GET: Referrers
        [HttpGet]
        public ActionResult Index(int? referrerId)
        {
            ImpersonateReferrer(referrerId);
            Referrer referrer = db.Referrers.Find(State.ReferrerId);
            if (referrer == null)
                return HttpNotFound();
            return View(referrer.Members.ToList());
        }

        //GET: Referrers/AddMember
        [HttpGet]
        public ActionResult AddMember(int? referrerId)
        {
            ImpersonateReferrer(referrerId);
            AddMemberViewModel model = new AddMemberViewModel
            {
                ReferringPartyId = State.ReferrerId
            };
            return View(model);
        }

        // POST: Referrers/AddMember
        [HttpPost]
        public ActionResult AddMember(AddMemberViewModel model)
        {
            ImpersonateReferrer(model.ReferringPartyId);
            if (ModelState.IsValid)
            {

                Member member = new Member
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    SurveyStep = Constants.Member.SurveyStep.Invited,
                    CreatedDate = DateTime.UtcNow,
                };
                Referrer referrer = db.Referrers.Find(State.ReferrerId);
                if (referrer != null)
                    referrer.Members.Add(member);
                else
                    db.Members.Add(member);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                CommunicationService service = new CommunicationService(this.db);
                string surveyUrl = string.Format("{0}/Members/CreateProfile?id={1}", Settings.Website.BaseUrl, member.Id);
                service.SendSurveyToMember(member, State.Referrer.Name, surveyUrl);
                return ReferrerRedirect(State.ReferrerId);
            }
            return View(model);
        }

        // GET: Referrers/Details/5
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
