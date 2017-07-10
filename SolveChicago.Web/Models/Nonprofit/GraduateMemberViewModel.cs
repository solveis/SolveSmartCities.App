using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Nonprofit
{
    public class GraduateMemberViewModel
    {
        public int NonprofitId { get; set; }
        public int MemberId { get; set; }
        public int? ReferredNonprofitId { get; set; }
        public Dictionary<int, string> NonprofitsList { get; set; }
        public GraduateMemberCheckbox[] Skills { get; set; }
        public string JobName { get;set; }
        public decimal JobPay { get; set; }
        public DateTime? Start { get; set; }
    }

    public class GraduateMemberCheckbox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}