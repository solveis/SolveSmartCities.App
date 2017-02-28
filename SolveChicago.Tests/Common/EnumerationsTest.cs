using System;
using Xunit;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.Web.Common;

namespace SolveChicago.Tests.Common
{
    public class EnumerationsTest
    {
        [Fact]
        public void Can_Instantiate_Roles()
        {
            int role = (int)Enumerations.Role.Admin;
            Assert.Equal(4, role);
        }
    }
}
