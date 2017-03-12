using System;
using Xunit;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.Web.Common;
using System.Diagnostics.CodeAnalysis;

namespace SolveChicago.Tests.Common
{
    [ExcludeFromCodeCoverage]
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
