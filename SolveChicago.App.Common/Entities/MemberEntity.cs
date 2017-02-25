using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SolveChicago.App.Common.Entities
{
    public class MemberEntity : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Required]
        public string ProfilePicturePath { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Email { get; set; }

        public virtual IEnumerable<CaseManagerEntity> CaseManagers { get; set; }
        public virtual IEnumerable<CaseNoteEntity> CaseNotes { get; set; }
        public virtual IEnumerable<MemberChildrenEntity> MemberChildrens { get; set; }
        public virtual IEnumerable<MemberChildrenGovernmentProgramEntity> MemberChildrenGovernmentPrograms { get; set; }
        public virtual IEnumerable<MemberCorporationEntity> MemberCorporations { get; set; }
        public virtual IEnumerable<MemberEmergencyContactEntity> MemberEmergencyContacts { get; set; }
        public virtual IEnumerable<MemberGovernmentProgramEntity> MemberGovernmentPrograms { get; set; }
        public virtual IEnumerable<MemberNonprofitEntity> MemberNonprofits { get; set; }
        public virtual IEnumerable<MemberStatusEntity> MemberStatuses { get; set; }
        public virtual IEnumerable<MemberSurveyEntity> MemberSurveys { get; set; }
        public virtual IEnumerable<OutcomeEntity> Outcomes { get; set; }
        public virtual IEnumerable<UserProfileEntity> UserProfiles { get; set; }
        
        public MemberEntity Map(Member model)
        {
            this.Id = model.Id;
            this.Address1 = model.Address1;
            this.Address2 = model.Address2;
            this.City = model.City;
            this.Country = model.Country;
            this.CreatedDate = model.CreatedDate;
            this.Email = model.Email;
            this.Name = model.Name;
            this.Phone = model.Phone;
            this.ProfilePicturePath = model.ProfilePicturePath;
            this.Province = model.Province;

            this.CaseManagers = model.MemberNonprofits.Select(x => new CaseManagerEntity().Map(x.CaseManager));
            this.CaseNotes = model.CaseNotes.Select(x => new CaseNoteEntity().Map(x));
            this.MemberChildrens = model.MemberChildrens.Select(x => new MemberChildrenEntity().Map(x));
            this.MemberChildrenGovernmentPrograms = model.MemberChildrenGovernmentPrograms.Select(x => new MemberChildrenGovernmentProgramEntity().Map(x));
            this.MemberCorporations = model.MemberCorporations.Select(x => new MemberCorporationEntity().Map(x));
            this.MemberEmergencyContacts = model.MemberEmergencyContacts.Select(x => new MemberEmergencyContactEntity().Map(x));
            this.MemberGovernmentPrograms = model.MemberGovernmentPrograms.Select(x => new MemberGovernmentProgramEntity().Map(x));
            this.MemberNonprofits = model.MemberNonprofits.Select(x => new MemberNonprofitEntity().Map(x));
            this.MemberStatuses = model.MemberStatuses.Select(x => new MemberStatusEntity().Map(x));
            this.MemberSurveys = model.MemberSurveys.Select(x => new MemberSurveyEntity().Map(x));
            this.Outcomes = model.Outcomes.Select(x => new OutcomeEntity().Map(x));
            this.UserProfiles = model.UserProfiles.Select(x => new UserProfileEntity().Map(x));

            return this;
        }

        //public bool UpdateProfile()
        //{
        //    try
        //    {
        //        Member member = db.Members.Single(x => x.Id == this.Id);

        //        if (this.ProfilePicture != null)
        //        {
        //            //update photo
        //            //set profile picture path
        //        }

        //        member.Address1 = this.Address1;
        //        member.Address2 = this.Address2;
        //        member.City = this.City;
        //        member.Country = this.Country;
        //        member.Email = this.Email;
        //        member.Name = this.Name;
        //        member.Phone = this.Phone;
        //        member.Province = this.Province;

        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        
    }
}
