using SolveChicago.Common;
using SolveChicago.Common.Models.Profile;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Service
{
    public class ReferrerService : BaseService
    {
        public ReferrerService(SolveChicagoEntities db) : base(db) { }

        
        public FamilyEntity[] GetMembers(int id)
        {
            List<FamilyEntity> families = new List<FamilyEntity>();
            Referrer referrer = db.Referrers.Find(id);
            MemberService service = new MemberService(this.db);
            if(referrer != null)
            {
                foreach(var m in referrer.Members)
                {
                    families.Add(service.GetFamily(m, true));
                }
            }
            return families.ToArray();
                
        }
        
        public ReferrerProfile Get(int referrerId)
        {
            Referrer referrer = db.Referrers.SingleOrDefault(x => x.Id == referrerId);
            if (referrer == null)
                return null;
            else
            {
                return new ReferrerProfile
                {
                    Id = referrer.Id,
                    Name = referrer.Name,
                    Email = referrer.Email,
                    Phone = referrer.PhoneNumbers.Any() ? referrer.PhoneNumbers.Last().Number : string.Empty
                };
            }
        }

        public void Post(ReferrerProfile model)
        {
            Referrer referrer = db.Referrers.Find(model.Id);
            if (referrer == null)
                throw new Exception($"Referrer with Id of {model.Id} not found.");
            else
            {
                referrer.Name = model.Name;
                referrer.Email = model.Email;
                UpdateReferrerPhone(model, referrer);

                db.SaveChanges();
            }
        }

        private void UpdateReferrerPhone(ReferrerProfile model, Referrer referrer)
        {
            PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
            if (phone == null)
            {
                phone = new PhoneNumber { Number = model.Phone };
            }
            referrer.PhoneNumbers.Add(phone);
        }
    }
}