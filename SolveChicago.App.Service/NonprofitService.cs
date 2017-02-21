using SolveChicago.App.Common.Entities;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Service
{
    public class NonprofitService : BaseService
    {
        public NonprofitService(SolveChicagoEntities db)
        {
            this.db = db;
        }
        public NonprofitService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(string email, int userId)
        {
            Nonprofit nonprofit = new Nonprofit
            {
                Email = email,
                CreatedDate = DateTime.UtcNow,
            };
            db.Nonprofits.Add(nonprofit);
            nonprofit.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId));
            db.SaveChanges();

            return nonprofit.Id;
        }

        public bool UpdateProfile(NonprofitEntity model)
        {
            try
            {
                Nonprofit nonprofit = db.Nonprofits.Single(x => x.Id == model.Id);
                nonprofit.Address1 = model.Address1;
                nonprofit.Address2 = model.Address2;
                nonprofit.City = model.City;
                nonprofit.Country = model.Country;
                nonprofit.Email = model.Email;
                nonprofit.Name = model.Name;
                nonprofit.Phone = model.Phone;
                nonprofit.Province = model.Province;

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
