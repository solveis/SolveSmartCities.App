using SolveChicago.Common.Models.Profile.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SolveChicago.Common.Interfaces.Profile.Member
{

    public interface IMemberProfilePersonal
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Gender { get; set; }
        DateTime? Birthday { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string Province { get; set; }
        string Country { get; set; }
        string ZipCode { get; set; }
        bool IsHeadOfHousehold { get; set; }
        string ProfilePicturePath { get; set; }
        HttpPostedFileBase ProfilePicture { get; set; }
        string Interests { get; set; }
        string Skills { get; set; }
        int[] SkillsIds { get; set; }
        decimal? Income { get; set; }
        bool IsMilitary { get; set; }
        int? MilitaryId { get; set; }
        MilitaryEntity[] Military { get; set; }
        string ContactPreference { get; set; }
    }
}
