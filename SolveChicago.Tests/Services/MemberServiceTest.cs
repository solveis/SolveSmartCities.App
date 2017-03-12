using System;
using Xunit;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SolveChicago.Web.Services;
using SolveChicago.Web.Models.Profile;

namespace SolveChicago.Tests.Services
{
    public class MemberServiceTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();
        public MemberServiceTest()
        {
            List<Member> data = new List<Member>
            {
                new Member
                {
                    Id = 1,
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            Address1 = "123 Main Street",
                            Address2 = "#1B",
                            City = "Chicago",
                            Country = "USA",
                            Id = 1,
                            Province = "Illinois",
                            ZipCode = "60640"
                        }
                    },
                    AspNetUsers = new List<AspNetUser>
                    {
                        new AspNetUser
                        {
                            Id = "HUIGYUFTYRD^$%&R^*T&(Y*)",
                            UserName = "member@member.com"
                        }
                    },
                    Birthday = new DateTime(1988, 1, 2),
                    Children = new List<MemberParent>
                    {
                        new MemberParent
                        {
                            ChildId = 2,
                            IsBiological = true,
                            ParentId = 1,
                            Children = new Member
                            {
                                Id = 2,
                                FirstName = "Samantha",
                                LastName = "Carter",
                                Gender = "Female",
                            }
                        },
                        new MemberParent
                        {
                            ChildId = 3,
                            IsBiological = true,
                            ParentId = 1,
                            Children = new Member
                            {
                                Id = 3,
                                FirstName = "Joe",
                                LastName = "Carter",
                                Gender = "Male",
                            }
                        },
                        new MemberParent
                        {
                            ChildId = 4,
                            IsBiological = true,
                            ParentId = 1,
                            Children = new Member
                            {
                                Id = 4,
                                FirstName = "Lynn",
                                LastName = "Carter",
                                Gender = "Other",
                            }
                        }
                    },
                    CreatedDate = DateTime.UtcNow.AddMonths(-1),
                    Email = "member@member.com",
                    FamilyId = 1,
                    Family = new Family
                    {
                        Addresses = new List<Address>
                        {
                            new Address
                            {
                                Address1 = "123 Main Street",
                                Address2 = "#1B",
                                City = "Chicago",
                                Country = "USA",
                                Id = 1,
                                Province = "Illinois",
                                ZipCode = "60640"
                            }
                        },
                        CreatedDate = DateTime.UtcNow.AddMonths(-1),
                        Id = 1,
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber { Id = 1, Number = "1234567890" }
                        },
                        FamilyName = "Carter"
                    },
                    FirstName = "Aaron",
                    Gender = "Male",
                    Interests = new List<Interest>
                    {
                        new Interest { Id = 1, Name = "Basketball" },
                        new Interest { Id = 2, Name = "Science" },
                        new Interest { Id = 3, Name = "Computers" },
                        new Interest { Id = 4, Name = "Archery" },
                    },
                    IsHeadOfHousehold = true,
                    LastName = "Carter",
                    MemberNonprofits = new List<MemberNonprofit>
                    {
                        new MemberNonprofit
                        {
                            NonprofitId = 1,
                            MemberId = 1,
                            Nonprofit = new Nonprofit
                            {
                                Id = 1,
                                Name = "Nonprofit 1",
                            }
                        }
                    },
                    MemberSkills = new List<MemberSkill>
                    {
                            new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 1, Name = "Powerpoint" } },
                            new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 2, Name = "Excel" } },
                            new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 3, Name = "Word" } },
                            new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 4, Name = "Interviewing" } },
                    },
                    MemberSchools = new List<MemberSchool>
                    {
                        new MemberSchool
                        {
                            School = new School
                            {
                                Id = 1,
                                SchoolName = "Lakeview High School",
                                Type = "High School"
                            },
                            Degree = "Diploma",
                            End = new DateTime(2014, 5, 1),
                            Start = new DateTime(2011, 8, 24),
                            IsCurrent = false,
                            SchoolId = 1,
                            MemberId = 1
                        }
                    },
                    PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber { Id = 1, Number = "1234567890" }
                    },
                    ProfilePicturePath = "../path.jpg"
                }
            };

            var set = new Mock<DbSet<Member>>().SetupData(data);
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(set.Object);
        }


        [Fact]
        public void Can_Get_Member()
        {
            MemberService service = new MemberService(context.Object);
            MemberProfile member = service.Get(1, 1);

            Assert.Equal("Aaron", member.FirstName);
        }
    }
}


