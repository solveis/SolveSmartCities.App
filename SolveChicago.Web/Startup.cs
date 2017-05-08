using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SolveChicago.Web.Models;

[assembly: OwinStartupAttribute(typeof(SolveChicago.Web.Startup))]
namespace SolveChicago.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Creating Admin role    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Admin"
                };
                roleManager.Create(role);
            }

            // Creating Community Member role    
            if (!roleManager.RoleExists("Member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Member"
                };
                roleManager.Create(role);

            }

           // Creating Case Manager role    
            if (!roleManager.RoleExists("CaseManager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "CaseManager"
                };
                roleManager.Create(role);

            }

            // Creating Corporation role    
            if (!roleManager.RoleExists("Corporation"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Corporation"
                };
                roleManager.Create(role);

            }

            // Creating Nonprofit role    
            if (!roleManager.RoleExists("Nonprofit"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = "Nonprofit"
                };
                roleManager.Create(role);

            }
        }
    }
}
