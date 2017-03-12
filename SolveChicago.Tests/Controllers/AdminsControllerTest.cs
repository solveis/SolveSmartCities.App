using Moq;
using SolveChicago.Web.Controllers;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace SolveChicago.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class AdminsControllerTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();

        public AdminsControllerTest()
        {
            List<AspNetUser> users = new List<AspNetUser>();
            List<Admin> data = new List<Admin>
            {
                new Admin
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = " Doe",
                    AspNetUsers = new List<AspNetUser>
                    {
                        new AspNetUser
                        {
                            Id = ")(*Y&T^RDTXFGCHVUJI"
                        }
                    }
                },
                new Admin
                {
                    Id = 2,
                    FirstName = "Sayid",
                    LastName = "Khan",
                    AspNetUsers = new List<AspNetUser>
                    {
                        new AspNetUser
                        {
                            Id = "BHVGYCRD%$^&*()O"
                        }
                    }
                },
                new Admin
                {
                    Id = 3,
                    FirstName = "Jerry",
                    LastName = "Ellis",
                    AspNetUsers = new List<AspNetUser>
                    {
                        new AspNetUser
                        {
                            Id = "HGSE#%$^&G&IUBH"
                        }
                    }
                }
            };

            var set = new Mock<DbSet<Admin>>().SetupData(data);
            var userSet = new Mock<DbSet<AspNetUser>>().SetupData(users);
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Admins).Returns(set.Object);
            context.Setup(c => c.AspNetUsers).Returns(userSet.Object);

        }
        

        [Fact]
        public void Admin_Index()
        {
            AdminsController controller = new AdminsController(context.Object);
            var result = (ViewResult)controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Admin>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void Admin_Details_Fails()
        {
            AdminsController controller = new AdminsController(context.Object);
            var result = (HttpStatusCodeResult)controller.Details(null);

            Assert.Equal(new HttpStatusCodeResult(HttpStatusCode.BadRequest).StatusCode, result.StatusCode);
        }

        [Fact]
        public void Admin_Details_NotFound()
        {
            AdminsController controller = new AdminsController(context.Object);
            var result = (HttpStatusCodeResult)controller.Details(0);

            Assert.Equal(new HttpStatusCodeResult(HttpStatusCode.NotFound).StatusCode, result.StatusCode);
        }

        [Fact]
        public void Admin_Details_Succeed()
        {
            AdminsController controller = new AdminsController(context.Object);
            var result = (ViewResult)controller.Details(3);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Admin>(
                viewResult.ViewData.Model);
            Assert.Equal("Jerry Ellis", string.Format("{0} {1}", model.FirstName, model.LastName));
        }

        [Fact]
        public void Admin_Create_Get()
        {
            AdminsController controller = new AdminsController(context.Object);
            var result = (ViewResult)controller.Create();
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Admin_Create_Post_Succeed()
        {
            Admin admin = new Admin
            {
                CreatedDate = DateTime.UtcNow,
                Email = "email@email.com",
                FirstName = "Joe",
                LastName = "Smith",
                InvitedBy = "BHVGYCRD%$^&*()O",
                Phone = "1234567890",
                ProfilePicturePath = "../path.jpg"
            };
            AdminsController controller = new AdminsController(context.Object);
            var result = (RedirectToRouteResult)controller.Create(admin);

            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Null(result.RouteValues["controller"]);

        }
    }
}