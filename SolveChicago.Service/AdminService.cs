using SolveChicago.Common;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Service
{
    public class AdminService : BaseService
    {
        public AdminService(SolveChicagoEntities db) : base(db) { }
        
        public AdminProfile Get(int adminId)
        {
            Admin admin = db.Admins.Find(adminId);
            if (admin == null)
                return null;
            else
            {
                return new AdminProfile
                {
                    Id = admin.Id,
                    Phone = admin.PhoneNumbers.Any() ? admin.PhoneNumbers.Last().Number : string.Empty,
                    ProfilePicturePath = admin.ProfilePicturePath,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    UserId = admin.UserId,
                };
            }
        }

        public void Post(AdminProfile model)
        {
            Admin admin = db.Admins.Find(model.Id);
            if (admin == null)
                throw new Exception($"Admin with Id of {model.Id} not found.");
            else
            {
                if (model.ProfilePicture != null)
                    admin.ProfilePicturePath = UploadPhoto(Constants.Upload.AdminPhotos, model.ProfilePicture, model.Id);
                
                admin.FirstName = model.FirstName;
                admin.LastName = model.LastName;

                UpdateAdminPhone(model, admin);

                db.SaveChanges();
            }
        }

        private void UpdateAdminPhone(AdminProfile model, Admin admin)
        {
            PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
            if (phone == null)
            {
                phone = new PhoneNumber { Number = model.Phone };
            }
            admin.PhoneNumbers.Add(phone);
        }


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

        public void MarkAdminInviteCodeAsUsed(string invitingUserId, string inviteCode, string receivingUserEmail)
        {
            try
            {
                AspNetUser user = db.AspNetUsers.Single(x => x.Email == receivingUserEmail);
                AdminInviteCode invite = db.AdminInviteCodes.Single(x => x.InviteCode == inviteCode);
                if (invite.IsStale)
                    throw new ApplicationException($"Invite Code {inviteCode} has already been used.");

                invite.RecevingAdminUserId = user.Id;
                invite.IsStale = true;
                db.SaveChanges();
            }
            catch
            {
                throw new ApplicationException($"Invite Code {inviteCode} is invalid");
            }

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