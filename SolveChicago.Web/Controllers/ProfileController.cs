using SolveChicago.Web.Common;
using SolveChicago.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SolveChicago.Web.Data;
using SolveChicago.Web.Models.Profile;
using SolveChicago.Web.Services;

namespace SolveChicago.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(SolveChicagoEntities db) : base(db) { }
        public ProfileController() : base() { }

        [AllowAnonymous]
        // GET: Profile/Member
        public ActionResult Member(int id, int nonprofitId)
        {
            using (MemberService service = new MemberService(db))
            {
                MemberProfile model = service.GetMember(id, nonprofitId);
                return View(model);
            }
        }

        // POST: Profile/Member
        [HttpPost]
        public ActionResult Member(Member model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return MemberRedirect(model);
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
            using (NonprofitService service = new NonprofitService())
            {
                NonprofitProfile model = service.Get(State.NonprofitId);
                return View(model);
            }
        }

        // POST: Profile/CaseManager
        [HttpPost]
        public ActionResult Nonprofit(NonprofitProfile model)
        {
            if (ModelState.IsValid)
            {
                using (NonprofitService service = new NonprofitService())
                {
                    service.Post(model);
                }
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