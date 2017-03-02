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
                Nonprofit npo = member.MemberNonprofits.Single(x => x.NonprofitId == nonprofitId).Nonprofit;
                MemberCorporation memberCorporation = member.MemberCorporations.Any() ? member.MemberCorporations.OrderByDescending(x => x.Start).First(): null;
                return new MemberProfile
                {
                    Address1 = member.Address1,
                    Address2 = member.Address2,
                    Birthday = member.Birthday,
                    City = member.City,
                    Country = member.Country,
                    Degree = member.Degree,
                    Email = member.Email,
                    Employer = memberCorporation.Corporation,
                    EmployerName = memberCorporation.Corporation.Name,
                    EmployerEnd = memberCorporation.End,
                    EmployerPay = memberCorporation.Pay,
                    EmployerReasonForLeaving = memberCorporation.ReasonForLeaving,
                    EmployerStart = memberCorporation.Start,
                    Family = member.Family,
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    HighestEducation = member.HighestEducation,
                    Id = member.Id,
                    Interests = member.Interests.Any() ? member.Interests.Select(x => x.Name).ToArray() : new string[0],
                    LastName = member.LastName,
                    LastSchool = member.LastSchool,
                    Nonprofit = npo,
                    NonprofitSkillsAcquired = member.MemberSkills.Where(x => x.NonprofitId == nonprofitId).Select(x => x.Skill.Name) .ToArray(),
                    NonprofitName = npo.Name,
                    Phone = member.Phone,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Province,
                };
            }
        }
    }
}