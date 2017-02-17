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
    }
}
