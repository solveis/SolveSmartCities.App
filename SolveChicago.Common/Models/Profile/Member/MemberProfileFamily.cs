using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SolveChicago.Common.Interfaces.Profile.Member;

namespace SolveChicago.Common.Models.Profile.Member
{
    
    public class MemberProfileFamily : IMemberProfileFamily
    {
        public int MemberId { get; set; }
        public bool IsHeadOfHousehold { get; set; }
        public FamilyEntity Family { get; set; }
    }
    public class FamilyEntity
    {
        public int? Id { get; set; }
        public string FamilyName { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public FamilyMember[] FamilyMembers { get; set; }
        public int AverageScore { get; set; }
        public string HeadOfHouseholdProfilePicturePath { get; set; }
        public string Address
        {
            get
            {
                return this.Address1 + " " + this.Address2 + " " + this.City + ", " + this.Province + " " + this.ZipCode;
            }
        }
    }

    public class FamilyMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }
        public string Relation { get; set; }
        public bool? RelationBiological { get; set; }
        public bool? IsMarriageCurrent { get; set; }
        public bool? IsHeadOfHousehold { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePicturePath { get; set; }
        public MemberStage MemberStage { get; set; }
        public string CurrentOccupation { get; set; }
    }
}
