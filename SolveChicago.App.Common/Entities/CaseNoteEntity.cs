using SolveChicago.Entities;
using System;

namespace SolveChicago.App.Common.Entities
{
    public class CaseNoteEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Nullable<int> CaseManagerId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Note { get; set; }
        public Nullable<int> OutcomeId { get; set; }
        public Nullable<decimal> OutcomeWeight { get; set; }

        public virtual CaseManagerEntity CaseManager { get; set; }
        public virtual MemberEntity Member { get; set; }
        public virtual OutcomeEntity Outcome { get; set; }

        public CaseNoteEntity Map(CaseNote model)
        {
            this.Id = model.Id;
            this.MemberId = model.MemberId;
            this.CaseManagerId = model.CaseManagerId;
            this.CreatedDate = model.CreatedDate;
            this.Note = model.Note;
            this.OutcomeId = model.OutcomeId;
            this.OutcomeWeight = model.OutcomeWeight;

            this.CaseManager = new CaseManagerEntity().Map(model.CaseManager);
            this.Member = new MemberEntity().Map(model.Member);
            this.Outcome = new OutcomeEntity().Map(model.Outcome);

            return this;
        }
    }
}