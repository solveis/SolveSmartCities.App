using SolveChicago.Web.Data;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Web.Services
{
    public class MemberService : BaseService
    {
        public MemberService(SolveChicagoEntities db) : base(db) { }
        public MemberService() : base() { }

        public MemberProfile GetMember(int id, int nonprofitId)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                return new MemberProfile
                {
                    Address1 = member.Family.Address1,
                    Address2 = member.Family.Address2,
                    Birthday = member.Birthday,
                    City = member.Family.City,
                    Country = member.Family.Country,
                    Degree = member.Degree,
                    Email = member.Email,
                    Employer = member.MemberCorporations.Any() ? member.MemberCorporations.OrderByDescending(x => x.Start).First().Corporation : new Corporation(),
                    Family = member.Family,
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    HighestEducation = member.HighestEducation,
                    Id = member.Id,
                    Interests = member.Interests.Select(x => x.Name).ToArray(),
                    LastName = member.LastName,
                    LastSchool = member.LastSchool,
                    Nonprofit = member.MemberNonprofits.Single(x => x.NonprofitId == nonprofitId).Nonprofit,
                    Phone = member.Phone,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Family.Province
                };
            }
        }
    }
}