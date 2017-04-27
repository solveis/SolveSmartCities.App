using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolveChicago.Common.Models.Profile.Member
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
        public bool IsHeadOfHousehold { get; set; }

        public string ProfilePicturePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Required]
        public string Interests { get; set; }
        public decimal? Income { get; set; }
        public bool? IsMilitary { get; set; }
        public string HighestEducation
        {
            get
            {
                return this.Schools != null && this.Schools.Any(x => x.End.HasValue) ? this.Schools.Where(x => x.End.HasValue).OrderByDescending(x => x.Start).First().Type : string.Empty;
            }
        }
        public string Degree {
            get
            {
                return this.Schools != null && this.Schools.Any(x => !string.IsNullOrEmpty(x.Degree)) ? this.Schools.Where(x => !string.IsNullOrEmpty(x.Degree)).OrderByDescending(x => x.Start).First().Degree : string.Empty;
            }
        }
        public string LastSchool
        {
            get
            {
                return this.Schools != null && this.Schools.Any() ? this.Schools.OrderByDescending(x => x.Start).First().Name : string.Empty;
            }
        }
        public MilitaryEntity[] Military { get; set; }
        public FamilyEntity Family { get; set; }
        public SchoolEntity[] Schools { get; set; }
        public NonprofitEntity[] Nonprofits { get; set; }
        public JobEntity[] Jobs { get; set; }
        public GovernmentProgramEntity[] GovernmentPrograms { get; set; }
    }
}