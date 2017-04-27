using System;
using Xunit;
using Moq;
using SolveChicago.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SolveChicago.Service;
using SolveChicago.Web.Controllers;

namespace SolveChicago.Tests.Services
{
    
    public class CaseManagerServiceTest
    {
        Mock<SolveChicagoEntities> context = new Mock<SolveChicagoEntities>();
        public CaseManagerServiceTest()
        {
            List<CaseNote> caseNotes = new List<CaseNote>();
            List<CaseManager> caseManagers = new List<CaseManager>
            {
                new CaseManager
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    NonprofitId = 1,
                    Nonprofit = new Nonprofit
                    {
                        Id = 1,
                        Name = "Nonprofit 1",
                         NonprofitMembers = new List<NonprofitMember>
                         {
                            new NonprofitMember
                            {
                                NonprofitId = 1,
                                Member = new Member
                                {
                                    Id = 123,
                                    FirstName = "Terry",
                                    LastName = "Jones"
                                }
                            }
                         }
                    }
                }
            };


            var caseManagerSet = new Mock<DbSet<CaseManager>>().SetupData(caseManagers);
            caseManagerSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => caseManagers.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.CaseManagers).Returns(caseManagerSet.Object);

            var caseNoteSet = new Mock<DbSet<CaseNote>>().SetupData(caseNotes);
            caseNoteSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => caseNotes.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.CaseNotes).Returns(caseNoteSet.Object);
        }

        [Fact]
        public void CaseManagerService_GetMembersForCaseManager_ReturnsMemberArray()
        {
            CaseManagerService service = new CaseManagerService(context.Object);
            Member[] members = service.GetMembersForCaseManager(1);

            Assert.Equal(1, members.Count());
            Assert.Equal("Terry", members.First().FirstName);
        }
    }
}
