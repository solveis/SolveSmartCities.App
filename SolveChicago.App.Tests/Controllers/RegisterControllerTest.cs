using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.App.Web.Controllers;

namespace SolveChicago.App.Tests.Controllers
{     
    [TestClass]
    public class RegisterControllerTest
    {
        //[TestMethod]
        public void Can_Register_Member()
        {
            string email = string.Format("{0}@solvechicago.com", Guid.NewGuid().ToString());
            string password = Guid.NewGuid().ToString();

            RegisterController controller = new RegisterController();
            controller.Member(new Web.Models.RegisterViewModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            }).Wait();
        }
    }
}
