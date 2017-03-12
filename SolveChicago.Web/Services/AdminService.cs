using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Services
{
    public class AdminService : BaseService
    {
        public AdminService(SolveChicagoEntities db) : base(db) { }

        public string GenerateAdminInviteCode(string userId)
        {
            if (db.AspNetUsers.Any(x => x.Id == userId) && db.AspNetUsers.Single(x => x.Id == userId).Admins.Any())
            {
                string inviteCode = Guid.NewGuid().ToString();
                db.AdminInviteCodes.Add(new AdminInviteCode { InviteCode = inviteCode, InvitingAdminUserId = userId });
                db.SaveChanges();

                return inviteCode;
            }
            throw new ApplicationException("You are not allowed to do this");
        }
    }
}