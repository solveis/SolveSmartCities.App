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
    public class CorporationService : BaseService
    {
        public CorporationService(SolveChicagoEntities db)
        {
            this.db = db;
        }

        [ExcludeFromCodeCoverage]
        public CorporationService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(CorporationEntity entity, int? userId = null)
        {
            Corporation corporation = new Corporation
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
            db.Corporations.Add(corporation);
            if (userId.HasValue)
                corporation.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId.Value));
            db.SaveChanges();

            return corporation.Id;
        }

        public void Delete(int id)
        {
            Corporation entity = db.Corporations.Single(x => x.Id == id);
            db.Corporations.Remove(entity);

            db.SaveChanges();
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

        public CorporationEntity GetCorporation(int corporationId)
        {
            return new CorporationEntity().Map(db.Corporations.Single(x => x.Id == corporationId));
        }

        public CorporationEntity[] GetCorporations()
        {
            return db.Corporations.Select(x => new CorporationEntity().Map(x)).ToArray();
        }

    }
}
