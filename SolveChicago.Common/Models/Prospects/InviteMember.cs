using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common.Models.Prospects
{
    public class InviteMember
    {
        public int NonprofitId { get; set; }
        public string NonprofitName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        [Required]
        public int? ProgramId { get; set; }
        public Dictionary<int, string> ProgramList { get; set; }
    }
}
