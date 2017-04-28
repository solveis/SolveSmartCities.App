using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SolveChicago.Common.Models.Profile.Member
{

    public class MemberProfilePersonal
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
        public bool IsMilitary { get; set; }
        public int? MilitaryId { get; set; }
        public MilitaryEntity[] Military { get; set; }
    }

    public class MilitaryEntity
    {
        public int Id { get; set; }
        public string MilitaryBranch { get; set; }
    }
}
