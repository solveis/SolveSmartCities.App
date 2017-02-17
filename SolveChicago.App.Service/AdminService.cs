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
    }
}
