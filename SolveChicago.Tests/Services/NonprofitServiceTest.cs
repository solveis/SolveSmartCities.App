using System;
using Xunit;
using Moq;
using SolveChicago.Entities;
using SolveChicago.Common;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SolveChicago.Service;
using SolveChicago.Web.Models.Profile;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Common.Models;

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
                    },
                    PhoneNumbers = new List<PhoneNumber>
                    {
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
            List<CaseManager> caseManagers = new List<CaseManager>
            {
                new CaseManager
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                }
            };
            List<Skill> skills = new List<Skill>();
            List<Address> addresses = new List<Address>();
            List<PhoneNumber> phones = new List<PhoneNumber>();

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

            var caseManagerSet = new Mock<DbSet<CaseManager>>().SetupData(caseManagers);
            caseManagerSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => caseManagers.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.CaseManagers).Returns(caseManagerSet.Object);

            var phoneSet = new Mock<DbSet<PhoneNumber>>().SetupData(phones);
            phoneSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => phones.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.PhoneNumbers).Returns(phoneSet.Object);
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
                Name = "Test 2 Nonprofit",
                Phone = "1234567890",
                PhoneExtension = "3124",
                Address1 = "123 Main Street",
                Address2 = "#2F",
                City = "Chicago",
                Country ="USA",
                Province = "IL",
                SkillsOffered = "manufacturing, cooking",
                TeachesSoftSkills = false,
                ZipCode = "60635",
                ProfilePicturePath = "../pathtoimg.jpg"
            };


            NonprofitService service = new NonprofitService(context.Object);
            service.Post(model);
            Assert.Equal("Test 2 Nonprofit", context.Object.Nonprofits.First().Name);
            Assert.Equal("1234567890", context.Object.Nonprofits.First().PhoneNumbers.First().Number);
            Assert.Equal("3124", context.Object.Nonprofits.First().PhoneNumbers.First().Extension);
            Assert.Equal("123 Main Street", context.Object.Nonprofits.First().Addresses.First().Address1);
            Assert.Equal("#2F", context.Object.Nonprofits.First().Addresses.First().Address2);
            Assert.Equal("Chicago", context.Object.Nonprofits.First().Addresses.First().City);
            Assert.Equal("USA", context.Object.Nonprofits.First().Addresses.First().Country);
            Assert.Equal("IL", context.Object.Nonprofits.First().Addresses.First().Province);
            Assert.False(context.Object.Nonprofits.First().Skills.Any(x => x.Name == Constants.Skills.SoftSkills));
            Assert.Equal("manufacturing, cooking", string.Join(", ", context.Object.Nonprofits.First().Skills.Select(x => x.Name).ToArray()));
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
            List<ClientList> clientList = new List<ClientList>();
            for(int i = 0; i < 200; i++)
            {
                clientList.Add(new ClientList
                {
                    SSN = Guid.NewGuid().ToString(),
                    FirstName = Guid.NewGuid().ToString(),
                    MiddleName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    Gender = "Male",
                    Birthday = new DateTime(1990, 4, 16),
                    IsHeadOfHousehold = true,
                    Income = 30000,
                    IsMilitary = true,
                    MilitaryBranch = "Army Reserve",
                    MilitaryCurrentServiceStatus = "Active",
                    MilitaryLastPayGrade = "E-1",
                    Address1 = Guid.NewGuid().ToString(),
                    Address2 = "",
                    City = Guid.NewGuid().ToString(),
                    State = Guid.NewGuid().ToString(),
                    ZipCode = Guid.NewGuid().ToString(),
                    Country = Guid.NewGuid().ToString(),
                    Skills = string.Format("{0}, {1}, {2}", Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                    Interests = string.Format("{0}, {1}, {2}", Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                    PhoneNumber = Guid.NewGuid().ToString(),
                    Email = string.Format("{0}@{1}.com", Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                });
            }

            List<Member> members = new List<Member>();
            List<Interest> interests = new List<Interest>();
            List<Skill> skills = new List<Skill>();
            List<Address> addresses = new List<Address>();
            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
            List<Family> families = new List<Family>();
            List<Nonprofit> nonprofits = new List<Nonprofit>
            {
                new Nonprofit { Id = 1, Name = "My NPO" }
            };
            List <MemberMilitary> memberMilitary = new List<MemberMilitary>();
            List<MilitaryBranch> militaryBranch = new List<MilitaryBranch>
            {
                new MilitaryBranch { Id = 13,    Country = "USA",    BranchName = "Air Force" },
                new MilitaryBranch { Id = 14,   Country = "USA",    BranchName = "Air Force Reserve" },
                new MilitaryBranch { Id = 15,   Country = "USA",    BranchName = "Air National Guard" },
                new MilitaryBranch { Id = 16,   Country = "USA",    BranchName = "Army" },
                new MilitaryBranch { Id = 17,   Country = "USA",    BranchName = "Army Reserve" },
                new MilitaryBranch { Id = 18,   Country = "USA",    BranchName = "Army National Guard" },
                new MilitaryBranch { Id = 19,   Country = "USA",    BranchName = "Coast Guard" },
                new MilitaryBranch { Id = 20,   Country = "USA",    BranchName = "Coast Guard Reserve" },
                new MilitaryBranch { Id = 21,   Country = "USA",    BranchName = "Marine Corps" },
                new MilitaryBranch { Id = 22,   Country = "USA",    BranchName = "Marine Corps Reserve" },
                new MilitaryBranch { Id = 23,   Country = "USA",    BranchName = "Navy" },
                new MilitaryBranch { Id = 24,   Country = "USA",    BranchName = "Navy Reserve" },
        };



            var MemberSet = new Mock<DbSet<Member>>().SetupData(members);
            MemberSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => members.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(MemberSet.Object);

            var InterestSet = new Mock<DbSet<Interest>>().SetupData(interests);
            InterestSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => interests.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Interests).Returns(InterestSet.Object);

            var SkillSet = new Mock<DbSet<Skill>>().SetupData(skills);
            SkillSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => skills.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Skills).Returns(SkillSet.Object);

            var AddressSet = new Mock<DbSet<Address>>().SetupData(addresses);
            AddressSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => addresses.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Addresses).Returns(AddressSet.Object);

            var PhoneNumberSet = new Mock<DbSet<PhoneNumber>>().SetupData(phoneNumbers);
            PhoneNumberSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => phoneNumbers.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.PhoneNumbers).Returns(PhoneNumberSet.Object);

            var FamilySet = new Mock<DbSet<Family>>().SetupData(families);
            FamilySet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => families.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Families).Returns(FamilySet.Object);

            var NonprofitSet = new Mock<DbSet<Nonprofit>>().SetupData(nonprofits);
            NonprofitSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => nonprofits.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Nonprofits).Returns(NonprofitSet.Object);
            
            var MemberMilitaryset = new Mock<DbSet<MemberMilitary>>().SetupData(memberMilitary);
            MemberMilitaryset.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => memberMilitary.FirstOrDefault(d => d.MilitaryId == (int)ids[0]));
            context.Setup(c => c.MemberMilitaries).Returns(MemberMilitaryset.Object);

            var MilitaryBranchSet = new Mock<DbSet<MilitaryBranch>>().SetupData(militaryBranch);
            MilitaryBranchSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => militaryBranch.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.MilitaryBranches).Returns(MilitaryBranchSet.Object);


            NonprofitService service = new NonprofitService(context.Object);
            service.CreateMembersFromClientList(1, clientList);

            Assert.True(context.Object.Members.Count() == 200);
            Assert.Equal("E-1", context.Object.Members.First().MemberMilitaries.First().LastPayGrade);
        }
    }
}
