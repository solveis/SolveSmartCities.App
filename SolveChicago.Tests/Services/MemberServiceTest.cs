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
using SolveChicago.Common.Models.Profile.Member;

namespace SolveChicago.Tests.Services
{
    public class MemberServiceTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();
        
        private void SetupDataForGet()
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
                    AspNetUser =
                        new AspNetUser
                        {
                            Id = "HUIGYUFTYRD^$%&R^*T&(Y*)",
                            UserName = "member@member.com"
                        },
                    Birthday = new DateTime(1988, 1, 2),
                    MemberParents = new List<MemberParent>
                    {
                        new MemberParent
                        {
                            ChildId = 2,
                            IsBiological = true,
                            ParentId = 1,
                            Member1 = new Member
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
                            Member1 = new Member
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
                            Member1 = new Member
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
                    NonprofitMembers = new List<NonprofitMember>
                    {
                        new NonprofitMember
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
                    ProfilePicturePath = "../path.jpg",
                    MemberGovernmentPrograms = new List<MemberGovernmentProgram>
                    {
                        new MemberGovernmentProgram
                        {
                            Amount = 10000.00m,
                            End = null,
                            GovernmentProgramId = 1,
                            GovernmentProgram = new GovernmentProgram
                            {
                                Name = "Program 1"
                            },
                            Id = 1,
                            MemberId = 1,
                            Start = new DateTime(2016, 1, 1)
                        }
                    }
                }
            };

            List<MemberGovernmentProgram> programs = new List<MemberGovernmentProgram>
            {
                new MemberGovernmentProgram
                {
                    Amount = 10000.00m,
                    End = null,
                    GovernmentProgramId = 1,
                    GovernmentProgram = new GovernmentProgram
                    {
                        Name = "Program 1"
                    },
                    Id = 1,
                    MemberId = 1,
                    Start = new DateTime(2016, 1, 1)
                }
            };
            
            var set = new Mock<DbSet<Member>>().SetupData(data);
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(set.Object);

            var programSet = new Mock<DbSet<MemberGovernmentProgram>>().SetupData(programs);
            programSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => programs.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.MemberGovernmentPrograms).Returns(programSet.Object);
        }
        
        [Fact]
        public void MemberService_Get_ReturnsMemberProfile()
        {
            SetupDataForGet();

            MemberService service = new MemberService(context.Object);
            MemberProfile member = service.Get(1);

            Assert.Equal("Aaron", member.FirstName);
            Assert.Equal("High School", member.HighestEducation);
            Assert.Equal("Diploma", member.Degree);
            Assert.Equal("Lakeview High School", member.LastSchool);
            Assert.Equal(10000.00m, member.GovernmentPrograms.First().Amount);
        }

        [Fact]
        public void MemberService_GetProfilePersonal_ReturnsMemberProfilePersonal()
        {
            SetupDataForGet();


            MemberService service = new MemberService(context.Object);
            MemberProfilePersonal member = service.GetProfilePersonal(1);

            Assert.Equal("Aaron", member.FirstName);
            Assert.Equal("Basketball,Science,Computers,Archery", member.Interests);
        }

        [Fact]
        public void MemberService_GetProfileFamily_ReturnsMemberProfileFamily()
        {
            SetupDataForGet();

            MemberService service = new MemberService(context.Object);
            MemberProfileFamily member = service.GetProfileFamily(1);

            Assert.Equal("123 Main Street", member.Family.Address1);
            Assert.Equal("Samantha", member.Family.FamilyMembers.Single(x => x.Id == 2).FirstName);
        }

        [Fact]
        public void MemberService_GetProfileJobs_ReturnsMemberProfileJobs()
        {
            SetupDataForGet();
            
            MemberService service = new MemberService(context.Object);
            MemberProfileJobs member = service.GetProfileJobs(1);

            Assert.Equal("ACME", member.Jobs.First().Name);
        }

        [Fact]
        public void MemberService_GetProfileNonprofits_ReturnsMemberProfileNonprofits()
        {
            SetupDataForGet();
            
            MemberService service = new MemberService(context.Object);
            MemberProfileNonprofits member = service.GetProfileNonprofits(1);

            Assert.Equal("Nonprofit 1", member.Nonprofits.First().NonprofitName);
        }

        [Fact]
        public void MemberService_GetProfileSchools_ReturnsMemberProfileSchools()
        {
            SetupDataForGet();

            MemberService service = new MemberService(context.Object);
            MemberProfileSchools member = service.GetProfileSchools(1);

            Assert.Equal("Lakeview High School", member.Schools.First().Name);
        }

        [Fact]
        public void MemberService_GetProfileGovernmentPrograms_ReturnsMemberProfileGovernmentPrograms()
        {
            SetupDataForGet();
            
            MemberService service = new MemberService(context.Object);
            MemberProfileGovernmentPrograms member = service.GetProfileGovernmentPrograms(1);

            Assert.Equal("Program 1", member.GovernmentPrograms.First().ProgramName);
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
                    AspNetUser =
                        new AspNetUser
                        {
                            Id = "HUIGYUFTYRD^$%&R^*T&(Y*)",
                            UserName = "member@member.com"
                        },
                    Birthday = new DateTime(1988, 1, 2),
                    MemberParents = new List<MemberParent>
                    {
                        new MemberParent
                        {
                            ChildId = 2,
                            IsBiological = true,
                            ParentId = 1,
                            Member = new Member
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
                            Member = new Member
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
                            Member = new Member
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
                    NonprofitMembers = new List<NonprofitMember>
                    {
                        new NonprofitMember
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
            MemberProfile member = service.Get(5);

            Assert.Null(member);
        }
        
        private Tuple<Mock<DbSet<Member>>, List<Member>> SetupPostData()
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
                    AspNetUser =
                        new AspNetUser
                        {
                            Id = "HUIGYUFTYRD^$%&R^*T&(Y*)",
                            UserName = "member@member.com"
                        },
                    Birthday = new DateTime(1988, 1, 2),
                    MemberParents = new List<MemberParent>
                    {
                        new MemberParent
                        {
                            ChildId = 6,
                            Member1 = new Member
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
                    NonprofitMembers = new List<NonprofitMember>
                    {
                        new NonprofitMember
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
                    ProfilePicturePath = "",
                    MemberGovernmentPrograms = new List<MemberGovernmentProgram>
                    {
                    }
                },
            };
            List<School> schools = new List<School>();
            List<Corporation> corps = new List<Corporation>();
            List<Skill> skills = new List<Skill> { new Skill { Id = 1, Name = "math" } };
            List<Interest> interests = new List<Interest> { new Interest { Id = 1, Name = "Jogging" } };
            List<PhoneNumber> phones = new List<PhoneNumber>();
            List<Nonprofit> nonprofits = new List<Nonprofit>();
            List<MemberGovernmentProgram> membergovernmentprograms = new List<MemberGovernmentProgram>();
            List<MemberParent> memberParents = new List<MemberParent>();
            List<MemberSpous> memberSpouses = new List<MemberSpous>();
            List<MilitaryBranch> militaryBranches = new List<MilitaryBranch>();
            List<Family> families = new List<Family>();

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

            var mgpSet = new Mock<DbSet<MemberGovernmentProgram>>().SetupData(membergovernmentprograms);
            mgpSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => membergovernmentprograms.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.MemberGovernmentPrograms).Returns(mgpSet.Object);

            var mpSet = new Mock<DbSet<MemberParent>>().SetupData(memberParents);
            mpSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => memberParents.FirstOrDefault(d => d.ParentId == (int)ids[0]));
            context.Setup(c => c.MemberParents).Returns(mpSet.Object);

            var msSet = new Mock<DbSet<MemberSpous>>().SetupData(memberSpouses);
            msSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => memberSpouses.FirstOrDefault(d => d.Spouse_1_Id == (int)ids[0]));
            context.Setup(c => c.MemberSpouses).Returns(msSet.Object);

            var militarySet = new Mock<DbSet<MilitaryBranch>>().SetupData(militaryBranches);
            militarySet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => militaryBranches.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.MilitaryBranches).Returns(militarySet.Object);

            var familySet = new Mock<DbSet<Family>>().SetupData(families);
            familySet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => families.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Families).Returns(familySet.Object);

            return new Tuple<Mock<DbSet<Member>>, List<Member>>(set, data);
        }

        [Fact]
        public void MemberService_UpdateMemberPersonal_Success()
        {
            Tuple<Mock<DbSet<Member>>, List<Member>> result = SetupPostData();

            MemberProfilePersonal model = new MemberProfilePersonal
            {
                Address1 = "123 Main Street",
                Address2 = "Apt 1B",
                Birthday = new DateTime(1989, 1, 2),
                City = "Chicago",
                Country = "USA",
                Email = "member@member.com",
                FirstName = "Helen",
                LastName = "Jones",
                Gender = Constants.Gender.Female,
                Interests = "Jogging, Sewing, Cooking, Karate, Racecar Driving",
                Phone = "1234567890",
                ProfilePicturePath = "../path.jpg",
                Province = "Illinois",
                Id = 1,
                ZipCode = "60640",
            };
            MemberService service = new MemberService(context.Object);

            service.UpdateMemberPersonal(model);

            // refresh, so .find() will work
            result.Item1.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => result.Item2.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(result.Item1.Object);

            MemberProfilePersonal updatedMember = service.GetProfilePersonal(model.Id);
            
            Assert.Equal("Illinois", updatedMember.Province);
            Assert.Equal("60640", updatedMember.ZipCode);
        }

        [Fact]
        public void MemberService_UpdateMemberFamily_Success()
        {
            Tuple<Mock<DbSet<Member>>, List<Member>> result = SetupPostData();

            MemberProfileFamily model = new MemberProfileFamily
            {
                MemberId = 1,
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
                        },
                        new FamilyMember
                        {
                            Id = 6,
                        }
                    }.ToArray(),
                },
            };
            MemberService service = new MemberService(context.Object);

            service.UpdateMemberFamily(model);

            // refresh, so .find() will work
            result.Item1.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => result.Item2.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(result.Item1.Object);

            MemberProfileFamily updatedMember = service.GetProfileFamily(model.MemberId);
            
            Assert.Equal("Isaac", updatedMember.Family.FamilyMembers.Where(x => x.Relation == "Father").First().FirstName);
        }

        [Fact]
        public void MemberService_UpdateMemberJobs_Success()
        {
            Tuple<Mock<DbSet<Member>>, List<Member>> result = SetupPostData();

            MemberProfileJobs model = new MemberProfileJobs
            {
                MemberId = 1,
                Jobs = new List<JobEntity>
                {
                    new JobEntity{ Name = "Walgreens", EmployeeStart = new DateTime(2015, 8, 25), EmployeePay = 12}
                }.ToArray(),
            };
            MemberService service = new MemberService(context.Object);

            service.UpdateMemberJobs(model);

            // refresh, so .find() will work
            result.Item1.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => result.Item2.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(result.Item1.Object);

            MemberProfile updatedMember = service.Get(model.MemberId);

            Assert.Equal("Walgreens", updatedMember.Jobs.First().Name);
        }

        [Fact]
        public void MemberService_UpdateNonprofitMembers_Success()
        {
            Tuple<Mock<DbSet<Member>>, List<Member>> result = SetupPostData();

            MemberProfileNonprofits model = new MemberProfileNonprofits
            {
                MemberId = 1,
                Nonprofits = new List<NonprofitEntity>
                {
                    new NonprofitEntity { Enjoyed = "reading, writing, singing", SkillsAcquired = "critical thinking, math", NonprofitName = "i.c. stars", Struggled = "leadership, brainstorming" }
                }.ToArray(),
            };
            MemberService service = new MemberService(context.Object);

            service.UpdateMemberNonprofits(model);

            // refresh, so .find() will work
            result.Item1.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => result.Item2.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Members).Returns(result.Item1.Object);

            MemberProfile updatedMember = service.Get(model.MemberId);

            Assert.Equal("i.c. stars", updatedMember.Nonprofits.Last().NonprofitName);
        }

        [Fact]
        public void MemberService_UpdateMemberGovernmentPrograms_Success()
        {
            Tuple<Mock<DbSet<Member>>, List<Member>> result = SetupPostData();

            MemberProfileGovernmentPrograms model = new MemberProfileGovernmentPrograms
            {
                MemberId = 1,
                GovernmentPrograms = new List<GovernmentProgramEntity>
                {
                    new GovernmentProgramEntity
                    {
                        Amount = 10000.00m,
                        End = null,
                        ProgramId = 1,
                        Id = 1,
                        MemberId = 1,
                        Start = new DateTime(2017, 1, 1)
                    }
                }.ToArray()
            };
            MemberService service = new MemberService(context.Object);

            service.UpdateMemberGovernmentPrograms(model);
            
        }
    }
}


