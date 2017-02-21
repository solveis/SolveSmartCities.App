using SolveChicago.App.Common.Entities;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.App.Service
{
    public class AdminService : BaseService
    {
        public AdminService(SolveChicagoEntities db)
        {
            this.db = db;
        }
        public AdminService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(string email, int userId)
        {
            Admin admin = new Admin
            {
                Email = email,
                CreatedDate = DateTime.UtcNow,
            };
            db.Admins.Add(admin);
            admin.UserProfiles.Add(db.UserProfiles.Single(x => x.Id == userId));
            db.SaveChanges();

            return admin.Id;
        }

        public bool UpdateProfile(AdminEntity model)
        {
            try
            {
                Admin admin = db.Admins.Single(x => x.Id == model.Id);
                admin.Address1 = model.Address1;
                admin.Address2 = model.Address2;
                admin.City = model.City;
                admin.Country = model.Country;
                admin.Email = model.Email;
                admin.Name = model.Name;
                admin.Phone = model.Phone;
                admin.Province = model.Province;
                admin.ProfilePicturePath = model.ProfilePicturePath;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
