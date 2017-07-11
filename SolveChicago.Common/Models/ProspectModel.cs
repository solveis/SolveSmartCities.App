using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;

namespace SolveChicago.Common.Models
{
    public class ProspectModel
    {
        public MemberProfile Member { get; set; }
        public Nonprofit ReferringNonprofit { get; set; }
        public CaseManager[] ReferringCaseManagers { get; set; }
    }
}
