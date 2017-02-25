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
//    public class AdminServiceTest
//    {
//        [TestMethod]
//        public void Can_Create_Admin()
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
//            List<Admin> admins = new List<Admin>();

//            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
//            var adminSet = new Mock<DbSet<Admin>>().SetupData(admins);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
//            context.Setup(c => c.Admins).Returns(adminSet.Object);


//            AdminService service = new AdminService(context.Object);
//            int adminId = service.Create(new AdminEntity { Email = email }, 1);

//            Assert.IsNotNull(adminId);
//        }

//        [TestMethod]
//        public void Can_Update_Admin_Profile()
//        {
//            List<Admin> data = new List<Admin>
//            {
//                new Admin { Id = 1 }
//            };

//            AdminEntity admin = new AdminEntity
//            {
//                Address1 = "123 Main Street",
//                Address2 = "Apt 2",
//                City = "Chicago",
//                Province = "IL",
//                Country = "USA",
//                Email = "admin@solvechicago.com",
//                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                Id = 1,
//                Name = "Tom Elliot",
//                Phone = "1234567890",
//                ProfilePicturePath = "../image.jpg"
//            };

//            var adminSet = new Mock<DbSet<Admin>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Admins).Returns(adminSet.Object);

//            using (AdminService service = new AdminService(context.Object))
//            {
//                var result = service.UpdateProfile(admin);
//                Assert.IsTrue(result);
//            }

//        }

//        [TestMethod]
//        public void Can_Get_Admins()
//        {
//            List<Admin> data = new List<Admin>
//            {
//                new Admin
//                {
//                    Id = 2,
//                    Name = "Tom Ford",
//                },
//                new Admin
//                {
//                    Id = 2,
//                    Name = "Eddie Arkansas",
//                },
//                new Admin
//                {
//                    Id = 2,
//                    Name = "Jill Hutherington",
//                }
//            };


//            var set = new Mock<DbSet<Admin>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Admins).Returns(set.Object);

//            using (AdminService service = new AdminService(context.Object))
//            {
//                var result = service.GetAdmins();
//                Assert.AreEqual(3, result.Length);
//            }
//        }
//    }
//}
