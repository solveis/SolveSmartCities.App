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
//    public class CaseManagerServiceTest
//    {
//        [TestMethod]
//        public void Can_Create_CaseManager()
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
//            List<CaseManager> caseManagers = new List<CaseManager>();

//            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
//            var caseManagerSet = new Mock<DbSet<CaseManager>>().SetupData(caseManagers);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
//            context.Setup(c => c.CaseManagers).Returns(caseManagerSet.Object);


//            CaseManagerService service = new CaseManagerService(context.Object);
//            int caseManagerId = service.Create(new CaseManagerEntity { Email = email }, 1);

//            Assert.IsNotNull(caseManagerId);
//        }

//        [TestMethod]
//        public void Can_Update_CaseManager_Profile()
//        {
//            List<CaseManager> data = new List<CaseManager>
//            {
//                new CaseManager { Id = 1 }
//            };

//            CaseManagerEntity caseManager = new CaseManagerEntity
//            {
//                Address1 = "123 Main Street",
//                Address2 = "Apt 2",
//                City = "Chicago",
//                Province = "IL",
//                Country = "USA",
//                Email = "caseManager@solvechicago.com",
//                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                Id = 1,
//                Name = "Tom Elliot",
//                Phone = "1234567890",
//                ProfilePicturePath = "../image.jpg"
//            };

//            var caseManagerSet = new Mock<DbSet<CaseManager>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.CaseManagers).Returns(caseManagerSet.Object);

//            using (CaseManagerService service = new CaseManagerService(context.Object))
//            {
//                var result = service.UpdateProfile(caseManager);
//                Assert.IsTrue(result);
//            }

//        }
//    }
//}
