using SolveChicago.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SolveChicago.App.Common.Entities
{
    public class MemberSurveyEntity
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int MemberId { get; set; }

        public virtual MemberEntity Member { get; set; }
        public virtual SurveyEntity Survey { get; set; }
        public virtual IEnumerable<SurveyAnswerEntity> SurveyAnswers { get; set; }

        public MemberSurveyEntity Map(MemberSurvey model)
        {
            this.Id = model.Id;
            this.SurveyId = model.SurveyId;
            this.MemberId = model.MemberId;

            this.Member = new MemberEntity().Map(model.Member);
            this.Survey = new SurveyEntity().Map(model.Survey);
            this.SurveyAnswers = model.SurveyAnswers.Select(x => new SurveyAnswerEntity().Map(x));

            return this;

        }
    }
}