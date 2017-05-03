using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common.Models.Profile.Member
{
    
    public class MemberProfileFamily
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
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
