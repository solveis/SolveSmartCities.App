using System;
using Moq;
using Xunit;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.Service;
using System.Diagnostics.CodeAnalysis;

namespace SolveChicago.Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class AdminServiceTest
    {
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
    }
}
