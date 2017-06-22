using SolveChicago.Common;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Service
{
    public class CaseManagerService : BaseService
    {
        public CaseManagerService(SolveChicagoEntities db) : base(db) { }

        public CaseManagerProfile Get(int id)
        {
            CaseManager caseManager = db.CaseManagers.Find(id);
            if (caseManager == null)
                return null;
            else
            {
                return new CaseManagerProfile
                {
                    Id = caseManager.Id,
                    Phone = caseManager.PhoneNumbers.Any() ? caseManager.PhoneNumbers.Last().Number : string.Empty,
                    ProfilePicturePath = caseManager.ProfilePicturePath,
                    FirstName = caseManager.FirstName,
                    LastName = caseManager.LastName,
                    Birthday = caseManager.Birthday,
                    Gender = caseManager.Gender,
                    NonprofitId = caseManager.NonprofitStaffs.Any() ? caseManager.NonprofitStaffs.First().NonprofitId : (int?)null,
                    UserId = caseManager.UserId
                };
            }
        }

        public void Post(CaseManagerProfile model)
        {
            CaseManager caseManager = db.CaseManagers.Find(model.Id);
            if (caseManager == null)
                throw new Exception($"Case Manager with Id of {model.Id} not found.");
            else
            {
                if (model.ProfilePicture != null)
                    caseManager.ProfilePicturePath = UploadPhoto(Constants.Upload.CaseManagerPhotos, model.ProfilePicture, model.Id);
                
                caseManager.FirstName = model.FirstName;
                caseManager.LastName = model.LastName;
                caseManager.Gender = model.Gender;
                caseManager.Birthday = model.Birthday;

                UpdateCaseManagerPhone(model, caseManager);

                db.SaveChanges();
            }
        }

        private void UpdateCaseManagerPhone(CaseManagerProfile model, CaseManager caseManager)
        {
            PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
            if (phone == null)
            {
                phone = new PhoneNumber { Number = model.Phone };
            }
            caseManager.PhoneNumbers.Add(phone);
        }

        public Member[] GetMembersForCaseManager(int caseManagerId)
        {
            CaseManager cm = db.CaseManagers.Single(x => x.Id == caseManagerId);
            int? nonprofitId = cm.NonprofitStaffs.Any() ? cm.NonprofitStaffs.First().NonprofitId : (int?)null;
            if (nonprofitId.HasValue)
                return db.CaseManagers.Single(x => x.Id == caseManagerId).NonprofitStaffs.SelectMany(y => y.NonprofitMembers.Where(x => !x.End.HasValue).Select(x => x.Member)).ToArray();
            else
                return new Member[0];
        }

        public FamilyEntity[] GetFamiliesForCaseManager(int caseManagerId)
        {
            Member[] members = GetMembersForCaseManager(caseManagerId);
            return GroupMembersInFamilies(members);
        }

        public FamilyEntity[] GroupMembersInFamilies(Member[] members)
        {
            MemberService service = new MemberService(this.db);
            List<FamilyEntity> families = new List<FamilyEntity>();
            foreach(Member m in members)
            {
                families.Add(service.GetFamily(m, true));
            }
            return families.ToArray();
        }

        public CaseManager GetCaseManagerByEmail(string email)
        {
            return db.CaseManagers.Single(x => x.Email == email);
        }

        public void AddToNonprofit(int caseManagerId, int nonprofitId)
        {
            Nonprofit npo = db.Nonprofits.Find(nonprofitId);
            CaseManager cm = db.CaseManagers.Find(caseManagerId);
            if (npo != null && cm != null)
                cm.NonprofitStaffs.Add(new NonprofitStaff { NonprofitId = npo.Id });
            db.SaveChanges();
        }
    }
}