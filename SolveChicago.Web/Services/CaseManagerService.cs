using SolveChicago.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Services
{
    public class CaseManagerService : BaseService
    {
        public CaseManagerService(SolveChicagoEntities db) : base(db) { }

        public Member[] GetMembersForCaseManager(int caseManagerId)
        {
            int nonprofitId = db.CaseManagers.Single(x => x.Id == caseManagerId).Nonprofit.Id;
            return db.CaseManagers.Single(x => x.Id == caseManagerId).MemberNonprofits.Where(x => x.NonprofitId == nonprofitId).Select(x => x.Member).ToArray();
        }
    }
}