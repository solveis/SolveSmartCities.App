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
            base.Dispose();
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
            var members = db.Members.ToArray();
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

        // GET: Admins/Create
        public ActionResult Create()
        {
            ViewBag.InvitedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Name,ProfilePicturePath,Phone,Address1,Address2,City,Province,Country,CreatedDate,InvitedBy")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvitedBy = new SelectList(db.AspNetUsers, "Id", "Email", admin.InvitedBy);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.InvitedBy = new SelectList(db.AspNetUsers, "Id", "Email", admin.InvitedBy);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Name,ProfilePicturePath,Phone,Address1,Address2,City,Province,Country,CreatedDate,InvitedBy")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvitedBy = new SelectList(db.AspNetUsers, "Id", "Email", admin.InvitedBy);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            string inviteCode = adminService.GenerateAdminInviteCode(GetUserId(User.Identity.Name));
            EmailService emailService = new EmailService(this.db);
            emailService.DeliverSmtpMessageAsync(model.EmailToInvite, "admin invite", string.Format("{0}/register/admin?invitecode={1}", Settings.Website.BaseUrl, HttpUtility.UrlEncode(inviteCode)),false).Wait();
            
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
