using SolveChicago.App.Common;
using SolveChicago.App.Common.Entities;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

        public bool UpdateProfile(MemberEntity model)
        {
            try
            {
                Member member = db.Members.Single(x => x.Id == model.Id);
                member.Address1 = model.Address1;
                member.Address2 = model.Address2;
                member.City = model.City;
                member.Country = model.Country;
                member.Email = model.Email;
                member.Name = model.Name;
                member.Phone = model.Phone;
                member.Province = model.Province;
                member.ProfilePicturePath = model.ProfilePicturePath;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
