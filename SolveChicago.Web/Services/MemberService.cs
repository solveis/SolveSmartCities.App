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

        public MemberProfile Get(int id, int nonprofitId)
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
                    Employer = memberCorporation != null ? memberCorporation.Corporation : null,
                    EmployerName = memberCorporation != null ? memberCorporation.Corporation.Name : string.Empty,
                    EmployerEnd = memberCorporation != null ? memberCorporation.End : (DateTime?)null,
                    EmployerPay = memberCorporation != null ? memberCorporation.Pay : (decimal?)null,
                    EmployerReasonForLeaving = memberCorporation != null ? memberCorporation.ReasonForLeaving : string.Empty,
                    EmployerStart = memberCorporation != null ? memberCorporation.Start : (DateTime?)null,
                    Family = member.Family,
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    HighestEducation = member.HighestEducation,
                    Id = member.Id,
                    Interests = member.Interests.Any() ? string.Join(",", member.Interests.Select(x => x.Name).ToArray()) : string.Empty,
                    LastName = member.LastName,
                    LastSchool = member.LastSchool,
                    Nonprofit = npo,
                    NonprofitSkillsAcquired = member.MemberSkills.Any(x => x.NonprofitId == nonprofitId) ? string.Join(",", member.MemberSkills.Where(x => x.NonprofitId == nonprofitId).Select(x => x.Skill.Name).ToArray()) : string.Empty,
                    NonprofitName = npo.Name,
                    Phone = member.Phone,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Province,
                };
            }
        }

        public bool Post(MemberProfile model)
        {
            Member member = db.Members.Find(model.Id);
            if (member == null)
                return false;
            else
            {
                try
                {
                    Nonprofit npo = model.Nonprofit;

                    member.Address1 = model.Address1;
                    member.Address2 = model.Address2;
                    member.Birthday = model.Birthday;
                    member.City = model.City;
                    member.Country = model.Country;
                    member.Degree = model.Degree;
                    member.Email = model.Email;
                    member.FirstName = model.FirstName;
                    member.Gender = model.Gender;
                    member.HighestEducation = model.HighestEducation;
                    member.Id = model.Id;
                    member.LastName = model.LastName;
                    member.LastSchool = model.LastSchool;
                    member.Phone = model.Phone;
                    member.ProfilePicturePath = model.ProfilePicturePath;
                    member.Province = model.Province;

                    Corporation corporation = model.Employer != null ? db.Corporations.Where(x => x.Id == model.Employer.Id).FirstOrDefault() : null;
                    if (corporation == null)
                    {
                        corporation = new Corporation
                        {
                            Name = model.EmployerName,
                            CreatedDate = DateTime.UtcNow,
                        };
                    }
                    member.MemberCorporations.Add(new MemberCorporation
                    {
                        CorporationId = corporation.Id,
                        End = model.EmployerEnd,
                        Start = model.EmployerStart.Value,
                        MemberId = model.Id,
                        Pay = model.EmployerPay,
                        ReasonForLeaving = model.EmployerReasonForLeaving
                    });

                    // add skills to db if doesn't exist
                    List<Skill> skills = db.Skills.ToList();
                    string[] newSkills = model.NonprofitSkillsAcquired.Split(',');
                    foreach (string skill in newSkills)
                    {
                        if (skills.Select(x => x.Name).Contains(skill))
                            member.MemberSkills.Add(new MemberSkill { MemberId = member.Id, SkillId = skills.Single(x => x.Name == skill).Id });
                        else
                            db.Skills.Add(new Skill { Name = skill, MemberSkills = new List<MemberSkill> { new MemberSkill { MemberId = member.Id } } });
                    }

                    // add interests to db if doesn't exist
                    List<Interest> interests = db.Interests.ToList();
                    string[] newInterests = model.Interests.Split(',');
                    foreach (string interest in newInterests)
                    {
                        if (interests.Select(x => x.Name).Contains(interest))
                            member.Interests.Add(interests.Single(x => x.Name == interest));
                        else
                            member.Interests.Add(new Interest { Name = interest });
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}