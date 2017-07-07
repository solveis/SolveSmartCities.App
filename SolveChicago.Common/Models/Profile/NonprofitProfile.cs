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
        public string[] CountryList { get; set; }
        [Required]
        public string Type { get; set; }
        public string[] TypeList { get; set; }
        public bool? HasPrograms { get; set; }
        public Program[] Programs { get; set; }
        public string[] WorkforceSkillsList { get; set; }
        public string[] SoftSkillsList { get; set; }
        public string[] GenderList { get; set; }
    }

    public class Program
    {
        public int? Id { get; set; }
        public int NonprofitId { get; set; }
        public string NonprofitName { get; set; }
        public string ProgramName { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string Gender { get; set; }
        public int? EthnicityId { get; set; }
        public string Ethnicity { get; set; }
        public Dictionary<int, string> EthnicityList { get; set; } 
        public string WorkforceSkillsOffered { get; set; }
        public string SoftSkillsOffered { get; set; }
    }
}