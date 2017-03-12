using SolveChicago.Web.Common;
using SolveChicago.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Profile;
using SolveChicago.Web.Services;

namespace SolveChicago.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController, IDisposable
    {
        public ProfileController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public ProfileController() : base() { }

        public new void Dispose()
        {
            base.Dispose();
        }

        [AllowAnonymous]
        // GET: Profile/Member
        public ActionResult Member(int id, int nonprofitId)
        {
            MemberService service = new MemberService(this.db);
            MemberProfile model = service.Get(id, nonprofitId);
            return View(model);
            
        }

        // POST: Profile/Member
        [HttpPost]
        public ActionResult Member(MemberProfile model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);
                
                    service.UpdateProfile(model);
                    return MemberRedirect(model.Id);
                
            }
            return View(model);
        }

        // GET: Profile/CaseManager
        public ActionResult CaseManager()
        {
            CaseManager model = State.CaseManager;
            return View(model);
        }

        // POST: Profile/CaseManager
        [HttpPost]
        public ActionResult CaseManager(CaseManager model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "CaseManager");
        }

        // GET: Profile/Nonprofit
        public ActionResult Nonprofit()
        {
            NonprofitService service = new NonprofitService(this.db);
            
                NonprofitProfile model = service.Get(State.NonprofitId);
                return View(model);
            
        }

        // POST: Profile/CaseManager
        [HttpPost]
        public ActionResult Nonprofit(NonprofitProfile model)
        {
            if (ModelState.IsValid)
            {
                NonprofitService service = new NonprofitService(this.db);
                service.Post(model);
            }
            return RedirectToAction("Index", "Nonprofits");
        }

        // GET: Profile/Corporation
        public ActionResult Corporation()
        {
            Corporation model = State.Corporation;
            return View(model);
        }

        // POST: Profile/Corporation
        [HttpPost]
        public ActionResult Corporation(Corporation model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Corporation");
        }

        // GET: Profile/Admin
        public ActionResult Admin()
        {
            Admin model = State.Admin;
            return View(model);
        }

        // POST: Profile/Admin
        [HttpPost]
        public ActionResult Admin(Admin model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}