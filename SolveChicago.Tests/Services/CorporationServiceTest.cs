//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using SolveChicago.Entities;
//using System.Data.Entity;
//using System.Collections.Generic;

//using SolveChicago.App.Common.Entities;

//namespace SolveChicago.Tests.Services
//{
//    [TestClass]
//    public class CorporationServiceTest
//    {
//        [TestMethod]
//        public void Can_Create_Corporation()
//        {
//            string email = string.Format("{0}@solvechicago.com", Guid.NewGuid().ToString());

//            List<UserProfile> users = new List<UserProfile>
//            {
//                new UserProfile
//                {
//                    Id = 1,
//                    IdentityUserId = Guid.NewGuid().ToString(),
//                    CreatedDate = DateTime.UtcNow
//                }
//            };
//            List<Corporation> corporations = new List<Corporation>();

//            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
//            var corporationSet = new Mock<DbSet<Corporation>>().SetupData(corporations);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
//            context.Setup(c => c.Corporations).Returns(corporationSet.Object);


//            CorporationService service = new CorporationService(context.Object);
//            int corporationId = service.Create(new CorporationEntity { Email = email }, 1);

//            Assert.IsNotNull(corporationId);
//        }

//        [TestMethod]
//        public void Can_Update_Corporation_Profile()
//        {
//            List<Corporation> data = new List<Corporation>
//            {
//                new Corporation { Id = 1 }
//            };

//            CorporationEntity corporation = new CorporationEntity
//            {
//                Address1 = "123 Main Street",
//                Address2 = "Apt 2",
//                City = "Chicago",
//                Province = "IL",
//                Country = "USA",
//                Email = "corporation@solvechicago.com",
//                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                Id = 1,
//                Name = "Tom Elliot",
//                Phone = "1234567890",
//            };

//            var corporationSet = new Mock<DbSet<Corporation>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Corporations).Returns(corporationSet.Object);

//            using (CorporationService service = new CorporationService(context.Object))
//            {
//                var result = service.UpdateProfile(corporation);
//                Assert.IsTrue(result);
//            }

//        }
//    }
//}
