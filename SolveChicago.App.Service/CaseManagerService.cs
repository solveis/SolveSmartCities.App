using SolveChicago.App.Common.Entities;
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

        public bool UpdateProfile(CaseManagerEntity model)
        {
            try
            {
                CaseManager casemanager = db.CaseManagers.Single(x => x.Id == model.Id);
                casemanager.Address1 = model.Address1;
                casemanager.Address2 = model.Address2;
                casemanager.City = model.City;
                casemanager.Country = model.Country;
                casemanager.Email = model.Email;
                casemanager.Name = model.Name;
                casemanager.Phone = model.Phone;
                casemanager.Province = model.Province;
                casemanager.ProfilePicturePath = model.ProfilePicturePath;

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
