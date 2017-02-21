using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.App.Service;
using SolveChicago.App.Common.Entities;
using SolveChicago.App.Common;

namespace SolveChicago.App.Tests.Common
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
