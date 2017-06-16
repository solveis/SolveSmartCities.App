using HtmlAgilityPack;
using Moq;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using SolveChicago.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace SolveChicago.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class NonprofitsControllerTest : BaseController
    {
        [Fact]
        public void NonprofitsController_Index_ReturnsView()
        {
            List<Nonprofit> data = new List<Nonprofit>
            {
                new Nonprofit
                {
                    Id = 1,
                    Name = "NPO 1",
                    NonprofitMembers = new List<NonprofitMember>
                    {
                        new NonprofitMember
                        {
                            Member = new Member
                            {
                                Id = 2,
                                FirstName = "Nick",
                                LastName = "Jenkins",
                                SurveyStep = "Complete",
                                Family = new Family
                                {
                                    Id = 1,
                                    FamilyName = "Jenkins",
                                    Addresses = new List<Address>
                                    {
                                        new Address
                                        {
                                            Id = 1,
                                            Address1 = "123 Main Street",
                                            Address2 = "Apt 2F",
                                            City = "Chicago",
                                            Country = "USA",
                                            Province = "IL",
                                            ZipCode = "60624"
                                        }
                                    },
                                    PhoneNumbers = new List<PhoneNumber>
                                    {
                                        new PhoneNumber
                                        {
                                            Id = 1,
                                            Number = "1234567890"
                                        }
                                    }
                                },
                                MemberParents = new List<MemberParent>
                                {
                                    new MemberParent
                                    {
                                        Member1 = new Member
                                        {
                                            Id = 1,
                                            Gender = "Male",
                                            FirstName = "Javier",
                                            LastName = "Jenkins"
                                        },
                                        Member = new Member
                                        {
                                            Id = 2,
                                            Gender = "Male",
                                            FirstName = "Nick",
                                            LastName = "Jenkins",
                                        }
                                    }
                                }
                            },
                        }
                    }
                }
            };

            List<AspNetUser> users = new List<AspNetUser>
            {
                new AspNetUser
                {
                    Id = ")*(&T*^F&TUGYIHUOIJ",
                    Email = "test@email.com",
                    UserName = "test@email.com",
                    AspNetRoles = new List<AspNetRole>
                    {
                        new AspNetRole
                        {
                            Id = "IVY*^&O*&FLI",
                            Name = "Nonprofit"
                        }
                    },
                    Nonprofits = new List<Nonprofit>
                    {
                        new Nonprofit
                        {
                            Id = 1,
                            Name = "NPO 1",
                            NonprofitMembers = new List<NonprofitMember>
                            {
                                new NonprofitMember
                                {
                                    Member = new Member
                                    {
                                        Id = 2,
                                        FirstName = "Nick",
                                        LastName = "Jenkins",
                                        SurveyStep = "Complete",
                                        Family = new Family
                                        {
                                            Id = 1,
                                            FamilyName = "Jenkins",
                                            Addresses = new List<Address>
                                            {
                                                new Address
                                                {
                                                    Id = 1,
                                                    Address1 = "123 Main Street",
                                                    Address2 = "Apt 2F",
                                                    City = "Chicago",
                                                    Country = "USA",
                                                    Province = "IL",
                                                    ZipCode = "60624"
                                                }
                                            },
                                            PhoneNumbers = new List<PhoneNumber>
                                            {
                                                new PhoneNumber
                                                {
                                                    Id = 1,
                                                    Number = "1234567890"
                                                }
                                            }
                                        },
                                        MemberParents1 = new List<MemberParent>
                                        {
                                            new MemberParent
                                            {
                                                Member = new Member
                                                {
                                                    Id = 2,
                                                    Gender = "Male",
                                                    FirstName = "Nick",
                                                    LastName = "Jenkins",
                                                },
                                                Member1 = new Member
                                                {
                                                    Id = 4,
                                                    Gender = "Other",
                                                    FirstName = "Pat",
                                                    LastName = "Jenkins",
                                                    MemberParents = new List<MemberParent>
                                                    {
                                                        new MemberParent
                                                        {
                                                            Member1 = new Member
                                                            {
                                                                Id = 5,
                                                                Gender = "Male",
                                                                FirstName = "Noah",
                                                                LastName = "Jenkins",
                                                            },
                                                            Member = new Member
                                                            {
                                                                Id = 4,
                                                                Gender = "Other",
                                                                FirstName = "Pat",
                                                                LastName = "Jenkins",
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        MemberParents = new List<MemberParent>
                                        {
                                            new MemberParent
                                            {
                                                Member1 = new Member
                                                {
                                                    Id = 1,
                                                    Gender = "Male",
                                                    FirstName = "Javier",
                                                    LastName = "Jenkins",
                                                    MemberParents = new List<MemberParent>
                                                    {
                                                        new MemberParent
                                                        {
                                                            Member1 = new Member
                                                            {
                                                                Id = 3,
                                                                Gender = "Female",
                                                                FirstName = "Natalie",
                                                                LastName = "Jenkins"
                                                            },
                                                            Member = new Member
                                                            {
                                                                Id = 1,
                                                                Gender = "Male",
                                                                FirstName = "Javier",
                                                                LastName = "Jenkins",
                                                            }
                                                        }
                                                    }
                                                },
                                                Member = new Member
                                                {
                                                    Id = 2,
                                                    Gender = "Male",
                                                    FirstName = "Nick",
                                                    LastName = "Jenkins",
                                                }
                                            }
                                        }
                                    },
                                }
                            }
                        }
                    }
                }
            };

            // mock solvechicagoentities
            var context = new Mock<SolveChicagoEntities>();
            var set = new Mock<DbSet<Nonprofit>>().SetupData(data);
            set.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));
            context.Setup(c => c.Nonprofits).Returns(set.Object);

            var userSet = new Mock<DbSet<AspNetUser>>().SetupData(users);
            userSet.Setup(m => m.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => users.FirstOrDefault(d => d.Id == (string)ids[0]));
            context.Setup(c => c.AspNetUsers).Returns(userSet.Object);

            // mock Identity
            var username = "test@email.com";
            var userId = ")*(&T*^F&TUGYIHUOIJ";
            var identity = new GenericIdentity(username, "");
            var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, userId);
            identity.AddClaim(nameIdentifierClaim);

            var mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            Thread.CurrentPrincipal = mockPrincipal.Object;
            
            // mock controller
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.User).Returns(mockPrincipal.Object);
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            
            NonprofitsController controller = new NonprofitsController(context.Object) { ControllerContext = controllerContext.Object };

            // act
            var result = controller.Index(1) as ViewResult;
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<FamilyEntity>>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }
    }
}
