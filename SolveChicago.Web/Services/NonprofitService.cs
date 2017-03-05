using SolveChicago.Web.Common;
using SolveChicago.Web.Data;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Services
{
    public class NonprofitService : BaseService
    {
        public NonprofitService(SolveChicagoEntities db) : base(db) { }

        public NonprofitProfile Get(int id)
        {
            Nonprofit nonprofit = db.Nonprofits.Find(id);
            if (nonprofit == null)
                return null;
            else
            {
                return new NonprofitProfile
                {
                    Id = nonprofit.Id,
                    Address1 = nonprofit.Address1,
                    Address2 = nonprofit.Address2,
                    City = nonprofit.City,
                    Country = nonprofit.Country,
                    Phone = nonprofit.Phone,
                    ProfilePicturePath = nonprofit.ProfilePicturePath,
                    Province = nonprofit.Province,
                    Name = nonprofit.Name,
                };
            }
        }

        public bool Post(NonprofitProfile model)
        {
            Nonprofit nonprofit = db.Nonprofits.Find(model.Id);
            if (nonprofit == null)
                return false;
            else
            {
                if (model.ProfilePicture != null)
                    nonprofit.ProfilePicturePath = UploadPhoto(Constants.Upload.NonprofitPhotos, model.ProfilePicture, model.Id);

                nonprofit.Address1 = model.Address1;
                nonprofit.Address2 = model.Address2;
                nonprofit.City = model.City;
                nonprofit.Country = model.Country;
                nonprofit.Phone = model.Phone;
                nonprofit.Province = model.Province;
                nonprofit.Name = model.Name;

                db.SaveChanges();
                return true;
            }
        }

        public CaseManager[] GetCaseManagers(int id)
        {
            return new CaseManager[0];
            //return db.CaseManagers.Where(x => x.Nonprofits.Select(y => y.Id).Contains(id)).ToArray();
        }
    }
}