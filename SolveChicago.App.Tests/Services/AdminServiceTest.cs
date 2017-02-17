using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.App.Service;

namespace SolveChicago.App.Tests.Services
{
    [TestClass]
    public class AdminServiceTest
    {
        [TestMethod]
        public void Can_Create_Admin()
        {
            string email = string.Format("{0}@solvechicago.com", Guid.NewGuid().ToString());

            List<UserProfile> users = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    IdentityUserId = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow
                }
            };
            List<Admin> admins = new List<Admin>();

            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var adminSet = new Mock<DbSet<Admin>>().SetupData(admins);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.Admins).Returns(adminSet.Object);


            AdminService service = new AdminService(context.Object);
            int adminId = service.Create(email, 1);

            Assert.IsNotNull(adminId);
        }
    }
}
