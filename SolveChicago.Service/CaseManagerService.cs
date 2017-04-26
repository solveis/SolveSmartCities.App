using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Service
{
    public class CaseManagerService : BaseService
    {
        public CaseManagerService(SolveChicagoEntities db) : base(db) { }

        public Member[] GetMembersForCaseManager(int caseManagerId)
        {
            int? nonprofitId = db.CaseManagers.Single(x => x.Id == caseManagerId).NonprofitId;
            if (nonprofitId.HasValue)
                return db.CaseManagers.Single(x => x.Id == caseManagerId).Nonprofit.NonprofitMembers.Select(x => x.Member).ToArray();
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
                families.Add(service.GetFamily(m));
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
                cm.NonprofitId = npo.Id;
            db.SaveChanges();
        }
    }
}