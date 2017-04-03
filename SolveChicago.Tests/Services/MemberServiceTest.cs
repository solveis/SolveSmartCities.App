using System;
using Xunit;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SolveChicago.Service;
using SolveChicago.Web.Models.Profile;
using SolveChicago.Common;
using SolveChicago.Common.Models.Profile;

namespace SolveChicago.Tests.Services
{
    public class MemberServiceTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();
        //public MemberServiceTest()
        //{
        //    List<Address> addresses = new List<Address>
        //    {
        //        new Address
        //        {
        //            Address1 = "123 Main Street",
        //            Address2 = "#1B",
        //            City = "Chicago",
        //            Country = "USA",
        //            Id = 1,
        //            Province = "Illinois",
        //            ZipCode = "60640"
        //        }
        //    };
        //    List<Member> data = new List<Member>
        //    {
        //        new Member
        //        {
        //            Id = 1,
        //            Addresses = new List<Address>
        //            {
        //                new Address
        //                {
        //                    Address1 = "123 Main Street",
        //                    Address2 = "#1B",
        //                    City = "Chicago",
        //                    Country = "USA",
        //                    Id = 1,
        //                    Province = "Illinois",
        //                    ZipCode = "60640"
        //                }
        //            },
        //            AspNetUsers = new List<AspNetUser>
        //            {
        //                new AspNetUser
        //                {
        //                    Id = "HUIGYUFTYRD^$%&R^*T&(Y*)",
        //                    UserName = "member@member.com"
        //                }
        //            },
        //            Birthday = new DateTime(1988, 1, 2),
        //            Children = new List<MemberParent>
        //            {
        //                new MemberParent
        //                {
        //                    ChildId = 2,
        //                    IsBiological = true,
        //                    ParentId = 1,
        //                    Children = new Member
        //                    {
        //                        Id = 2,
        //                        FirstName = "Samantha",
        //                        LastName = "Carter",
        //                        Gender = "Female",
        //                    }
        //                },
        //                new MemberParent
        //                {
        //                    ChildId = 3,
        //                    IsBiological = true,
        //                    ParentId = 1,
        //                    Children = new Member
        //                    {
        //                        Id = 3,
        //                        FirstName = "Joe",
        //                        LastName = "Carter",
        //                        Gender = "Male",
        //                    }
        //                },
        //                new MemberParent
        //                {
        //                    ChildId = 4,
        //                    IsBiological = true,
        //                    ParentId = 1,
        //                    Children = new Member
        //                    {
        //                        Id = 4,
        //                        FirstName = "Lynn",
        //                        LastName = "Carter",
        //                        Gender = "Other",
        //                    }
        //                }
        //            },
        //            CreatedDate = DateTime.UtcNow.AddMonths(-1),
        //            Email = "member@member.com",
        //            FamilyId = 1,
        //            Family = new Family
        //            {
        //                Addresses = new List<Address>
        //                {
        //                    new Address
        //                    {
        //                        Address1 = "123 Main Street",
        //                        Address2 = "#1B",
        //                        City = "Chicago",
        //                        Country = "USA",
        //                        Id = 1,
        //                        Province = "Illinois",
        //                        ZipCode = "60640"
        //                    }
        //                },
        //                CreatedDate = DateTime.UtcNow.AddMonths(-1),
        //                Id = 1,
        //                PhoneNumbers = new List<PhoneNumber>
        //                {
        //                    new PhoneNumber { Id = 1, Number = "1234567890" }
        //                },
        //                FamilyName = "Carter"
        //            },
        //            FirstName = "Aaron",
        //            Gender = "Male",
        //            Interests = new List<Interest>
        //            {
        //                new Interest { Id = 1, Name = "Basketball" },
        //                new Interest { Id = 2, Name = "Science" },
        //                new Interest { Id = 3, Name = "Computers" },
        //                new Interest { Id = 4, Name = "Archery" },
        //            },
        //            IsHeadOfHousehold = true,
        //            LastName = "Carter",
        //            MemberCorporations = new List<MemberCorporation>
        //            {
        //                new MemberCorporation
        //                {
        //                    CorporationId = 1,
        //                    MemberId = 1,
        //                    Pay = 12,
        //                    Start = new DateTime(2015,1,2),
        //                    Corporation = new Corporation
        //                    {
        //                        Id = 1,
        //                        Name = "ACME",
        //                    }
        //                }
        //            },
        //            MemberNonprofits = new List<MemberNonprofit>
        //            {
        //                new MemberNonprofit
        //                {
        //                    NonprofitId = 1,
        //                    MemberId = 1,
        //                    Nonprofit = new Nonprofit
        //                    {
        //                        Id = 1,
        //                        Name = "Nonprofit 1",
        //                    }
        //                }
        //            },
        //            MemberSkills = new List<MemberSkill>
        //            {
        //                    new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 1, Name = "Powerpoint" } },
        //                    new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 2, Name = "Excel" } },
        //                    new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 3, Name = "Word" } },
        //                    new MemberSkill { MemberId = 1, NonprofitId = 1, Skill = new Skill{ Id = 4, Name = "Interviewing" } },
        //            },
        //            MemberSchools = new List<MemberSchool>
        //            {
        //                new MemberSchool
        //                {
        //                    School = new School
        //                    {
        //                        Id = 1,
        //                        SchoolName = "Lakeview High School",
        //                        Type = "High School"
        //                    },
        //                    Degree = "Diploma",
        //                    End = new DateTime(2014, 5, 1),
        //                    Start = new DateTime(2011, 8, 24),
        //                    IsCurrent = false,
        //                    SchoolId = 1,
        //                    MemberId = 1
        //                }
        //            },
        //            PhoneNumbers = new List<PhoneNumber>
        //            {
        //                new PhoneNumber { Id = 1, Number = "1234567890" }
        //            },
        //            ProfilePicturePath = "../path.jpg"
        //        }
        //    };
        //    List<School> schools = new List<School>();
        //    List<Corporation> corps = new List<Corporation>();
        //    List<Skill> skills = new List<Skill>();
        //    List<Interest> interests = new List<Interest>();
        //    List<PhoneNumber> phones = new List<PhoneNumber>();

        //    var set = new Mock<DbSet<Member>>().SetupData(data);
        //    set.Setup(m => m.Find(It.IsAny<object[]>()))
        //        .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
        //    context.Setup(c => c.Members).Returns(set.Object);

        //    var addressSet = new Mock<DbSet<Address>>().SetupData(addresses);
        //    addressSet.Setup(m => m.Find(It.IsAny<object[]>()))
        //        .Returns<object[]>(ids => addresses.FirstOrDefault(d => d.Id == (int)ids[0]));
        //    context.Setup(c => c.Addresses).Returns(addressSet.Object);

        //    var corpsSet = new Mock<DbSet<Corporation>>().SetupData(corps);
        //    corpsSet.Setup(m => m.Find(It.IsAny<object[]>()))
        //        .Returns<object[]>(ids => corps.FirstOrDefault(d => d.Id == (int)ids[0]));
        //    context.Setup(c => c.Corporations).Returns(corpsSet.Object);

        //    var schoolsSet = new Mock<DbSet<School>>().SetupData(schools);
        //    schoolsSet.Setup(m => m.Find(It.IsAny<object[]>()))
        //        .Returns<object[]>(ids => schools.FirstOrDefault(d => d.Id == (int)ids[0]));
        //    context.Setup(c => c.Schools).Returns(schoolsSet.Object);

        //    var skillSet = new Mock<DbSet<Skill>>().SetupData(skills);
        //    skillSet.Setup(m => m.Find(It.IsAny<object[]>()))
        //        .Returns<object[]>(ids => skills.FirstOrDefault(d => d.Id == (int)ids[0]));
        //    context.Setup(c => c.Skills).Returns(skillSet.Object);

        //    var interestSet = new Mock<DbSet<Interest>>().SetupData(interests);
        //    interestSet.Setup(m => m.Find(It.IsAny<object[]>()))
        //        .Returns<object[]>(ids => interests.FirstOrDefault(d => d.Id == (int)ids[0]));
        //    context.Setup(c => c.Interests).Returns(interestSet.Object);

        //    var phoneSet = new Mock<DbSet<PhoneNumber>>().SetupData(phones);
        //    phoneSet.Setup(m => m.Find(It.IsAny<object[]>()))
        //        .Returns<object[]>(ids => phones.FirstOrDefault(d => d.Id == (int)ids[0]));
        //    context.Setup(c => c.PhoneNumbers).Returns(phoneSet.Object);
        //}


        [Fact]
        public void MemberService_Get_ReturnsMemberProfile()
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
                    MemberCorporations = new List<MemberCorporation>
                    {
                        new MemberCorporation
                        {
                            CorporationId = 1,
                            MemberId = 1,
                            Pay = 12,
                            Start = new DateTime(2015,1,2),
                            Corporation = new Corporation
                            {
                                Id = 1,
                                Name = "ACME",
                            }
                        }
                    },
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


            MemberService service = new MemberService(context.Object);
            MemberProfile member = service.Get(1, 1);

            Assert.Equal("Aaron", member.FirstName);
            Assert.Equal("High School", member.HighestEducation);
            Assert.Equal("Diploma", member.Degree);
            Assert.Equal("Lakeview High School", member.LastSchool);
        }

        [Fact]
        public void MemberService_Get_ReturnsNull()
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
                    MemberCorporations = new List<MemberCorporation>
                    {
                        new MemberCorporation
                        {
                            CorporationId = 1,
                            MemberId = 1,
                            Pay = 12,
                            Start = new DateTime(2015,1,2),
                            Corporation = new Corporation
                            {
                                Id = 1,
                                Name = "ACME",
                            }
                        }
                    },
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

            MemberService service = new MemberService(context.Object);
            MemberProfile member = service.Get(5, 1);

            Assert.Null(member);
        }

        [Fact]
        public void MemberService_Post_Success()
        {
            List<Address> addresses = new List<Address>
            {
            };
            List<Member> data = new List<Member>
            {
                new Member
                {
                    Id = 1,
                    Addresses = new List<Address>
                    {

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
                            ChildId = 6,
                            Children = new Member
                            {
                                Id = 6,
                                FirstName = "Isaac",
                                LastName = "Jones",
                                Gender = Constants.Gender.Male,
                            }
                        }
                    },
                    CreatedDate = new DateTime(2017, 2, 17),
                    Email = "member@member.com",
                    FamilyId = 1,
                    Family = new Family
                    {
                    },
                    FirstName = "Helen",
                    Gender = Constants.Gender.Female,
                    Interests = new List<Interest>
                    {
                    },
                    IsHeadOfHousehold = true,
                    LastName = "Jones",
                    MemberCorporations = new List<MemberCorporation>
                    {
                    },
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
                    },
                    MemberSchools = new List<MemberSchool>
                    {
                    },
                    PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber { Id = 1, Number = "1234567890" }
                    },
                    ProfilePicturePath = ""
                },
            };
            List<School> schools = new List<School>();
            List<Corporation> corps = new List<Corporation>();
            List<Skill> skills = new List<Skill> { new Skill { Id = 1, Name = "math" } };
            List<Interest> interests = new List<Interest> { new Interest { Id = 1, Name = "Jogging" } };
            List<PhoneNumber> phones = new List<PhoneNumber>();
            List<Nonprofit> nonprofits = new List<Nonprofit>();

            var set = new Mock<DbSet<Member>>().SetupData(data);
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(set.Object);

            var addressSet = new Mock<DbSet<Address>>().SetupData(addresses);
            addressSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => addresses.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Addresses).Returns(addressSet.Object);

            var corpsSet = new Mock<DbSet<Corporation>>().SetupData(corps);
            corpsSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => corps.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Corporations).Returns(corpsSet.Object);

            var schoolsSet = new Mock<DbSet<School>>().SetupData(schools);
            schoolsSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => schools.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Schools).Returns(schoolsSet.Object);

            var skillSet = new Mock<DbSet<Skill>>().SetupData(skills);
            skillSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => skills.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Skills).Returns(skillSet.Object);

            var interestSet = new Mock<DbSet<Interest>>().SetupData(interests);
            interestSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => interests.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Interests).Returns(interestSet.Object);

            var phoneSet = new Mock<DbSet<PhoneNumber>>().SetupData(phones);
            phoneSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => phones.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.PhoneNumbers).Returns(phoneSet.Object);

            var npoSet = new Mock<DbSet<Nonprofit>>().SetupData(nonprofits);
            npoSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => nonprofits.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Nonprofits).Returns(npoSet.Object);


            MemberProfile model = new MemberProfile
            {
                Address1 = "123 Main Street",
                Address2 = "Apt 1B",
                Birthday = new DateTime(1989, 1, 2),
                City = "Chicago",
                Country = "USA",
                Email = "member@member.com",
                Family = new FamilyEntity
                {
                    FamilyMembers = new List<FamilyMember>
                    {
                        new FamilyMember
                        {
                            Birthday = new DateTime(2005, 10, 6),
                            FirstName = "Mary",
                            Gender = Constants.Gender.Female,
                            LastName = "Jones",
                            Relation = "Child",
                            RelationBiological = true,
                            Id = 2
                        },
                        new FamilyMember
                        {
                            Birthday = new DateTime(1987, 4, 27),
                            FirstName = "Sam",
                            Gender = Constants.Gender.Male,
                            LastName = "Jones",
                            Relation = "Spouse",
                            IsHeadOfHousehold = true,
                            IsMarriageCurrent = true,
                            Id = 3
                        },
                        new FamilyMember
                        {
                            Id = 6,
                        }
                    }.ToArray(),
                },
                FirstName = "Helen",
                LastName = "Jones",
                Gender = Constants.Gender.Female,
                Interests = "Jogging, Sewing, Cooking, Karate, Racecar Driving",
                Jobs = new List<JobEntity>
                {
                    new JobEntity{ Name = "Walgreens", EmployeeStart = new DateTime(2015, 8, 25), EmployeePay = 12}
                }.ToArray(),
                Nonprofits = new List<NonprofitEntity>
                {
                    new NonprofitEntity { Enjoyed = "reading, writing, singing", SkillsAcquired = "critical thinking, math", NonprofitName = "i.c. stars", Struggled = "leadership, brainstorming" }
                }.ToArray(),
                Phone = "1234567890",
                ProfilePicturePath = "../path.jpg",
                Province = "Illinois",
                Schools = new List<SchoolEntity>
                {
                    new SchoolEntity
                    {
                        Name = "Lakeview High School",
                        Start = new DateTime(2004, 8, 24),
                        End = new DateTime(2007, 5, 1),
                        Degree = Constants.School.Degrees.HSDiploma,
                        IsCurrent = false,
                        Type = Constants.School.Types.HighSchool
                    }
                }.ToArray(),
                Id = 1,
                ZipCode = "60640"
            };
            MemberService service = new MemberService(context.Object);

            service.UpdateProfile(model);

            // refresh, so .find() will work
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(set.Object);

            MemberProfile updatedMember = service.Get(model.Id, 1);

            Assert.Equal(new DateTime(2007, 5, 1), updatedMember.Schools.First().End);
            Assert.Equal("Illinois", updatedMember.Province);
            Assert.Equal("Sam", updatedMember.Family.FamilyMembers.Where(x => x.Relation == "Husband").First().FirstName);
            Assert.Equal("i.c. stars", updatedMember.Nonprofits.Last().NonprofitName);
        }
    }
}


