using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace SolveChicago.App.Service.Tests
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
    }
}
