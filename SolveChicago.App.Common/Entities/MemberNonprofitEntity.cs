using SolveChicago.Entities;
using System;

namespace SolveChicago.App.Common.Entities
{
    public class MemberNonprofitEntity
    {
        public int MemberId { get; set; }
        public int NonprofitId { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }

        public virtual MemberEntity Member { get; set; }
        public virtual NonprofitEntity Nonprofit { get; set; }

        public MemberNonprofitEntity Map(MemberNonprofit model)
        {
            this.MemberId = model.MemberId;
            this.NonprofitId = model.NonprofitId;
            this.Start = model.Start;
            this.End = model.End;

            this.Member = new MemberEntity().Map(model.Member);
            this.Nonprofit = new NonprofitEntity().Map(model.Nonprofit);

            return this;
        }
    }
}