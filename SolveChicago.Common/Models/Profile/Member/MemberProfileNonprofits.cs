using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common.Models.Profile.Member
{
    public class MemberProfileNonprofits
    {
        public int MemberId { get; set; }
        public NonprofitEntity[] Nonprofits { get; set; }
    }

    public class NonprofitEntity
    {
        public int? NonprofitId { get; set; }
        [Required]
        public string NonprofitName { get; set; }
        public int? CaseManagerId { get; set; }
        public string CaseManagerName { get; set; }
        [Required]
        public string SkillsAcquired { get; set; }
        [Required]
        public string Enjoyed { get; set; }
        [Required]
        public string Struggled { get; set; }
    }
}
