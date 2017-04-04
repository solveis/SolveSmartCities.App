using SolveChicago.Common;
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
using SolveChicago.Service;
using SolveChicago.Common.Models.Profile;

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
        public ActionResult Member(int? id)
        {
            MemberService service = new MemberService(this.db);
            MemberProfile member = service.Get(10, 3);
            MemberProfileViewModel model = FormatMemberProfileViewModel(member);
            return View(model);
            
        }

        // POST: Profile/Member
        [HttpPost]
        public ActionResult Member(MemberProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);
                
                    service.UpdateProfile(model.Member);
                    return MemberRedirect(model.Member.Id);
            }
            model = FormatMemberProfileViewModel(model.Member);
            return MemberRedirect(model.Member.Id);
        }

        private MemberProfileViewModel FormatMemberProfileViewModel(MemberProfile member)
        {
            MemberProfileViewModel model = new MemberProfileViewModel
            {
                Member = PopulateEmptyFields(member),
                DegreeList = GetDegreeList(),
                GenderList = GetGenderList(),
                RelationshipList = GetRelationshipList(),
                SchoolTypeList = GetSchoolTypeList()
            };
            return model;
        }

        private MemberProfile PopulateEmptyFields(MemberProfile model)
        {
            if (model.Family == null)
                model.Family = new FamilyEntity();
            if (model.Family != null && (model.Family.FamilyMembers == null || model.Family.FamilyMembers.Count() == 0))
                model.Family.FamilyMembers = new FamilyMember[1] { new FamilyMember() };
            if (model.Nonprofits == null)
                model.Nonprofits = new NonprofitEntity[1] { new NonprofitEntity() };
            if (model.Jobs == null || (model.Jobs != null && model.Jobs.Count() == 0))
                model.Jobs = new JobEntity[1] { new JobEntity() };
            if (model.Schools == null)
                model.Schools = new SchoolEntity[1] { new SchoolEntity() };

            return model;

        }
        
        private string[] GetGenderList()
        {
            return new string[]
            {
                Constants.Gender.Male,
                Constants.Gender.Female,
                Constants.Gender.Other,
            };
        }
        private string[] GetDegreeList()
        {
            return new string[]
            {
                Constants.School.Degrees.HSDiploma,
                Constants.School.Degrees.GED,
                Constants.School.Degrees.BachelorsDegree,
                Constants.School.Degrees.MastersDegree,
                Constants.School.Degrees.PostGraduateDegree,
            };
        }

        private string[] GetSchoolTypeList()
        {
            return new string[] 
            {
                Constants.School.Types.HighSchool,
                Constants.School.Types.UndergraduateCollege,
                Constants.School.Types.GraduateCollege,
                Constants.School.Types.PostGraduateCollege,
            };
        }

        private string[] GetRelationshipList()
        {
            return new string[]
            {
                Constants.Family.Relationships.Parent,
                Constants.Family.Relationships.Child,
                Constants.Family.Relationships.Spouse,
            };
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