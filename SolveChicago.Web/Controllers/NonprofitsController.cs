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
using SolveChicago.Common;
using SolveChicago.Common.Models.Profile.Member;

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
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // GET: Nonprofits
        public ActionResult Index(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            NonprofitService service = new NonprofitService(this.db);
            FamilyEntity[] members = service.GetMembers(State.NonprofitId);
            return View(members.ToList());
        }

        // GET : Nonprofits/CaseManagers
        public ActionResult CaseManagers(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            NonprofitService service = new NonprofitService(this.db);
            CaseManager[] caseManagers = service.GetCaseManagers(State.Nonprofit);
            return View(caseManagers.ToList());
        }

        //GET: Nonprofits/AddMember
        [HttpGet]
        public ActionResult AddMember(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            AddMemberViewModel model = new AddMemberViewModel
            {
                ReferringPartyId = State.NonprofitId
            };
            return View(model);
        }

        // POST: Nonprofits/AddMember
        [HttpPost]
        public ActionResult AddMember(AddMemberViewModel model)
        {
            ImpersonateNonprofit(model.ReferringPartyId);
            if (ModelState.IsValid)
            {
                Member member = db.Members.Find(model.Email);
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
                    Nonprofit nonprofit = db.Nonprofits.Find(State.NonprofitId);
                    if (nonprofit != null)
                        nonprofit.NonprofitMembers.Add(new NonprofitMember { Member = member, Start = DateTime.UtcNow });
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
                service.SendSurveyToMember(member, State.Nonprofit.Name, surveyUrl);
                return NonprofitRedirect(State.NonprofitId);
            }
            return View(model);
        }

        //GET: Nonprofits/AddCaseManager
        [HttpGet]
        public ActionResult AddCaseManager(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            AddCaseManagerViewModel model = new AddCaseManagerViewModel
            {
                NonprofitId = State.NonprofitId
            };
            return View(model);
        }

        // POST: Nonprofits/AddCaseManager
        [HttpPost]
        public ActionResult AddCaseManager(AddCaseManagerViewModel model)
        {
            ImpersonateNonprofit(model.NonprofitId);
            if (ModelState.IsValid)
            {

                CaseManager caseManager = new CaseManager
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    CreatedDate = DateTime.UtcNow,
                    NonprofitId = State.NonprofitId
                };
                db.CaseManagers.Add(caseManager);
                db.SaveChanges();

                CommunicationService service = new CommunicationService(this.db);
                string inviteUrl = string.Format("{0}/Register/CaseManager?id={1}", Settings.Website.BaseUrl, model.NonprofitId);
                service.NonprofitInviteCaseManager(caseManager, State.Nonprofit.Name, inviteUrl);
                return NonprofitRedirect(State.NonprofitId);
            }
            return View(model);
        }

        // POST : Nonprofits/AssignCaseManager
        public ActionResult AssignCaseManager(int? nonprofitId, int memberId)
        {
            ImpersonateNonprofit(nonprofitId);
            NonprofitService service = new NonprofitService(this.db);
            Member member = db.Members.Find(memberId);
            AssignCaseManagerViewModel model = new AssignCaseManagerViewModel
            {
                NonprofitId = State.NonprofitId,
                NonprofitName = State.Nonprofit.Name,
                MemberId = memberId,
                MemberName = string.Format("{0} {1}", member.FirstName, member.LastName),
                CaseManagers = service.GetCaseManagers(State.Nonprofit)
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
