using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using SolveChicago.Web.Common;

namespace SolveChicago.Tests.Common
{
    [TestClass]
    public class ConstantsTest
    {
        [TestMethod]
        public void Can_Instantiate_Roles()
        {
            string admin = Constants.Roles.Admin;
            string member = Constants.Roles.Member;
            string caseManager = Constants.Roles.CaseManager;
            string corporation = Constants.Roles.Corporation;
            string nonprofit = Constants.Roles.Nonprofit;

            Assert.AreEqual("Admin", admin);
            Assert.AreEqual("Member", member);
            Assert.AreEqual("CaseManager", caseManager);
            Assert.AreEqual("Corporation", corporation);
            Assert.AreEqual("Nonprofit", nonprofit);
        }

        [TestMethod]
        public void Can_Instantiate_Uploads()
        {
            string AdminPhotos = Constants.Upload.AdminPhotos;
            string MemberPhotos = Constants.Upload.MemberPhotos;
            string CaseManagerPhotos = Constants.Upload.CaseManagerPhotos;
            string CorporationPhotos = Constants.Upload.CorporationPhotos;
            string NonprofitPhotos = Constants.Upload.NonprofitPhotos;

            Assert.AreEqual("adminphotos", AdminPhotos);
            Assert.AreEqual("memberphotos", MemberPhotos);
            Assert.AreEqual("casemanagerphotos", CaseManagerPhotos);
            Assert.AreEqual("corporationphotos", CorporationPhotos);
            Assert.AreEqual("nonprofitphotos", NonprofitPhotos);
        }
    }
}
