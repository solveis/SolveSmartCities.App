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
using SolveChicago.Common.Models.Profile.Member;

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

        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        // GET: Profile/MemberPersonal
        public ActionResult MemberPersonal(int? id)
        {
            ImpersonateMember(id);
            MemberService service = new MemberService(this.db);
            MemberProfilePersonal member = service.GetProfilePersonal(State.MemberId);
            MemberProfilePersonalViewModel model = FormatMemberProfilePersonalViewModel(member);
            return View(model);
        }

        // POST: Profile/MemberPersonal
        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        [HttpPost]
        public ActionResult MemberPersonal(MemberProfilePersonalViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);
                
                    service.UpdateMemberPersonal(model.Member);
                    return UpdateSurveyStatus(model.Member.Id, Constants.Member.SurveyStep.Personal);
            }
            model = FormatMemberProfilePersonalViewModel(model.Member);
            return View(model);
        }

        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        // GET: Profile/MemberFamily
        public ActionResult MemberFamily(int? id)
        {
            ImpersonateMember(id);
            MemberService service = new MemberService(this.db);
            MemberProfileFamily member = service.GetProfileFamily(State.MemberId);
            MemberProfileFamilyViewModel model = FormatMemberProfileFamilyViewModel(member);
            return View(model);
        }

        // POST: Profile/MemberFamily
        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        [HttpPost]
        public ActionResult MemberFamily(MemberProfileFamilyViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);

                service.UpdateMemberFamily(model.Member);
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Family);
            }
            model = FormatMemberProfileFamilyViewModel(model.Member);
            return View(model);
        }

        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        // GET: Profile/MemberSchools
        public ActionResult MemberSchools(int? id)
        {
            ImpersonateMember(id);
            MemberService service = new MemberService(this.db);
            MemberProfileSchools member = service.GetProfileSchools(State.MemberId);
            MemberProfileSchoolViewModel model = FormatMemberProfileSchoolsViewModel(member);
            return View(model);
        }

        // POST: Profile/MemberSchools
        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        [HttpPost]
        public ActionResult MemberSchools(MemberProfileSchoolViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);

                service.UpdateMemberSchools(model.Member);
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Education);
            }
            model = FormatMemberProfileSchoolsViewModel(model.Member);
            return View(model);
        }

        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        // GET: Profile/MemberNonprofits
        public ActionResult MemberNonprofits(int? id)
        {
            ImpersonateMember(id);
            MemberService service = new MemberService(this.db);
            MemberProfileNonprofits member = service.GetProfileNonprofits(State.MemberId);
            MemberProfileNonprofitViewModel model = FormatMemberProfileNonprofitsViewModel(member);
            return View(model);
        }

        // POST: Profile/MemberNonprofits
        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        [HttpPost]
        public ActionResult MemberNonprofits(MemberProfileNonprofitViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);

                service.UpdateMemberNonprofits(model.Member);
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Nonprofits);
            }
            model = FormatMemberProfileNonprofitsViewModel(model.Member);
            return View(model);
        }

        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        // GET: Profile/MemberJobs
        public ActionResult MemberJobs(int? id)
        {
            ImpersonateMember(id);
            MemberService service = new MemberService(this.db);
            MemberProfileJobs member = service.GetProfileJobs(State.MemberId);
            MemberProfileJobViewModel model = FormatMemberProfileJobsViewModel(member);
            return View(model);
        }

        // POST: Profile/MemberJobs
        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        [HttpPost]
        public ActionResult MemberJobs(MemberProfileJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);

                service.UpdateMemberJobs(model.Member);
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Jobs);
            }
            model = FormatMemberProfileJobsViewModel(model.Member);
            return View(model);
        }

        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        // GET: Profile/MemberGovernmentPrograms
        public ActionResult MemberGovernmentPrograms(int? id)
        {
            ImpersonateMember(id);
            MemberService service = new MemberService(this.db);
            MemberProfileGovernmentPrograms member = service.GetProfileGovernmentPrograms(State.MemberId);
            MemberProfileGovernmentProgramViewModel model = FormatMemberProfileGovernmentProgramsViewModel(member);
            return View(model);
        }

        // POST: Profile/MemberGovernmentPrograms
        [HttpPost]
        [Authorize(Roles = "Admin, Nonprofit, CaseManager, Member")]
        public ActionResult MemberGovernmentPrograms(MemberProfileGovernmentProgramViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberService service = new MemberService(this.db);

                service.UpdateMemberGovernmentPrograms(model.Member);
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.GovernmentPrograms);
            }
            model = FormatMemberProfileGovernmentProgramsViewModel(model.Member);
            return View(model);
        }

        private MemberProfilePersonalViewModel FormatMemberProfilePersonalViewModel(MemberProfilePersonal member)
        {
            MemberProfilePersonalViewModel model = new MemberProfilePersonalViewModel
            {
                Member = member,
                GenderList = GetGenderList(),
            };
            return model;
        }

        private MemberProfileFamilyViewModel FormatMemberProfileFamilyViewModel(MemberProfileFamily member)
        {
            MemberProfileFamilyViewModel model = new MemberProfileFamilyViewModel
            {
                Member = PopulateFamilyEmptyFields(member),
                GenderList = GetGenderList(),
                RelationshipList = GetRelationshipList(),
            };
            return model;
        }

        private MemberProfileSchoolViewModel FormatMemberProfileSchoolsViewModel(MemberProfileSchools member)
        {
            MemberProfileSchoolViewModel model = new MemberProfileSchoolViewModel
            {
                Member = PopulateSchoolEmptyFields(member),
                SchoolTypeList = GetSchoolTypeList(),
                DegreeList = GetDegreeList(),
            };
            return model;
        }

        private MemberProfileNonprofitViewModel FormatMemberProfileNonprofitsViewModel(MemberProfileNonprofits member)
        {
            MemberProfileNonprofitViewModel model = new MemberProfileNonprofitViewModel
            {
                Member = PopulateNonprofitEmptyFields(member),
            };
            return model;
        }

        private MemberProfileJobViewModel FormatMemberProfileJobsViewModel(MemberProfileJobs member)
        {
            MemberProfileJobViewModel model = new MemberProfileJobViewModel
            {
                Member = PopulateJobEmptyFields(member),
            };
            return model;
        }

        private MemberProfileGovernmentProgramViewModel FormatMemberProfileGovernmentProgramsViewModel(MemberProfileGovernmentPrograms member)
        {
            MemberService mService = new MemberService(this.db);
            GovernmentProgramService gService = new GovernmentProgramService(this.db);
            MemberProfileGovernmentProgramViewModel model = new MemberProfileGovernmentProgramViewModel
            {
                Member = PopulateGovernmentProgramEmptyFields(member),
                GovernmentProgramList = gService.Get().ToDictionary(x => x.Id, x => x.Name),
                FamilyList = mService.GetFamily(member.MemberId)?.FamilyMembers?.Where(x => x.Id.HasValue).ToDictionary(x => x.Id.Value, x => x.Name)
            };
            return model;
        }

        private MemberProfileFamily PopulateFamilyEmptyFields(MemberProfileFamily model)
        {
            if (model.Family == null)
                model.Family = new FamilyEntity();
            if (model.Family != null && (model.Family.FamilyMembers == null || model.Family.FamilyMembers.Count() == 0))
                model.Family.FamilyMembers = new FamilyMember[1] { new FamilyMember() };

            return model;
        }

        private MemberProfileSchools PopulateSchoolEmptyFields(MemberProfileSchools model)
        {
            if (model.Schools == null)
                model.Schools = new SchoolEntity[1] { new SchoolEntity() };

            return model;
        }

        private MemberProfileNonprofits PopulateNonprofitEmptyFields(MemberProfileNonprofits model)
        {
            if (model.Nonprofits == null)
                model.Nonprofits = new NonprofitEntity[1] { new NonprofitEntity() };

            return model;
        }

        private MemberProfileJobs PopulateJobEmptyFields(MemberProfileJobs model)
        {
            if (model.Jobs == null || (model.Jobs != null && model.Jobs.Count() == 0))
                model.Jobs = new JobEntity[1] { new JobEntity() };

            return model;
        }

        private MemberProfileGovernmentPrograms PopulateGovernmentProgramEmptyFields(MemberProfileGovernmentPrograms model)
        {
            if (model.GovernmentPrograms == null)
                model.GovernmentPrograms = new GovernmentProgramEntity[1] { new GovernmentProgramEntity() };

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