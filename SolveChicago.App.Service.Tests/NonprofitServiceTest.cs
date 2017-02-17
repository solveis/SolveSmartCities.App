using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace SolveChicago.App.Service.Tests
{
    [TestClass]
    public class NonprofitServiceTest
    {
        [TestMethod]
        public void Can_Create_Nonprofit()
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
            List<Nonprofit> nonprofits = new List<Nonprofit>();

            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var nonprofitSet = new Mock<DbSet<Nonprofit>>().SetupData(nonprofits);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.Nonprofits).Returns(nonprofitSet.Object);


            NonprofitService service = new NonprofitService(context.Object);
            int nonprofitId = service.Create(email, 1);

            Assert.IsNotNull(nonprofitId);
        }
    }
}
