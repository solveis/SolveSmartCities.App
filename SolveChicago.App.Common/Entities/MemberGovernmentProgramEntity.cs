using SolveChicago.Entities;
using System;

namespace SolveChicago.App.Common.Entities
{
    public class MemberGovernmentProgramEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int GovernmentProgramId { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }

        public virtual GovernmentProgramEntity GovernmentProgram { get; set; }
        public virtual MemberEntity Member { get; set; }

        public MemberGovernmentProgramEntity Map(MemberGovernmentProgram model)
        {
            this.Id = model.Id;
            this.MemberId = model.MemberId;
            this.GovernmentProgramId = model.GovernmentProgramId;
            this.Start = model.Start;
            this.End = model.End;

            this.GovernmentProgram = new GovernmentProgramEntity().Map(model.GovernmentProgram);
            this.Member = new MemberEntity().Map(model.Member);

            return this;
        }
    }
}