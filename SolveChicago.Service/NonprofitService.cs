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
                NonprofitProfile npo = new NonprofitProfile
                {
                    Id = nonprofit.Id,
                    Address1 = nonprofit.Address1,
                    Address2 = nonprofit.Address2,
                    City = nonprofit.City,
                    Country = nonprofit.Country,
                    Phone = nonprofit.Phone,
                    ProfilePicturePath = nonprofit.ProfilePicturePath,
                    Province = nonprofit.Province,
                    Name = nonprofit.Name,
                    SkillsOffered = GetSkillsOffered(nonprofit),
                    SkillsList = GetSkillsList()
                };
                npo.TeachesSoftSkills = npo.SkillsOffered.ToLower().Contains("soft skills");
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

                nonprofit.Address1 = model.Address1;
                nonprofit.Address2 = model.Address2;
                nonprofit.City = model.City;
                nonprofit.Country = model.Country;
                nonprofit.Phone = model.Phone;
                nonprofit.Province = model.Province;
                nonprofit.Name = model.Name;

                UpdateNonprofitSkills(nonprofit, model, model.TeachesSoftSkills);

                db.SaveChanges();
            }
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