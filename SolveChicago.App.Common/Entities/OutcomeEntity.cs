using SolveChicago.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SolveChicago.App.Common.Entities
{
    public class OutcomeEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        
        public virtual IEnumerable<CaseNoteEntity> CaseNotes { get; set; }
        public virtual MemberEntity Member { get; set; }

        public OutcomeEntity Map(Outcome model)
        {
            this.Id = model.Id;
            this.MemberId = model.MemberId;
            this.Name = model.Name;

            this.CaseNotes = model.CaseNotes.Select(x => new CaseNoteEntity().Map(x));
            this.Member = new MemberEntity().Map(model.Member);

            return this;
        }
    }
}