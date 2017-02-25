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
    public class AdminService : BaseService
    {
        public AdminService(SolveChicagoEntities db)
        {
            this.db = db;
        }

        [ExcludeFromCodeCoverage]
        public AdminService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(Admin entity, int? userId = null)
        {
            Admin admin = new Admin
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
            db.Admins.Add(admin);
            if (userId.HasValue)
                admin.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId.Value));
            db.SaveChanges();

            return admin.Id;
        }

        public void Delete(int id)
        {
            Admin entity = db.Admins.Single(x => x.Id == id);
            db.Admins.Remove(entity);

            db.SaveChanges();
        }

        public bool UpdateProfile(Admin model)
        {
            try
            {
                string url = UploadPhoto(Constants.Upload.AdminPhotos, model.ProfilePicture, model.Id);

                Admin admin = db.Admins.Single(x => x.Id == model.Id);
                admin.Address1 = model.Address1;
                admin.Address2 = model.Address2;
                admin.City = model.City;
                admin.Country = model.Country;
                admin.Email = model.Email;
                admin.Name = model.Name;
                admin.Phone = model.Phone;
                admin.Province = model.Province;
                admin.ProfilePicturePath = url;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Admin GetAdmin(int adminId)
        {
            return new Admin().Map(db.Admins.Single(x => x.Id == adminId));
        }

        public Admin[] GetAdmins()
        {
            return db.Admins.Select(x => new Admin().Map(x)).ToArray();
        }

        public bool UserIsAdmin(int userId)
        {
            return db.UserProfiles.Single(x => x.Id == userId).AspNetUser.AspNetRoles.Any(x => x.Name == Constants.Roles.Admin);
        }

        public bool ValidateAdminInvite(string inviteCode)
        {
            int userId = Convert.ToInt32(Crypto.DecryptStringAES(inviteCode, Settings.CryptoSharedSecret));
            if (UserIsAdmin(userId))
                return true;
            else
                return false;
        }
    }
}
