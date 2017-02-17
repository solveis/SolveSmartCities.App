using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace SolveChicago.App.Service.Tests
{
    [TestClass]
    public class CaseManagerServiceTest
    {
        [TestMethod]
        public void Can_Create_CaseManager()
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
            List<CaseManager> caseManagers = new List<CaseManager>();

            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var caseManagerSet = new Mock<DbSet<CaseManager>>().SetupData(caseManagers);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.CaseManagers).Returns(caseManagerSet.Object);


            CaseManagerService service = new CaseManagerService(context.Object);
            int caseManagerId = service.Create(email, 1);

            Assert.IsNotNull(caseManagerId);
        }
    }
}
