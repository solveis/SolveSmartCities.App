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
using SolveChicago.Common.Models;

namespace SolveChicago.Web.Controllers
{
    [Authorize(Roles = "Admin, Nonprofit, CaseManager")]
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
                Member member = db.Members.SingleOrDefault(x => x.Email == model.Email);
                MemberService mService = new MemberService(this.db);
                if (member != null && !mService.MemberExists(model.FirstName, null, model.LastName, null, null, null, null, null, null, null, null, model.Email))
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

                    // add soft skills as a desired skill for pipeline
                    // TODO refactor this into a stored proc
                    Skill softSkills = db.Skills.SingleOrDefault(x => x.Name == Common.Constants.Skills.SoftSkills);
                    if (softSkills == null)
                        softSkills = new Skill { Name = Common.Constants.Skills.SoftSkills };
                    member.MemberSkills.Add(new MemberSkill { IsComplete = false, Skill = softSkills, NonprofitId = State.NonprofitId });
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
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
                return RedirectToAction("CaseManagers", new { nonprofitId = State.NonprofitId });
            }
            return View(model);
        }

        // GET : Nonprofits/AssignCaseManager
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



        // GET : Nonprofits/JobPlaced
        public ActionResult JobPlaced(int? nonprofitId, int memberId)
        {
            ImpersonateNonprofit(nonprofitId);
            Skill[] npoSkills = State.Nonprofit.Skills.ToArray();
            GraduateMemberViewModel model = new GraduateMemberViewModel
            {
                MemberId = memberId,
                NonprofitId = State.NonprofitId,
                Skills = npoSkills.Select(x => new GraduateMemberCheckbox { Id = x.Id, Name = x.Name, IsComplete = false }).ToArray(),
            };
            return View(model);
        }

        // POST : Nonprofits/JobPlaced
        [HttpPost]
        public ActionResult JobPlaced(GraduateMemberViewModel model)
        {
            ImpersonateNonprofit(model.NonprofitId);
            Member member = db.Members.Single(x => x.Id == model.MemberId);
            NonprofitMember nonprofitMember = member.NonprofitMembers.Single(x => x.NonprofitId == State.NonprofitId);
            Corporation corporation = null; // predefine so we can grab the Id later.
            nonprofitMember.End = DateTime.UtcNow;
            if (model.Skills != null)
            {
                foreach (var ms in model.Skills)
                {
                    MemberSkill memberSkill = member.MemberSkills.Where(x => x.SkillId == ms.Id).FirstOrDefault();
                    if (memberSkill != null)
                    {
                        memberSkill.IsComplete = ms.IsComplete;
                        memberSkill.NonprofitId = memberSkill.NonprofitId ?? State.NonprofitId; // only set this if it's null, lest npos overwrite previous npos skills
                    }   
                    else
                    {
                        if(ms.IsComplete)
                            member.MemberSkills.Add(new MemberSkill { IsComplete = ms.IsComplete, MemberId = member.Id, NonprofitId = State.NonprofitId, SkillId = ms.Id });
                    }   
                }
            }
            if (!string.IsNullOrEmpty(model.JobName))
            {
                corporation = db.Corporations.Where(x => x.Name == model.JobName).FirstOrDefault();
                if(corporation == null)
                {
                    corporation = new Corporation
                    {
                        Name = model.JobName,
                        CreatedDate = DateTime.UtcNow,
                    };
                }
                member.MemberCorporations.Add(new MemberCorporation
                {
                    Corporation = corporation,
                    Pay = model.JobPay,
                    Start = model.Start ?? DateTime.UtcNow,
                    NonprofitId = model.NonprofitId
                });
            }

            db.SaveChanges();

            if (corporation != null)
            {
                CommunicationService service = new CommunicationService(this.db);
                string confirmUrl = $"{Settings.Website.BaseUrl}/Members/ConfirmJob?memberId={member.Id}&nonprofitId={State.NonprofitId}&corporationId={corporation.Id}";
                service.JobPlacedVerification(model.JobName, member, State.Nonprofit.Name, confirmUrl);
            }

            return NonprofitRedirect(State.NonprofitId);
        }

        // GET : Nonprofits/NonprofitReferral
        public ActionResult NonprofitReferral(int? nonprofitId, int memberId)
        {
            ImpersonateNonprofit(nonprofitId);
            Skill[] npoSkills = State.Nonprofit.Skills.ToArray();
            GraduateMemberViewModel model = new GraduateMemberViewModel
            {
                MemberId = memberId,
                NonprofitId = State.NonprofitId,
                Skills = npoSkills.Select(x => new GraduateMemberCheckbox { Id = x.Id, Name = x.Name, IsComplete = false }).ToArray(),
            };
            return View(model);
        }

        // POST : Nonprofits/NonprofitReferral
        [HttpPost]
        public ActionResult NonprofitReferral(GraduateMemberViewModel model)
        {
            ImpersonateNonprofit(model.NonprofitId);
            Member member = db.Members.Single(x => x.Id == model.MemberId);
            NonprofitMember nonprofitMember = member.NonprofitMembers.Single(x => x.NonprofitId == State.NonprofitId);
            nonprofitMember.End = DateTime.UtcNow;
            if (model.Skills != null)
            {
                foreach (var ms in model.Skills)
                {
                    MemberSkill memberSkill = member.MemberSkills.Where(x => x.NonprofitId == State.NonprofitId && x.SkillId == ms.Id).FirstOrDefault();
                    if (memberSkill != null)
                        memberSkill.IsComplete = ms.IsComplete;
                    else
                    {
                        if (ms.IsComplete)
                            member.MemberSkills.Add(new MemberSkill { IsComplete = ms.IsComplete, MemberId = member.Id, NonprofitId = State.NonprofitId, SkillId = ms.Id });
                    }
                }
            }
            db.SaveChanges();

            return NonprofitRedirect(State.NonprofitId);
        }

        [Authorize(Roles = "Admin, Nonprofit, CaseManager")]
        public ActionResult UploadClients(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            UploadModel model = new UploadModel { NonprofitId = State.NonprofitId };
            return View(model);
        }

        [Authorize(Roles = "Admin, Nonprofit, CaseManager")]
        [HttpPost]
        public ActionResult UploadClients(UploadModel model)
        {
            ImpersonateNonprofit(model.NonprofitId);
            NonprofitService service = new NonprofitService(this.db);
            service.UploadClients(State.NonprofitId, model.CsvFile);
            return NonprofitRedirect(State.NonprofitId);
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
