using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Service
{
    public class CaseManagerService : BaseService
    {
        public CaseManagerService(SolveChicagoEntities db)
        {
            this.db = db;
        }
        public CaseManagerService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(string email, int userId)
        {
            CaseManager caseManager = new CaseManager
            {
                Email = email,
                CreatedDate = DateTime.UtcNow,
            };
            db.CaseManagers.Add(caseManager);
            caseManager.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId));
            db.SaveChanges();

            return caseManager.Id;
        }
    }
}
