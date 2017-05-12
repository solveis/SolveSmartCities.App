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
    public class ReferrerService : BaseService
    {
        public ReferrerService(SolveChicagoEntities db) : base(db) { }

        
        public FamilyEntity[] GetMembers(int id)
        {
            List<FamilyEntity> families = new List<FamilyEntity>();
            Referrer referrer = db.Referrers.Find(id);
            MemberService service = new MemberService(this.db);
            if(referrer != null)
            {
                foreach(var m in referrer.Members)
                {
                    families.Add(service.GetFamily(m, true));
                }
            }
            return families.ToArray();
                
        }
    }
}