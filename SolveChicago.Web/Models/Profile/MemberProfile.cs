using SolveChicago.Entities;
using SolveChicago.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Profile
{
    public class MemberProfileViewModel
    {
        public MemberProfile Member { get; set; }
        public string[] SchoolTypeList { get; set; }
        public string[] RelationshipList { get; set; }
        public string[] DegreeList { get; set; }
        public string[] GenderList { get; set; }
    }

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
        public FamilyEntity Family { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public string ProfilePicturePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Required]
        public string HighestEducation
        {
            get
            {
                return this.Schools != null && this.Schools.Any(x => x.End.HasValue) ? this.Schools.Where(x => x.End.HasValue).OrderByDescending(x => x.Start).First().Type : string.Empty;
            }
        }
        [Required]
        public string Degree {
            get
            {
                return this.Schools != null && this.Schools.Any(x => !string.IsNullOrEmpty(x.Degree)) ? this.Schools.Where(x => !string.IsNullOrEmpty(x.Degree)).OrderByDescending(x => x.Start).First().Degree : string.Empty;
            }
        }
        [Required]
        public string LastSchool
        {
            get
            {
                return this.Schools != null && this.Schools.Any() ? this.Schools.OrderByDescending(x => x.Start).First().Name : string.Empty;
            }
        }
        public SchoolEntity[] Schools { get; set; }
        [Required]
        public string Interests { get; set; }
        [Required]
        public string NonprofitName { get; set; }
        [Required]
        public string NonprofitSkillsAcquired { get; set; }
        [Required]
        public string NonprofitEnjoyed { get; set; }
        [Required]
        public string NonprofitStruggled { get; set; }
        public JobEntity[] Jobs { get; set; }
    }

    public class JobEntity
    {
        public int CorporationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? EmployeeStart { get; set; }
        public DateTime? EmployeeEnd { get; set; }
        [Required]
        public decimal? EmployeePay { get; set; }
        public string EmployeeReasonForLeaving { get; set; }
    }

    public class FamilyEntity
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public FamilyMember[] FamilyMembers { get; set; }
    }

    public class FamilyMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
        public string Relation { get; set; }
        public bool? RelationBiological { get; set; }
        public bool? IsMarriageCurrent { get; set; }
        public bool IsHeadOfHousehold { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public int Id { get; set; }
    }

    public class SchoolEntity
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsCurrent { get; set; }
        public string Degree { get; set; }

    }
}