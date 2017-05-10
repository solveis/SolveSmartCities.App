using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common.Models.Profile.Member
{

    public class MemberProfileJobs
    {
        public int MemberId { get; set; }
        public bool CurrentlyLooking { get; set; }
        public JobEntity[] Jobs { get; set; }
    }


    public class JobEntity
    {
        public int? CorporationId { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EmployeeStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EmployeeEnd { get; set; }
        public decimal? EmployeePay { get; set; }
        public string EmployeeReasonForLeaving { get; set; }
        public bool IsCurrent { get; set; }
    }
}
