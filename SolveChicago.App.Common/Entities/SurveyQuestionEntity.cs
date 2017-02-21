using SolveChicago.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SolveChicago.App.Common.Entities
{
    public class SurveyQuestionEntity
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string Question { get; set; }
        public string DataType { get; set; }
        
        public virtual IEnumerable<SurveyAnswerEntity> SurveyAnswers { get; set; }
        public virtual IEnumerable<SurveyQuestionOptionEntity> SurveyQuestionOptions { get; set; }
        public virtual SurveyEntity Survey { get; set; }

        public SurveyQuestionEntity Map(SurveyQuestion model)
        {
            this.Id = model.Id;
            this.SurveyId = model.SurveyId;
            this.Question = model.Question;
            this.DataType = model.DataType;

            this.Survey = new SurveyEntity().Map(model.Survey);
            this.SurveyAnswers = model.SurveyAnswers.Select(x => new SurveyAnswerEntity().Map(x));
            this.SurveyQuestionOptions = model.SurveyQuestionOptions.Select(x => new SurveyQuestionOptionEntity().Map(x));

            return this;
        }
    }
}