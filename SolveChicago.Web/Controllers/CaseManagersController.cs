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
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        // GET: CaseManagers
        public ActionResult Index(int? caseManagerId)
        {
            ImpersonateCaseManager(caseManagerId);
            CaseManagerService service = new CaseManagerService(this.db);
            
                FamilyEntity[] families = service.GetFamiliesForCaseManager(State.CaseManagerId);
                return View(families.ToList());
        }

        // GET: CaseManagers/AddCaseNote
        public ActionResult AddCaseNote(int? caseManagerId, int memberId)
        {
            ImpersonateCaseManager(caseManagerId);
            Member member = db.Members.Find(memberId);
            if (member == null)
                throw new ApplicationException($"No Member with id of {memberId}");
            CaseNote model = new CaseNote() { CaseManagerId = State.CaseManagerId, MemberId = member.Id, Member = member };
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
