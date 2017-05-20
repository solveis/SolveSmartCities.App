using SolveChicago.Common;
using SolveChicago.Entities;
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
            Member[] model = db.Members.Where(x => x.SurveyStep == Constants.Member.SurveyStep.Complete && !x.NonprofitMembers.Any(y => !y.End.HasValue || y.NonprofitId == State.NonprofitId) && (x.IsWorkforceInterested ?? true)).ToArray(); // TODO restrict it to interested
            ViewBag.NonprofitId = State.NonprofitId;
            return View(model);
        }

        // POST: Prospects/InviteMember
        public ActionResult InviteMember(int memberId, int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);

            Member member = db.Members.Find(memberId);
            Nonprofit npo = db.Nonprofits.Find(State.NonprofitId);
            if (member != null && npo != null)
            {
                npo.NonprofitMembers.Add(new NonprofitMember { Member = member, Start = DateTime.UtcNow, });
                MemberSkill[] skills = member.MemberSkills.Where(x => !x.IsComplete && x.Skill.Nonprofits.Any(y => y.Id == npo.Id)).ToArray();
                foreach(MemberSkill skill in skills)
                {
                    skill.NonprofitId = npo.Id;
                }
                db.SaveChanges();
            }   
            else
                throw new ApplicationException("We're sorry, we are unable to make this match at this time");

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}