using SolveChicago.Entities;
using System;

namespace SolveChicago.App.Common.Entities
{
    public class MemberCorporationEntity
    {
        public int MemberId { get; set; }
        public int CorporationId { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }

        public virtual CorporationEntity Corporation { get; set; }
        public virtual MemberEntity Member { get; set; }

        public MemberCorporationEntity Map(MemberCorporation model)
        {
            this.MemberId = model.MemberId;
            this.CorporationId = model.CorporationId;
            this.Start = model.Start;
            this.End = model.End;

            this.Corporation = new CorporationEntity().Map(model.Corporation);
            this.Member = new MemberEntity().Map(model.Member);

            return this;
        }
    }
}