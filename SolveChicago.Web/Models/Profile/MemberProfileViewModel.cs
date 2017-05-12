using SolveChicago.Common.Models.Profile.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Profile
{
    public class MemberProfilePersonalViewModel
    {
        public MemberProfilePersonal Member { get; set; }
        public string[] GenderList { get; set; }
        public Dictionary<int, string> MilitaryBranchList { get; set; }
        public string[] InterestList { get; set; }
        public string[] CountryList { get; set; }
    }

    public class MemberProfileFamilyViewModel
    {
        public MemberProfileFamily Member { get; set; }
        public string[] RelationshipList { get; set; }
        public string[] GenderList { get; set; }
    }

    public class MemberProfileSchoolViewModel
    {
        public MemberProfileSchools Member { get; set; }
        public string[] SchoolTypeList { get; set; }
        public string[] DegreeList { get; set; }
        public string[] SchoolsList { get; set; }
    }

    public class MemberProfileNonprofitViewModel
    {
        public MemberProfileNonprofits Member { get; set; }
        public Dictionary<int, string> SkillsList { get; set; }
    }

    public class MemberProfileJobViewModel
    {
        public MemberProfileJobs Member { get; set; }
        public string[] CorporationList { get; set; }
    }

    public class MemberProfileGovernmentProgramViewModel
    {
        public MemberProfileGovernmentPrograms Member { get; set; }
        public bool IsUtilizingGovernmentPrograms { get; set; }
        public Dictionary<int, string> GovernmentProgramList { get; set; }
    }
}