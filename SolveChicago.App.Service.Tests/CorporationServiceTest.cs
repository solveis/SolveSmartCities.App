using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace SolveChicago.App.Service.Tests
{
    [TestClass]
    public class CorporationServiceTest
    {
        [TestMethod]
        public void Can_Create_Corporation()
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
            List<Corporation> corporations = new List<Corporation>();

            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var corporationSet = new Mock<DbSet<Corporation>>().SetupData(corporations);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.Corporations).Returns(corporationSet.Object);


            CorporationService service = new CorporationService(context.Object);
            int corporationId = service.Create(email, 1);

            Assert.IsNotNull(corporationId);
        }
    }
}
