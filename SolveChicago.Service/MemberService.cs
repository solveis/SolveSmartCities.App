using SolveChicago.Common;
using SolveChicago.Common.Models.Profile.Member;
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

        public Member GetMemberByEmail(string email)
        {
            return db.Members.Single(x => x.Email == email);
        }

        public void AddToReferrer(int memberId, int referrerId)
        {
            Member member = db.Members.Find(memberId);
            Referrer referrer = db.Referrers.Find(referrerId);
            if (member != null && referrer != null)
                member.Referrers.Add(referrer);
            db.SaveChanges();
        }

        public FamilyEntity[] GetAll()
        {
            Family[] dbFamilies = db.Families.ToArray();
            List<FamilyEntity> families = new List<FamilyEntity>();
            foreach(Family fam in dbFamilies)
            {
                families.Add(GetFamily(fam.Members.First(), true));
            }
            return families.ToArray();
        }

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
                    CaseManagers = member.NonprofitMembers.SelectMany(x => x.CaseManagers).ToArray(),
                    City = member.Addresses.Any() ? member.Addresses.Last().City : string.Empty,
                    ContactPreference = member.ContactPreference,
                    Country = member.Addresses.Any() ? member.Addresses.Last().Country : string.Empty,
                    Email = member.Email,
                    Jobs = GetJobs(member),
                    Family = GetFamily(member),
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    Id = member.Id,
                    Interests = member.Interests.Any() ? string.Join(", ", member.Interests.Select(x => x.Name).ToArray()) : string.Empty,
                    IsHeadOfHousehold = member.IsHeadOfHousehold ?? false,
                    LastName = member.LastName,
                    Nonprofits = GetNonprofits(member),
                    Phone = member.PhoneNumbers.Any() ? string.Join(", ", member.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Addresses.Any() ? member.Addresses.Last().Province : string.Empty,
                    Schools = GetSchools(member),
                    Income = member.Income,
                    IsMilitary = member.IsMilitary ?? false,
                    Military = GetMilitary(member),
                    GovernmentPrograms = GetGovernmentPrograms(member),
                    MemberStage = GetMemberStage(member),
                    Skills = GetMemberSkills(member, true),
                    SkillsDesired = GetMemberSkills(member, false),
                    InterestedInWorkforceSkill = member.IsWorkforceInterested ?? false,
                    ZipCode = member.Addresses.Any() ? member.Addresses.Last().ZipCode : string.Empty,
                };
            }
        }

        private string GetMemberSkills(Member member, bool isComplete)
        {
            if (member.MemberSchools.Count() > 0)
                return string.Join(", ", member.MemberSkills.Where(x => x.IsComplete == isComplete).Select(x => x.Skill.Name));
            else
                return string.Empty;
        }

        private static MemberStage GetMemberStage(Member member)
        {
            MemberStage model = new MemberStage
            {
                Stage = Constants.Member.Stage.OffTrack,
                Percent = 0,
            };
            if (member.SurveyStep == Constants.Member.SurveyStep.Invited)
            { // survey sent
                model.Stage = Constants.Member.Stage.InviteSent;
                model.Percent = (int)Math.Round(12.5);
            }
            if (member.SurveyStep != null && member.SurveyStep != Constants.Member.SurveyStep.Invited && member.SurveyStep != Constants.Member.SurveyStep.Complete)
            { // survey is in progress
                model.Stage = Constants.Member.Stage.ProfileInProgress;
                model.Percent = (int)Math.Round(25.0);
            }
            if (member.SurveyStep == Constants.Member.SurveyStep.Complete && !member.NonprofitMembers.Any(x => x.Start > member.CreatedDate && !x.End.HasValue) && !member.MemberCorporations.Any(x => x.Start > member.CreatedDate && !x.End.HasValue))
            { // survey is complete, and they are not currently in a NPO or Job
                model.Stage = Constants.Member.Stage.ProfileCompleted;
                model.Percent = (int)Math.Round(37.5);
            }
            if (member.NonprofitMembers.Any(x => x.Start >= member.CreatedDate && !x.End.HasValue && x.Nonprofit.Skills.Any(y => y.Name == Constants.Skills.SoftSkills)))
            { // they are in a soft skills NPO
                model.Stage = Constants.Member.Stage.InSoftSkillsTraining;
                model.Percent = (int)Math.Round(50.0);
            }
            if (member.NonprofitMembers.Any(x => x.Start >= member.CreatedDate && x.End.HasValue && x.End.Value <= DateTime.UtcNow && x.Nonprofit.Skills.Any(y => y.Name == Constants.Skills.SoftSkills)) && member.MemberSkills.Any(x => x.IsComplete && x.Skill.Name == Constants.Skills.SoftSkills))
            { // they have completed a soft skills NPO, and have gained soft skills
                model.Stage = Constants.Member.Stage.SoftSkillsAcquired;
                model.Percent = (int)Math.Round(62.5);
            }
            if (member.NonprofitMembers.Any(x => x.Start >= member.CreatedDate && !x.End.HasValue && x.Nonprofit.Skills.Any(y => y.Name != Constants.Skills.SoftSkills)))
            { // they are in a workforce NPO
                model.Stage = Constants.Member.Stage.InWorkforceTraining;
                model.Percent = (int)Math.Round(75.0);
            }
            if (member.NonprofitMembers.Any(x => x.Start >= member.CreatedDate && x.End.HasValue && x.End.Value <= DateTime.UtcNow && x.Nonprofit.Skills.Any(y => y.Name != Constants.Skills.SoftSkills)) && !member.NonprofitMembers.Any(x => x.Start > member.CreatedDate && !x.End.HasValue) && member.MemberSkills.Any(x => x.IsComplete && x.Skill.Name != Constants.Skills.SoftSkills))
            { // they have completed a workforce NPO, and have a workforce skill
                model.Stage = Constants.Member.Stage.WorkforceSkillsAcquired;
                model.Percent = (int)Math.Round(87.5);
            }
            if (member.MemberCorporations.Any(x => x.Start >= member.CreatedDate && !x.End.HasValue))
            { // they have a job
                model.Stage = Constants.Member.Stage.JobPlaced;
                model.Percent = 100;
            }
            return model;
        }

        public MemberProfilePersonal GetProfilePersonal(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                return new MemberProfilePersonal
                {
                    Address1 = member.Addresses.Any() ? member.Addresses.Last().Address1 : string.Empty,
                    Address2 = member.Addresses.Any() ? member.Addresses.Last().Address2 : string.Empty,
                    Birthday = member.Birthday,
                    City = member.Addresses.Any() ? member.Addresses.Last().City : string.Empty,
                    Country = member.Addresses.Any() ? member.Addresses.Last().Country : string.Empty,
                    Email = member.Email,
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    Id = member.Id,
                    Interests = member.Interests.Any() ? string.Join(", ", member.Interests.Select(x => x.Name).ToArray()) : string.Empty,
                    IsHeadOfHousehold = member.IsHeadOfHousehold ?? false,
                    LastName = member.LastName,
                    Phone = member.PhoneNumbers.Any() ? string.Join(",", member.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty,
                    ProfilePicturePath = member.ProfilePicturePath,
                    Province = member.Addresses.Any() ? member.Addresses.Last().Province : string.Empty,
                    Income = member.Income,
                    ZipCode = member.Addresses.Any() ? member.Addresses.Last().ZipCode : string.Empty,
                    ContactPreference = member.ContactPreference,
                    IsMilitary = member.IsMilitary ?? false,
                    Skills = member.MemberSkills.Any(x => x.IsComplete) ? string.Join(", ", member.MemberSkills.Where(x => x.IsComplete).Select(x => x.Skill.Name).ToArray()) : string.Empty,
                };
            }
        }

        public MemberProfileFamily GetProfileFamily(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                return new MemberProfileFamily
                {
                    MemberId = member.Id,
                    IsHeadOfHousehold = member.IsHeadOfHousehold ?? false,
                    Family = GetFamily(member),
                };
            }
        }

        public MemberProfileSchools GetProfileSchools(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                return new MemberProfileSchools
                {
                    MemberId = member.Id,
                    Schools = GetSchools(member),
                };
            }
        }

        public MemberProfileNonprofits GetProfileNonprofits(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                return new MemberProfileNonprofits
                {
                    MemberId = member.Id,    
                    InterestedInWorkforceSkill = member.IsWorkforceInterested,
                    SkillsDesiredIds = member.MemberSkills.Where(x => !x.IsComplete).Select(x => x.SkillId).ToArray(),
                };
            }
        }

        public MemberProfileJobs GetProfileJobs(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                return new MemberProfileJobs
                {
                    MemberId = member.Id,
                    CurrentlyLooking = member.IsJobSearching,
                    Jobs = GetJobs(member),
                };
            }
        }

        public MemberProfileGovernmentPrograms GetProfileGovernmentPrograms(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
                return null;
            else
            {
                return new MemberProfileGovernmentPrograms
                {
                    MemberId = member.Id,
                    GovernmentProgramsIds = GetGovernmentProgramIds(member)
                };
            }
        }

        private MilitaryEntity[] GetMilitary(Member member)
        {
            return member.MemberMilitaries.Select(x => new MilitaryEntity
            {
                Id = x.MilitaryId,
                MilitaryBranch = x.MilitaryBranch.BranchName,
            }).ToArray();
        }

        private GovernmentProgramEntity[] GetGovernmentPrograms(Member member)
        {
            List<GovernmentProgramEntity> programs = member.MemberGovernmentPrograms.Select(x => new GovernmentProgramEntity
            {
                ProgramId = x.Id,
                ProgramName = x.GovernmentProgram.Name,
            }).ToList();

            if (programs.Count() > 0)
                return programs.ToArray();
            else
                return null;
        }

        private int[] GetGovernmentProgramIds(Member member)
        {
            FamilyEntity family = GetFamily(member);
            int[] programs = member.MemberGovernmentPrograms.Select(x => x.GovernmentProgramId).ToArray();

            //if(family != null && family.FamilyMembers.Count() > 0)
            //{
            //    foreach (FamilyMember fMember in family.FamilyMembers)
            //    {
            //        programs.AddRange(db.MemberGovernmentPrograms.Where(x => x.MemberId == fMember.Id).Select(x => new GovernmentProgramEntity
            //        {
            //            Amount = x.Amount,
            //            End = x.End,
            //            Id = x.Id,
            //            IsCurrent = x.End == null,
            //            MemberId = x.MemberId,
            //            ProgramId = x.GovernmentProgramId,
            //            Start = x.Start,
            //            ProgramList = programList,
            //            FamilyList = familyDictionary
            //        }).ToList());
            //    }
            //}
            if (programs.Count() > 0)
                return programs;
            else
                return null;
        }

        private NonprofitEntity[] GetNonprofits(Member member)
        {
            NonprofitMember[] NonprofitMembers = member.NonprofitMembers.ToArray();
            if (NonprofitMembers.Count() > 0)
            {
                return NonprofitMembers.Select(x => new NonprofitEntity
                {
                    CaseManagers = x.CaseManagers.ToArray(),
                    Enjoyed = x.MemberEnjoyed,
                    Struggled = x.MemberStruggled,
                    NonprofitId = x.NonprofitId,
                    NonprofitName = x.Nonprofit?.Name,
                    SkillsAcquired = member.MemberSkills.Any(y => y.NonprofitId == x.NonprofitId) ? string.Join(",", member.MemberSkills.Where(y => y.NonprofitId == x.NonprofitId).Select(y => y.Skill.Name).ToArray()) : string.Empty,
                    Start = x.Start,
                    End = x.End,
                }).ToArray();
            }
            else
                return null;
        }

        private JobEntity[] GetJobs(Member member)
        {
            MemberCorporation[] memberCorporations = member.MemberCorporations.OrderByDescending(x => x.Start).ToArray();
            if (memberCorporations.Count() > 0)
                return memberCorporations.Select(x => new JobEntity { CorporationId = x.CorporationId, EmployeeEnd = x.End, EmployeePay = x.Pay, EmployeeStart = x.Start, Name = x.Corporation.Name, IsCurrent = !x.End.HasValue, EmployeeReasonForLeaving = x.ReasonForLeaving }).ToArray();
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

        public FamilyEntity GetFamily(Member member, bool includeSelf = false)
        {
            Family memberFamily = member.Family;
            if (memberFamily != null)
            {
                FamilyEntity family = new FamilyEntity
                {
                    Id = memberFamily.Id,
                    Address1 = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Address1 : string.Empty,
                    Address2 = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Address2 : string.Empty,
                    City = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().City : string.Empty,
                    Province = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Province : string.Empty,
                    Country = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().Country : string.Empty,
                    FamilyName = memberFamily.FamilyName,
                    Phone = memberFamily.PhoneNumbers.Any() ? memberFamily.PhoneNumbers.Last().Number : string.Empty,
                    ZipCode = memberFamily.Addresses.Any() ? memberFamily.Addresses.Last().ZipCode : string.Empty,
                    FamilyMembers = GetFamilyMembers(member, includeSelf),
                };
                family.AverageScore = (int)(family.FamilyMembers.Where(x => x.IsEditable).Count() > 0 ? family.FamilyMembers.Where(x => x.IsEditable).Average(x => x.MemberStage.Percent) : 0);
                family.HeadOfHouseholdProfilePicturePath = family.FamilyMembers.Any(x => (x.IsHeadOfHousehold ?? false)) ? family.FamilyMembers.FirstOrDefault(x => (x.IsHeadOfHousehold.Value)).ProfilePicturePath : Constants.Member.NoPhotoUrl;
                
                return family;
            }
            else
            {
                FamilyEntity family = new FamilyEntity
                {
                    Address1 = member.Addresses.Any() ? member.Addresses.Last().Address1 : string.Empty,
                    Address2 = member.Addresses.Any() ? member.Addresses.Last().Address2 : string.Empty,
                    City = member.Addresses.Any() ? member.Addresses.Last().City : string.Empty,
                    Province = member.Addresses.Any() ? member.Addresses.Last().Province : string.Empty,
                    Country = member.Addresses.Any() ? member.Addresses.Last().Country : string.Empty,
                    FamilyName = member.LastName,
                    Phone = member.PhoneNumbers.Any() ? member.PhoneNumbers.Last().Number : string.Empty,
                    ZipCode = member.Addresses.Any() ? member.Addresses.Last().ZipCode : string.Empty,
                    FamilyMembers = GetFamilyMembers(member, includeSelf),
                    HeadOfHouseholdProfilePicturePath = !string.IsNullOrEmpty(member.ProfilePicturePath) ? member.ProfilePicturePath : Constants.Member.NoPhotoUrl,
                };
                family.AverageScore = GetMemberStage(member).Percent;
                return family;
            }
        }

        public FamilyEntity GetFamily(int memberId)
        {
            Member member= db.Members.Find(memberId);
            if (member == null)
                return null;
            else
                return GetFamily(member);
        }

        private FamilyMember[] BuildFamilyTree(Member member, List<FamilyMember> familyMembers = null)
        {
            if (familyMembers == null)
                familyMembers = new List<FamilyMember>();

            GetParentTree(familyMembers, member);
            GetSiblingTree(familyMembers, member);
            GetChildTree(familyMembers, member);
            GetSpouseTree(familyMembers, member);
            GetUnknownRelationTree(familyMembers, member);

            return familyMembers.ToArray();
        }

        private FamilyMember[] GetFamilyMembers(Member member, bool includeSelf = false)
        {
            List<FamilyMember> familyMembers = new List<FamilyMember>();

            if (includeSelf)
                familyMembers.Add(new FamilyMember { Birthday = member.Birthday, FirstName = member.FirstName, Gender = member.Gender, Id = member.Id, IsHeadOfHousehold = member.IsHeadOfHousehold, LastName = member.LastName, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member), IsEditable = true });
            
            return BuildFamilyTree(member, familyMembers);
        }

        private static void GetUnknownRelationTree(List<FamilyMember> familyMembers, Member member)
        {
            if (member.Family != null && member.Family.Members.Count() > 0)
            {
                Member[] fm = member.Family?.Members?.ToArray();
                foreach (Member m in fm)
                {
                    FamilyMember f = new FamilyMember { FirstName = m.FirstName, Birthday = m.Birthday, Gender = m.Gender, Id = m.Id, IsHeadOfHousehold = m.IsHeadOfHousehold, LastName = m.LastName, Email = m.Email, Phone = m.PhoneNumbers.Any() ? string.Join(", ", m.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(m.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : m.ProfilePicturePath, MemberStage = GetMemberStage(m), CurrentOccupation = GetFamilyMemberOccupation(m) };
                    if (!familyMembers.Select(x => x.Id).Contains(f.Id) && f.Id != member.Id)
                        familyMembers.Add(f);

                }
            }
        }

        private static void GetSpouseTree(List<FamilyMember> familyMembers, Member member)
        {
            // get spouse tree in both directions
            if (member.MemberSpouses.Any(x => x.Member1 != null))
                familyMembers.AddRange(member.MemberSpouses.Select(x => new FamilyMember { FirstName = x.Member1.FirstName, LastName = x.Member1.LastName, Relation = "Spouse", FriendlyRelationName = string.Format("{0}{1}",(x.IsCurrent ? "Ex-" : ""), (x.Member1.Gender.ToLower() == "male" ? "Husband" : x.Member1.Gender.ToLower() == "female" ? "Wife" : "Spouse")), Id = x.Member1.Id, Email = x.Member1.Email, Phone = x.Member1.PhoneNumbers.Any() ? string.Join(", ", x.Member1.PhoneNumbers.Select(y => y.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(x.Member1.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : x.Member1.ProfilePicturePath, MemberStage = GetMemberStage(x.Member1), CurrentOccupation = GetFamilyMemberOccupation(x.Member1), Birthday = x.Member1.Birthday, Gender = x.Member1.Gender }));
            if (member.MemberSpouses1.Any(x => x.Member != null))
                familyMembers.AddRange(member.MemberSpouses1.Select(x => new FamilyMember { FirstName = x.Member.FirstName, LastName = x.Member.LastName, Relation = "Spouse", FriendlyRelationName = string.Format("{0}{1}", (x.IsCurrent ? "Ex-" : ""), (x.Member.Gender.ToLower() == "male" ? "Husband" : x.Member.Gender.ToLower() == "female" ? "Wife" : "Spouse")), Id = x.Member.Id, Email = x.Member.Email, Phone = x.Member.PhoneNumbers.Any() ? string.Join(", ", x.Member.PhoneNumbers.Select(y => y.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(x.Member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : x.Member.ProfilePicturePath, MemberStage = GetMemberStage(x.Member), CurrentOccupation = GetFamilyMemberOccupation(x.Member), Birthday = x.Member.Birthday, Gender = x.Member.Gender }));
        }

        private static void GetSiblingTree(List<FamilyMember> familyMembers, Member member)
        {
            Member currentMember = member;
            if (currentMember.MemberParents.Any())
            {
                Member[] currentParents = currentMember.MemberParents.Select(x => x.Member1).ToArray();
                foreach (var parents in currentParents)
                {
                    Member[] siblings = parents.MemberParents1.Where(x => x.Member.Id != member.Id).Select(x => x.Member).ToArray();
                    foreach (Member sibling in siblings)
                    {
                        FamilyMember fm = new FamilyMember { FirstName = sibling.FirstName, Birthday = sibling.Birthday, Gender = sibling.Gender, Id = sibling.Id, IsHeadOfHousehold = sibling.IsHeadOfHousehold, LastName = sibling.LastName, Relation = "Sibling", FriendlyRelationName = (sibling.Gender.ToLower() == "male" ? "Brother" : sibling.Gender.ToLower() == "female" ? "Sister" : "Sibling"), Email = sibling.Email, Phone = sibling.PhoneNumbers.Any() ? string.Join(", ", sibling.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(sibling.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : sibling.ProfilePicturePath, MemberStage = GetMemberStage(sibling), CurrentOccupation = GetFamilyMemberOccupation(sibling) };
                        if (!familyMembers.Select(x => x.Id).Contains(fm.Id))
                            familyMembers.Add(fm);
                    }
                }
            }
        }

        private static void GetChildTree(List<FamilyMember> familyMembers, Member member, string currentChildPrefix = "")
        {
            Member currentMember = member;
            if (currentMember.MemberParents1.Any())
            {
                Member[] currentChildren = currentMember.MemberParents1.Select(x => x.Member).ToArray();
                foreach (var child in currentChildren)
                {
                    string currentChildTitle = currentChildPrefix + (child.Gender.ToLower() == "male" ? (string.IsNullOrEmpty(currentChildPrefix) ? "Son" : "son") : child.Gender.ToLower() == "female" ? (string.IsNullOrEmpty(currentChildPrefix) ? "Daughter" : "daughter") : (string.IsNullOrEmpty(currentChildPrefix) ? "Child" : "child"));
                    familyMembers.Add(new FamilyMember { FirstName = child.FirstName, LastName = child.LastName, IsHeadOfHousehold = (child.IsHeadOfHousehold ?? false), Relation = $"{currentChildPrefix}Child", FriendlyRelationName = currentChildTitle, Gender = child.Gender, Birthday = child.Birthday, Id = child.Id, Email = child.Email, Phone = child.PhoneNumbers.Any() ? string.Join(", ", child.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(child.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : child.ProfilePicturePath, MemberStage = GetMemberStage(child), CurrentOccupation = GetFamilyMemberOccupation(child) });

                    //set prefix for recursive child generations
                    string futureChildPrefix = currentChildPrefix;
                    if (string.IsNullOrEmpty(futureChildPrefix))
                        futureChildPrefix = "Grand";
                    else if (!string.IsNullOrEmpty(futureChildPrefix) && !futureChildPrefix.ToLower().Contains("great") && futureChildPrefix.ToLower().Contains("grand"))
                        futureChildPrefix = "Great-grand";
                    else
                        futureChildPrefix = "Great-" + futureChildPrefix.ToLower();

                    GetChildTree(familyMembers, child, futureChildPrefix);
                }
            }
        }

        private static void GetParentTree(List<FamilyMember> familyMembers, Member member, string currentParentPrefix = "")
        {
            Member currentMember = member;
            if (currentMember.MemberParents.Any())
            {
                Member[] currentParents = currentMember.MemberParents.Select(x => x.Member1).ToArray();
                foreach (var parent in currentParents)
                {
                    string currentParentTitle = currentParentPrefix + (parent.Gender.ToLower() == "male" ? (string.IsNullOrEmpty(currentParentPrefix) ? "Father" : "father") : parent.Gender.ToLower() == "female" ? (string.IsNullOrEmpty(currentParentPrefix) ? "Mother" : "mother") : (string.IsNullOrEmpty(currentParentPrefix) ? "Parent" : "parent"));
                    familyMembers.Add(new FamilyMember { FirstName = parent.FirstName, LastName = parent.LastName, IsHeadOfHousehold = (parent.IsHeadOfHousehold ?? false), Relation = $"{currentParentPrefix}Parent", FriendlyRelationName = currentParentTitle, Gender = parent.Gender, Birthday = parent.Birthday, Id = parent.Id, Email = parent.Email, Phone = parent.PhoneNumbers.Any() ? string.Join(", ", parent.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(parent.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : parent.ProfilePicturePath, MemberStage = GetMemberStage(parent), CurrentOccupation = GetFamilyMemberOccupation(parent) });

                    //set prefix for recursive parent generations
                    string futureParentPrefix = currentParentPrefix;
                    if (string.IsNullOrEmpty(currentParentPrefix))
                        futureParentPrefix = "Grand";
                    else if (!string.IsNullOrEmpty(currentParentPrefix) && !currentParentPrefix.ToLower().Contains("great") && currentParentPrefix.ToLower().Contains("grand"))
                        futureParentPrefix = "Great-grand";
                    else
                        futureParentPrefix = "Great-" + currentParentPrefix.ToLower();

                    GetParentTree(familyMembers, parent, futureParentPrefix);
                }
            }
        }

        private static string GetFamilyMemberOccupation(Member member)
        {
            string stage = GetMemberStage(member).Stage;
            if (stage == Constants.Member.Stage.InSoftSkillsTraining || stage == Constants.Member.Stage.InWorkforceTraining)
            {
                if (member.NonprofitMembers.Any(x => x.Start > member.CreatedDate))
                    return member.NonprofitMembers.OrderByDescending(x => x.Start).FirstOrDefault().Nonprofit.Name;
                else
                    return "In Training";
            }
            else if (stage == Constants.Member.Stage.JobPlaced)
            {
                if (member.MemberCorporations.Any(x => x.Start > member.CreatedDate))
                    return member.MemberCorporations.OrderByDescending(x => x.Start).FirstOrDefault().Corporation.Name;
                else
                    return "Job Placed";
            }
            else
                return string.Empty;
        }

        public void UpdateMemberGovernmentPrograms(MemberProfileGovernmentPrograms model)
        {
            if (model.GovernmentProgramsIds != null)
            {
                Member member = db.Members.Find(model.MemberId);
                if (member == null)
                    throw new Exception($"Member with an id of {model.MemberId} not found");
                else
                {
                    foreach (var program in model.GovernmentProgramsIds)
                    {
                        MemberGovernmentProgram mgp = member.MemberGovernmentPrograms.Where(x => x.GovernmentProgramId == program).FirstOrDefault();
                        if (mgp == null)
                        {
                            mgp = new MemberGovernmentProgram
                            {
                                MemberId = member.Id,
                                GovernmentProgramId = program,
                            };
                            member.MemberGovernmentPrograms.Add(mgp);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public void UpdateMemberPersonal(MemberProfilePersonal model)
        {
            Member member = db.Members.Find(model.Id);
            if (member == null)
                throw new Exception($"Member with an id of {model.Id} not found");
            else
            {
                member.Birthday = model.Birthday;
                member.Email = model.Email;
                member.FirstName = model.FirstName;
                member.Gender = model.Gender;
                member.Id = model.Id;
                member.LastName = model.LastName;
                if(model.ProfilePicture != null)
                    member.ProfilePicturePath = UploadPhoto(Constants.Upload.MemberPhotos, model.ProfilePicture, member.Id);
                member.IsMilitary = model.IsMilitary;
                member.ContactPreference = model.ContactPreference;
                member.Income = model.Income;
                member.IsHeadOfHousehold = model.IsHeadOfHousehold;

                UpdateMemberAddress(model.Address1, model.Address2, model.City, model.Province, model.ZipCode, model.Country, member);
                MatchOrCreateFamily(member);
                UpdateMemberPhone(model.Phone, member);
                UpdateMemberInterests(model.Interests, member);
                UpdateMemberMilitary(model.MilitaryId, "", "", "", member);
                UpdateMemberSkills(!string.IsNullOrEmpty(model.Skills) ? model.Skills : "", member, true);

                db.SaveChanges();
            }
        }

        public void MatchOrCreateFamily(Member member)
        {
            Family[] families = db.Families.ToArray();
            Address address = member.Addresses.FirstOrDefault();
            if (member.FamilyId.HasValue)
                return;
            else if(families.Any(x => x.Addresses.Any(y => y.Address1 == address.Address1 && y.Address2 == address.Address2 && y.City == address.City && y.Province == address.Province && y.ZipCode == address.ZipCode && y.Country == address.Country)))
            {
                Family family = families.Where(x => x.Addresses.Any(y => y.Address1 == address.Address1 && y.Address2 == address.Address2 && y.City == address.City && y.Province == address.Province && y.ZipCode == address.ZipCode && y.Country == address.Country)).First();
                family.Members.Add(member);
            }
            else
            {
                member.Family = new Family { FamilyName = member.LastName, PhoneNumbers = member.PhoneNumbers.ToArray(), Addresses = member.Addresses.ToArray(), CreatedDate = DateTime.UtcNow };
            }
        }

        public void UpdateMemberFamily(MemberProfileFamily model)
        {
            Member member = db.Members.Find(model.MemberId);
            if (member == null)
                throw new Exception($"Member with an id of {model.MemberId} not found");
            else
            {
                List<FamilyMember> familyMembers = GetFamilyMembers(member).ToList();
                foreach (FamilyMember familyMember in model.Family?.FamilyMembers)
                {
                    if (familyMember != null && familyMember.Gender != null && familyMember.Relation != null)
                    {
                        Member existingFamilyMember = null;
                        if (familyMember.Id.HasValue)
                             existingFamilyMember = db.Members.Find(familyMember.Id);
                        if (existingFamilyMember == null)
                        {
                            existingFamilyMember = new Member { FirstName = familyMember.FirstName, LastName = familyMember.LastName, IsHeadOfHousehold = familyMember.IsHeadOfHousehold, Birthday = familyMember.Birthday, Gender = familyMember.Gender, FamilyId = member.FamilyId, Email = familyMember.Email, CreatedDate = DateTime.UtcNow };
                            db.Members.Add(existingFamilyMember);
                            db.SaveChanges();
                        }
                        else if (string.IsNullOrEmpty(familyMember.FirstName) && string.IsNullOrEmpty(familyMember.LastName))
                            db.Members.Remove(existingFamilyMember);
                        if (!familyMembers.Select(x => x.Id).Contains(existingFamilyMember.Id))
                            AddFamilyMemberRelationship(existingFamilyMember, member, familyMember.Relation);
                    }
                }
            }                

            db.SaveChanges();
        }

        private void AddFamilyMemberRelationship(Member existingFamilyMember, Member member, string relation, bool isCurrent = true, bool isBiological = true)
        {
            switch(relation)
            {
                case Constants.Family.Relationships.Parent:
                    if(!member.MemberParents1.Any(x => x.ParentId == existingFamilyMember.Id && x.ChildId == member.Id))
                        db.MemberParents.Add(new MemberParent { ParentId = existingFamilyMember.Id, ChildId = member.Id, IsBiological = isBiological });
                    break;
                case Constants.Family.Relationships.Child:
                    if (!member.MemberParents.Any(x => x.ParentId == member.Id && x.ChildId == existingFamilyMember.Id))
                        db.MemberParents.Add(new MemberParent { ParentId = member.Id, ChildId = existingFamilyMember.Id, IsBiological = isBiological });
                    break;
                case Constants.Family.Relationships.Spouse:
                    if(!member.MemberSpouses.Any(x => x.Spouse_1_Id == existingFamilyMember.Id && x.Spouse_2_Id == member.Id) && !member.MemberSpouses.Any(x => x.Spouse_1_Id == member.Id && x.Spouse_2_Id == existingFamilyMember.Id))
                        db.MemberSpouses.Add(new MemberSpous { Spouse_1_Id = member.Id, Spouse_2_Id = existingFamilyMember.Id, IsCurrent = isCurrent });
                    break;
            }
        }

        public bool MemberExists(string FirstName, string MiddleName, string LastName, string Address1, string Address2, string City, string State, string ZipCode, string Gender, DateTime? Birthday, string Email)
        {
            return db.Members.Any(x => x.FirstName == FirstName && x.MiddleName == MiddleName && x.LastName == LastName && x.Gender == Gender && x.Birthday == Birthday && x.Email == Email &&
                x.Addresses.Any(y => y.Address1 == Address1 && y.Address2 == Address2 && y.City == City && y.Province == State && y.ZipCode == ZipCode));
        }

        public void UpdateMemberMilitary(int? branchId, string branchName, string lastPayGrade, string currentStatus, Member member)
        {
            MilitaryBranch mBranch = null;
            if (branchId.HasValue)
                mBranch = db.MilitaryBranches.Single(x => x.Id == branchId);
            else if (!string.IsNullOrEmpty(branchName))
                mBranch = db.MilitaryBranches.Single(x => x.BranchName == branchName);

            if (mBranch != null && !member.MemberMilitaries.Select(x => x.MilitaryId).Contains(mBranch.Id))
                member.MemberMilitaries.Add(new MemberMilitary { CurrentStatus = currentStatus, MilitaryBranch = mBranch, LastPayGrade = lastPayGrade });
        }

        public void UpdateMemberInterests(string interestList, Member member)
        {
            List<Interest> interests = db.Interests.ToList();
            string[] newInterests = !string.IsNullOrEmpty(interestList) ? interestList.Split(',') : new string[0];
            foreach (string interest in newInterests)
            {
                string trimInterest = interest.Trim();
                if(!string.IsNullOrEmpty(trimInterest))
                {
                    if (interests.Select(x => x.Name.ToLower()).Contains(trimInterest.ToLower()))
                    {
                        Interest existingInterest = interests.First(x => x.Name.ToLower() == trimInterest.ToLower());
                        if (!member.Interests.Contains(existingInterest))
                            member.Interests.Add(existingInterest);
                    }
                    else
                    {
                        member.Interests.Add(new Interest { Name = trimInterest });
                    }
                }
            }

            db.SaveChanges();
        }

        public void UpdateMemberNonprofits(MemberProfileNonprofits model)
        {
            Member member = db.Members.Find(model.MemberId);
            if (member == null)
                throw new Exception($"Member with an id of {model.MemberId} not found");
            else
            {
                member.IsWorkforceInterested = model.InterestedInWorkforceSkill;
                if(model.SkillsDesiredIds != null)
                {
                    foreach (int skillId in model.SkillsDesiredIds)
                    {
                        if (!member.MemberSkills.Select(x => x.SkillId).Contains(skillId))
                            member.MemberSkills.Add(new MemberSkill { MemberId = member.Id, SkillId = skillId, IsComplete = false });
                    }
                }
            }                

            db.SaveChanges();
        }

        public void UpdateMemberSkills(string newSkillsString, Member member, bool isComplete = true, int? nonprofitId = null)
        {
            List<Skill> skills = db.Skills.ToList();
            string[] newSkills = newSkillsString.Split(',').ToArray();
            foreach (string skill in newSkills)
            {
                string trimSkill = skill.Trim();
                if(!string.IsNullOrEmpty(trimSkill))
                {
                    if (skills.Select(x => x.Name.ToLower()).Contains(trimSkill.ToLower()))
                    {
                        Skill existingSkill = skills.Single(x => x.Name.ToLower() == trimSkill.ToLower());
                        if (!member.MemberSkills.Select(x => x.SkillId).Contains(existingSkill.Id))
                            member.MemberSkills.Add(new MemberSkill { MemberId = member.Id, SkillId = existingSkill.Id, IsComplete = isComplete, NonprofitId = nonprofitId });
                    }
                    else
                    {
                        Skill newSkill = new Skill { Name = trimSkill };
                        db.Skills.Add(newSkill);
                        member.MemberSkills.Add(new MemberSkill { Skill = newSkill, IsComplete = isComplete, NonprofitId = nonprofitId });
                    }
                }
            }
        }

        public void UpdateMemberJobs(MemberProfileJobs model)
        {
            Member member = db.Members.Find(model.MemberId);
            if (member == null)
                throw new Exception($"Member with an id of {model.MemberId} not found");
            else
            {
                member.IsJobSearching = model.CurrentlyLooking;
                foreach (var job in model.Jobs)
                {
                    if(!string.IsNullOrEmpty(job.Name) || job.CorporationId.HasValue) // not the best way to do this, but the default value for IsCurrent makes the model bind an empty object to itself on POST. Should refactor this later.
                    {
                        Corporation corporation = db.Corporations.Where(x => (x.Name == job.Name)).FirstOrDefault();
                        if (corporation == null)
                        {
                            corporation = new Corporation
                            {
                                Name = job.Name,
                                CreatedDate = DateTime.UtcNow,
                            };
                            db.Corporations.Add(corporation);
                        }
                        if (!member.MemberCorporations.Select(x => x.CorporationId).Contains(corporation.Id))
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
            }

            db.SaveChanges();
        }

        public void UpdateMemberPhone(string phoneNumber, Member member)
        {
            if(!string.IsNullOrEmpty(phoneNumber))
            {
                PhoneNumber phone = phoneNumber != null ? db.PhoneNumbers.Where(x => x.Number == phoneNumber).FirstOrDefault() : null;
                if (phone == null)
                {
                    phone = new PhoneNumber { Number = phoneNumber };
                }
                member.PhoneNumbers.Add(phone);
            }
        }

        public void UpdateMemberSchools(MemberProfileSchools model)
        {
            Member member = db.Members.Find(model.MemberId);
            if (member == null)
                throw new Exception($"Member with an id of {model.MemberId} not found");
            else
            {
                foreach (var s in model.Schools)
                {
                    School school = db.Schools.Where(x => (x.SchoolName == s.Name)).FirstOrDefault();
                    if (school == null)
                    {
                        school = new School
                        {
                            SchoolName = s.Name,
                            Type = s.Type,
                        };
                        db.Schools.Add(school);
                    }
                    if (!member.MemberSchools.Select(x => x.SchoolId).Contains(school.Id))
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

            db.SaveChanges();
        }

        public void UpdateMemberAddress(string address1, string address2, string city, string province, string zipcode, string country, Member member)
        {
            Address address = db.Addresses.SingleOrDefault(x => x.Address1 == address1 && x.Address2 == address2 && x.City == city && x.Country == country && x.Province == province && x.ZipCode == zipcode);
            if (address == null)
            {
                address = new Address
                {
                    Address1 = address1,
                    Address2 = address2,
                    City = city,
                    Country = "USA",
                    ZipCode = zipcode,
                    Province = province,
                };
                db.Addresses.Add(address);
            }
            member.Addresses.Add(address);
        }
    }
}