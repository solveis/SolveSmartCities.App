using SolveChicago.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Profile
{
    public class MemberProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public Family Family { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public string ProfilePicturePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        public string HighestEducation { get; set; }
        public string Degree { get; set; }
        public string LastSchool { get; set; }

        public string[] Interests { get; set; }
        public Nonprofit Nonprofit { get; set; }
        public string NonprofitName { get; set; }
        public string[] NonprofitSkillsAcquired { get; set; }
        public string NonprofitEnjoyed { get; set; }
        public string NonprofitStruggled { get; set; }
        public Corporation Employer { get; set; }
        public string EmployerName { get; set; }
        public DateTime? EmployerStart { get; set; }
        public DateTime? EmployerEnd { get; set; }
        public string EmployerReasonForLeaving { get; set; }
        public decimal? EmployerPay { get; set; }

    }
}