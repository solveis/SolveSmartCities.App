using SolveChicago.Common;
using SolveChicago.Common.Models;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using SolveChicago.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveChicago.Web.Controllers
{
    public class ProspectsController : BaseController
    {
        public ProspectsController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public ProspectsController() : base() { }

        [Authorize(Roles = "Admin, CaseManager, Nonprofit")]
        // GET: Prospects
        public ActionResult Index(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            Member[] members = new Member[0];
            List<ProspectModel> model = new List<ProspectModel>();
            MemberService service = new MemberService(this.db);
            // workforce service providers only see members who have soft skills
            if (State.Nonprofit != null && State.Nonprofit.NonprofitSkills.Any(x => !x.Skill.IsWorkforce))
            {
                members = db.Members
                    .Where(x => 
                        !x.NonprofitMembers.Any(y => !y.End.HasValue || y.NonprofitId == State.NonprofitId) // not currently in a nonprofit
                        && !x.MemberCorporations.Any(y => y.Start > x.CreatedDate) // not in a job started after onboarding
                        && (x.IsWorkforceInterested ?? true)) // is interested in training
                    .ToArray();
            }
            else
            {
                members = db.Members
                    .Where(x => 
                        (!x.NonprofitMembers.Any(y => !y.End.HasValue || y.NonprofitId == State.NonprofitId) // not currently in a nonprofit
                        && !x.MemberCorporations.Any(y => y.Start > x.CreatedDate) // not in a job started after onboarding
                        && (x.IsWorkforceInterested ?? true) // is interested in training
                        && x.MemberSkills.Any(y => !y.Skill.IsWorkforce && y.IsComplete == true)  // has completed soft skill training
                        && x.MemberSkills.Any(y => !y.IsComplete && y.Skill.NonprofitSkills.Any(z => z.NonprofitId == State.NonprofitId))) // is interested in a skill the nonprofit offers
                        || x.Referrals.Any(y => y.ReferredId == State.NonprofitId)) // or has been directly referred
                    .ToArray();
            }

            foreach (var member in members)
            {
                ProspectModel prospect = new ProspectModel()
                {
                    Member = service.Get(member.Id, false),
                    ReferringNonprofit = member.Referrals.Any(x => x.ReferredId == State.NonprofitId) ? member.Referrals.First(x => x.ReferredId == State.NonprofitId).Nonprofit1 : null,
                    ReferringCaseManagers = member.Referrals.Any(x => x.ReferredId == State.NonprofitId) && member.Referrals.First(x => x.ReferredId == State.NonprofitId).Nonprofit1.NonprofitStaffs.Any() ? member.Referrals.First(x => x.ReferredId == State.NonprofitId).Nonprofit1.NonprofitStaffs.Where(y => y.NonprofitMembers.Any(z => z.MemberId == member.Id)).Select(y => y.CaseManager).ToArray() : null,
                };
                model.Add(prospect);
            }   

            ViewBag.NonprofitId = State.NonprofitId;
            return View(model.ToArray());
        }

        // POST: Prospects/InviteMember
        public ActionResult InviteMember(int memberId, int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);

            Member member = db.Members.Find(memberId);
            Nonprofit npo = db.Nonprofits.Find(State.NonprofitId);
            if (member != null && npo != null)
            {
                CommunicationService service = new CommunicationService(this.db);
                    service.InviteMemberToNonprofit(member, npo);
            }   
            else
                throw new ApplicationException("We're sorry, we are unable to make this match at this time");

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult AcceptInvitation(int memberId, int nonprofitId, int? referringNonprofitId)
        {
            Member member = db.Members.Find(memberId);
            Nonprofit npo = db.Nonprofits.Find(State.NonprofitId);
            if (member != null && npo != null)
            {
                npo.NonprofitMembers.Add(new NonprofitMember { Member = member, Start = DateTime.UtcNow, });
                MemberSkill[] skills = member.MemberSkills.Where(x => !x.IsComplete && x.Skill.NonprofitSkills.Any(y => y.NonprofitId == npo.Id)).ToArray();
                foreach (MemberSkill skill in skills)
                {
                    skill.NonprofitSkill.NonprofitId = npo.Id;
                }

                CommunicationService service = new CommunicationService(this.db);
                service.InvitationAccepted(npo, member);
                if (referringNonprofitId.HasValue)
                {
                    Nonprofit referringNpo = db.Nonprofits.Find(referringNonprofitId.Value);
                    if (referringNpo != null)
                        service.ReferralSucceeded(referringNpo, member, npo);
                }

                db.SaveChanges();
            }
            else
                throw new ApplicationException("We're sorry, we are unable to make this match at this time");

            if (member.AspNetUser != null)
                return RedirectToAction("Index", "Thanks");
            else
                return RedirectToAction("Member", "Register", new { id = member.Id });
        }
    }
}