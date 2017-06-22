using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SolveChicago.Web.Models;
using SolveChicago.Common;
using SolveChicago.Web.Controllers;
using System.Diagnostics.CodeAnalysis;
using SolveChicago.Entities;
using SolveChicago.Service;

namespace SolveChicago.Web.Controllers
{
    [ExcludeFromCodeCoverage]
    public class RegisterController : BaseController
    {

        //
        // GET: /Register/Admin
        [AllowAnonymous]
        public ActionResult Admin(string inviteCode)
        {
            string userId = "";
            AdminService service = new AdminService(this.db);
            if (!service.ValidateAdminInvite(HttpUtility.UrlDecode(inviteCode), ref userId)) // refactor to be encryption check
            {
                return HttpNotFound();
            }
            AdminRegisterViewModel model = new AdminRegisterViewModel();
            model.InvitedByUserId = userId;
            model.InviteCode = inviteCode;
            return View(model);
        }

        //
        // POST: /Register/Admin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Admin(AdminRegisterViewModel model)
        {
            var result = await CreateAccount(model.Email, model.Password, Enumerations.Role.Admin, model.InvitedByUserId, model.InviteCode);
            AdminService service = new AdminService(this.db);
            if (result != null)
                return result;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Register/Member
        [AllowAnonymous]
        public ActionResult Member(int? id, int? referrerId)
        {
            MemberRegisterViewModel model = new MemberRegisterViewModel
            {
                MemberId = id,
            };
            if(id.HasValue)
            {
                Member member = db.Members.Find(id.Value);
                if (member != null)
                    model.Email = member.Email;
            }
            return View(model);
        }

        //
        // POST: /Register/Member
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Member(MemberRegisterViewModel model)
        {
            var result = await CreateAccount(model.Email, model.Password, Enumerations.Role.Member);
            if (result != null)
                return result;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Register/Corporation
        [AllowAnonymous]
        public ActionResult Corporation()
        {
            return View();
        }

        //
        // POST: /Register/Corporation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Corporation(RegisterViewModel model)
        {
            var result = await CreateAccount(model.Email, model.Password, Enumerations.Role.Corporation);
            if (result != null)
                return result;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Register/CaseManager
        [AllowAnonymous]
        public ActionResult CaseManager(int? id)
        {

            CaseManagerRegisterViewModel model = new CaseManagerRegisterViewModel
            {
                CaseManagerId = id,
            };
            if (id.HasValue)
            {
                CaseManager caseManager = db.CaseManagers.Find(id.Value);
                if (caseManager != null)
                {
                    model.Email = caseManager.Email;
                    model.NonprofitId = caseManager.NonprofitStaffs.Any() ? caseManager.NonprofitStaffs.First().NonprofitId : (int?)null;
                }   
            }

            ViewBag.NonProfitId = model.NonprofitId;
            ViewBag.CaseManagerId = model.CaseManagerId;

            return View(model);
        }

        //
        // POST: /Register/CaseManager
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CaseManager(CaseManagerRegisterViewModel model)
        {
            var result = await CreateAccount(model.Email, model.Password, Enumerations.Role.CaseManager);
            if(model.NonprofitId.HasValue)
            {
                CaseManagerService service = new CaseManagerService(this.db);
                service.AddToNonprofit(service.GetCaseManagerByEmail(model.Email).Id, model.NonprofitId.Value);
            }
            if (result != null)
                return result;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Register/Nonprofit
        [AllowAnonymous]
        public ActionResult Nonprofit()
        {
            return View();
        }

        //
        // POST: /Register/Nonprofit
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Nonprofit(RegisterViewModel model)
        {
            var result = await CreateAccount(model.Email, model.Password, Enumerations.Role.Nonprofit);
            if (result != null)
                return result;
            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}