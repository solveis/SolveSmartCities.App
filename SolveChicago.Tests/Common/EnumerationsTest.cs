using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.Web.Common;

namespace SolveChicago.Tests.Common
{
    [TestClass]
    public class EnumerationsTest
    {
        [TestMethod]
        public void Can_Instantiate_Roles()
        {
            int role = (int)Enumerations.Role.Admin;
            Assert.AreEqual(4, role);
        }
    }
}
