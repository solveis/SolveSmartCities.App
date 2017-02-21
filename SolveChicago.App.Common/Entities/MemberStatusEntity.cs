using SolveChicago.Entities;

namespace SolveChicago.App.Common.Entities
{
    public class MemberStatusEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public virtual MemberEntity Member { get; set; }

        public MemberStatusEntity Map(MemberStatus model)
        {
            this.Id = model.Id;
            this.MemberId = model.MemberId;
            this.Status = model.Status;
            this.CreatedDate = model.CreatedDate;

            this.Member = new MemberEntity().Map(model.Member);

            return this;
        }
    }
}