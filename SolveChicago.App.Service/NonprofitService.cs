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
    }
}
