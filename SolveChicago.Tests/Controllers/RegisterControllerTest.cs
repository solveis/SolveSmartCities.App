using System;
using Xunit;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.Web.Controllers;
using System.Diagnostics.CodeAnalysis;

namespace SolveChicago.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class RegisterControllerTest
    {
        //[Fact]
        public void Can_Register_Member()
        {
            string email = string.Format("{0}@solvechicago.com", Guid.NewGuid().ToString());
            string password = Guid.NewGuid().ToString();

            RegisterController controller = new RegisterController();
            controller.Member(new Web.Models.MemberRegisterViewModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = password
            }).Wait();
        }
    }
}
