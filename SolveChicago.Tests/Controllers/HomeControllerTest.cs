using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Xunit;
using SolveChicago.Web;
using SolveChicago.Web.Controllers;
using System.Diagnostics.CodeAnalysis;

namespace SolveChicago.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeControllerTest
    {
        [Fact]
        public void HomeController_Index_ReturnsViewResult()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HomeController_About_ReturnsViewResult()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HomeController_Contact_ReturnsViewResult()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
