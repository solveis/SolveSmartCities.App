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
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Collections.Generic;
using SolveChicago.Entities;
using SolveChicago.Service;

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
            public int ReferrerId { get; set; }
            public Referrer Referrer { get; set; }
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
                        RefreshState();
                    }

                }

                return _state;
            }
        }

        protected void RefreshState(string loginName = "")
        {
            string userName = !string.IsNullOrEmpty(loginName) ? loginName :  User.Identity.Name;
            _state = new StateModel()
            {
                UserName = userName,
                Roles = GetRoles(userName)
            };
            string userId = !string.IsNullOrEmpty(loginName) ? GetUserId(loginName) : User.Identity.GetUserId();
            _state.FirstName = _state.DisplayName != null ? _state.DisplayName.Split(' ')[0] : "";
            ViewBag.Email = userName;

            if (_state.Roles.Contains(Enumerations.Role.Member))
            {
                Member member = db.AspNetUsers.Single(x => x.Id == userId).Members.First();

                if (member != null)
                {
                    _state.Member = member;
                    _state.MemberId = member.Id;

                    ViewBag.Member = member;
                    ViewBag.MemberId = member.Id;
                    ViewBag.MemberName = string.Format("{0} {1}", member.FirstName, member.LastName);
                    ViewBag.MemberProfilePicture = !string.IsNullOrEmpty(member.ProfilePicturePath) ? member.ProfilePicturePath : Common.Constants.Member.NoPhotoUrl;
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.CaseManager))
            {
                CaseManager caseManager = db.AspNetUsers.Single(x => x.Id == userId).CaseManagers.First();

                if (caseManager != null)
                {
                    _state.CaseManager = caseManager;
                    _state.CaseManagerId = caseManager.Id;

                    ViewBag.CaseManager = caseManager;
                    ViewBag.CaseManagerId = caseManager.Id;
                    ViewBag.CaseManagerName = string.Format("{0} {1}", caseManager.FirstName, caseManager.LastName);
                    ViewBag.CaseManagerProfilePicture = !string.IsNullOrEmpty(caseManager.ProfilePicturePath) ? caseManager.ProfilePicturePath : Common.Constants.Member.NoPhotoUrl;

                    if (caseManager.NonprofitId.HasValue)
                    {
                        _state.Nonprofit = caseManager.Nonprofit;
                        _state.NonprofitId = caseManager.Nonprofit.Id;

                        ViewBag.Nonprofit = caseManager.Nonprofit;
                        ViewBag.NonprofitId = caseManager.Nonprofit.Id;
                        ViewBag.NonprofitName = caseManager.Nonprofit.Name;
                    }
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.Corporation))
            {
                Corporation corporation = db.AspNetUsers.Single(x => x.Id == userId).Corporations.First();

                if (corporation != null)
                {
                    _state.Corporation = corporation;
                    _state.CorporationId = corporation.Id;

                    ViewBag.Corporation = corporation;
                    ViewBag.CorporationId = corporation.Id;
                    ViewBag.CorporationName = corporation.Name;
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.Nonprofit))
            {
                Nonprofit nonprofit = db.AspNetUsers.Single(x => x.Id == userId).Nonprofits.First();

                if (nonprofit != null)
                {
                    _state.Nonprofit = nonprofit;
                    _state.NonprofitId = nonprofit.Id;

                    ViewBag.Nonprofit = nonprofit;
                    ViewBag.NonprofitId = nonprofit.Id;
                    ViewBag.NonprofitName = nonprofit.Name;
                    ViewBag.NonprofitProfilePicture = !string.IsNullOrEmpty(nonprofit.ProfilePicturePath) ? nonprofit.ProfilePicturePath : Common.Constants.Member.NoPhotoUrl;
                }
            }

            if (_state.Roles.Contains(Enumerations.Role.Admin))
            {
                Admin admin = db.AspNetUsers.Single(x => x.Id == userId).Admins.First();

                if (admin != null)
                {
                    _state.Admin = admin;
                    _state.AdminId = admin.Id;

                    ViewBag.Admin = admin;
                    ViewBag.AdminId = admin.Id;
                    ViewBag.AdminName = string.Format("{0} {1}", admin.FirstName, admin.LastName);
                    ViewBag.AdminProfilePicture = !string.IsNullOrEmpty(admin.ProfilePicturePath) ? admin.ProfilePicturePath : Common.Constants.Member.NoPhotoUrl;
                }
            }

            if (_state.Roles.Contains(Enumerations.Role.Referrer))
            {
                Referrer referrer = db.AspNetUsers.Single(x => x.Id == userId).Referrers.First();

                if (referrer != null)
                {
                    _state.Referrer = referrer;
                    _state.ReferrerId = referrer.Id;

                    ViewBag.Referrer = referrer;
                    ViewBag.ReferrerId = referrer.Id;
                    ViewBag.ReferrerName = referrer.Name;
                }
            }
        }

        protected string GetUserId(string userName)
        {
            return db.AspNetUsers.First(x => x.UserName == userName).Id;
        }

        public ActionResult UserRedirect()
        {
            if(State.Roles.Count() == 0)
                return HttpNotFound();
            else
            {
                switch (State.Roles.FirstOrDefault())
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
                    case Enumerations.Role.Referrer:
                        return ReferrerRedirect(State.Referrer);
                    default:
                        return HttpNotFound();
                }
            }
        }

        public ActionResult MemberRedirect(int memberId)
        {
            Member entity = db.Members.Single(x => x.Id == memberId);
            return MemberRedirect(entity);
        }

        public ActionResult UpdateSurveyStatus(int memberId, string currentStep)
        {
            Member entity = db.Members.Single(x => x.Id == memberId);
            return UpdateSurveyStatus(entity, currentStep);
        }

        public ActionResult UpdateSurveyStatus(Member entity, string currentStep)
        {
            string latestStep = entity.SurveyStep;
            if (GetSurveyStepOrderNumber(currentStep) > GetSurveyStepOrderNumber(latestStep))
                entity.SurveyStep = currentStep;
            db.SaveChanges();
            return MemberRedirect(entity);
        }

        private int GetSurveyStepOrderNumber(string surveyStep)
        {
            switch(surveyStep)
            {
                case Common.Constants.Member.SurveyStep.Invited: // invited
                    return 0;
                case Common.Constants.Member.SurveyStep.Personal: // personal
                    return 1;
                case Common.Constants.Member.SurveyStep.Family: // family
                    return 2;
                case Common.Constants.Member.SurveyStep.Education: // education
                    return 3;
                case Common.Constants.Member.SurveyStep.Jobs: // jobs
                    return 4;
                case Common.Constants.Member.SurveyStep.Nonprofits: // nonprofits
                    return 5;
                case Common.Constants.Member.SurveyStep.GovernmentPrograms: // gov benefits
                    return 6;
                case Common.Constants.Member.SurveyStep.Complete: // complete
                    return 7;
                default:
                    return 0;
            }
        }

        public ActionResult MemberRedirect(Member entity)
        {
            if(entity.SurveyStep == Common.Constants.Member.SurveyStep.Complete)
                return RedirectToAction("MemberOverview", "Profile", new { memberId = entity.Id });
            else
            {
                switch(entity.SurveyStep)
                {
                    case Common.Constants.Member.SurveyStep.Invited: // invited
                        return RedirectToAction("Survey", "Members", new { id = entity.Id });
                    case Common.Constants.Member.SurveyStep.Personal: // personal
                        return RedirectToAction("MemberPersonal", "Profile", new { id = entity.Id });
                    case Common.Constants.Member.SurveyStep.Family: // family
                        return RedirectToAction("MemberFamily", "Profile", new { id = entity.Id });
                    case Common.Constants.Member.SurveyStep.Education: // education
                        return RedirectToAction("MemberSchools", "Profile", new { id = entity.Id });
                    case Common.Constants.Member.SurveyStep.Jobs: // jobs
                        return RedirectToAction("MemberJobs", "Profile", new { id = entity.Id });
                    case Common.Constants.Member.SurveyStep.Nonprofits: // nonprofits
                        return RedirectToAction("MemberNonprofits", "Profile", new { id = entity.Id });
                    case Common.Constants.Member.SurveyStep.GovernmentPrograms: // gov benefits
                        return RedirectToAction("MemberGovernmentPrograms", "Profile", new { id = entity.Id });
                    default:
                        return RedirectToAction("MemberPersonal", "Profile", new { id = entity.Id });
                }
            }
        }

        public ActionResult CaseManagerRedirect(int caseManagerId)
        {
            CaseManager entity = db.CaseManagers.Single(x => x.Id == caseManagerId);
            return CaseManagerRedirect(entity);
        }

        public ActionResult CaseManagerRedirect(CaseManager entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) || !entity.PhoneNumbers.Any() || string.IsNullOrEmpty(entity.ProfilePicturePath))
            {
                return RedirectToAction("CaseManager", "Profile", new { id = entity.Id });
            }
            else
            {
                return RedirectToAction("Index", "CaseManagers", new { caseManagerId = entity.Id });
            }
        }

        public ActionResult CorporationRedirect(int corporationId)
        {
            Corporation entity = db.Corporations.Single(x => x.Id == corporationId);
            return CorporationRedirect(entity);
        }

        public ActionResult ReferrerRedirect(Referrer entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                return RedirectToAction("Referrer", "Profile", new { id = entity.Id });
            }
            else
            {
                return RedirectToAction("Index", "Referrers", new { referrerId = entity.Id });
            }
        }

        public ActionResult ReferrerRedirect(int ReferrerId)
        {
            Referrer entity = db.Referrers.Single(x => x.Id == ReferrerId);
            return ReferrerRedirect(entity);
        }

        public ActionResult CorporationRedirect(Corporation entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                return RedirectToAction("Corporation", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Corporations" );
            }
        }

        public ActionResult NonprofitRedirect(int nonprofitId)
        {
            Nonprofit entity = db.Nonprofits.Single(x => x.Id == nonprofitId);
            return NonprofitRedirect(entity);
        }

        public ActionResult NonprofitRedirect(Nonprofit entity)
        {
            if (!entity.Addresses.Any() || !entity.PhoneNumbers.Any())
            {
                return RedirectToAction("Nonprofit", "Profile", new { id = entity.Id });
            }
            else
            {
                return RedirectToAction("Index", "Nonprofits", new { nonprofitId = entity.Id });
            }
        }

        public ActionResult AdminRedirect(int adminId)
        {
            Admin entity = db.Admins.Single(x => x.Id == adminId);
            return AdminRedirect(entity);
        }

        public ActionResult AdminRedirect(Admin entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) || !entity.PhoneNumbers.Any() || string.IsNullOrEmpty(entity.ProfilePicturePath))
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

        protected Enumerations.Role[] GetRoles(string userName)
        {
            //var userRoles = ((ClaimsIdentity)User.Identity).Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            List<Enumerations.Role> result = new List<Enumerations.Role>();
            var userRoles = db.AspNetUsers.Single(x => x.UserName == userName).AspNetRoles.Select(x => x.Name).ToArray();
            foreach (var role in Enum.GetValues(typeof(Enumerations.Role)).Cast<Enumerations.Role>())
            {
                if (userRoles.Contains(role.ToString()))
                    result.Add(role);
            }
            return result.ToArray();
        }


        protected async Task<ActionResult> CreateAccount(string userName, string password, Enumerations.Role role, string invitedByUserId = "", string inviteCode = "")
        {
            var user = new ApplicationUser { UserName = userName, Email = userName };
            if (ModelState.IsValid)
            {
                var result = await UserManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var eventualResult = await CreateUserAndAssignRoles(userName, password, role, invitedByUserId, user, inviteCode);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return eventualResult;
                }
                AddErrors(result);
            }
            return null;
        }

        private async Task<ActionResult> CreateUserAndAssignRoles(string userName, string password, Enumerations.Role role, string invitedByUserId, ApplicationUser user, string inviteCode)
        {
            switch (role)
            {
                case Enumerations.Role.Member:
                    {
                        AspNetUser aspnetUser = GetUserById(user.Id);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Member);
                        if (!UserProfileHasValidMappings(aspnetUser))
                        {
                            Member model = db.Members.Where(x => x.Email == userName).FirstOrDefault();
                            if (model == null)
                            {
                                model = new Member { Email = userName, AspNetUser = aspnetUser, CreatedDate = DateTime.UtcNow };
                                db.Members.Add(model);
                            }
                            else
                                model.AspNetUser = aspnetUser;

                            // add soft skills as a desired skill for pipeline
                            // TODO refactor this into a stored proc
                            Skill softSkills = db.Skills.SingleOrDefault(x => x.Name == Common.Constants.Skills.SoftSkills);
                            if (softSkills == null)
                                softSkills = new Skill { Name = Common.Constants.Skills.SoftSkills };
                            if(!model.MemberSkills.Any(x => x.Skill.Name == Common.Constants.Skills.SoftSkills))
                                model.MemberSkills.Add(new MemberSkill { IsComplete = false, Skill = softSkills });

                            try
                            {
                                db.SaveChanges();
                            }
                            catch(Exception ex)
                            {
                            }
                            return UpdateSurveyStatus(model, Common.Constants.Member.SurveyStep.Personal);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.CaseManager:
                    {
                        AspNetUser aspnetUser = GetUserById(user.Id);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.CaseManager);
                        if (!UserProfileHasValidMappings(aspnetUser))
                        {
                            CaseManager model = db.CaseManagers.Where(x => x.Email == userName).FirstOrDefault();
                            if (model == null)
                            {
                                model = new CaseManager { Email = userName, AspNetUser = aspnetUser, CreatedDate = DateTime.UtcNow };
                                db.CaseManagers.Add(model);
                            }
                            else
                                model.AspNetUser = aspnetUser;
                            db.SaveChanges();
                            return CaseManagerRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.Nonprofit:
                    {
                        AspNetUser aspnetUser = GetUserById(user.Id);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Nonprofit);
                        if (!UserProfileHasValidMappings(aspnetUser))
                        {
                            Nonprofit model = new Nonprofit { Email = userName, CreatedDate = DateTime.UtcNow };
                            model.AspNetUsers.Add(aspnetUser);
                            db.Nonprofits.Add(model);
                            db.SaveChanges();
                            return NonprofitRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.Corporation:
                    {
                        AspNetUser aspnetUser = GetUserById(user.Id);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Corporation);
                        if (!UserProfileHasValidMappings(aspnetUser))
                        {
                            Corporation model = new Corporation { Email = userName, CreatedDate = DateTime.UtcNow };
                            model.AspNetUsers.Add(aspnetUser);
                            db.Corporations.Add(model);
                            db.SaveChanges();
                            return CorporationRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.Referrer:
                    {
                        AspNetUser aspnetUser = GetUserById(user.Id);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Referrer);
                        if (!UserProfileHasValidMappings(aspnetUser))
                        {
                            Referrer model = new Referrer { Email = userName, CreatedDate = DateTime.UtcNow };
                            model.AspNetUsers.Add(aspnetUser);
                            db.Referrers.Add(model);
                            db.SaveChanges();
                            return ReferrerRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
                case Enumerations.Role.Admin:
                    {
                        AspNetUser aspnetUser = GetUserById(user.Id);
                        await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Admin);
                        if (!UserProfileHasValidMappings(aspnetUser))
                        {
                            Admin model = new Admin { Email = userName, InvitedBy = invitedByUserId, AspNetUser = aspnetUser, CreatedDate = DateTime.UtcNow };
                            AdminService service = new AdminService(this.db);
                            service.MarkAdminInviteCodeAsUsed(invitedByUserId, inviteCode, userName);
                            db.Admins.Add(model);
                            db.SaveChanges();
                            return AdminRedirect(model.Id);
                        }
                        return RedirectToAction("Index");
                    }
            }
            return RedirectToAction("Index", "Home");
        }

        public bool UserProfileHasValidMappings(AspNetUser profile)
        {
            return profile.Corporations.Any() || profile.Admins.Any() || profile.Nonprofits.Any() || profile.Members.Any() || profile.CaseManagers.Any();
        }

        public AspNetUser GetUserById(string userId)
        {
            return db.AspNetUsers.Single(x => x.Id == userId);
        }

        public void ImpersonateMember(int? memberId)
        {
            if ((memberId.HasValue) && (memberId > 0))
            {
                if (State.Roles.Contains(Enumerations.Role.Admin) || State.Roles.Contains(Enumerations.Role.Nonprofit) || State.Roles.Contains(Enumerations.Role.CaseManager))
                {
                    Member member = db.Members.Find(memberId.Value);
                    if (State.Roles.Contains(Enumerations.Role.Admin) || 
                        (State.Roles.Contains(Enumerations.Role.Nonprofit) || State.Roles.Contains(Enumerations.Role.CaseManager)) && member.NonprofitMembers.Any(x => x.NonprofitId == State.NonprofitId))
                    {
                        State.Member = member;
                        State.MemberId = member.Id;

                        ViewBag.MemberId = member.Id;
                    }
                    else
                    {
                        throw new ApplicationException("You do not have permission to view this page");
                    }
                }
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

                        ViewBag.CaseManagerId = caseManager.Id;
                    }
                }
            }
        }

        public void ImpersonateCorporation(int? corporationId)
        {
            if ((corporationId.HasValue) && (corporationId > 0))
            {
                if (State.Roles.Contains(Enumerations.Role.Corporation))
                {
                    Corporation corporation = db.Corporations.Find(corporationId.Value);
                    State.CorporationId = corporation.Id;
                    State.Corporation = corporation;

                    ViewBag.CorporationId = corporation.Id;
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

                    ViewBag.NonprofitId = nonprofit.Id;
                }
            }
        }

        public void ImpersonateReferrer(int? referrerId)
        {
            if ((referrerId.HasValue) && (referrerId > 0))
            {
                if (State.Roles.Contains(Enumerations.Role.Admin))
                {
                    Referrer referrer = db.Referrers.Find(referrerId.Value);
                    State.ReferrerId = referrer.Id;
                    State.Referrer = referrer;

                    ViewBag.ReferrerId = referrer.Id;
                }
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            Elmah.ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
            TempData["ErrorMessage"] = filterContext.Exception.Message;
            // Redirect on error:
            filterContext.Result = RedirectToAction("Index", "Error");
        }
    }

    public static class RazorViewToString
    {
        public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}