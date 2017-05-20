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
using SolveChicago.Common.Models.Profile.Member;

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
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // GET: Referrers
        [HttpGet]
        public ActionResult Index(int? referrerId)
        {
            ImpersonateReferrer(referrerId);
            Referrer referrer = db.Referrers.Find(State.ReferrerId);
            if (referrer == null)
                return HttpNotFound();

            ReferrerService service = new ReferrerService(this.db);
            FamilyEntity[] members = service.GetMembers(State.ReferrerId);
            return View(members);
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
                Member member = db.Members.SingleOrDefault(x => x.Email == model.Email);
                if (member != null)
                    throw new ApplicationException("That email already associated with an account.");
                else
                {
                    member = new Member
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
                }
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
