using SolveChicago.Common;
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
            int[] members = new int[0];
            List<MemberProfile> model = new List<MemberProfile>();
            MemberService service = new MemberService(this.db);
            // workforce service providers only see members who have soft skills
            if (State.Nonprofit != null && State.Nonprofit.NonprofitSkills.Any(x => !x.Skill.IsWorkforce))
                members = db.Members.Where(x => !x.NonprofitMembers.Any(y => !y.End.HasValue || y.NonprofitId == State.NonprofitId) && !x.MemberCorporations.Any(y => y.Start > x.CreatedDate) && (x.IsWorkforceInterested ?? true)).Select(x => x.Id).ToArray();
            else
                members = db.Members.Where(x => !x.NonprofitMembers.Any(y => !y.End.HasValue || y.NonprofitId == State.NonprofitId) && !x.MemberCorporations.Any(y => y.Start > x.CreatedDate) && (x.IsWorkforceInterested ?? true) && x.MemberSkills.Any(y => y.Skill.Name == Constants.Skills.SoftSkills && y.IsComplete == true)).Select(x => x.Id).ToArray();

            foreach (var member in members)
                model.Add(service.Get(member));

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
                npo.NonprofitMembers.Add(new NonprofitMember { Member = member, Start = DateTime.UtcNow, });
                MemberSkill[] skills = member.MemberSkills.Where(x => !x.IsComplete && x.Skill.NonprofitSkills.Any(y => y.NonprofitId == npo.Id)).ToArray();
                foreach(MemberSkill skill in skills)
                {
                    skill.NonprofitSkill.NonprofitId = npo.Id;
                }
                db.SaveChanges();
            }   
            else
                throw new ApplicationException("We're sorry, we are unable to make this match at this time");

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}