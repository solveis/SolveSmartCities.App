using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common.Models.Profile.Member
{

    public class MemberProfileGovernmentPrograms
    {
        public int MemberId { get; set; }
        public GovernmentProgramEntity[] GovernmentPrograms { get; set; }
    }

    public class GovernmentProgramEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public DateTime Start { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime? End { get; set; }
        public decimal? Amount { get; set; }
    }
}
