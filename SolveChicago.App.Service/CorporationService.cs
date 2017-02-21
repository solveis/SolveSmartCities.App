using SolveChicago.App.Common.Entities;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Service
{
    public class CorporationService : BaseService
    {
        public CorporationService(SolveChicagoEntities db)
        {
            this.db = db;
        }
        public CorporationService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(string email, int userId)
        {
            Corporation corporation = new Corporation
            {
                Email = email,
                CreatedDate = DateTime.UtcNow,
            };
            db.Corporations.Add(corporation);
            corporation.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId));
            db.SaveChanges();

            return corporation.Id;
        }
        
        public bool UpdateProfile(CorporationEntity model)
        {
            try
            {
                Corporation corporation = db.Corporations.Single(x => x.Id == model.Id);
                corporation.Address1 = model.Address1;
                corporation.Address2 = model.Address2;
                corporation.City = model.City;
                corporation.Country = model.Country;
                corporation.Email = model.Email;
                corporation.Name = model.Name;
                corporation.Phone = model.Phone;
                corporation.Province = model.Province;

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
