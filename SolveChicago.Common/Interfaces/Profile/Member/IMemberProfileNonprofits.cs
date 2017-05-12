using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SolveChicago.Common.Enumerations;

namespace SolveChicago.Common.Interfaces.Profile.Member
{
    public interface IMemberProfileNonprofits
    {
        int MemberId { get; set; }
        bool? InterestedInWorkforceSkill { get; set; }
        string SkillsDesired { get; set; }
        int[] SkillsDesiredIds { get; set; }
    }
}
