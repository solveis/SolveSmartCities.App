using SolveChicago.Common;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using SolveChicago.Service;
using SolveChicago.Web.Models.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveChicago.Web.Controllers
{
    // GET: Members
    [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
    public class MembersController : BaseController, IDisposable
    {
        public MembersController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public MembersController() : base() { }

        public new void Dispose()
        {
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ActionResult Index(int? memberId)
        {
            ImpersonateMember(memberId);
            return MemberRedirect(State.MemberId);
        }

        public ActionResult Details(int? memberId)
        {
            ImpersonateMember(memberId);
            MemberService service = new MemberService(this.db);
            MemberProfile model = service.Get(State.MemberId);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ConfirmJob(int memberId, int nonprofitId, int corporationId)
        {
            MemberCorporation job = db.MemberCorporations.SingleOrDefault(x => x.MemberId == memberId && x.NonprofitId == nonprofitId && x.CorporationId == corporationId);
            if (job == null)
                return HttpNotFound();
            else
            {
                job.IsMemberConfirmed = true;
                db.SaveChanges();
                return RedirectToAction("Index", "Thanks");
            }   
        }

        [Authorize(Roles = "Admin, CaseManager, Nonprofit")]
        public ActionResult MemberOverview(int? memberId)
        {
            ImpersonateMember(memberId);
            MemberService service = new MemberService(this.db);
            MemberProfile model = service.Get(State.MemberId);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult CreateProfile(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                Member member = db.Members.Find(id.Value);
                if (member.SurveyStep == Constants.Member.SurveyStep.Invited)
                    return RedirectToAction("Member", "Register", new { id = id.Value });
                else
                    return RedirectToAction("Login", "Account");
            }
                
        }
    }
}