using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Service
{
    public class MemberService : BaseService
    {
        public MemberService(SolveChicagoEntities db)
        {
            this.db = db;
        }
        public MemberService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(string email, int userId)
        {
            Member member = new Member
            {
                Email = email,
                CreatedDate = DateTime.UtcNow,
            };
            db.Members.Add(member);
            member.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId));
            db.SaveChanges();

            return member.Id;
        }
    }
}
