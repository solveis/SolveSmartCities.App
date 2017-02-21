using SolveChicago.Entities;

namespace SolveChicago.App.Common.Entities
{
    public class SurveyAnswerEntity
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public int MemberSurveyId { get; set; }
        public int UserId { get; set; }

        public virtual MemberSurveyEntity MemberSurvey { get; set; }
        public virtual SurveyQuestionEntity SurveyQuestion { get; set; }
        public virtual SurveyEntity Survey { get; set; }
        public virtual UserProfileEntity UserProfile { get; set; }

        public SurveyAnswerEntity Map(SurveyAnswer model)
        {
            this.Id = model.Id;
            this.SurveyId = model.SurveyId;
            this.QuestionId = model.QuestionId;
            this.Answer = model.Answer;
            this.MemberSurveyId = model.MemberSurveyId;
            this.UserId = model.UserId;

            this.MemberSurvey = new MemberSurveyEntity().Map(model.MemberSurvey);
            this.SurveyQuestion = new SurveyQuestionEntity().Map(model.SurveyQuestion);
            this.Survey = new SurveyEntity().Map(model.Survey);
            this.UserProfile = new UserProfileEntity().Map(model.UserProfile);

            return this;
        }
    }
}