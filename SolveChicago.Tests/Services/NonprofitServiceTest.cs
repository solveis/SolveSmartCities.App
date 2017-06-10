using System;
using Xunit;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SolveChicago.Service;
using SolveChicago.Web.Models.Profile;
using SolveChicago.Common.Models.Profile.Member;

namespace SolveChicago.Tests.Services
{
    public class NonprofitServiceTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();
        public NonprofitServiceTest()
        {
            List<Nonprofit> data = new List<Nonprofit>
            {
                new Nonprofit
                {
                    Id = 1,
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            Address1 = "123 Main Stree",
                            Address2 = null,
                            City = "Chicago",
                            Country = "USA",
                            Province = "Illinois",
                            ZipCode = "60245"
                        }
                    },
                    PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                        {
                            Number = "1234567890",
                        }
                    },
                    ProfilePicturePath = "../path.jpg",
                    Name = "Test Nonprofit",
                    CaseManagers = new List<CaseManager>
                    {
                        new CaseManager { Id = 1, FirstName = "Tim", LastName = "Keller" },
                        new CaseManager { Id = 2, FirstName = "Esme", LastName = "Pirouet" },
                    },
                    NonprofitMembers = new List<NonprofitMember>
                    {
                        new NonprofitMember
                        {
                            MemberId = 1,
                            Member = new Member(),
                            MemberEnjoyed = "everything",
                            MemberStruggled = "nothing",
                            NonprofitId = 1,
                        },

                        new NonprofitMember
                        {
                            MemberId = 2,
                            Member = new Member(),
                            MemberEnjoyed = "everything",
                            MemberStruggled = "nothing",
                            NonprofitId = 1,
                        },

                        new NonprofitMember
                        {
                            MemberId = 3,
                            Member = new Member(),
                            MemberEnjoyed = "everything",
                            MemberStruggled = "nothing",
                            NonprofitId = 1,
                        }
                    }
                }
            };

            List<NonprofitMember> NonprofitMembers = new List<NonprofitMember>
            {
                new NonprofitMember
                {
                    MemberId = 1,
                    Member = new Member(),
                    MemberEnjoyed = "everything",
                    MemberStruggled = "nothing",
                    NonprofitId = 1,
                },

                new NonprofitMember
                {
                    MemberId = 2,
                    Member = new Member(),
                    MemberEnjoyed = "everything",
                    MemberStruggled = "nothing",
                    NonprofitId = 1,
                },

                new NonprofitMember
                {
                    MemberId = 3,
                    Member = new Member(),
                    MemberEnjoyed = "everything",
                    MemberStruggled = "nothing",
                    NonprofitId = 1,
                }
            };
            List<Skill> skills = new List<Skill>();
            List<Address> addresses = new List<Address>();

            var set = new Mock<DbSet<Nonprofit>>().SetupData(data);
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Nonprofits).Returns(set.Object);

            var NonprofitMemberset = new Mock<DbSet<NonprofitMember>>().SetupData(NonprofitMembers);
            NonprofitMemberset.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => NonprofitMembers.FirstOrDefault(d => d.NonprofitId == (int)ids[0]));
            context.Setup(c => c.NonprofitMembers).Returns(NonprofitMemberset.Object);

            var skillSet = new Mock<DbSet<Skill>>().SetupData(skills);
            skillSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => skills.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Skills).Returns(skillSet.Object);

            var addressSet = new Mock<DbSet<Address>>().SetupData(addresses);
            addressSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => addresses.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Addresses).Returns(addressSet.Object);
        }

        [Fact]
        public void NonprofitService_Get_ReturnsNonprofitProfile()
        {
            NonprofitService service = new NonprofitService(context.Object);
            NonprofitProfile npo = service.Get(1);

            Assert.Equal("Test Nonprofit", npo.Name);
        }

        [Fact]
        public void NonprofitService_Get_ReturnsNull()
        {
            NonprofitService service = new NonprofitService(context.Object);
            NonprofitProfile npo = service.Get(2);

            Assert.Equal(null, npo);
        }

        [Fact]
        public void NonprofitService_Post_Success()
        {
            NonprofitProfile model = new NonprofitProfile
            {
                Id = 1,
                Name = "Test 2 Nonprofit"
            };


            NonprofitService service = new NonprofitService(context.Object);
            service.Post(model);
            Assert.Equal("Test 2 Nonprofit", context.Object.Nonprofits.First().Name);
        }

        [Fact]
        public void NonprofitService_Post_ThrowsException()
        {
            NonprofitProfile model = new NonprofitProfile
            {
                Id = 5,
                Name = "Test 2 Nonprofit"
            };


            NonprofitService service = new NonprofitService(context.Object);
            Assert.Throws<Exception>(() => service.Post(model));
        }

        [Fact]
        public void NonprofitService_GetCaseManagers_ReturnsCaseManagerArray()
        {
            NonprofitService service = new NonprofitService(context.Object);
            CaseManager[] cm = service.GetCaseManagers(1);

            Assert.Equal(2, cm.Count());
        }

        [Fact]
        public void NonprofitService_GetCaseManagers_InvalidId_ReturnsEmptyArray()
        {
            NonprofitService service = new NonprofitService(context.Object);
            CaseManager[] cm = service.GetCaseManagers(10);

            Assert.Equal(0, cm.Count());
        }

        [Fact]
        public void NonprofitService_GetMembers_ReturnsMemberArray()
        {
            NonprofitService service = new NonprofitService(context.Object);
            FamilyEntity[] members = service.GetMembers(1);

            Assert.Equal(3, members.Count());
        }

        [Fact]
        public void NonprofitService_GetMembers_ReturnsEmptyArray()
        {
            NonprofitService service = new NonprofitService(context.Object);
            FamilyEntity[] members = service.GetMembers(10);

            Assert.Equal(0, members.Count());
        }

        [Fact]
        public void NonprofitService_AssignCaseManager_Success()
        {
            NonprofitService service = new NonprofitService(context.Object);
            service.AssignCaseManager(1, 1, 1);

        }

        [Fact]
        public void NonprofitService_AssignCaseManager_ThrowsException()
        {
            NonprofitService service = new NonprofitService(context.Object);
            Assert.Throws<ApplicationException>(() => service.AssignCaseManager(1, 4, 1));

        }

        [Fact]
        public void NonprofitService_UploadClients_Success()
        {
            
        }
    }
}
