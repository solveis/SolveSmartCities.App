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

        public MemberProfile Get(int id, int nonprofitId)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                Nonprofit npo = member.MemberNonprofits.Single(x => x.NonprofitId == nonprofitId).Nonprofit;
                MemberCorporation memberCorporation = member.MemberCorporations.Any() ? member.MemberCorporations.OrderByDescending(x => x.Start).First(): null;
                SchoolEntity[] schools = GetSchools(member);
                return new MemberProfile
                {
                    Address1 = member.Addresses.Any() ? member.Addresses.Last().Address1 : string.Empty,
                    Address2 = member.Addresses.Any() ? member.Addresses.Last().Address2 : string.Empty,
                    Birthday = member.Birthday,
                    City = member.Addresses.Any() ? member.Addresses.Last().City : string.Empty,
                    Country = member.Addresses.Any() ? member.Addresses.Last().Country : string.Empty,
                    Email = member.Email,
                    Employer = memberCorporation != null ? memberCorporation.Corporation : null,
                    EmployerName = memberCorporation != null ? memberCorporation.Corporation.Name : string.Empty,
                    EmployerEnd = memberCorporation != null ? memberCorporation.End : (DateTime?)null,
                    EmployerPay = memberCorporation != null ? memberCorporation.Pay : (decimal?)null,
                    EmployerReasonForLeaving = memberCorporation != null ? memberCorporation.ReasonForLeaving : string.Empty,
                    EmployerStart = memberCorporation != null ? memberCorporation.Start : (DateTime?)null,
                    Family = GetFamily(member),
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    Id = member.Id,
                    Interests = member.Interests.Any() ? string.Join(",", member.Interests.Select(x => x.Name).ToArray()) : string.Empty,
                    LastName = member.LastName,
                    Nonprofit = npo,
                    NonprofitSkillsAcquired = member.MemberSkills.Any(x => x.NonprofitId == nonprofitId) ? string.Join(",", member.MemberSkills.Where(x => x.NonprofitId == nonprofitId).Select(x => x.Skill.Name).ToArray()) : string.Empty,
                    NonprofitName = npo.Name,
                    Phone = member.PhoneNumbers.Any() ? string.Join(",", member.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Addresses.Any() ? member.Addresses.Last().Province : string.Empty,
                };
            }
        }

        public SchoolEntity[] GetSchools(Member member)
        {
            SchoolEntity[] schools = member.MemberSchools.Select(x => new SchoolEntity { Id = x.School.Id, Degree = x.Degree,  End = x.End, IsCurrent = x.IsCurrent, Name = x.School.SchoolName, Type = x.School.Type, DegreeList = GetDegreeList(), Start = x.Start, TypeList = GetTypeList(), }).ToArray();
            return schools.OrderByDescending(x => x.Start).ToArray();
        }

        public string[] GetDegreeList()
        {
            return new string[]
            {
                "HS Diploma",
                "GED",
                "Bachelor's Degree",
                "Master's Degree",
                "Post Graduate Degree"
            };
        }

        public string[] GetTypeList()
        {
            return new string[] {
                "High School",
                "Undergraduate College",
                "Graduate College",
                "Post Graduate College",
            };
        }

        private FamilyEntity GetFamily(Member member)
        {
            Family memberFamily = member.Family;
            FamilyEntity family = new FamilyEntity
            {
                Address1 = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Address1 : string.Empty,
                Address2 = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Address2 : string.Empty,
                City = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().City : string.Empty,
                Province = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Province : string.Empty,
                Country = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Country : string.Empty,
                FamilyName = memberFamily.FamilyName,
                Phone = memberFamily.PhoneNumbers.Any() ? memberFamily.PhoneNumbers.Last().Number : string.Empty,
                ZipCode = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().ZipCode : string.Empty,
                FamilyMembers = memberFamily.Members.Select(x => new FamilyMember { FirstName = x.FirstName, LastName = x.LastName, IsHeadOfHousehold = (x.IsHeadOfHousehold ?? false) }).ToArray()
            };
            if(member.MemberSpouses.Any(x => x.Member1 != null))
                family.FamilyMembers.ToList().AddRange(member.MemberSpouses.Select(x => new FamilyMember { FirstName = x.Member1.FirstName, LastName = x.Member1.LastName, Relation = x.Member1.Gender.ToLowerInvariant() == "male" ? "Husband" : x.Member1.Gender.ToLowerInvariant() == "female" ? "Wife" : "Spouse" }));
            else if (member.MemberSpouses1.Any(x => x.Member != null))
                family.FamilyMembers.ToList().AddRange(member.MemberSpouses1.Select(x => new FamilyMember { FirstName = x.Member.FirstName, LastName = x.Member.LastName, Relation = x.Member.Gender.ToLowerInvariant() == "male" ? "Husband" : x.Member.Gender.ToLowerInvariant() == "female" ? "Wife" : "Spouse" }));

            return family;            
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
                    member.Birthday = model.Birthday;
                    member.Email = model.Email;
                    member.FirstName = model.FirstName;
                    member.Gender = model.Gender;
                    member.Id = model.Id;
                    member.LastName = model.LastName;
                    member.ProfilePicturePath = model.ProfilePicturePath;


                    // add address
                    Address address = db.Addresses.SingleOrDefault(x => x.Address1 == model.Address1 && x.Address2 == model.Address2 && x.City == model.City && x.Country == model.Country && x.Province == model.Province && x.ZipCode == model.ZipCode);
                    if(address == null)
                    {
                        address = new Address
                        {
                            Address1 = model.Address1,
                            Address2 = model.Address2,
                            City = model.City,
                            Country = model.Country,
                            ZipCode = model.ZipCode,
                            Province = model.Province,
                        };
                    }
                    member.Addresses.Add(address);
                    

                    foreach(var s in model.Schools)
                    {
                        School school = db.Schools.Where(x => x.Id == s.Id).FirstOrDefault();
                        if(school == null)
                        {
                            school = new School
                            {
                                SchoolName = s.Name,
                                Type = s.Type,
                            };
                        }
                        if(!member.MemberSchools.Select(x => x.SchoolId).Contains(s.Id))
                        {
                            member.MemberSchools.Add(new MemberSchool
                            {
                                Degree = s.Degree,
                                End = s.End,
                                IsCurrent = s.IsCurrent,
                                SchoolId = school.Id,
                                Start = s.Start
                            });
                        }
                    }

                    // add phone
                    PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
                    if (phone == null)
                        phone = new PhoneNumber { Number = model.Phone };
                    member.PhoneNumbers.Add(phone);


                    // add corporation
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