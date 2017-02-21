using SolveChicago.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SolveChicago.App.Common.Entities
{
    public class GovernmentProgramEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual IEnumerable<MemberChildrenGovernmentProgramEntity> MemberChildrenGovernmentPrograms { get; set; }
        public virtual IEnumerable<MemberGovernmentProgramEntity> MemberGovernmentPrograms { get; set; }

        public GovernmentProgramEntity Map(GovernmentProgram model)
        {
            this.Id = model.Id;
            this.Name = model.Name;

            this.MemberChildrenGovernmentPrograms = model.MemberChildrenGovernmentPrograms.Select(x => new MemberChildrenGovernmentProgramEntity().Map(x));
            this.MemberGovernmentPrograms = model.MemberGovernmentPrograms.Select(x => new MemberGovernmentProgramEntity().Map(x));

            return this;
        }
    }
}