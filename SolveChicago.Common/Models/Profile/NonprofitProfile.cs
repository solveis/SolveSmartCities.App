using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Profile
{
    public class NonprofitProfile
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Phone cannot be longer than 10 digits.")]
        public string Phone { get; set; }
        public string PhoneExtension { get; set; }
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
        public string SkillsOffered { get; set; }
        public string[] SkillsList { get; set; }
        [Required]
        public bool? TeachesSoftSkills { get; set; }
        public string[] CountryList { get; set; }
    }
}