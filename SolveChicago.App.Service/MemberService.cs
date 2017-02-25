using SolveChicago.App.Common;
using SolveChicago.App.Common.Entities;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Web.Service
{
    public class MemberService : BaseService
    {
        public MemberService(SolveChicagoEntities db)
        {
            this.db = db;
        }

        [ExcludeFromCodeCoverage]
        public MemberService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(MemberEntity entity, int? userId = null)
        {
            Member member = new Member
            {
                Address1 = entity.Address1,
                Address2 = entity.Address2,
                City = entity.City,
                Country = entity.Country,
                CreatedDate = DateTime.UtcNow,
                Email = entity.Email,
                Name = entity.Name,
                Phone = entity.Phone,
                ProfilePicturePath = entity.ProfilePicturePath,
                Province = entity.Province,
            };
            db.Members.Add(member);
            if(userId.HasValue)
                member.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId.Value));
            db.SaveChanges();

            return member.Id;
        }

        public void Delete(int id)
        {
            Member entity = db.Members.Single(x => x.Id == id);
            db.Members.Remove(entity);

            db.SaveChanges();
        }

        public bool UpdateProfile(MemberEntity model)
        {
            try
            {
                string url = UploadPhoto(Constants.Upload.MemberPhotos, model.ProfilePicture, model.Id);

                Member member = db.Members.Single(x => x.Id == model.Id);
                member.Address1 = model.Address1;
                member.Address2 = model.Address2;
                member.City = model.City;
                member.Country = model.Country;
                member.Email = model.Email;
                member.Name = model.Name;
                member.Phone = model.Phone;
                member.Province = model.Province;
                member.ProfilePicturePath = url;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MemberEntity GetMember(int memberId)
        {
            return new MemberEntity().Map(db.Members.Single(x => x.Id == memberId));
        }

        public MemberEntity[] GetMembers(int? caseManagerId = null, int? nonprofitId = null, int? corporationId = null)
        {
            List<MemberEntity> members = new List<MemberEntity>();
            if (caseManagerId.HasValue)
            {
                return db.CaseManagers.Single(x => x.Id == caseManagerId).MemberNonprofits.Select(x => new MemberEntity().Map(x.Member)).ToArray();
            }
            else if (nonprofitId.HasValue)
            { 
                return db.Nonprofits.Single(x => x.Id == nonprofitId).MemberNonprofits.Select(x => new MemberEntity().Map(x.Member)).ToArray();
            }
            else if (corporationId.HasValue)
            {
                return db.Corporations.Single(x => x.Id == corporationId).MemberCorporations.Select(x => new MemberEntity().Map(x.Member)).ToArray();
            }
            else
            {
                return db.Members.Select(x => new MemberEntity().Map(x)).ToArray();
            }
        }
        
    }
}
