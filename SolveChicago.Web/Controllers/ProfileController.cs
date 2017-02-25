using SolveChicago.Web.Common;
using SolveChicago.Web.Controllers;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SolveChicago.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        // GET: Profile/Member
        public ActionResult Member()
        {
            Member model = State.Members.FirstOrDefault();
            return View(model);
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
            CaseManager model = State.CaseManagers.FirstOrDefault();
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
            Nonprofit model = State.Nonprofits.FirstOrDefault();
            return View(model);
        }

        // POST: Profile/CaseManager
        [HttpPost]
        public ActionResult Nonprofit(Nonprofit model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Nonprofit");
        }

        // GET: Profile/Corporation
        public ActionResult Corporation()
        {
            Corporation model = State.Corporations.FirstOrDefault();
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
            Admin model = State.Admins.FirstOrDefault();
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