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
    public class NonprofitService : BaseService
    {
        public NonprofitService(SolveChicagoEntities db)
        {
            this.db = db;
        }

        [ExcludeFromCodeCoverage]
        public NonprofitService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(NonprofitEntity entity, int? userId = null)
        {
            Nonprofit nonprofit = new Nonprofit
            {
                Address1 = entity.Address1,
                Address2 = entity.Address2,
                City = entity.City,
                Country = entity.Country,
                CreatedDate = DateTime.UtcNow,
                Email = entity.Email,
                Name = entity.Name,
                Phone = entity.Phone,
                Province = entity.Province,
            };
            db.Nonprofits.Add(nonprofit);
            if (userId.HasValue)
                nonprofit.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId.Value));
            db.SaveChanges();

            return nonprofit.Id;
        }

        public void Delete(int id)
        {
            Nonprofit entity = db.Nonprofits.Single(x => x.Id == id);
            db.Nonprofits.Remove(entity);

            db.SaveChanges();
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

        public NonprofitEntity GetNonprofit(int nonprofitId)
        {
            return new NonprofitEntity().Map(db.Nonprofits.Single(x => x.Id == nonprofitId));
        }

        public NonprofitEntity[] GetNonprofits()
        {
            return db.Nonprofits.Select(x => new NonprofitEntity().Map(x)).ToArray();
        }

    }
}
