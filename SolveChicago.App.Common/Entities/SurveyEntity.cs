using SolveChicago.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SolveChicago.App.Common.Entities
{
    public class SurveyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Audience { get; set; }
        public int UserId { get; set; }
        
        public virtual IEnumerable<MemberSurveyEntity> MemberSurveys { get; set; }
        public virtual IEnumerable<SurveyAnswerEntity> SurveyAnswers { get; set; }
        public virtual IEnumerable<SurveyQuestionEntity> SurveyQuestions { get; set; }
        public virtual UserProfileEntity UserProfile { get; set; }

        public SurveyEntity Map(Survey model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Audience = model.Audience;
            this.UserId = model.UserId;

            this.UserProfile = new UserProfileEntity().Map(model.UserProfile);
            this.MemberSurveys = model.MemberSurveys.Select(x => new MemberSurveyEntity().Map(x));
            this.SurveyAnswers = model.SurveyAnswers.Select(x => new SurveyAnswerEntity().Map(x));
            this.SurveyQuestions = model.SurveyQuestions.Select(x => new SurveyQuestionEntity().Map(x));

            return this;
        }
    }
}