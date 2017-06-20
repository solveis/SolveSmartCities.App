using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Models.Profile
{
    public class CaseManagerProfile
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
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
        [Required]
        [StringLength(10, ErrorMessage = "Phone cannot be longer than 10 digits.")]
        public string Phone { get; set; }
        public string ProfilePicturePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> NonprofitId { get; set; }
        public string UserId { get; set; }
    }

    public class CaseManagerProfileViewModel
    {
        public CaseManagerProfile CaseManager { get; set; }
        public string[] GenderList { get; set; }
    }
}
