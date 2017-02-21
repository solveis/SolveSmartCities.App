using SolveChicago.Entities;

namespace SolveChicago.App.Common.Entities
{
    public class MemberEmergencyContactEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual MemberEntity Member { get; set; }

        public MemberEmergencyContactEntity Map(MemberEmergencyContact model)
        {
            this.Id = model.Id;
            this.MemberId = model.MemberId;
            this.Name = model.Name;
            this.Phone = model.Phone;
            this.Email = model.Email;

            this.Member = new MemberEntity().Map(model.Member);

            return this;
        }
    }
}