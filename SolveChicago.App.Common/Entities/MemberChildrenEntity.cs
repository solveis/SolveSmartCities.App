using SolveChicago.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SolveChicago.App.Common.Entities
{
    public class MemberChildrenEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MemberId { get; set; }
        public System.DateTime BirthDate { get; set; }

        public virtual MemberEntity Member { get; set; }
        public virtual IEnumerable<MemberChildrenGovernmentProgramEntity> MemberChildrenGovernmentPrograms { get; set; }

        public MemberChildrenEntity Map(MemberChildren model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.MemberId = model.MemberId;
            this.BirthDate = model.BirthDate;

            this.Member = new MemberEntity().Map(model.Member);
            this.MemberChildrenGovernmentPrograms = model.MemberChildrenGovernmentPrograms.Select(x => new MemberChildrenGovernmentProgramEntity().Map(x));

            return this;
        }
    }
}