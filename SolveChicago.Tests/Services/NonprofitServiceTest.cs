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
//    public class NonprofitServiceTest
//    {
//        [TestMethod]
//        public void Can_Create_Nonprofit()
//        {
//            string email = string.Format("{0}@solvechicago.com", Guid.NewGuid().ToString());

//            List<AspNetUser> users = new List<AspNetUser>
//            {
//                new AspNetUser
//                {
//                    Id = 1,
//                    IdentityUserId = Guid.NewGuid().ToString(),
//                    CreatedDate = DateTime.UtcNow
//                }
//            };
//            List<Nonprofit> nonprofits = new List<Nonprofit>();

//            var userSet = new Mock<DbSet<AspNetUser>>().SetupData(users);
//            var nonprofitSet = new Mock<DbSet<Nonprofit>>().SetupData(nonprofits);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.AspNetUsers).Returns(userSet.Object);
//            context.Setup(c => c.Nonprofits).Returns(nonprofitSet.Object);


//            NonprofitService service = new NonprofitService(context.Object);
//            int nonprofitId = service.Create(new NonprofitEntity { Email = email }, 1);

//            Assert.IsNotNull(nonprofitId);
//        }

//        [TestMethod]
//        public void Can_Update_Nonprofit_Profile()
//        {
//            List<Nonprofit> data = new List<Nonprofit>
//            {
//                new Nonprofit { Id = 1 }
//            };

//            NonprofitEntity nonprofit = new NonprofitEntity
//            {
//                Address1 = "123 Main Street",
//                Address2 = "Apt 2",
//                City = "Chicago",
//                Province = "IL",
//                Country = "USA",
//                Email = "nonprofit@solvechicago.com",
//                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                Id = 1,
//                Name = "Tom Elliot",
//                Phone = "1234567890",
//            };

//            var nonprofitSet = new Mock<DbSet<Nonprofit>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Nonprofits).Returns(nonprofitSet.Object);

//            using (NonprofitService service = new NonprofitService(context.Object))
//            {
//                var result = service.UpdateProfile(nonprofit);
//                Assert.IsTrue(result);
//            }

//        }
//    }
//}
