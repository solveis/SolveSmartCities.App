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
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
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
        public string Address
        {
            get
            {
                return this.Address1 + " " + this.Address2 + " " + this.City + ", " + this.Province + " " + this.Country;
            }
        }
        public bool IsHeadOfHousehold { get; set; }

        public string ProfilePicturePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Required]
        public string Interests { get; set; }
        public decimal? Income { get; set; }
        public bool? IsMilitary { get; set; }
        public string ContactPreference { get; set; }
        public string HighestEducation
        {
            get
            {
                return this.Schools != null && this.Schools.Any(x => x.End.HasValue) ? this.Schools.Where(x => x.End.HasValue).OrderByDescending(x => x.Start).First().Type : "-";
            }
        }
        public string Degree {
            get
            {
                return this.Schools != null && this.Schools.Any(x => !string.IsNullOrEmpty(x.Degree)) ? this.Schools.Where(x => !string.IsNullOrEmpty(x.Degree)).OrderByDescending(x => x.Start).First().Degree : "-";
            }
        }
        public string LastSchool
        {
            get
            {
                return this.Schools != null && this.Schools.Any() ? this.Schools.OrderByDescending(x => x.Start).First().Name : "-";
            }
        }
        public MilitaryEntity[] Military { get; set; }
        public FamilyEntity Family { get; set; }
        public SchoolEntity[] Schools { get; set; }
        public NonprofitEntity[] Nonprofits { get; set; }
        public JobEntity[] Jobs { get; set; }
        public GovernmentProgramEntity[] GovernmentPrograms { get; set; }
        public MemberStage MemberStage { get; set; }
        public string CurrentOccupation
        {
            get
            {
                return this.MemberStage.Stage == Constants.Member.Stage.InTraining && this.Nonprofits.Any() ? this.Nonprofits.OrderByDescending(x => x.Start).FirstOrDefault().NonprofitName : this.MemberStage.Stage == Constants.Member.Stage.JobPlaced && this.Jobs.Any() ? this.Jobs.OrderByDescending(x => x.EmployeeStart).FirstOrDefault().Name : "-";
            }
        }
    }

    public class MemberStage
    {
        public string Stage { get; set; }
        public int Percent { get; set; }
    }
}