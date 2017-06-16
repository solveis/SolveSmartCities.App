using System;
using Moq;
using Xunit;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.Service;
using System.Diagnostics.CodeAnalysis;
using SolveChicago.Web.Models.Profile;
using System.Linq;

namespace SolveChicago.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class AdminServiceTest
    {
        [Fact]
        public void AdminService_Get_ReturnsAdminProfile()
        {
            List<Admin> data = new List<Admin>
            {
                new Admin
                {
                    Id = 1,
                    UserId = "IOUIGY^*F&%^$SEXTCYTVUGYHIU",
                    CreatedDate = DateTime.UtcNow.AddDays(-7),
                    Email = "test@123.com",
                    FirstName = "Joe",
                    LastName = "Arthur",
                    ProfilePicturePath = "../path.jpg",
                    PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                        {
                            Id = 1,
                            Number = "123456890"
                        },
                        new PhoneNumber
                        {
                            Id = 2,
                            Number = "2345678901"
                        }
                    }
                }
            };


            var context = new Mock<SolveChicagoEntities>();

            var adminSet = new Mock<DbSet<Admin>>().SetupData(data);
            adminSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Admins).Returns(adminSet.Object);

            AdminService service = new AdminService(context.Object);
            AdminProfile profile = service.Get(1);

            Assert.Equal(1, profile.Id);
            Assert.Equal("IOUIGY^*F&%^$SEXTCYTVUGYHIU", profile.UserId);
            Assert.Equal("Joe", profile.FirstName);
            Assert.Equal("Arthur", profile.LastName);
            Assert.Equal("2345678901", profile.Phone);
            Assert.Equal("../path.jpg", profile.ProfilePicturePath);
        }

        [Fact]
        public void AdminService_Post_Success()
        {
            List<Admin> data = new List<Admin>
            {
                new Admin { Id = 1, CreatedDate = DateTime.UtcNow }
            };

            List<PhoneNumber> numbers = new List<PhoneNumber>();

            var context = new Mock<SolveChicagoEntities>();
            
            var adminSet = new Mock<DbSet<Admin>>().SetupData(data);
            adminSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Admins).Returns(adminSet.Object);

            var numberSet = new Mock<DbSet<PhoneNumber>>().SetupData(numbers);
            numberSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => numbers.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.PhoneNumbers).Returns(numberSet.Object);

            AdminProfile model = new AdminProfile
            {
                FirstName = "Thomas",
                LastName = "Jones",
                Phone = "1234567890",
                UserId = "()*(&*^R^TEDRFGH",
                Id = 1
            };

            AdminService service = new AdminService(context.Object);
            service.Post(model);

            Assert.Equal("Thomas", context.Object.Admins.First().FirstName);
            Assert.Equal("Jones", context.Object.Admins.First().LastName);
        }

        [Fact]
        public void AdminService_GenerateAdminInviteCode_ReturnsString()
        {
            string UserId = Guid.NewGuid().ToString();
            List<AspNetUser> users = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = UserId,
                    CreatedDate = DateTime.UtcNow,
                    Admins = new List<Admin>
                    {
                        new Admin { Id = 1 }
                    }
                }
            };
            List<AdminInviteCode> codes = new List<AdminInviteCode>();


            var userSet = new Mock<DbSet<AspNetUser>>().SetupData(users);
            var codeSet = new Mock<DbSet<AdminInviteCode>>().SetupData(codes);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AspNetUsers).Returns(userSet.Object);
            context.Setup(c => c.AdminInviteCodes).Returns(codeSet.Object);


            AdminService service = new AdminService(context.Object);
            string inviteCode = service.GenerateAdminInviteCode(UserId);
            Assert.True(!string.IsNullOrEmpty(inviteCode));
        }

        [Fact]
        public void AdminService_GenerateAdminInviteCode_ThrowsApplicationException()
        {
            string UserId = Guid.NewGuid().ToString();
            List<AspNetUser> users = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = UserId,
                    CreatedDate = DateTime.UtcNow,
                    Admins = new List<Admin>
                    {
                        new Admin { Id = 1 }
                    }
                }
            };
            List<AdminInviteCode> codes = new List<AdminInviteCode>();


            var userSet = new Mock<DbSet<AspNetUser>>().SetupData(users);
            var codeSet = new Mock<DbSet<AdminInviteCode>>().SetupData(codes);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AspNetUsers).Returns(userSet.Object);
            context.Setup(c => c.AdminInviteCodes).Returns(codeSet.Object);


            AdminService service = new AdminService(context.Object);
            Assert.Throws<ApplicationException>(() => service.GenerateAdminInviteCode("ThisIsNotAUserId"));
        }

        [Fact]
        public void AdminService_ValidateAdminInviteCode_ReturnsTrue()
        {
            List<AdminInviteCode> data = new List<AdminInviteCode>
            {
                new AdminInviteCode
                {
                    Id = 1,
                    InviteCode = "InviteCode1234",
                    InvitingAdminUserId = "*&^%$EDFGHJ",
                    IsStale = false,
                }
            };
            
            var dataSet = new Mock<DbSet<AdminInviteCode>>().SetupData(data);
            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AdminInviteCodes).Returns(dataSet.Object);

            AdminService service = new AdminService(context.Object);
            string userId = "";
            bool success = service.ValidateAdminInvite("InviteCode1234", ref userId);

            Assert.True(success);
            Assert.Equal(userId, "*&^%$EDFGHJ");
        }

        [Fact]
        public void AdminService_ValidateAdminInviteCode_ReturnFalseStale()
        {
            List<AdminInviteCode> data = new List<AdminInviteCode>
            {
                new AdminInviteCode
                {
                    Id = 1,
                    InviteCode = "InviteCode1234",
                    InvitingAdminUserId = "*&^%$EDFGHJ",
                    IsStale = true,
                }
            };

            var dataSet = new Mock<DbSet<AdminInviteCode>>().SetupData(data);
            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AdminInviteCodes).Returns(dataSet.Object);

            AdminService service = new AdminService(context.Object);
            string userId = "";
            bool success = service.ValidateAdminInvite("InviteCode1234", ref userId);

            Assert.False(success);
        }

        [Fact]
        public void AdminService_ValidateAdminInviteCode_ReturnFalseBadCode()
        {
            List<AdminInviteCode> data = new List<AdminInviteCode>
            {
                new AdminInviteCode
                {
                    Id = 1,
                    InviteCode = "InviteCode1234",
                    InvitingAdminUserId = "*&^%$EDFGHJ",
                    IsStale = false,
                }
            };

            var dataSet = new Mock<DbSet<AdminInviteCode>>().SetupData(data);
            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AdminInviteCodes).Returns(dataSet.Object);

            AdminService service = new AdminService(context.Object);
            string userId = "";
            bool success = service.ValidateAdminInvite("InviteCode1234567890", ref userId);

            Assert.False(success);
        }

        [Fact]
        public void AdminService_MarkAdminInviteCodeAsUsed_NoExceptions()
        {
            List<AspNetUser> users = new List<AspNetUser>
            {
                new AspNetUser { Id = "*&^%$EDFGHJ", Email = "test@test.com" }
            };
            List<AdminInviteCode> data = new List<AdminInviteCode>
            {
                new AdminInviteCode
                {
                    Id = 1,
                    InviteCode = "InviteCode1234",
                    InvitingAdminUserId = "*&^%$EDFGHJ",
                    IsStale = false,
                }
            };

            var dataSet = new Mock<DbSet<AdminInviteCode>>().SetupData(data);
            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AdminInviteCodes).Returns(dataSet.Object);


            var userSet = new Mock<DbSet<AspNetUser>>().SetupData(users);
            context.Setup(c => c.AspNetUsers).Returns(userSet.Object);

            AdminService service = new AdminService(context.Object);
            service.MarkAdminInviteCodeAsUsed("*&^%$EDFGHJ", "InviteCode1234", "test@test.com");
        }

        [Fact]
        public void AdminService_MarkAdminInviteCodeAsUsed_ThrowsExceptionIsStale()
        {
            List<AdminInviteCode> data = new List<AdminInviteCode>
            {
                new AdminInviteCode
                {
                    Id = 1,
                    InviteCode = "InviteCode1234",
                    InvitingAdminUserId = "*&^%$EDFGHJ",
                    IsStale = true,
                }
            };

            var dataSet = new Mock<DbSet<AdminInviteCode>>().SetupData(data);
            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AdminInviteCodes).Returns(dataSet.Object);

            AdminService service = new AdminService(context.Object);
            Assert.Throws<ApplicationException>(() => service.MarkAdminInviteCodeAsUsed("*&^%$EDFGHJ", "InviteCode1234", "IOUGYTDR%I&^F*G(H"));
        }

        [Fact]
        public void AdminService_MarkAdminInviteCodeAsUsed_ThrowsExceptionBadCode()
        {
            List<AdminInviteCode> data = new List<AdminInviteCode>
            {
                new AdminInviteCode
                {
                    Id = 1,
                    InviteCode = "InviteCode1234",
                    InvitingAdminUserId = "*&^%$EDFGHJ",
                    IsStale = false,
                }
            };

            var dataSet = new Mock<DbSet<AdminInviteCode>>().SetupData(data);
            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.AdminInviteCodes).Returns(dataSet.Object);

            AdminService service = new AdminService(context.Object);
            Assert.Throws<ApplicationException>(() => service.MarkAdminInviteCodeAsUsed("*&^%$EDFGHJ", "InviteCode1234567890", "IOUGYTDR%I&^F*G(H"));
        }
    }
}
