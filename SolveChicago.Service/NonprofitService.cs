using SolveChicago.Common;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Service
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

        public void Post(NonprofitProfile model)
        {
            Nonprofit nonprofit = db.Nonprofits.Find(model.Id);
            if (nonprofit == null)
                throw new Exception($"Nonprofit with Id of {model.Id} not found.");
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
            }
        }

        public CaseManager[] GetCaseManagers(int id)
        {
            Nonprofit npo = db.Nonprofits.Find(id);
            if (npo == null)
                return new CaseManager[0];
            else
                return npo.CaseManagers.ToArray();
        }

        public Member[] GetMembers(int id)
        {
            Nonprofit npo = db.Nonprofits.Find(id);
            if (npo == null)
                return new Member[0];
            else
                return npo.MemberNonprofits.Select(x => x.Member).ToArray();
        }

        public void AssignCaseManager(int nonprofitId, int memberId, int caseManagerId)
        {
            MemberNonprofit memberNonprofit = db.MemberNonprofits.SingleOrDefault(x => x.NonprofitId == nonprofitId && x.MemberId == memberId);
            if (memberNonprofit == null)
                throw new ApplicationException("No Member-Nonprofit relationship exists between these two entities.");
            else
            {
                memberNonprofit.CaseManagerId = caseManagerId;
                db.SaveChanges();
            }
        }
    }
}