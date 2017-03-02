using SolveChicago.Web.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Profile
{
    public class MemberProfile
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime? Birthday { get; set; }
        public Family Family { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }

        public string ProfilePicturePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Required]
        public string HighestEducation { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string LastSchool { get; set; }
        [Required]
        public string Interests { get; set; }
        public Nonprofit Nonprofit { get; set; }
        [Required]
        public string NonprofitName { get; set; }
        [Required]
        public string NonprofitSkillsAcquired { get; set; }
        [Required]
        public string NonprofitEnjoyed { get; set; }
        [Required]
        public string NonprofitStruggled { get; set; }
        public Corporation Employer { get; set; }
        [Required]
        public string EmployerName { get; set; }
        [Required]
        public DateTime? EmployerStart { get; set; }
        public DateTime? EmployerEnd { get; set; }
        public string EmployerReasonForLeaving { get; set; }
        [Required]
        public decimal? EmployerPay { get; set; }

    }
}