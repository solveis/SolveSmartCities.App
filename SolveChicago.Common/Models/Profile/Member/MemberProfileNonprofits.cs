using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SolveChicago.Common.Enumerations;

namespace SolveChicago.Common.Models.Profile.Member
{
    public class MemberProfileNonprofits
    {
        public int MemberId { get; set; }
        public string InterestedInWorkforceSkill { get; set; }
        public NonprofitEntity[] Nonprofits { get; set; }
    }

    public class NonprofitEntity
    {
        public int? NonprofitId { get; set; }
        public string NonprofitName { get; set; }
        public int? CaseManagerId { get; set; }
        public string CaseManagerName { get; set; }
        public string SkillsAcquired { get; set; }
        public string Enjoyed { get; set; }
        public string Struggled { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Start { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? End { get; set; }
    }
}
