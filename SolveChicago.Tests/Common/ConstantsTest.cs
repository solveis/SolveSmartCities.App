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
    public class ConstantsTest
    {
        [Fact]
        public void Constants_Roles()
        {
            string admin = Constants.Roles.Admin;
            string member = Constants.Roles.Member;
            string caseManager = Constants.Roles.CaseManager;
            string corporation = Constants.Roles.Corporation;
            string nonprofit = Constants.Roles.Nonprofit;

            Assert.Equal("Admin", admin);
            Assert.Equal("Member", member);
            Assert.Equal("CaseManager", caseManager);
            Assert.Equal("Corporation", corporation);
            Assert.Equal("Nonprofit", nonprofit);
        }

        [Fact]
        public void Constants_Uploads()
        {
            string AdminPhotos = Constants.Upload.AdminPhotos;
            string MemberPhotos = Constants.Upload.MemberPhotos;
            string CaseManagerPhotos = Constants.Upload.CaseManagerPhotos;
            string CorporationPhotos = Constants.Upload.CorporationPhotos;
            string NonprofitPhotos = Constants.Upload.NonprofitPhotos;

            Assert.Equal("adminphotos", AdminPhotos);
            Assert.Equal("memberphotos", MemberPhotos);
            Assert.Equal("casemanagerphotos", CaseManagerPhotos);
            Assert.Equal("corporationphotos", CorporationPhotos);
            Assert.Equal("nonprofitphotos", NonprofitPhotos);
        }
    }
}
