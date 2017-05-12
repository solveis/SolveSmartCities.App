using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SolveChicago.Common.Models.Profile.Member;

namespace SolveChicago.Common.Interfaces.Profile.Member
{
    
    public interface IMemberProfileFamily
    {
        int MemberId { get; set; }
        bool IsHeadOfHousehold { get; set; }
        FamilyEntity Family { get; set; }
    }
}
