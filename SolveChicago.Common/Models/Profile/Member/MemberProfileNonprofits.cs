using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SolveChicago.Common.Enumerations;
using SolveChicago.Common.Interfaces.Profile.Member;
using SolveChicago.Entities;

namespace SolveChicago.Common.Models.Profile.Member
{
    public class MemberProfileNonprofits : IMemberProfileNonprofits
    {
        public int MemberId { get; set; }
        [Required]
        public bool? InterestedInWorkforceSkill { get; set; }
        public int[] SkillsDesiredIds { get; set; }
        public string SkillsDesired { get; set; }
    }

    public class NonprofitEntity
    {
        public int? NonprofitId { get; set; }
        public string NonprofitName { get; set; }
        public CaseManager[] CaseManagers { get; set; }
        public string SkillsAcquired { get; set; }
        public string Enjoyed { get; set; }
        public string Struggled { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Start { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? End { get; set; }
    }
}
