//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using SolveChicago.Entities;
//using System.Data.Entity;
//using System.Collections.Generic;

//using SolveChicago.App.Common.Entities;
//using System.Linq;

//namespace SolveChicago.Tests.Services
//{
//    [TestClass]
//    public class MemberServiceTest
//    {
//        [TestMethod]
//        public void Can_Create_Member()
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
//            List<Member> members = new List<Member>();

//            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
//            var memberSet = new Mock<DbSet<Member>>().SetupData(members);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
//            context.Setup(c => c.Members).Returns(memberSet.Object);


//            MemberService service = new MemberService(context.Object);
//            int memberId = service.Create(new MemberEntity { Email = email }, 1);

//            Assert.IsNotNull(memberId);
//        }

//        [TestMethod]
//        public void Can_Update_Member_Profile()
//        {
//            List<Member> data = new List<Member>
//            {
//                new Member { Id = 1 }
//            };

//            MemberEntity member = new MemberEntity
//            {
//                Address1 = "123 Main Street",
//                Address2 = "Apt 2",
//                City = "Chicago",
//                Province = "IL",
//                Country = "USA",
//                Email = "member@solvechicago.com",
//                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                Id = 1,
//                Name = "Tom Elliot",
//                Phone = "1234567890",
//                ProfilePicturePath = "../image.jpg"
//            };

//            var memberSet = new Mock<DbSet<Member>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Members).Returns(memberSet.Object);

//            using (MemberService service = new MemberService(context.Object))
//            {
//                var result = service.UpdateProfile(member);
//                Assert.IsTrue(result);
//            }
//        }

//        [TestMethod]
//        public void Can_Get_Members_By_CaseManager()
//        {
//            List<CaseManager> data = new List<CaseManager>
//            {
//                new CaseManager
//                {
//                    Id = 2,
//                    MemberNonprofits = new List<MemberNonprofit>
//                    {
//                        new MemberNonprofit
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Tom Elliot",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                            }
//                        },
//                        new MemberNonprofit
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Jody Jones",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                            }
//                        },
//                        new MemberNonprofit
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Arlo Mitchell",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                            }
//                        }
//                    }
//                }
//            };
            
            
//            var set = new Mock<DbSet<CaseManager>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.CaseManagers).Returns(set.Object);

//            using (MemberService service = new MemberService(context.Object))
//            {
//                var result = service.GetMembers(2);
//                Assert.AreEqual(3, result.Count());
//            }
//        }

//        [TestMethod]
//        public void Can_Get_Members_By_Nonprofit()
//        {
//            List<Nonprofit> data = new List<Nonprofit>
//            {
//                new Nonprofit
//                {
//                    Id = 2,
//                    MemberNonprofits = new List<MemberNonprofit>
//                    {
//                        new MemberNonprofit
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Tom Elliot",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                                MemberNonprofits = new List<MemberNonprofit>
//                                {
//                                    new MemberNonprofit
//                                    {
//                                        NonprofitId = 2
//                                    }
//                                }
//                            }
//                        },
//                        new MemberNonprofit
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Jody Jones",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                                MemberNonprofits = new List<MemberNonprofit>
//                                {
//                                    new MemberNonprofit
//                                    {
//                                        NonprofitId = 2
//                                    }
//                                }
//                            }
//                        },
//                        new MemberNonprofit
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Arlo Mitchell",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                                MemberNonprofits = new List<MemberNonprofit>
//                                {
//                                    new MemberNonprofit
//                                    {
//                                        NonprofitId = 2
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            };


//            var set = new Mock<DbSet<Nonprofit>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Nonprofits).Returns(set.Object);

//            using (MemberService service = new MemberService(context.Object))
//            {
//                var result = service.GetMembers(null, 2);
//                Assert.AreEqual(3, result.Count());
//            }
//        }

//        [TestMethod]
//        public void Can_Get_Members_By_Corporation()
//        {
//            List<Corporation> data = new List<Corporation>
//            {
//                new Corporation
//                {
//                    Id = 2,
//                    MemberCorporations = new List<MemberCorporation>
//                    {
//                        new MemberCorporation
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Tom Elliot",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                                MemberCorporations = new List<MemberCorporation>
//                                {
//                                    new MemberCorporation
//                                    {
//                                        CorporationId = 2
//                                    }
//                                }
//                            }
//                        },
//                        new MemberCorporation
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Jody Jones",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                                MemberCorporations = new List<MemberCorporation>
//                                {
//                                    new MemberCorporation
//                                    {
//                                        CorporationId = 2
//                                    }
//                                }
//                            }
//                        },
//                        new MemberCorporation
//                        {
//                            Member = new Member
//                            {
//                                Address1 = "123 Main Street",
//                                Address2 = "Apt 2",
//                                City = "Chicago",
//                                Province = "IL",
//                                Country = "USA",
//                                Email = "member@solvechicago.com",
//                                CreatedDate = DateTime.UtcNow.AddDays(-10),
//                                Id = 1,
//                                Name = "Arlo Mitchell",
//                                Phone = "1234567890",
//                                ProfilePicturePath = "../image.jpg",
//                                MemberCorporations = new List<MemberCorporation>
//                                {
//                                    new MemberCorporation
//                                    {
//                                        CorporationId = 2
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            };


//            var set = new Mock<DbSet<Corporation>>().SetupData(data);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Corporations).Returns(set.Object);

//            using (MemberService service = new MemberService(context.Object))
//            {
//                var result = service.GetMembers(null, null, 2);
//                Assert.AreEqual(3, result.Count());
//            }
//        }

//        [TestMethod]
//        public void Can_Get_Member()
//        {
//            List<Member> members = new List<Member>
//            {
//                new Member
//                {
//                    Address1 = "123 Main Street",
//                    Address2 = "Apt 2",
//                    City = "Chicago",
//                    Province = "IL",
//                    Country = "USA",
//                    Email = "member@solvechicago.com",
//                    CreatedDate = DateTime.UtcNow.AddDays(-10),
//                    Id = 1024,
//                    Name = "Arlo Mitchell",
//                    Phone = "1234567890",
//                    ProfilePicturePath = "../image.jpg",
//                    CaseNotes = new List<CaseNote>
//                    {
//                        new CaseNote
//                        {
//                            Id = 1,
//                            Note = "great"
//                        }
//                    }
//                }
//            };

//            var set = new Mock<DbSet<Member>>().SetupData(members);

//            var context = new Mock<SolveChicagoEntities>();
//            context.Setup(c => c.Members).Returns(set.Object);

//            using (MemberService service = new MemberService(context.Object))
//            {
//                var result = service.GetMember(1024);
//                Assert.AreEqual("Chicago", result.City);
//            }
//        }
//    }
//}


