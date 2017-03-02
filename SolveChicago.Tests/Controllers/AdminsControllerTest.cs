using Moq;
using SolveChicago.Web.Controllers;
using SolveChicago.Web.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace SolveChicago.App.Tests.Controllers
{
    public class AdminsControllerTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();

        public AdminsControllerTest()
        {
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
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Admins).Returns(set.Object);
            
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
        public void Admin_Details_Succeed()
        {
            AdminsController controller = new AdminsController(context.Object);
            var result = (ViewResult)controller.Details(3);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Admin>(
                viewResult.ViewData.Model);
            Assert.Equal("Jerry Ellis", string.Format("{0} {1}", model.FirstName, model.LastName));
        }
    }
}