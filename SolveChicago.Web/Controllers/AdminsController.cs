using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Entities;
using SolveChicago.Common;
using SolveChicago.Web.Models;
using SolveChicago.Web.Models.Admin;
using SolveChicago.Service;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Web.Models.Member;

namespace SolveChicago.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminsController : BaseController, IDisposable
    {
        public AdminsController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public AdminsController() : base() { }

        public new void Dispose()
        {
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // GET: Admins - Nonprofit List
        public ActionResult Index()
        {
            var nonprofits = db.Nonprofits.ToArray();
            return View(nonprofits.ToList());
        }

        // GET: Admins/CaseManagers
        public ActionResult CaseManagers()
        {
            var caseManagers = db.CaseManagers.ToArray();
            return View(caseManagers.ToList());
        }

        // GET: Admins/Members
        public ActionResult Members()
        {
            MemberService service = new MemberService(this.db);
            FamilyEntity[] members = service.GetAll();
            return View(members.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Invite
        public ActionResult Invite()
        {
            AdminInviteModel model = new AdminInviteModel() { AdminId = State.AdminId };
            return View(model);
        }

        // POST: Admins/Invite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(AdminInviteModel model)
        {
            AdminService adminService = new AdminService(this.db);
            Admin admin = db.Admins.Find(model.AdminId);
            string inviteCode = adminService.GenerateAdminInviteCode(admin.UserId);
            CommunicationService commService = new CommunicationService(this.db);
            string inviteUrl = string.Format("{0}/register/admin?invitecode={1}", Settings.Website.BaseUrl, inviteCode);
            commService.AdminInvite(model.EmailToInvite, string.Format("{0} {1}", admin.FirstName, admin.LastName), inviteUrl);
            
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
