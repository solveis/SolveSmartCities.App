using SolveChicago.Common;
using SolveChicago.Common.Models.Profile;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Service
{
    public class MemberService : BaseService
    {
        public MemberService(SolveChicagoEntities db) : base(db) { }

        public MemberProfile Get(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
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
                    Id = member.Id,
                    Interests = member.Interests.Any() ? string.Join(",", member.Interests.Select(x => x.Name).ToArray()) : string.Empty,
                    LastName = member.LastName,
                    Nonprofits = GetNonprofits(member),
                    Phone = member.PhoneNumbers.Any() ? string.Join(",", member.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Addresses.Any() ? member.Addresses.Last().Province : string.Empty,
                    Schools = GetSchools(member),
                };
            }
        }

        private NonprofitEntity[] GetNonprofits(Member member)
        {
            MemberNonprofit[] memberNonprofits = member.MemberNonprofits.ToArray();
            if (memberNonprofits.Count() > 0)
            {
                return memberNonprofits.Select(x => new NonprofitEntity
                {
                    CaseManagerId = x.CaseManagerId,
                    CaseManagerName = string.Format("{0} {1}", x.CaseManager?.FirstName, x.CaseManager?.LastName),
                    Enjoyed = x.MemberEnjoyed,
                    Struggled = x.MemberStruggled,
                    NonprofitId = x.NonprofitId,
                    NonprofitName = x.Nonprofit?.Name,
                    SkillsAcquired = member.MemberSkills.Any(y => y.NonprofitId == x.NonprofitId) ? string.Join(",", member.MemberSkills.Where(y => y.NonprofitId == x.NonprofitId).Select(y => y.Skill.Name).ToArray()) : string.Empty,
                }).ToArray();
            }
            else
                return null;
        }

        private JobEntity[] GetJobs(Member member)
        {
            MemberCorporation[] memberCorporations = member.MemberCorporations.OrderByDescending(x => x.Start).ToArray();
            if (memberCorporations.Count() > 0)
                return memberCorporations.Select(x => new JobEntity { CorporationId = x.CorporationId, EmployeeEnd = x.End, EmployeePay = x.Pay, EmployeeStart = x.Start, Name = x.Corporation.Name }).ToArray();
            else
                return null;
        }

        private SchoolEntity[] GetSchools(Member member)
        {
            SchoolEntity[] schools = member.MemberSchools.Select(x => new SchoolEntity { Id = x.School.Id, Degree = x.Degree, End = x.End, IsCurrent = x.IsCurrent, Name = x.School.SchoolName, Type = x.School.Type, Start = x.Start, }).ToArray();
            if (schools.Count() > 0)
                return schools.OrderByDescending(x => x.Start).ToArray();
            else
                return null;
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
                return null;
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
                familyMembers.AddRange(member.MemberSpouses.Select(x => new FamilyMember { FirstName = x.Member1.FirstName, LastName = x.Member1.LastName, Relation = x.Member1.Gender.ToLowerInvariant() == "male" ? "Husband" : x.Member1.Gender.ToLowerInvariant() == "female" ? "Wife" : "Spouse", Id = x.Member1.Id }));
            else if (member.MemberSpouses1.Any(x => x.Member != null))
                familyMembers.AddRange(member.MemberSpouses1.Select(x => new FamilyMember { FirstName = x.Member.FirstName, LastName = x.Member.LastName, Relation = x.Member.Gender.ToLowerInvariant() == "male" ? "Husband" : x.Member.Gender.ToLowerInvariant() == "female" ? "Wife" : "Spouse", Id = x.Member.Id }));
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

                    familyMembers.Add(new FamilyMember { FirstName = child.FirstName, LastName = child.LastName, IsHeadOfHousehold = (child.IsHeadOfHousehold ?? false), Relation = currentChildTitle, Gender = child.Gender, Birthday = child.Birthday, Id = child.Id });

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

                    familyMembers.Add(new FamilyMember { FirstName = parent.FirstName, LastName = parent.LastName, IsHeadOfHousehold = (parent.IsHeadOfHousehold ?? false), Relation = currentParentTitle, Gender = parent.Gender, Birthday = parent.Birthday, Id = parent.Id });

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
                    UpdateMemberFamily(model, member);
                    UpdateMemberAddress(model, member);
                    UpdateMemberSchools(model, member);
                    UpdateMemberPhone(model, member);
                    UpdateMemberCorporations(model, member);
                    UpdateMemberNonprofits(model, member);
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

        private void UpdateMemberFamily(MemberProfile model, Member member)
        {
            List<FamilyMember> familyMembers = GetFamilyMembers(member).ToList();
            foreach(FamilyMember familyMember in model.Family?.FamilyMembers)
            {
                if(familyMember != null && !string.IsNullOrEmpty(familyMember.Name.Trim()) && familyMember.Gender != null && familyMember.Relation != null)
                {
                    Member existingFamilyMember = db.Members.Find(familyMember.Id);
                    if (existingFamilyMember == null)
                        existingFamilyMember = db.Members.Add(new Member { FirstName = familyMember.FirstName, LastName = familyMember.LastName, IsHeadOfHousehold = familyMember.IsHeadOfHousehold, Birthday = familyMember.Birthday, Gender = familyMember.Gender });
                    else if (string.IsNullOrEmpty(familyMember.FirstName) && string.IsNullOrEmpty(familyMember.LastName))
                        db.Members.Remove(existingFamilyMember);
                    if (!familyMembers.Select(x => x.Id).Contains(existingFamilyMember.Id))
                        AddFamilyMemberRelationship(existingFamilyMember, member, familyMember.Relation);
                }
            }
        }

        private void AddFamilyMemberRelationship(Member existingFamilyMember, Member member, string relation, bool isCurrent = true, bool isBiological = true)
        {
            switch(relation)
            {
                case Constants.Family.Relationships.Parent:
                    if(!member.Parents.Any(x => x.ParentId == existingFamilyMember.Id && x.ChildId == member.Id))
                        member.Parents.Add(new MemberParent { ParentId = existingFamilyMember.Id, ChildId = member.Id, IsBiological = isBiological, Children = member, Parents = existingFamilyMember });
                    break;
                case Constants.Family.Relationships.Child:
                    if (!member.Parents.Any(x => x.ParentId == member.Id && x.ChildId == existingFamilyMember.Id))
                        member.Children.Add(new MemberParent { ParentId = member.Id, ChildId = existingFamilyMember.Id, IsBiological = isBiological, Children = existingFamilyMember, Parents = member });
                    break;
                case Constants.Family.Relationships.Spouse:
                    if(!member.MemberSpouses.Any(x => x.Spouse_1_Id == existingFamilyMember.Id && x.Spouse_2_Id == member.Id) && !member.MemberSpouses.Any(x => x.Spouse_1_Id == member.Id && x.Spouse_2_Id == existingFamilyMember.Id))
                        member.MemberSpouses.Add(new MemberSpous { Spouse_1_Id = member.Id, Spouse_2_Id = existingFamilyMember.Id, IsCurrent = isCurrent, Member = member, Member1 = existingFamilyMember });
                    break;
            }
        }

        private void UpdateMemberInterests(MemberProfile model, Member member)
        {
            List<Interest> interests = db.Interests.ToList();
            string[] newInterests = model.Interests.Split(',');
            foreach (string interest in newInterests)
            {
                string trimInterest = interest.Trim();
                if (interests.Select(x => x.Name).Contains(trimInterest))
                {
                    Interest existingInterest = interests.Single(x => x.Name == trimInterest);
                    if (!member.Interests.Contains(existingInterest))
                        member.Interests.Add(existingInterest);
                }   
                else
                {
                    member.Interests.Add(new Interest { Name = interest });
                }
            }
        }

        private void UpdateMemberNonprofits(MemberProfile model, Member member)
        {
            foreach (var nonprofit in model.Nonprofits)
            {
                Nonprofit npo = db.Nonprofits.Where(x => (x.Id == nonprofit.NonprofitId || x.Name == nonprofit.NonprofitName)).FirstOrDefault();
                if (npo == null)
                {
                    npo = new Nonprofit
                    {
                        Name = nonprofit.NonprofitName,
                        CreatedDate = DateTime.UtcNow,
                    };
                    db.Nonprofits.Add(npo);
                }
                if (!member.MemberNonprofits.Select(x => x.NonprofitId).Contains(nonprofit.NonprofitId ?? 0))
                {
                    member.MemberNonprofits.Add(new MemberNonprofit
                    {
                        MemberEnjoyed = nonprofit.Enjoyed,
                        MemberStruggled = nonprofit.Struggled,
                        MemberId = member.Id,
                        NonprofitId = npo.Id,
                        Nonprofit = npo
                    });
                }
                UpdateMemberSkills(nonprofit, member);
            }
        }

        private void UpdateMemberSkills(NonprofitEntity nonprofit, Member member)
        {
            List<Skill> skills = db.Skills.ToList();
            string[] newSkills = nonprofit.SkillsAcquired.Split(',').ToArray();
            foreach (string skill in newSkills)
            {
                string trimSkill = skill.Trim();
                if (skills.Select(x => x.Name).Contains(trimSkill))
                {
                    Skill existingSkill = skills.Single(x => x.Name == trimSkill);
                    if(!member.MemberSkills.Select(x => x.SkillId).Contains(existingSkill.Id))
                        member.MemberSkills.Add(new MemberSkill { MemberId = member.Id, SkillId = existingSkill.Id, NonprofitId = nonprofit.NonprofitId });
                }
                else
                {
                    Skill newSkill = new Skill { Name = trimSkill };
                    db.Skills.Add(newSkill);
                    member.MemberSkills.Add(new MemberSkill { Skill = newSkill, NonprofitId = nonprofit.NonprofitId });
                }
            }
        }

        private void UpdateMemberCorporations(MemberProfile model, Member member)
        {
            foreach (var job in model.Jobs)
            {
                Corporation corporation = db.Corporations.Where(x => (x.Id == job.CorporationId || x.Name == job.Name)).FirstOrDefault();
                if (corporation == null)
                {
                    corporation = new Corporation
                    {
                        Name = job.Name,
                        CreatedDate = DateTime.UtcNow,
                    };
                    db.Corporations.Add(corporation);
                }
                if (!member.MemberCorporations.Select(x => x.CorporationId).Contains(job.CorporationId ?? 0))
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
                School school = db.Schools.Where(x => (x.Id == s.Id || x.SchoolName == s.Name)).FirstOrDefault();
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
                        Start = s.Start.Value
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