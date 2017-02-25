using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolveChicago.App.Common.Entities;
using System.Web;
using SolveChicago.Web.Controllers;
using System.IO;
using System.Web.Mvc;
using SolveChicago.Entities;
using System.Data.Entity;
using Moq;
using System.Security.Principal;
using static SolveChicago.Web.Controllers.BaseController;

namespace SolveChicago.Tests.Controllers
{
    [TestClass]
    public class BaseControllerTest
    {
        [TestMethod]
        public void MemberRedirect_WithEntity_Returns_Member_Index()
        {
            Member entity = new Member
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
                ProfilePicturePath = "../image.jpg",
            };
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.MemberRedirect(entity);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Members", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void MemberRedirect_WithEntity_Returns_Profile_Member()
        {
            Member entity = new Member();
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.MemberRedirect(entity);

            Assert.AreEqual("Member", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void MemberRedirect_NoEntity_Returns_Member_Index()
        {
            List<Member> data = new List<Member>
            {
                new Member()
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
                    ProfilePicturePath = "../image.jpg",
                }
            };

            var set = new Mock<DbSet<Member>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Members).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.MemberRedirect(1);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Members", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void MemberRedirect_NoEntity_Returns_Profile_Member()
        {
            List<Member> data = new List<Member>
            {
                new Member() { Id = 1 }
            };

            var set = new Mock<DbSet<Member>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Members).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.MemberRedirect(1);

            Assert.AreEqual("Member", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CaseManagerRedirect_WithEntity_Returns_CaseManager_Index()
        {
            CaseManager entity = new CaseManager
            {
                Address1 = "123 Main Street",
                Address2 = "Apt 2",
                City = "Chicago",
                Province = "IL",
                Country = "USA",
                Email = "casemanager@solvechicago.com",
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                Id = 1,
                Name = "Tom Elliot",
                Phone = "1234567890",
                ProfilePicturePath = "../image.jpg",
            };
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.CaseManagerRedirect(entity);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("CaseManagers", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CaseManagerRedirect_WithEntity_Returns_Profile_CaseManager()
        {
            CaseManager entity = new CaseManager();
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.CaseManagerRedirect(entity);

            Assert.AreEqual("CaseManager", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CaseManagerRedirect_NoEntity_Returns_CaseManager_Index()
        {
            List<CaseManager> data = new List<CaseManager>
            {
                new CaseManager()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "casemanager@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Tom Elliot",
                    Phone = "1234567890",
                    ProfilePicturePath = "../image.jpg",
                }
            };

            var set = new Mock<DbSet<CaseManager>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.CaseManagers).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.CaseManagerRedirect(1);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("CaseManagers", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CaseManagerRedirect_NoEntity_Returns_Profile_CaseManager()
        {
            List<CaseManager> data = new List<CaseManager>
            {
                new CaseManager() { Id = 1 }
            };

            var set = new Mock<DbSet<CaseManager>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.CaseManagers).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.CaseManagerRedirect(1);

            Assert.AreEqual("CaseManager", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CorporationRedirect_WithEntity_Returns_Corporation_Index()
        {
            Corporation entity = new Corporation
            {
                Address1 = "123 Main Street",
                Address2 = "Apt 2",
                City = "Chicago",
                Province = "IL",
                Country = "USA",
                Email = "corporation@solvechicago.com",
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                Id = 1,
                Name = "Tom Elliot",
                Phone = "1234567890",
            };
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.CorporationRedirect(entity);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Corporations", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CorporationRedirect_WithEntity_Returns_Profile_Corporation()
        {
            Corporation entity = new Corporation();
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.CorporationRedirect(entity);

            Assert.AreEqual("Corporation", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CorporationRedirect_NoEntity_Returns_Corporation_Index()
        {
            List<Corporation> data = new List<Corporation>
            {
                new Corporation()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "corporation@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Tom Elliot",
                    Phone = "1234567890",
                }
            };

            var set = new Mock<DbSet<Corporation>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Corporations).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.CorporationRedirect(1);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Corporations", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CorporationRedirect_NoEntity_Returns_Profile_Corporation()
        {
            List<Corporation> data = new List<Corporation>
            {
                new Corporation() { Id = 1 }
            };

            var set = new Mock<DbSet<Corporation>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Corporations).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.CorporationRedirect(1);

            Assert.AreEqual("Corporation", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void NonprofitRedirect_WithEntity_Returns_Nonprofit_Index()
        {
            Nonprofit entity = new Nonprofit
            {
                Address1 = "123 Main Street",
                Address2 = "Apt 2",
                City = "Chicago",
                Province = "IL",
                Country = "USA",
                Email = "nonprofit@solvechicago.com",
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                Id = 1,
                Name = "Tom Elliot",
                Phone = "1234567890",
            };
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.NonprofitRedirect(entity);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Nonprofits", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void NonprofitRedirect_WithEntity_Returns_Profile_Nonprofit()
        {
            Nonprofit entity = new Nonprofit();
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.NonprofitRedirect(entity);

            Assert.AreEqual("Nonprofit", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void NonprofitRedirect_NoEntity_Returns_Nonprofit_Index()
        {
            List<Nonprofit> data = new List<Nonprofit>
            {
                new Nonprofit()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "nonprofit@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Tom Elliot",
                    Phone = "1234567890",
                }
            };

            var set = new Mock<DbSet<Nonprofit>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Nonprofits).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.NonprofitRedirect(1);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Nonprofits", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void NonprofitRedirect_NoEntity_Returns_Profile_Nonprofit()
        {
            List<Nonprofit> data = new List<Nonprofit>
            {
                new Nonprofit() { Id = 1 }
            };

            var set = new Mock<DbSet<Nonprofit>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Nonprofits).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.NonprofitRedirect(1);

            Assert.AreEqual("Nonprofit", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void AdminRedirect_WithEntity_Returns_Admin_Index()
        {
            Admin entity = new Admin
            {
                Address1 = "123 Main Street",
                Address2 = "Apt 2",
                City = "Chicago",
                Province = "IL",
                Country = "USA",
                Email = "admin@solvechicago.com",
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                Id = 1,
                Name = "Tom Elliot",
                Phone = "1234567890",
                ProfilePicturePath = "../image.jpg",
            };
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.AdminRedirect(entity);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Admins", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void AdminRedirect_WithEntity_Returns_Profile_Admin()
        {
            Admin entity = new Admin();
            BaseController controller = new BaseController();
            var result = (RedirectToRouteResult)controller.AdminRedirect(entity);

            Assert.AreEqual("Admin", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void AdminRedirect_NoEntity_Returns_Admin_Index()
        {
            List<Admin> data = new List<Admin>
            {
                new Admin()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "admin@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Tom Elliot",
                    Phone = "1234567890",
                    ProfilePicturePath = "../image.jpg",
                }
            };

            var set = new Mock<DbSet<Admin>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Admins).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.AdminRedirect(1);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Admins", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void AdminRedirect_NoEntity_Returns_Profile_Admin()
        {
            List<Admin> data = new List<Admin>
            {
                new Admin() { Id = 1 }
            };

            var set = new Mock<DbSet<Admin>>().SetupData(data);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Admins).Returns(set.Object);

            BaseController controller = new BaseController(context.Object);
            var result = (RedirectToRouteResult)controller.AdminRedirect(1);

            Assert.AreEqual("Admin", result.RouteValues["action"]);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void RefreshState_WithCaseManager_Member()
        {
            List<UserProfile> users = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    UserName = "member@solvechicago.com",
                    Members = new List<Member>
                    {
                        new Member()
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
                    ProfilePicturePath = "../image.jpg",
                    MemberNonprofits = new List<MemberNonprofit>
                    {
                        new MemberNonprofit
                        {
                            CaseManagerId = 1,
                            MemberId = 1,
                            Member = new Member
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
                                ProfilePicturePath = "../image.jpg",
                            },
                            CaseManager = new CaseManager
                            {
                                Id = 1,
                                Name = "Terry Jones"
                            },
                            Start = DateTime.UtcNow.AddMonths(-1)
                        }
                    }
                }
                    },
                }
            };
            List<AspNetUser> aspnetUsers = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = "JHUIGYFTRDXS",
                    UserName = "member@solvechicago.com",
                    AspNetRoles = new List<AspNetRole>
                        {
                            new AspNetRole { Id = "KJHGF", Name = "Member" }
                        }
                }
            };

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("member@solvechicago.com");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var aspnetUserSet = new Mock<DbSet<AspNetUser>>().SetupData(aspnetUsers);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.AspNetUsers).Returns(aspnetUserSet.Object);
            BaseController controller = new BaseController(context.Object);

            //Set your controller ControllerContext with fake context
            controller.ControllerContext = controllerContext.Object;

            string name = controller.State.Members.First().Name;
            Assert.AreEqual("Tom Elliot", name);

            string caseManagerName = controller.State.Members.First().MemberNonprofits.Select(x => x.CaseManager).First().Name;
            Assert.AreEqual("Terry Jones", caseManagerName);

        }

        [TestMethod]
        public void RefreshState_CaseManager()
        {
            List<CaseManager> data = new List<CaseManager>
            {
                new CaseManager()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "casemanager@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Terry Jones",
                    Phone = "1234567890",
                    ProfilePicturePath = "../image.jpg",
                }
            };
            List<UserProfile> users = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    UserName = "casemanager@solvechicago.com",
                    CaseManagers = new List<CaseManager>
                    {
                        new CaseManager()
                        {
                            Address1 = "123 Main Street",
                            Address2 = "Apt 2",
                            City = "Chicago",
                            Province = "IL",
                            Country = "USA",
                            Email = "casemanager@solvechicago.com",
                            CreatedDate = DateTime.UtcNow.AddDays(-10),
                            Id = 1,
                            Name = "Terry Jones",
                            Phone = "1234567890",
                            ProfilePicturePath = "../image.jpg",
                        }
                    },
                }
            };
            List<AspNetUser> aspnetUsers = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = "JHUIGYFTRDXS",
                    UserName = "casemanager@solvechicago.com",
                    AspNetRoles = new List<AspNetRole>
                        {
                            new AspNetRole { Id = "KJHGF", Name = "CaseManager" }
                        }
                }
            };

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("casemanager@solvechicago.com");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var set = new Mock<DbSet<CaseManager>>().SetupData(data);
            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var aspnetUserSet = new Mock<DbSet<AspNetUser>>().SetupData(aspnetUsers);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.CaseManagers).Returns(set.Object);
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.AspNetUsers).Returns(aspnetUserSet.Object);
            BaseController controller = new BaseController(context.Object);

            //Set your controller ControllerContext with fake context
            controller.ControllerContext = controllerContext.Object;

            string name = controller.State.CaseManagers.First().Name;
            Assert.AreEqual("Terry Jones", name);

        }

        [TestMethod]
        public void RefreshState_Corporation()
        {
            List<Corporation> data = new List<Corporation>
            {
                new Corporation()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "corporation@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Honda Ferguson",
                    Phone = "1234567890",
                }
            };
            List<UserProfile> users = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    UserName = "corporation@solvechicago.com",
                    Corporations = new List<Corporation>
                    {
                        new Corporation()
                        {
                            Address1 = "123 Main Street",
                            Address2 = "Apt 2",
                            City = "Chicago",
                            Province = "IL",
                            Country = "USA",
                            Email = "corporation@solvechicago.com",
                            CreatedDate = DateTime.UtcNow.AddDays(-10),
                            Id = 1,
                            Name = "Honda Ferguson",
                            Phone = "1234567890",
                        }
                    },
                }
            };
            List<AspNetUser> aspnetUsers = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = "JHUIGYFTRDXS",
                    UserName = "corporation@solvechicago.com",
                    AspNetRoles = new List<AspNetRole>
                        {
                            new AspNetRole { Id = "KJHGF", Name = "Corporation" }
                        }
                }
            };

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("corporation@solvechicago.com");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var set = new Mock<DbSet<Corporation>>().SetupData(data);
            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var aspnetUserSet = new Mock<DbSet<AspNetUser>>().SetupData(aspnetUsers);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Corporations).Returns(set.Object);
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.AspNetUsers).Returns(aspnetUserSet.Object);
            BaseController controller = new BaseController(context.Object);

            //Set your controller ControllerContext with fake context
            controller.ControllerContext = controllerContext.Object;

            string name = controller.State.Corporations.First().Name;
            Assert.AreEqual("Honda Ferguson", name);

        }

        [TestMethod]
        public void RefreshState_Nonprofit()
        {
            List<Nonprofit> data = new List<Nonprofit>
            {
                new Nonprofit()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "nonprofit@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Vladimir Kruschev",
                    Phone = "1234567890",
                }
            };
            List<UserProfile> users = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    UserName = "nonprofit@solvechicago.com",
                    Nonprofits = new List<Nonprofit>
                    {
                        new Nonprofit()
                        {
                            Address1 = "123 Main Street",
                            Address2 = "Apt 2",
                            City = "Chicago",
                            Province = "IL",
                            Country = "USA",
                            Email = "nonprofit@solvechicago.com",
                            CreatedDate = DateTime.UtcNow.AddDays(-10),
                            Id = 1,
                            Name = "Vladimir Kruschev",
                            Phone = "1234567890",
                        }
                    },
                }
            };
            List<AspNetUser> aspnetUsers = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = "JHUIGYFTRDXS",
                    UserName = "nonprofit@solvechicago.com",
                    AspNetRoles = new List<AspNetRole>
                        {
                            new AspNetRole { Id = "KJHGF", Name = "Nonprofit" }
                        }
                }
            };

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("nonprofit@solvechicago.com");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var set = new Mock<DbSet<Nonprofit>>().SetupData(data);
            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var aspnetUserSet = new Mock<DbSet<AspNetUser>>().SetupData(aspnetUsers);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Nonprofits).Returns(set.Object);
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.AspNetUsers).Returns(aspnetUserSet.Object);
            BaseController controller = new BaseController(context.Object);

            //Set your controller ControllerContext with fake context
            controller.ControllerContext = controllerContext.Object;

            string name = controller.State.Nonprofits.First().Name;
            Assert.AreEqual("Vladimir Kruschev", name);

        }

        [TestMethod]
        public void RefreshState_Admin()
        {
            List<Admin> data = new List<Admin>
            {
                new Admin()
                {
                    Address1 = "123 Main Street",
                    Address2 = "Apt 2",
                    City = "Chicago",
                    Province = "IL",
                    Country = "USA",
                    Email = "admin@solvechicago.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Id = 1,
                    Name = "Peter Thompson",
                    Phone = "1234567890",
                    ProfilePicturePath = "../image.jpg",
                }
            };
            List<UserProfile> users = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    UserName = "admin@solvechicago.com",
                    Admins = new List<Admin>
                    {
                        new Admin()
                        {
                            Address1 = "123 Main Street",
                            Address2 = "Apt 2",
                            City = "Chicago",
                            Province = "IL",
                            Country = "USA",
                            Email = "admin@solvechicago.com",
                            CreatedDate = DateTime.UtcNow.AddDays(-10),
                            Id = 1,
                            Name = "Peter Thompson",
                            Phone = "1234567890",
                            ProfilePicturePath = "../image.jpg",
                        }
                    },
                }
            };
            List<AspNetUser> aspnetUsers = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = "JHUIGYFTRDXS",
                    UserName = "admin@solvechicago.com",
                    AspNetRoles = new List<AspNetRole>
                        {
                            new AspNetRole { Id = "KJHGF", Name = "Admin" }
                        }
                }
            };

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("admin@solvechicago.com");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            var set = new Mock<DbSet<Admin>>().SetupData(data);
            var userSet = new Mock<DbSet<UserProfile>>().SetupData(users);
            var aspnetUserSet = new Mock<DbSet<AspNetUser>>().SetupData(aspnetUsers);

            var context = new Mock<SolveChicagoEntities>();
            context.Setup(c => c.Admins).Returns(set.Object);
            context.Setup(c => c.UserProfiles).Returns(userSet.Object);
            context.Setup(c => c.AspNetUsers).Returns(aspnetUserSet.Object);
            BaseController controller = new BaseController(context.Object);

            //Set your controller ControllerContext with fake context
            controller.ControllerContext = controllerContext.Object;

            string name = controller.State.Admins.First().Name;
            Assert.AreEqual("Peter Thompson", name);

        }
    }
}