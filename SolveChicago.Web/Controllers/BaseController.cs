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
using SolveChicago.Web.Common;
using SolveChicago.Web.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Collections.Generic;
using SolveChicago.Entities;

namespace SolveChicago.Web.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        public BaseController(SolveChicagoEntities entities = null) : base()
        {
            if(entities == null)
            {
                db = new SolveChicagoEntities();
            }
            else
            {
                db = entities;
            }
        }
        public BaseController() { db = new SolveChicagoEntities(); }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        protected SolveChicagoEntities db;
        protected ApplicationSignInManager _signInManager;
        protected ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            protected set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        public class StateModel
        {
            public string DisplayName { get; set; }
            public string FirstName { get; set; }
            public string UserName { get; set; }
            public int MemberId { get; set; }
            public Member Member { get; set; }
            public int CaseManagerId { get; set; }
            public CaseManager CaseManager { get; set; }
            public int CorporationId { get; set; }
            public Corporation Corporation { get; set; }
            public int NonprofitId { get; set; }
            public Nonprofit Nonprofit { get; set; }
            public int AdminId { get; set; }
            public Admin Admin { get; set; }
            public Enumerations.Role[] Roles { get; set; }
        }

        private StateModel _state;

        public StateModel State
        {
            get
            {
                if (_state == null)
                {
                    _state = new StateModel();
                    if ((User != null) && (User.Identity != null) && (User.Identity.IsAuthenticated))
                    {
                        RefreshState(User.Identity.Name);
                    }

                }

                return _state;
            }
        }

        protected void RefreshState(string userName)
        {
            _state = new StateModel();
            _state.UserName = userName;
            _state.Roles = GetRoles(userName);
            string userId = GetUserId(userName);
            _state.FirstName = _state.DisplayName != null ? _state.DisplayName.Split(' ')[0] : "";
            ViewBag.Email = userName;

            if (_state.Roles.Contains(Enumerations.Role.Member))
            {
                Member member = db.AspNetUsers.Single(x => x.Id == userId).Members.First();

                if (member != null)
                {
                    _state.Member = member;
                    _state.MemberId = member.Id;
                    if (member.MemberNonprofits.Select(x => x.CaseManager) != null)
                    {
                        _state.CaseManager = member.MemberNonprofits.Select(x => x.CaseManager).First();
                        _state.CaseManagerId = member.MemberNonprofits.Select(x => x.CaseManager).First().Id;
                    }
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.CaseManager))
            {
                CaseManager caseManager = db.AspNetUsers.Single(x => x.Id == userId).CaseManagers.First();

                if (caseManager != null)
                {
                    _state.CaseManager = caseManager;
                    _state.CaseManagerId = caseManager.Id;
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.Corporation))
            {
                Corporation corporation = db.AspNetUsers.Single(x => x.Id == userId).Corporations.First();

                if (corporation != null)
                {
                    _state.Corporation = corporation;
                    _state.CorporationId = corporation.Id;
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.Nonprofit))
            {
                Nonprofit nonprofit = db.AspNetUsers.Single(x => x.Id == userId).Nonprofits.First();

                if (nonprofit != null)
                {
                    _state.Nonprofit = nonprofit;
                    _state.NonprofitId = nonprofit.Id;
                }
            }

            if (_state.Roles.Contains(Enumerations.Role.Admin))
            {
                Admin admin = db.AspNetUsers.Single(x => x.Id == userId).Admins.First();

                if (admin != null)
                {
                    _state.Admin = admin;
                    _state.AdminId = admin.Id;
                }
            }
        }

        public ActionResult UserRedirect()
        {
            switch(State.Roles.First())
            {
                case Enumerations.Role.Admin:
                    return AdminRedirect(State.Admin);
                case Enumerations.Role.CaseManager:
                    return CaseManagerRedirect(State.CaseManager);
                case Enumerations.Role.Corporation:
                    return CorporationRedirect(State.Corporation);
                case Enumerations.Role.Member:
                    return MemberRedirect(State.Member);
                case Enumerations.Role.Nonprofit:
                    return NonprofitRedirect(State.Nonprofit);
                default:
                    return HttpNotFound();
            }
        }

        public ActionResult MemberRedirect(int memberId)
        {
            Member entity = db.Members.Single(x => x.Id == memberId);
            return MemberRedirect(entity);
        }

        public ActionResult MemberRedirect(Member entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) || string.IsNullOrEmpty(entity.ProfilePicturePath))
            {
                return RedirectToAction("Member", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Members");
            }
            // TODO add more cases for member enrollment
        }

        public ActionResult CaseManagerRedirect(int caseManagerId)
        {
            CaseManager entity = db.CaseManagers.Single(x => x.Id == caseManagerId);
            return CaseManagerRedirect(entity);
        }

        public ActionResult CaseManagerRedirect(CaseManager entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.ProfilePicturePath))
            {
                return RedirectToAction("CaseManager", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "CaseManagers");
            }
        }

        public ActionResult CorporationRedirect(int corporationId)
        {
            Corporation entity = db.Corporations.Single(x => x.Id == corporationId);
            return CorporationRedirect(entity);
        }

        public ActionResult CorporationRedirect(Corporation entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                return RedirectToAction("Corporation", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Corporations");
            }
        }

        public ActionResult NonprofitRedirect(int nonprofitId)
        {
            Nonprofit entity = db.Nonprofits.Single(x => x.Id == nonprofitId);
            return NonprofitRedirect(entity);
        }

        public ActionResult NonprofitRedirect(Nonprofit entity)
        {
            if (string.IsNullOrEmpty(entity.Address1) || string.IsNullOrEmpty(entity.City) || string.IsNullOrEmpty(entity.Country)
                       || string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.Province))
            {
                return RedirectToAction("Nonprofit", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Nonprofits");
            }
        }

        public ActionResult AdminRedirect(int adminId)
        {
            Admin entity = db.Admins.Single(x => x.Id == adminId);
            return AdminRedirect(entity);
        }

        public ActionResult AdminRedirect(Admin entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.ProfilePicturePath))
            {
                return RedirectToAction("Admin", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Admins");
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return UserRedirect();
        }

        protected void AssertRole(string roleName)
        {
            if (!db.AspNetRoles.Any(x => x.Name == roleName))
                db.AspNetRoles.Add(new AspNetRole { Name = roleName });
            db.SaveChanges();
        }

        protected Enumerations.Role[] GetRoles(string userName)
        {
            List<Enumerations.Role> result = new List<Enumerations.Role>();
            var userRoles = db.AspNetUsers.Single(x => x.UserName == userName).AspNetRoles.Select(x => x.Name).ToArray();
            foreach (var role in Enum.GetValues(typeof(Enumerations.Role)).Cast<Enumerations.Role>())
            {
                if (userRoles.Contains(role.ToString()))
                    result.Add(role);
            }
            return result.ToArray();
        }

        protected string GetUserId(string userName)
        {
            return db.AspNetUsers.First(x => x.UserName == userName).Id;
        }


        protected async Task<ActionResult> CreateAccount(string userName, string password, Enumerations.Role role, string invitedByUserId = "")
        {
            var user = new ApplicationUser { UserName = userName, Email = userName };
            AspNetUser aspnetUser = new AspNetUser();
            if (ModelState.IsValid)
            {
                var result = await UserManager.CreateAsync(user, password);
                aspnetUser = GetUserById(user.Id);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                }
                AddErrors(result);
            }
            return await CreateUserAndAssignRoles(userName, role, invitedByUserId, user, aspnetUser);
        }

        private async Task<ActionResult> CreateUserAndAssignRoles(string userName, Enumerations.Role role, string invitedByUserId, ApplicationUser user, AspNetUser aspnetUser)
        {
            switch (role)
            {
                case Enumerations.Role.Member:
                    {
                        AssertRole(Common.Constants.Roles.Member);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Member);
                        if (!UserProfileHasValidMappings(user.Id))
                        {
                            Member model = new Member { Email = userName };
                            model.AspNetUsers.Add(aspnetUser);
                            db.Members.Add(model);
                            db.SaveChanges();
                            return MemberRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.CaseManager:
                    {
                        AssertRole(Common.Constants.Roles.CaseManager);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.CaseManager);
                        if (!UserProfileHasValidMappings(user.Id))
                        {
                            CaseManager model = new CaseManager { Email = userName };
                            model.AspNetUsers.Add(aspnetUser);
                            db.CaseManagers.Add(model);
                            db.SaveChanges();
                            return CaseManagerRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.Nonprofit:
                    {
                        AssertRole(Common.Constants.Roles.Nonprofit);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Nonprofit);
                        if (!UserProfileHasValidMappings(user.Id))
                        {
                            Nonprofit model = new Nonprofit { Email = userName };
                            model.AspNetUsers.Add(aspnetUser);
                            db.Nonprofits.Add(model);
                            db.SaveChanges();
                            return NonprofitRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.Corporation:
                    {
                        AssertRole(Common.Constants.Roles.Corporation);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Corporation);
                        if (!UserProfileHasValidMappings(user.Id))
                        {
                            Corporation model = new Corporation { Email = userName };
                            model.AspNetUsers.Add(aspnetUser);
                            db.Corporations.Add(model);
                            db.SaveChanges();
                            return CorporationRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.Admin:
                    {
                        AssertRole(Common.Constants.Roles.Admin);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Admin);
                        if (!UserProfileHasValidMappings(user.Id))
                        {
                            Admin model = new Admin { Email = userName, InvitedBy = invitedByUserId };
                            model.AspNetUsers.Add(aspnetUser);
                            db.Admins.Add(model);
                            db.SaveChanges();
                            return AdminRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
            }
            return RedirectToAction("Index", "Home");
        }

        public bool UserProfileHasValidMappings(string userId)
        {
            AspNetUser profile = db.AspNetUsers.Single(x => x.Id == userId);
            return profile.Corporations.Any() || profile.Admins.Any() || profile.Nonprofits.Any() || profile.Members.Any() || profile.CaseManagers.Any();
        }

        public string GetUserIdFromUsername(string userName)
        {
            return db.AspNetUsers.Single(x => x.UserName == userName).Id;
        }

        public AspNetUser GetUserById(string userId)
        {
            return db.AspNetUsers.Single(x => x.Id == userId);
        }

        public bool ValidateAdminInvite(string inviteCode, ref string userId)
        {
            try
            {
                if (db.AdminInviteCodes.Any(x => x.InviteCode == inviteCode))
                {
                    userId = db.AdminInviteCodes.Single(x => x.InviteCode == inviteCode).InvitingAdminUserId;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public void ImpersonateCaseManager(int? caseManagerId)
        {
            if ((caseManagerId.HasValue) && (caseManagerId > 0))
            {
                if (State.Roles.Contains(Enumerations.Role.Admin) || State.Roles.Contains(Enumerations.Role.Nonprofit))
                {
                    CaseManager caseManager = db.CaseManagers.Find(caseManagerId.Value);
                    if (State.Roles.Contains(Enumerations.Role.Admin) ||
                       (State.Roles.Contains(Enumerations.Role.Nonprofit) && caseManager.Nonprofit.Id == State.NonprofitId) || State.Roles.Contains(Enumerations.Role.Admin))
                    {
                        State.CaseManager = caseManager;
                        State.CaseManagerId = caseManager.Id;
                    }
                }
            }
        }

        public void ImpersonateNonprofit(int? nonprofitId)
        {
            if ((nonprofitId.HasValue) && (nonprofitId > 0))
            {
                if (State.Roles.Contains(Enumerations.Role.Admin))
                {
                    Nonprofit nonprofit = db.Nonprofits.Find(nonprofitId.Value);
                    State.NonprofitId = nonprofit.Id;
                    State.Nonprofit = nonprofit;
                }
            }
        }
    }
}