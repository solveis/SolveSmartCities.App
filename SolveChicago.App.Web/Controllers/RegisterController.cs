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

namespace SolveChicago.App.Web.Controllers
{
    public class RegisterController : BaseController
    {
        //
        // GET: /Account/MemberRegister
        [AllowAnonymous]
        public ActionResult Member()
        {
            return View();
        }

        //
        // POST: /Account/MemberRegister
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Member(RegisterViewModel model)
        {
            await CreateAccount(model.Email, model.Password, Enumerations.Role.Member);
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/CorporationRegister
        [AllowAnonymous]
        public ActionResult Corporation()
        {
            return View();
        }

        //
        // POST: /Account/CorporationRegister
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Corporation(RegisterViewModel model)
        {
            await CreateAccount(model.Email, model.Password, Enumerations.Role.Corporation);
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/CaseManagerRegister
        [AllowAnonymous]
        public ActionResult CaseManager()
        {
            return View();
        }

        //
        // POST: /Account/CaseManagerRegister
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CaseManager(RegisterViewModel model)
        {
            await CreateAccount(model.Email, model.Password, Enumerations.Role.CaseManager);
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/NonprofitRegister
        [AllowAnonymous]
        public ActionResult Nonprofit()
        {
            return View();
        }

        //
        // POST: /Account/NonprofitRegister
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Nonprofit(RegisterViewModel model)
        {
            await CreateAccount(model.Email, model.Password, Enumerations.Role.Nonprofit);
            
            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}