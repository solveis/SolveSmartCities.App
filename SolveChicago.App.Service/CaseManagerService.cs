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
    public class CaseManagerService : BaseService
    {
        public CaseManagerService(SolveChicagoEntities db)
        {
            this.db = db;
        }

        [ExcludeFromCodeCoverage]
        public CaseManagerService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(CaseManagerEntity entity, int? userId = null)
        {
            CaseManager caseManager = new CaseManager
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
            db.CaseManagers.Add(caseManager);
            if (userId.HasValue)
                caseManager.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId.Value));
            db.SaveChanges();

            return caseManager.Id;
        }

        public void Delete(int id)
        {
            CaseManager entity = db.CaseManagers.Single(x => x.Id == id);
            db.CaseManagers.Remove(entity);

            db.SaveChanges();
        }

        public bool UpdateProfile(CaseManagerEntity model)
        {
            try
            {
                string url = UploadPhoto(Constants.Upload.CaseManagerPhotos, model.ProfilePicture, model.Id);

                CaseManager caseManager = db.CaseManagers.Single(x => x.Id == model.Id);
                caseManager.Address1 = model.Address1;
                caseManager.Address2 = model.Address2;
                caseManager.City = model.City;
                caseManager.Country = model.Country;
                caseManager.Email = model.Email;
                caseManager.Name = model.Name;
                caseManager.Phone = model.Phone;
                caseManager.Province = model.Province;
                caseManager.ProfilePicturePath = url;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CaseManagerEntity GetCaseManager(int caseManagerId)
        {
            return new CaseManagerEntity().Map(db.CaseManagers.Single(x => x.Id == caseManagerId));
        }

        public CaseManagerEntity[] GetCaseManagers(int? nonprofitId)
        {
            if (nonprofitId.HasValue)
            {
                return db.Nonprofits.Single(x => x.Id == nonprofitId).MemberNonprofits.Select(x => new CaseManagerEntity().Map(x.CaseManager)).ToArray();
            }
            return db.CaseManagers.Select(x => new CaseManagerEntity().Map(x)).ToArray();
        }

    }
}
