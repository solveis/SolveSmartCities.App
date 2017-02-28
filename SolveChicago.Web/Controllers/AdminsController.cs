using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Web.Data;
using DonorPath.Web.Common;
using SolveChicago.Web.Common;
using SolveChicago.Web.Models;

namespace SolveChicago.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminsController : BaseController
    {
        public AdminsController(SolveChicagoEntities db) : base(db) { }
        public AdminsController() : base() { }

        // GET: Admins
        public ActionResult Index()
        {
            var admins = db.Admins.Include(a => a.AspNetUsers);
            return View(admins.ToList());
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
            AdminInviteModel model = new AdminInviteModel() { AdminId = State.AdminIds.First() };
            return View(model);
        }

        // POST: Admins/Invite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(AdminInviteModel model)
        {
            string inviteCode = Crypto.EncryptStringAES(model.AdminId.ToString(), Settings.Crypto.SharedSecret);
            EmailService service = new EmailService();
            service.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage { Destination = model.EmailToInvite, Body = string.Format("http://localhost:2486/register/admin?invitecode={0}", HttpUtility.UrlEncode(inviteCode)), Subject = "admin invite" });
            
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
