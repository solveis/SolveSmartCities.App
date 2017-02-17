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
using SolveChicago.App.Web.Models;
using SolveChicago.App.Common;
using SolveChicago.App.Web.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using SolveChicago.Entities;
using SolveChicago.App.Service;

namespace SolveChicago.App.Web.Controllers
{
    public class BaseController : Controller
    {
        protected SolveChicagoEntities db = new SolveChicagoEntities();
        protected ApplicationSignInManager _signInManager;
        protected ApplicationUserManager _userManager;

        public BaseController(SolveChicagoEntities db)
        {
            this.db = db;
        }

        public BaseController()
        {
            this.db = new SolveChicagoEntities();
        }
        
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
            return RedirectToAction("Index", "Home");
        }

        protected void AssertRole(string roleName)
        {
            if(!db.AspNetRoles.Any(x => x.Name == roleName))
                db.AspNetRoles.Add(new AspNetRole { Name = roleName });
        }


        protected async Task<ActionResult> CreateAccount(string userName, string password, Enumerations.Role role)
        {
            var user = new ApplicationUser { UserName = userName, Email = userName };
           
            if (ModelState.IsValid)
            {
                var result = await UserManager.CreateAsync(user, password);
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
            switch (role)
            {
                case Enumerations.Role.Member:
                {
                    AssertRole(Common.Constants.Roles.Member);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Member);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (MemberService innerService = new MemberService(db))
                            {
                                innerService.Create(userName, userId);
                            }
                            return RedirectToAction("Profile", "Member");
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.CaseManager:
                {
                    AssertRole(Common.Constants.Roles.CaseManager);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.CaseManager);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (CaseManagerService innerService = new CaseManagerService(db))
                            {
                                innerService.Create(userName, userId);
                            }
                            return RedirectToAction("Profile", "CaseManager");
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.Nonprofit:
                {
                    AssertRole(Common.Constants.Roles.Nonprofit);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Nonprofit);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (NonprofitService innerService = new NonprofitService(db))
                            {
                                    innerService.Create(userName, userId);
                            }
                            return RedirectToAction("Profile", "Nonprofit");
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.Corporation:
                {
                    AssertRole(Common.Constants.Roles.Corporation);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Corporation);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (CorporationService innerService = new CorporationService(db))
                            {
                                    innerService.Create(userName, userId);
                            }
                            return RedirectToAction("Profile", "Corporation");
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.Admin:
                {
                    AssertRole(Common.Constants.Roles.Admin);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Admin);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (AdminService innerService = new AdminService(db))
                            {
                                innerService.Create(userName, userId);
                            }
                            return RedirectToAction("Profile", "Admin");
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}