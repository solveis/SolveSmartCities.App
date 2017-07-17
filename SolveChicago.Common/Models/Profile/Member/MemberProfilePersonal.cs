using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SolveChicago.Common.Interfaces.Profile.Member;

namespace SolveChicago.Common.Models.Profile.Member
{

    public class MemberProfilePersonal : IMemberProfilePersonal
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Phone cannot be longer than 10 digits.")]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
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
        [Required]
        public int EthnicityId { get; set; }
        public string Ethnicity { get; set; }
       
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public bool IsHeadOfHousehold { get; set; }
        public string ProfilePicturePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Required]
        public string Interests { get; set; }
        public string Skills { get; set; }
        public int[] SkillsIds { get; set; }
        [Required]
        public decimal? Income { get; set; }
        [Required]
        public bool IsMilitary { get; set; }
        public int? MilitaryId { get; set; }
        public MilitaryEntity[] Military { get; set; }
        [Required]
        public string ContactPreference { get; set; }
    }

    public class MilitaryEntity
    {
        public int Id { get; set; }
        public string MilitaryBranch { get; set; }
    }
}
