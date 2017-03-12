using SolveChicago.Web.Common;
using SolveChicago.Entities;
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
                return new MemberProfile
                {
                    Address1 = member.Addresses.Any() ? member.Addresses.Last().Address1 : string.Empty,
                    Address2 = member.Addresses.Any() ? member.Addresses.Last().Address2 : string.Empty,
                    Birthday = member.Birthday,
                    City = member.Addresses.Any() ? member.Addresses.Last().City : string.Empty,
                    Country = member.Addresses.Any() ? member.Addresses.Last().Country : string.Empty,
                    Email = member.Email,
                    Jobs = GetJobs(member),
                    Family = GetFamily(member),
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    GenderList = GetGenderList(),
                    Id = member.Id,
                    Interests = member.Interests.Any() ? string.Join(",", member.Interests.Select(x => x.Name).ToArray()) : string.Empty,
                    LastName = member.LastName,
                    NonprofitSkillsAcquired = member.MemberSkills.Any(x => x.NonprofitId == nonprofitId) ? string.Join(",", member.MemberSkills.Where(x => x.NonprofitId == nonprofitId).Select(x => x.Skill.Name).ToArray()) : string.Empty,
                    NonprofitName = npo.Name,
                    Phone = member.PhoneNumbers.Any() ? string.Join(",", member.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Addresses.Any() ? member.Addresses.Last().Province : string.Empty,
                    RelationshipList = GetRelationshipList(),
                    Schools = GetSchools(member),
                    SchoolTypeList = GetTypeList()
                };
            }
        }

        private JobEntity[] GetJobs(Member member)
        {
            MemberCorporation[] memberCorporations = member.MemberCorporations.OrderByDescending(x => x.Start).ToArray();
            if (memberCorporations.Count() > 0)
                return memberCorporations.Select(x => new JobEntity { CorporationId = x.CorporationId, EmployeeEnd = x.End, EmployeePay = x.Pay, EmployeeStart = x.Start, Name = x.Corporation.Name }).ToArray();
            else
                return new JobEntity[1];
        }

        private string[] GetGenderList()
        {
            return new string[]
            {
                "Male",
                "Female",
                "Other"
            };
        }

        private SchoolEntity[] GetSchools(Member member)
        {
            SchoolEntity[] schools = member.MemberSchools.Select(x => new SchoolEntity { Id = x.School.Id, Degree = x.Degree, End = x.End, IsCurrent = x.IsCurrent, Name = x.School.SchoolName, Type = x.School.Type, DegreeList = GetDegreeList(), Start = x.Start, }).ToArray();
            if (schools.Count() > 0)
                return schools.OrderByDescending(x => x.Start).ToArray();
            else
                return new SchoolEntity[1];
        }

        private string[] GetDegreeList()
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

        private string[] GetTypeList()
        {
            return new string[] {
                "High School",
                "Undergraduate College",
                "Graduate College",
                "Post Graduate College",
            };
        }

        private string[] GetRelationshipList()
        {
            return new string[]
            {
                "Parent",
                "Child",
                "Spouse"
            };
        }

        private FamilyEntity GetFamily(Member member)
        {
            Family memberFamily = member.Family;
            if (memberFamily != null)
            {
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
                    FamilyMembers = GetFamilyMembers(member),
                };
                
                return family;
            }
            else
            {
                return new FamilyEntity() { FamilyMembers = new FamilyMember[1] };
            }
        }

        private FamilyMember[] GetFamilyMembers(Member member)
        {
            List<FamilyMember> familyMembers = new List<FamilyMember>();
            // build tree;

            GetParentTree(familyMembers, member);
            GetChildTree(familyMembers, member);
            GetSpouseTree(familyMembers, member);

            return familyMembers.ToArray();
        }

        private static void GetSpouseTree(List<FamilyMember> familyMembers, Member member)
        {
            if (member.MemberSpouses.Any(x => x.Member1 != null))
                familyMembers.AddRange(member.MemberSpouses.Select(x => new FamilyMember { FirstName = x.Member1.FirstName, LastName = x.Member1.LastName, Relation = x.Member1.Gender.ToLowerInvariant() == "male" ? "Husband" : x.Member1.Gender.ToLowerInvariant() == "female" ? "Wife" : "Spouse" }));
            else if (member.MemberSpouses1.Any(x => x.Member != null))
                familyMembers.AddRange(member.MemberSpouses1.Select(x => new FamilyMember { FirstName = x.Member.FirstName, LastName = x.Member.LastName, Relation = x.Member.Gender.ToLowerInvariant() == "male" ? "Husband" : x.Member.Gender.ToLowerInvariant() == "female" ? "Wife" : "Spouse" }));
        }

        private static void GetChildTree(List<FamilyMember> familyMembers, Member member)
        {
            Member currentMember = member;
            if (currentMember.Children.Any())
            {
                Member[] currentChildren = currentMember.Children.Select(x => x.Children).ToArray();
                string currentChildPrefix = "";
                foreach (var child in currentChildren)
                {
                    if (string.IsNullOrEmpty(currentChildPrefix))
                        currentChildPrefix = "";
                    else if (!string.IsNullOrEmpty(currentChildPrefix) && !currentChildPrefix.ToLowerInvariant().Contains("great") && !currentChildPrefix.ToLowerInvariant().Contains("grand"))
                        currentChildPrefix = "Grand"; //
                    else if (!string.IsNullOrEmpty(currentChildPrefix) && currentChildPrefix.ToLowerInvariant().Contains("grand") && !currentChildPrefix.ToLowerInvariant().Contains("great"))
                        currentChildPrefix = "Great-grand";
                    else
                        currentChildPrefix = "Great-" + currentChildPrefix.ToLowerInvariant();

                    string currentChildTitle = currentChildPrefix + (child.Gender.ToLowerInvariant() == "male" ? (string.IsNullOrEmpty(currentChildPrefix) ? "Son" : "son") : child.Gender.ToLowerInvariant() == "female" ? (string.IsNullOrEmpty(currentChildPrefix) ? "Daughter" : "daughter") : (string.IsNullOrEmpty(currentChildPrefix) ? "Child" : "child"));

                    familyMembers.Add(new FamilyMember { FirstName = child.FirstName, LastName = child.LastName, IsHeadOfHousehold = (child.IsHeadOfHousehold ?? false), Relation = currentChildTitle, Gender = child.Gender, Birthday = child.Birthday });

                    GetChildTree(familyMembers, child);
                }
            }
        }

        private static void GetParentTree(List<FamilyMember> familyMembers, Member member)
        {
            Member currentMember = member;
            if (currentMember.Parents.Any())
            {
                Member[] currentParents = currentMember.Parents.Select(x => x.Parents).ToArray();
                string currentParentPrefix = "";
                foreach (var parent in currentParents)
                {
                    if (string.IsNullOrEmpty(currentParentPrefix))
                        currentParentPrefix = "";
                    else if (!string.IsNullOrEmpty(currentParentPrefix) && !currentParentPrefix.ToLowerInvariant().Contains("great") && !currentParentPrefix.ToLowerInvariant().Contains("grand"))
                        currentParentPrefix = "Grand"; //
                    else if (!string.IsNullOrEmpty(currentParentPrefix) && currentParentPrefix.ToLowerInvariant().Contains("grand") && !currentParentPrefix.ToLowerInvariant().Contains("great"))
                        currentParentPrefix = "Great-grand";
                    else
                        currentParentPrefix = "Great-" + currentParentPrefix.ToLowerInvariant();

                    string currentParentTitle = currentParentPrefix + (parent.Gender.ToLowerInvariant() == "male" ? (string.IsNullOrEmpty(currentParentPrefix) ? "Father" : "father") : parent.Gender.ToLowerInvariant() == "female" ? (string.IsNullOrEmpty(currentParentPrefix) ? "Mother" : "mother") : (string.IsNullOrEmpty(currentParentPrefix) ? "Parent" : "parent"));

                    familyMembers.Add(new FamilyMember { FirstName = parent.FirstName, LastName = parent.LastName, IsHeadOfHousehold = (parent.IsHeadOfHousehold ?? false), Relation = currentParentTitle, Gender = parent.Gender, Birthday = parent.Birthday });

                    GetParentTree(familyMembers, parent);
                }
            }
        }

        public void UpdateProfile(MemberProfile model)
        {
            Member member = db.Members.Find(model.Id);
            if (member == null)
                throw new Exception($"Member with an id of {model.Id} not found");
            else
            {
                try
                {
                    UpdateMemberPersonalInformation(model, member);
                    UpdateMemberAddress(model, member);
                    UpdateMemberSchools(model, member);
                    UpdateMemberPhone(model, member);
                    UpdateMemberCorporations(model, member);
                    UpdateMemberSkills(model, member);
                    UpdateMemberInterests(model, member);

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void UpdateMemberPersonalInformation(MemberProfile model, Member member)
        {
            member.Birthday = model.Birthday;
            member.Email = model.Email;
            member.FirstName = model.FirstName;
            member.Gender = model.Gender;
            member.Id = model.Id;
            member.LastName = model.LastName;
            member.ProfilePicturePath = UploadPhoto(Constants.Upload.MemberPhotos, model.ProfilePicture, member.Id);
        }

        private void UpdateMemberInterests(MemberProfile model, Member member)
        {
            List<Interest> interests = db.Interests.ToList();
            string[] newInterests = model.Interests.Split(',');
            foreach (string interest in newInterests)
            {
                if (interests.Select(x => x.Name).Contains(interest))
                    member.Interests.Add(interests.Single(x => x.Name == interest));
                else
                    member.Interests.Add(new Interest { Name = interest });
            }
        }

        private void UpdateMemberSkills(MemberProfile model, Member member)
        {
            List<Skill> skills = db.Skills.ToList();
            string[] newSkills = model.NonprofitSkillsAcquired.Split(',');
            foreach (string skill in newSkills)
            {
                if (skills.Select(x => x.Name).Contains(skill))
                    member.MemberSkills.Add(new MemberSkill { MemberId = member.Id, SkillId = skills.Single(x => x.Name == skill).Id });
                else
                    db.Skills.Add(new Skill { Name = skill, MemberSkills = new List<MemberSkill> { new MemberSkill { MemberId = member.Id } } });
            }
        }

        private void UpdateMemberCorporations(MemberProfile model, Member member)
        {
            foreach (var job in model.Jobs)
            {
                Corporation corporation = db.Corporations.Where(x => x.Id == job.CorporationId).FirstOrDefault();
                if (corporation == null)
                {
                    corporation = new Corporation
                    {
                        Name = job.Name,
                        CreatedDate = DateTime.UtcNow,
                    };
                    db.Corporations.Add(corporation);
                }
                if (!member.MemberCorporations.Select(x => x.CorporationId).Contains(job.CorporationId))
                {
                    member.MemberCorporations.Add(new MemberCorporation
                    {
                        End = job.EmployeeEnd,
                        Pay = job.EmployeePay,
                        ReasonForLeaving = job.EmployeeReasonForLeaving,
                        Start = job.EmployeeStart.Value,
                        Corporation = corporation
                    });
                }
            }
        }

        private void UpdateMemberPhone(MemberProfile model, Member member)
        {
            PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
            if (phone == null)
            {
                phone = new PhoneNumber { Number = model.Phone };
                db.PhoneNumbers.Add(phone);
            }
            member.PhoneNumbers.Add(phone);
        }

        private void UpdateMemberSchools(MemberProfile model, Member member)
        {
            foreach (var s in model.Schools)
            {
                School school = db.Schools.Where(x => x.Id == s.Id).FirstOrDefault();
                if (school == null)
                {
                    school = new School
                    {
                        SchoolName = s.Name,
                        Type = s.Type,
                    };
                    db.Schools.Add(school);
                }
                if (!member.MemberSchools.Select(x => x.SchoolId).Contains(s.Id))
                {
                    member.MemberSchools.Add(new MemberSchool
                    {
                        Degree = s.Degree,
                        End = s.End,
                        IsCurrent = s.IsCurrent,
                        School = school,
                        Start = s.Start
                    });
                }
            }
        }

        private void UpdateMemberAddress(MemberProfile model, Member member)
        {
            Address address = db.Addresses.SingleOrDefault(x => x.Address1 == model.Address1 && x.Address2 == model.Address2 && x.City == model.City && x.Country == model.Country && x.Province == model.Province && x.ZipCode == model.ZipCode);
            if (address == null)
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
                db.Addresses.Add(address);
            }
            member.Addresses.Add(address);
        }
    }
}