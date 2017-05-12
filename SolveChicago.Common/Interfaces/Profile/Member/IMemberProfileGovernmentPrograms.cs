using SolveChicago.Common.Models.Profile.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common.Interfaces.Profile.Member
{

    public interface IMemberProfileGovernmentPrograms
    {
        int MemberId { get; set; }
        int[] GovernmentProgramsIds { get; set; }
        GovernmentProgramEntity[] GovernmentPrograms { get; set; }
    }
}
