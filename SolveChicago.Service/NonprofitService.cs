using SolveChicago.Common;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolveChicago.Service
{
    public class NonprofitService : BaseService
    {
        public NonprofitService(SolveChicagoEntities db) : base(db) { }

        public NonprofitProfile Get(int id)
        {
            Nonprofit nonprofit = db.Nonprofits.Find(id);
            if (nonprofit == null)
                return null;
            else
            {
                Address address = nonprofit.Addresses.LastOrDefault();
                NonprofitProfile npo = new NonprofitProfile
                {
                    Id = nonprofit.Id,
                    Address1 = address != null ? address.Address1 : string.Empty,
                    Address2 = address != null ? address.Address2 : string.Empty,
                    City = address != null ? address.City : string.Empty,
                    Country = address != null ? address.Country : string.Empty,
                    Phone = nonprofit.PhoneNumbers.Any() ? nonprofit.PhoneNumbers.Last().Number : string.Empty,
                    ProfilePicturePath = nonprofit.ProfilePicturePath,
                    Province = address != null ? address.Province : string.Empty,
                    ZipCode = address != null ? address.ZipCode : string.Empty,
                    Name = nonprofit.Name,
                    SkillsOffered = GetSkillsOffered(nonprofit),
                    SkillsList = GetSkillsList()
                };
                npo.TeachesSoftSkills = npo.SkillsOffered.Contains(Constants.Skills.SoftSkills);
                return npo;
            }
        }

        private string[] GetSkillsList()
        {
            return db.Skills.Where(x => x.Name.ToLower() != "soft skills").Select(x => x.Name).ToArray();
        }

        private string GetSkillsOffered(Nonprofit nonprofit)
        {
            return string.Join(", ", nonprofit.Skills.Select(x => x.Name));
        }

        public void Post(NonprofitProfile model)
        {
            Nonprofit nonprofit = db.Nonprofits.Find(model.Id);
            if (nonprofit == null)
                throw new Exception($"Nonprofit with Id of {model.Id} not found.");
            else
            {
                if (model.ProfilePicture != null)
                    nonprofit.ProfilePicturePath = UploadPhoto(Constants.Upload.NonprofitPhotos, model.ProfilePicture, model.Id);
                
                nonprofit.Name = model.Name;
                UpdateNonprofitPhone(model, nonprofit);
                UpdateNonprofitAddress(model, nonprofit);
                UpdateNonprofitSkills(nonprofit, model, model.TeachesSoftSkills);

                db.SaveChanges();
            }
        }

        private void UpdateNonprofitPhone(NonprofitProfile model, Nonprofit nonprofit)
        {
            PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
            if (phone == null)
            {
                phone = new PhoneNumber { Number = model.Phone };
            }
            nonprofit.PhoneNumbers.Add(phone);
        }

        private void UpdateNonprofitAddress(NonprofitProfile model, Nonprofit nonprofit)
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
            nonprofit.Addresses.Add(address);
        }

        private void UpdateNonprofitSkills(Nonprofit nonprofit, NonprofitProfile model, bool? teachesSoftSkills)
        {
            List<Skill> skills = db.Skills.ToList();
            string[] newSkills = model.SkillsOffered != null ? model.SkillsOffered.Split(',') : new string[0];
            foreach (string skill in newSkills)
            {
                string trimSkill = skill.Trim();
                if (!string.IsNullOrEmpty(trimSkill))
                {
                    if (skills.Select(x => x.Name.ToLower()).Contains(trimSkill.ToLower()))
                    {
                        Skill existingSkill = skills.Single(x => x.Name.ToLower() == trimSkill.ToLower());
                        if (!nonprofit.Skills.Select(x => x.Id).Contains(existingSkill.Id))
                            nonprofit.Skills.Add(existingSkill);
                    }
                    else
                    {
                        Skill newSkill = new Skill { Name = trimSkill };
                        db.Skills.Add(newSkill);
                        nonprofit.Skills.Add(newSkill);
                    }
                }
            }
            if(teachesSoftSkills ?? false)
            {
                Skill existingSkill = skills.Single(x => x.Name.ToLower() == "soft skills");
                nonprofit.Skills.Add(existingSkill);
            }
        }

        public CaseManager[] GetCaseManagers(int nonprofitId)
        {
            Nonprofit npo = db.Nonprofits.Find(nonprofitId);
            if (npo == null)
                return new CaseManager[0];
            else
                return GetCaseManagers(npo);
        }

        public CaseManager[] GetCaseManagers(Nonprofit npo)
        {
            return npo.CaseManagers.ToArray();
        }

        public FamilyEntity[] GetMembers(int id)
        {
            List<FamilyEntity> families = new List<FamilyEntity>();
            Nonprofit npo = db.Nonprofits.Find(id);
            if(npo != null)
            {
                MemberService service = new MemberService(this.db);
                foreach(var member in npo.NonprofitMembers.Where(x => !x.End.HasValue))
                {
                    families.Add(service.GetFamily(member.Member, true));
                }
            }
            return families.ToArray();
                
        }

        public void AssignCaseManager(int nonprofitId, int memberId, int caseManagerId)
        {
            NonprofitMember nonprofitMember = db.NonprofitMembers.SingleOrDefault(x => x.NonprofitId == nonprofitId && x.MemberId == memberId);
            if (nonprofitMember == null)
                throw new ApplicationException("No Member-Nonprofit relationship exists between these two entities.");
            else
            {
                nonprofitMember.CaseManagerId = caseManagerId;
                db.SaveChanges();
            }
        }
    }
}