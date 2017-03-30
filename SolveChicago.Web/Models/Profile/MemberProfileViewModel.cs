using SolveChicago.Common.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Profile
{
    public class MemberProfileViewModel
    {
        public MemberProfile Member { get; set; }
        public string[] SchoolTypeList { get; set; }
        public string[] RelationshipList { get; set; }
        public string[] DegreeList { get; set; }
        public string[] GenderList { get; set; }
    }
}