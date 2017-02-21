using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Common.Entities
{
    public class UserProfileEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastActivityDate { get; set; }
        public Nullable<bool> ReceiveEmail { get; set; }
        public string IdentityUserId { get; set; }

        public virtual IEnumerable<SurveyAnswerEntity> SurveyAnswers { get; set; }
        public virtual IEnumerable<SurveyEntity> Surveys { get; set; }
        public virtual IEnumerable<AdminEntity> Admins { get; set; }
        public virtual IEnumerable<CaseManagerEntity> CaseManagers { get; set; }
        public virtual IEnumerable<CorporationEntity> Corporations { get; set; }
        public virtual IEnumerable<MemberEntity> Members { get; set; }
        public virtual IEnumerable<NonprofitEntity> Nonprofits { get; set; }

        public UserProfileEntity Map(UserProfile model)
        {
            this.Id = model.Id;
            this.UserName = model.UserName;
            this.Name = model.Name;
            this.CreatedDate = model.CreatedDate;
            this.LastActivityDate = model.LastActivityDate;
            this.ReceiveEmail = model.ReceiveEmail;
            this.IdentityUserId = model.IdentityUserId;

            this.SurveyAnswers = model.SurveyAnswers.Select(x => new SurveyAnswerEntity().Map(x));
            this.Surveys = model.Surveys.Select(x => new SurveyEntity().Map(x));
            this.Admins = model.Admins.Select(x => new AdminEntity().Map(x));
            this.CaseManagers = model.CaseManagers.Select(x => new CaseManagerEntity().Map(x));
            this.Corporations = model.Corporations.Select(x => new CorporationEntity().Map(x));
            this.Members = model.Members.Select(x => new MemberEntity().Map(x));
            this.Nonprofits = model.Nonprofits.Select(x => new NonprofitEntity().Map(x));

            return this;
        }
    }
}
