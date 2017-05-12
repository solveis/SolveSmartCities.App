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
            MemberStage model = new MemberStage();
            if (member.SurveyStep == Constants.Member.SurveyStep.Invited)
            {
                model.Stage = Constants.Member.Stage.InviteSent;
                model.Percent = (int)Math.Round(12.5);
                return model;
            }
            if (member.SurveyStep != null && member.SurveyStep != Constants.Member.SurveyStep.Invited && member.SurveyStep != Constants.Member.SurveyStep.Complete)
            {
                model.Stage = Constants.Member.Stage.ProfileInProgress;
                model.Percent = (int)Math.Round(25.0);
                return model;
            }
            if (member.SurveyStep == Constants.Member.SurveyStep.Complete && !member.NonprofitMembers.Any(x => !x.End.HasValue) && !member.MemberCorporations.Any(x => !x.End.HasValue))
            {
                model.Stage = Constants.Member.Stage.ProfileCompleted;
                model.Percent = (int)Math.Round(37.5);
                return model;
            }
            if (member.NonprofitMembers.Any(x => x.Start > member.CreatedDate && !x.End.HasValue) && !member.MemberSkills.Any(x => x.IsComplete && x.Skill.Name.ToLower() == "soft skills"))
            {
                model.Stage = Constants.Member.Stage.InSoftSkillsTraining;
                model.Percent = (int)Math.Round(50.0);
                return model;
            }
            if (member.NonprofitMembers.Any(x => x.Start > member.CreatedDate && x.End.HasValue && x.End.Value < DateTime.UtcNow) && member.MemberSkills.Any(x => x.IsComplete && x.Skill.Name.ToLower() == "soft skills"))
            {
                model.Stage = Constants.Member.Stage.SoftSkillsAcquired;
                model.Percent = (int)Math.Round(62.5);
                return model;
            }
            if (member.NonprofitMembers.Any(x => x.Start > member.CreatedDate && !x.End.HasValue) && member.MemberSkills.Any(x => x.IsComplete && x.Skill.Name.ToLower() == "soft skills"))
            {
                model.Stage = Constants.Member.Stage.InWorkforceTraining;
                model.Percent = (int)Math.Round(75.0);
                return model;
            }
            // TODO: This logic is too broad. Should tie into the NPO associated with a desired skill. Make sure that when NPO claims a member, they also claim their desired skills.
            if (member.NonprofitMembers.Any(x => x.Start > member.CreatedDate && x.End.HasValue && x.End.Value < DateTime.UtcNow) && member.MemberSkills.Any(x => x.IsComplete && x.Skill.Name.ToLower() == "soft skills") && member.MemberSkills.Any(x => x.IsComplete && x.Skill.Name.ToLower() != "soft skills"))
            {
                model.Stage = Constants.Member.Stage.RecruiterPlacing;
                model.Percent = (int)Math.Round(87.5);
                return model;
            }
            if (member.MemberCorporations.Any(x => x.Start > member.CreatedDate && !x.End.HasValue))
            {
                model.Stage = Constants.Member.Stage.JobPlaced;
                model.Percent = 100;
                return model;
            }
            else
            {
                model.Stage = Constants.Member.Stage.OffTrack;
                model.Percent = 0;
                return model;
            }
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
                    Skills = member.MemberSkills.Any() ? string.Join(", ", member.MemberSkills.Select(x => x.Skill.Name).ToArray()) : string.Empty,
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
            return member.MilitaryBranches.Select(x => new MilitaryEntity
            {
                Id = x.Id,
                MilitaryBranch = x.BranchName,
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
                    CaseManagerId = x.CaseManager?.Id,
                    CaseManagerName = string.Format("{0} {1}", x.CaseManager?.FirstName, x.CaseManager?.LastName),
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
                return memberCorporations.Select(x => new JobEntity { CorporationId = x.CorporationId, EmployeeEnd = x.End, EmployeePay = x.Pay, EmployeeStart = x.Start, Name = x.Corporation.Name, IsCurrent = !x.End.HasValue }).ToArray();
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
                family.AverageScore = (int)(family.FamilyMembers.Count() > 0 ? family.FamilyMembers.Average(x => x.MemberStage.Percent) : 0);
                family.HeadOfHouseholdProfilePicturePath = family.FamilyMembers.Any(x => (x.IsHeadOfHousehold ?? false)) ? family.FamilyMembers.FirstOrDefault(x => (x.IsHeadOfHousehold.Value)).ProfilePicturePath : Constants.Member.NoPhotoUrl;
                
                return family;
            }
            else
            {
                return new FamilyEntity
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
                    HeadOfHouseholdProfilePicturePath = member.ProfilePicturePath,
                };
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
                familyMembers.Add(new FamilyMember { Birthday = member.Birthday, FirstName = member.FirstName, Gender = member.Gender, Id = member.Id, IsHeadOfHousehold = member.IsHeadOfHousehold, LastName = member.LastName, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member) });
            
            return BuildFamilyTree(member, familyMembers);
        }

        private static void GetUnknownRelationTree(List<FamilyMember> familyMembers, Member member)
        {
            if (member.Family != null && member.Family.Members.Count() > 0)
            {
                Member[] fm = member.Family?.Members?.ToArray();
                foreach (Member m in fm)
                {
                    FamilyMember f = new FamilyMember { FirstName = m.FirstName, Birthday = m.Birthday, Gender = m.Gender, Id = m.Id, IsHeadOfHousehold = m.IsHeadOfHousehold, LastName = m.LastName, Email = m.Email, Phone = m.PhoneNumbers.Any() ? string.Join(", ", m.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member) };
                    if (!familyMembers.Select(x => x.Id).Contains(f.Id) && f.Id != member.Id)
                        familyMembers.Add(f);

                }
            }
        }

        private static void GetSpouseTree(List<FamilyMember> familyMembers, Member member)
        {
            if (member.MemberSpouses.Any(x => x.Member1 != null))
                familyMembers.AddRange(member.MemberSpouses.Select(x => new FamilyMember { FirstName = x.Member1.FirstName, LastName = x.Member1.LastName, Relation = x.Member1.Gender.ToLower() == "male" ? "Husband" : x.Member1.Gender.ToLower() == "female" ? "Wife" : "Spouse", Id = x.Member1.Id, Email = x.Member1.Email, Phone = x.Member1.PhoneNumbers.Any() ? string.Join(", ", x.Member1.PhoneNumbers.Select(y => y.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member) }));
            else if (member.MemberSpouses1.Any(x => x.Member != null))
                familyMembers.AddRange(member.MemberSpouses1.Select(x => new FamilyMember { FirstName = x.Member.FirstName, LastName = x.Member.LastName, Relation = x.Member.Gender.ToLower() == "male" ? "Husband" : x.Member.Gender.ToLower() == "female" ? "Wife" : "Spouse", Id = x.Member.Id, Email = x.Member.Email, Phone = x.Member.PhoneNumbers.Any() ? string.Join(", ", x.Member.PhoneNumbers.Select(y => y.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member) }));
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
                        FamilyMember fm = new FamilyMember { FirstName = sibling.FirstName, Birthday = sibling.Birthday, Gender = sibling.Gender, Id = sibling.Id, IsHeadOfHousehold = sibling.IsHeadOfHousehold, LastName = sibling.LastName, Relation = (sibling.Gender.ToLower() == "male" ? "Brother" : sibling.Gender.ToLower() == "female" ? "Sister" : "Sibling"), Email = sibling.Email, Phone = sibling.PhoneNumbers.Any() ? string.Join(", ", sibling.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member) };
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
                    familyMembers.Add(new FamilyMember { FirstName = child.FirstName, LastName = child.LastName, IsHeadOfHousehold = (child.IsHeadOfHousehold ?? false), Relation = currentChildTitle, Gender = child.Gender, Birthday = child.Birthday, Id = child.Id, Email = child.Email, Phone = child.PhoneNumbers.Any() ? string.Join(", ", child.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member) });

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
                    familyMembers.Add(new FamilyMember { FirstName = parent.FirstName, LastName = parent.LastName, IsHeadOfHousehold = (parent.IsHeadOfHousehold ?? false), Relation = currentParentTitle, Gender = parent.Gender, Birthday = parent.Birthday, Id = parent.Id, Email = parent.Email, Phone = parent.PhoneNumbers.Any() ? string.Join(", ", parent.PhoneNumbers.Select(x => x.Number).ToArray()) : string.Empty, ProfilePicturePath = string.IsNullOrEmpty(member.ProfilePicturePath) ? Constants.Member.NoPhotoUrl : member.ProfilePicturePath, MemberStage = GetMemberStage(member), CurrentOccupation = GetFamilyMemberOccupation(member) });

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
            if (model.GovernmentPrograms != null)
            {
                Member member = db.Members.Find(model.MemberId);
                if (member == null)
                    throw new Exception($"Member with an id of {model.MemberId} not found");
                else
                {
                        foreach (var program in model.GovernmentPrograms)
                        {
                            MemberGovernmentProgram mgp = member.MemberGovernmentPrograms.Where(x => x.GovernmentProgramId == program.ProgramId).FirstOrDefault();
                            if (mgp == null)
                            {
                                mgp = new MemberGovernmentProgram
                                {
                                    MemberId = member.Id,
                                    GovernmentProgramId = program.ProgramId,
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

                UpdateMemberAddress(model, member);
                MatchOrCreateFamily(model, member);
                UpdateMemberPhone(model, member);
                UpdateMemberInterests(model, member);
                UpdateMemberMilitary(model, member);
                UpdateMemberSkills(model, member, true);

                db.SaveChanges();
            }
        }

        private void MatchOrCreateFamily(MemberProfilePersonal model, Member member)
        {
            Family[] families = db.Families.ToArray();
            Address address = member.Addresses.FirstOrDefault();
            if (member.FamilyId.HasValue)
                return;
            else if(families.Any(x => x.Addresses.Contains(address)))
            {
                Family family = families.Where(x => x.Addresses.Contains(address)).First();
                family.Members.Add(member);
            }
            else
            {
                member.Family = new Family { FamilyName = member.LastName, PhoneNumbers = member.PhoneNumbers.ToArray(), Addresses = member.Addresses.ToArray() };
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
                            existingFamilyMember = new Member { FirstName = familyMember.FirstName, LastName = familyMember.LastName, IsHeadOfHousehold = familyMember.IsHeadOfHousehold, Birthday = familyMember.Birthday, Gender = familyMember.Gender, FamilyId = member.FamilyId };
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

        private void UpdateMemberMilitary(MemberProfilePersonal model, Member member)
        {
            if(model.MilitaryId.HasValue)
            {
                MilitaryBranch mBranch = db.MilitaryBranches.Single(x => x.Id == model.MilitaryId);
                if (!member.MilitaryBranches.Contains(mBranch))
                    member.MilitaryBranches.Add(mBranch);
            }
        }

        private void UpdateMemberInterests(MemberProfilePersonal model, Member member)
        {
            List<Interest> interests = db.Interests.ToList();
            string[] newInterests = model.Interests != null ? model.Interests.Split(',') : new string[0];
            foreach (string interest in newInterests)
            {
                string trimInterest = interest.Trim();
                if (interests.Select(x => x.Name.ToLower()).Contains(trimInterest.ToLower()))
                {
                    Interest existingInterest = interests.Single(x => x.Name.ToLower() == trimInterest.ToLower());
                    if (!member.Interests.Contains(existingInterest))
                        member.Interests.Add(existingInterest);
                }   
                else
                {
                    member.Interests.Add(new Interest { Name = trimInterest });
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
                foreach (int skillId in model.SkillsDesiredIds)
                {
                    if (!member.MemberSkills.Select(x => x.SkillId).Contains(skillId))
                        member.MemberSkills.Add(new MemberSkill { MemberId = member.Id, SkillId = skillId, IsComplete = false });
                }
            }                

            db.SaveChanges();
        }

        private void UpdateMemberSkills(MemberProfilePersonal model, Member member, bool isComplete = true)
        {
            List<Skill> skills = db.Skills.ToList();
            string[] newSkills = model.Skills != null ? model.Skills.Split(',').ToArray() : new string[0];
            foreach (string skill in newSkills)
            {
                string trimSkill = skill.Trim();
                if(!string.IsNullOrEmpty(skill))
                {
                    if (skills.Select(x => x.Name.ToLower()).Contains(trimSkill.ToLower()))
                    {
                        Skill existingSkill = skills.Single(x => x.Name.ToLower() == trimSkill.ToLower());
                        if (!member.MemberSkills.Select(x => x.SkillId).Contains(existingSkill.Id))
                            member.MemberSkills.Add(new MemberSkill { MemberId = member.Id, SkillId = existingSkill.Id, IsComplete = isComplete });
                    }
                    else
                    {
                        Skill newSkill = new Skill { Name = trimSkill };
                        db.Skills.Add(newSkill);
                        member.MemberSkills.Add(new MemberSkill { Skill = newSkill, IsComplete = isComplete });
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
                foreach (var job in model.Jobs)
                {
                    if(!string.IsNullOrEmpty(job.Name) || job.CorporationId.HasValue) // not the best way to do this, but the default value for IsCurrent makes the model bind an empty object to itself on POST. Should refactor this later.
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
            }

            db.SaveChanges();
        }

        private void UpdateMemberPhone(MemberProfilePersonal model, Member member)
        {
            PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
            if (phone == null)
            {
                phone = new PhoneNumber { Number = model.Phone };
            }
            member.PhoneNumbers.Add(phone);
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

            db.SaveChanges();
        }

        private void UpdateMemberAddress(MemberProfilePersonal model, Member member)
        {
            Address address = db.Addresses.SingleOrDefault(x => x.Address1 == model.Address1 && x.Address2 == model.Address2 && x.City == model.City && x.Country == model.Country && x.Province == model.Province && x.ZipCode == model.ZipCode);
            if (address == null)
            {
                address = new Address
                {
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    Country = "USA",
                    ZipCode = model.ZipCode,
                    Province = model.Province,
                };
                db.Addresses.Add(address);
            }
            member.Addresses.Add(address);
        }
    }
}