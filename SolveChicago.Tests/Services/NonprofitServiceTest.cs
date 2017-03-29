using System;
using Xunit;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SolveChicago.Web.Services;
using SolveChicago.Web.Models.Profile;

namespace SolveChicago.Tests.Services
{
    public class NonprofitServiceTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();
        public NonprofitServiceTest()
        {
            List<Nonprofit> data = new List<Nonprofit>
            {
                new Nonprofit
                {
                    Id = 1,
                    Address1 = "123 Main Stree",
                    Address2 = null,
                    City = "Chicago",
                    Country = "USA",
                    Phone = "1234567890",
                    ProfilePicturePath = "../path.jpg",
                    Province = "Illinois",
                    Name = "Test Nonprofit",
                    CaseManagers = new List<CaseManager>
                    {
                        new CaseManager { Id = 1, FirstName = "Tim", LastName = "Keller" },
                        new CaseManager { Id = 2, FirstName = "Esme", LastName = "Pirouet" },
                    }
                }
            };

            var set = new Mock<DbSet<Nonprofit>>().SetupData(data);
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Nonprofits).Returns(set.Object);
        }

        [Fact]
        public void NonprofitService_Get_ReturnsNonprofitProfile()
        {
            NonprofitService service = new NonprofitService(context.Object);
            NonprofitProfile npo = service.Get(1);

            Assert.Equal("Test Nonprofit", npo.Name);
        }

        [Fact]
        public void NonprofitService_Get_ReturnsNull()
        {
            NonprofitService service = new NonprofitService(context.Object);
            NonprofitProfile npo = service.Get(2);

            Assert.Equal(null, npo);
        }

        [Fact]
        public void NonprofitService_Post_Success()
        {
            NonprofitProfile model = new NonprofitProfile
            {
                Id = 1,
                Name = "Test 2 Nonprofit"
            };


            NonprofitService service = new NonprofitService(context.Object);
            service.Post(model);
            Assert.Equal("Test 2 Nonprofit", context.Object.Nonprofits.First().Name);
        }

        [Fact]
        public void NonprofitService_Post_ThrowsException()
        {
            NonprofitProfile model = new NonprofitProfile
            {
                Id = 5,
                Name = "Test 2 Nonprofit"
            };


            NonprofitService service = new NonprofitService(context.Object);
            Assert.Throws<Exception>(() => service.Post(model));
        }

        [Fact]
        public void NonprofitService_GetCaseManagers_ReturnsCaseManagerArray()
        {
            NonprofitService service = new NonprofitService(context.Object);
            CaseManager[] cm = service.GetCaseManagers(1);

            Assert.Equal(2, cm.Count());
        }

        [Fact]
        public void NonprofitService_GetCaseManagers_InvalidId_ReturnsEmptyArray()
        {
            NonprofitService service = new NonprofitService(context.Object);
            CaseManager[] cm = service.GetCaseManagers(10);

            Assert.Equal(0, cm.Count());
        }
    }
}
