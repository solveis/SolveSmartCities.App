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

        public void MarkAdminInviteCodeAsUsed(string invitingUserId, string inviteCode, string receivingUserId)
        {
            AdminInviteCode invite = db.AdminInviteCodes.Single(x => x.InviteCode == inviteCode);
            invite.RecevingAdminUserId = receivingUserId;
            invite.IsStale = true;

            db.SaveChanges();
        }

        public bool ValidateAdminInvite(string inviteCode, ref string userId)
        {
            try
            {
                if (db.AdminInviteCodes.Any(x => x.InviteCode == inviteCode))
                {
                    userId = db.AdminInviteCodes.Single(x => x.InviteCode == inviteCode && !x.IsStale).InvitingAdminUserId;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}