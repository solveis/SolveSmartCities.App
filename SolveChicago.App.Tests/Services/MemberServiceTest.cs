using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.App.Service;
using SolveChicago.App.Common.Entities;

namespace SolveChicago.App.Tests.Services
{
    [TestClass]
    public class MemberServiceTest
    {
        [TestMethod]
        public void Can_Create_Member()
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
            List<Member> members = new List<Member>();

            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var memberSet = new Mock<DbSet<Member>>().SetupData(members);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.Members).Returns(memberSet.Object);


            MemberService service = new MemberService(context.Object);
            int memberId = service.Create(email, 1);

            Assert.IsNotNull(memberId);
        }

        [TestMethod]
        public void Can_Update_Member_Profile()
        {
            List<Member> data = new List<Member>
            {
                new Member { Id = 1 }
            };

            MemberEntity member = new MemberEntity
            {
                Address1 = "123 Main Street",
                Address2 = "Apt 2",
                City = "Chicago",
                Province = "IL",
                Country = "USA",
                Email = "member@solvechicago.com",
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                Id = 1,
                Name = "Tom Elliot",
                Phone = "1234567890",
                ProfilePicturePath = "../image.jpg"
            };

            var memberSet = new Mock<DbSet<Member>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Members).Returns(memberSet.Object);

            using (MemberService service = new MemberService(context.Object))
            {
                var result = service.UpdateProfile(member);
                Assert.IsTrue(result);
            }

        }
    }
}
