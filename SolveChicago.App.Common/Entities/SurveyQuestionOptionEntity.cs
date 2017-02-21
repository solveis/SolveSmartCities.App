using SolveChicago.Entities;

namespace SolveChicago.App.Common.Entities
{
    public class SurveyQuestionOptionEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Option { get; set; }

        public virtual SurveyQuestionEntity SurveyQuestion { get; set; }

        public SurveyQuestionOptionEntity Map(SurveyQuestionOption model)
        {
            this.Id = model.Id;
            this.QuestionId = model.QuestionId;
            this.Option = model.Option;

            this.SurveyQuestion = new SurveyQuestionEntity().Map(model.SurveyQuestion);

            return this;
        }
    }
}