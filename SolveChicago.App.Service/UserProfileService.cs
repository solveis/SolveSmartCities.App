using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Web.Service
{
    public class UserProfileService : BaseService
    {
        public UserProfileService(SolveChicagoEntities db)
        {
            this.db = db;
        }
        public UserProfileService()
        {
            this.db = new SolveChicagoEntities();
        }

        public int Create(string userName, string identityUserid)
        {
            UserProfile user = new UserProfile
            {
                UserName = userName,
                IdentityUserId = identityUserid,
                CreatedDate = DateTime.UtcNow
            };
            db.UserProfiles.Add(user);
            db.SaveChanges();

            return user.Id;
        }

        public bool UserProfileHasValidMappings(int userId)
        {
            UserProfile profile = db.UserProfiles.Single(x => x.Id == userId);
            return profile.Corporations.Any() || profile.Admins.Any() || profile.Nonprofits.Any() || profile.Members.Any() || profile.CaseManagers.Any();
        }

        public int GetUserIdFromUsername(string userName)
        {
            return db.AspNetUsers.Single(x => x.UserName == userName).UserProfiles.First().Id;
        }

    }
}
