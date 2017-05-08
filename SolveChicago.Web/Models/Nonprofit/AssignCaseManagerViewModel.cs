using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Nonprofit
{
    public class AssignCaseManagerViewModel
    {
        public int NonprofitId { get; set; }
        public string NonprofitName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        [Required]
        public int? CaseManagerId { get; set; }
        public CaseManager[] CaseManagers { get; set; }
    }
}