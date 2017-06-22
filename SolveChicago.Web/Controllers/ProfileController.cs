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
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Authorize]
        public ActionResult Index()
        {
            if (State.Roles.Select(x => x.ToString()).Contains(Constants.Roles.Admin))
                return RedirectToAction("Admin");
            else if (State.Roles.Select(x => x.ToString()).Contains(Constants.Roles.Nonprofit))
                return RedirectToAction("Nonprofit");
            else if (State.Roles.Select(x => x.ToString()).Contains(Constants.Roles.CaseManager))
                return RedirectToAction("CaseManager");
            else if (State.Roles.Select(x => x.ToString()).Contains(Constants.Roles.Member))
                return RedirectToAction("MemberPersonal");
            else if (State.Roles.Select(x => x.ToString()).Contains(Constants.Roles.Corporation))
                return RedirectToAction("Corporation");
            else
                throw new ApplicationException("User has no roles assigned");


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
                    return UpdateSurveyStatus(model.Member.Id, Constants.Member.SurveyStep.Family);
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
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Education);
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
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Jobs);
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
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.GovernmentPrograms);
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
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Nonprofits);
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
                return UpdateSurveyStatus(model.Member.MemberId, Constants.Member.SurveyStep.Complete);
            }
            model = FormatMemberProfileGovernmentProgramsViewModel(model.Member);
            return View(model);
        }

        // GET: Profile/MemberOverview
        public ActionResult MemberOverview(int? memberId)
        {
            ImpersonateMember(memberId);
            MemberService service = new MemberService(this.db);
            MemberProfile model = service.Get(State.MemberId);
            return View(model);
        }

        private MemberProfilePersonalViewModel FormatMemberProfilePersonalViewModel(MemberProfilePersonal member)
        {
            MemberProfilePersonalViewModel model = new MemberProfilePersonalViewModel
            {
                Member = member,
                GenderList = GetGenderList(),
                MilitaryBranchList = GetMilitaryBranchList(),
                InterestList = GetInterestList(),
                CountryList = GetCountryList(),
            };
            return model;
        }

        private string[] GetCountryList()
        {
            return new string[] { "USA" };
        }

        public string[] GetInterestList()
        {
            return db.Interests.Select(x => x.Name).ToArray();
        }

        private Dictionary<int, string> GetMilitaryBranchList()
        {
            return db.MilitaryBranches.ToDictionary(x => x.Id, x => x.BranchName);
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
                SchoolsList = GetSchoolsList(),
            };
            return model;
        }

        private string[] GetSchoolsList()
        {
            return db.Schools.Select(x => x.SchoolName).ToArray();
        }

        private MemberProfileNonprofitViewModel FormatMemberProfileNonprofitsViewModel(MemberProfileNonprofits member)
        {
            MemberProfileNonprofitViewModel model = new MemberProfileNonprofitViewModel
            {
                Member = PopulateNonprofitEmptyFields(member),
                SkillsList = GetAvailableSkillsList(),
            };
            return model;
        }

        private Dictionary<int, string> GetAvailableSkillsList()
        {
            Dictionary<int, string> availableSkills = db.Skills.Where(x => x.NonprofitSkills.Count() > 0 && x.Name != Constants.Skills.SoftSkills).ToDictionary(x => x.Id, x => x.Name);

            return availableSkills;
        }

        private string[] GetNonprofitsList()
        {
            return db.Nonprofits.Select(x => x.Name).ToArray();
        }

        private MemberProfileJobViewModel FormatMemberProfileJobsViewModel(MemberProfileJobs member)
        {
            MemberProfileJobViewModel model = new MemberProfileJobViewModel
            {
                Member = PopulateJobEmptyFields(member),
                CorporationList = GetCorporationList(),
            };
            return model;
        }

        private string[] GetCorporationList()
        {
            return db.Corporations.Select(x => x.Name).ToArray();
        }

        private MemberProfileGovernmentProgramViewModel FormatMemberProfileGovernmentProgramsViewModel(MemberProfileGovernmentPrograms member)
        {
            MemberService mService = new MemberService(this.db);
            GovernmentProgramService gService = new GovernmentProgramService(this.db);
            MemberProfileGovernmentProgramViewModel model = new MemberProfileGovernmentProgramViewModel
            {
                Member = PopulateGovernmentProgramEmptyFields(member),
                IsUtilizingGovernmentPrograms = member.GovernmentPrograms != null && member.GovernmentPrograms.Count() > 0,
                GovernmentProgramList = gService.Get().ToDictionary(x => x.Id, x => x.Name),
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
            return model;
        }

        private MemberProfileJobs PopulateJobEmptyFields(MemberProfileJobs model)
        {
            if (model.Jobs == null || (model.Jobs != null && model.Jobs.Count() == 0))
                model.Jobs = new JobEntity[1] { new JobEntity() { IsCurrent = true } };

            return model;
        }

        private MemberProfileGovernmentPrograms PopulateGovernmentProgramEmptyFields(MemberProfileGovernmentPrograms model)
        {
            if (model.GovernmentPrograms == null)
                model.GovernmentProgramsIds = new int[0];

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
                Constants.School.Degrees.None,
                Constants.School.Degrees.HSDiploma,
                Constants.School.Degrees.GED,
                Constants.School.Degrees.Certificate,
                Constants.School.Degrees.AssociateOfArts,
                Constants.School.Degrees.BachelorsDegree,
                Constants.School.Degrees.MastersDegree,
                Constants.School.Degrees.PostGraduateDegree,
            };
        }

        private string[] GetSchoolTypeList()
        {
            return new string[]
            {
                Constants.School.Types.ElementarySchool,
                Constants.School.Types.MiddleSchool,
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
        [Authorize(Roles = "Admin, Nonprofit, CaseManager")]
        public ActionResult CaseManager(int? id)
        {
            ImpersonateCaseManager(id);
            CaseManagerService service = new CaseManagerService(this.db);
            CaseManagerProfile model = service.Get(State.CaseManagerId);
            CaseManagerProfileViewModel viewModel = FormatCaseManagerProfileViewModel(model);
            return View(viewModel);
        }

        private CaseManagerProfileViewModel FormatCaseManagerProfileViewModel(CaseManagerProfile model)
        {
            CaseManagerProfileViewModel viewModel = new CaseManagerProfileViewModel
            {
                CaseManager = model,
                GenderList = GetGenderList(),
            };
            return viewModel;
        }

        // POST: Profile/CaseManager
        [Authorize(Roles = "Admin, Nonprofit, CaseManager")]
        [HttpPost]
        public ActionResult CaseManager(CaseManagerProfileViewModel model)
        {
            ImpersonateCaseManager(model.CaseManager.Id);
            if (ModelState.IsValid)
            {
                CaseManagerService service = new CaseManagerService(this.db);
                service.Post(model.CaseManager);
            }
            return RedirectToAction("Index", "CaseManagers", new { caseManagerId = State.CaseManagerId });
        }

        // GET: Profile/Nonprofit
        [Authorize(Roles = "Admin, Nonprofit")]
        public ActionResult Nonprofit(int? id)
        {
            ImpersonateNonprofit(id);
            NonprofitService service = new NonprofitService(this.db);
            NonprofitProfile model = service.Get(State.NonprofitId);
            model.CountryList = GetCountryList();
            return View(model);
            
        }

        // POST: Profile/Nonprofit
        [Authorize(Roles = "Admin, Nonprofit")]
        [HttpPost]
        public ActionResult Nonprofit(NonprofitProfile model)
        {
            ImpersonateNonprofit(model.Id);
            if (ModelState.IsValid)
            {
                NonprofitService service = new NonprofitService(this.db);
                service.Post(model);
            }
            return RedirectToAction("Index", "Nonprofits", new { nonprofitId = State.NonprofitId });
        }

        // GET: Profile/Corporation
        [Authorize(Roles = "Admin, Corporation")]
        public ActionResult Corporation(int? id)
        {
            ImpersonateCorporation(id);
            Corporation model = State.Corporation;
            return View(model);
        }

        // POST: Profile/Corporation
        [Authorize(Roles = "Admin, Corporation")]
        [HttpPost]
        public ActionResult Corporation(Corporation model)
        {
            ImpersonateCorporation(model.Id);
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Corporations", new { corporationId = State.CorporationId });
        }

        // GET: Profile/Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            AdminService service = new AdminService(this.db);
            AdminProfile model = service.Get(State.AdminId);
            return View(model);
        }

        // POST: Profile/Admin
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Admin(AdminProfile model)
        {
            if (ModelState.IsValid)
            {
                AdminService service = new AdminService(this.db);
                service.Post(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Admins");
        }
    }
}