using SolveChicago.Entities;
using System;

namespace SolveChicago.App.Common.Entities
{
    public class MemberChildrenGovernmentProgramEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MemberChildrenId { get; set; }
        public int GovernmentProgramId { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }

        public virtual GovernmentProgramEntity GovernmentProgram { get; set; }
        public virtual MemberChildrenEntity MemberChildren { get; set; }
        public virtual MemberEntity Member { get; set; }

        public MemberChildrenGovernmentProgramEntity Map(MemberChildrenGovernmentProgram model)
        {
            this.Id = model.Id;
            this.MemberId = model.MemberId;
            this.MemberChildrenId = model.MemberChildrenId;
            this.GovernmentProgramId = model.GovernmentProgramId;
            this.Start = model.Start;
            this.End = model.End;

            this.GovernmentProgram = new GovernmentProgramEntity().Map(model.GovernmentProgram);
            this.MemberChildren = new MemberChildrenEntity().Map(model.MemberChildren);
            this.Member = new MemberEntity().Map(model.Member);

            return this;
        }
    }
}