using CsvHelper;
using SolveChicago.Common;
using SolveChicago.Common.Models;
using SolveChicago.Common.Models.Profile.Member;
using SolveChicago.Entities;
using SolveChicago.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.IO;
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
                PhoneNumber phone = nonprofit.PhoneNumbers.LastOrDefault();
                NonprofitProfile npo = new NonprofitProfile
                {
                    Id = nonprofit.Id,
                    Address1 = address != null ? address.Address1 : string.Empty,
                    Address2 = address != null ? address.Address2 : string.Empty,
                    City = address != null ? address.City : string.Empty,
                    Country = address != null ? address.Country : string.Empty,
                    Phone = phone != null ? phone.Number : string.Empty,
                    PhoneExtension = phone != null ? phone.Extension : string.Empty,
                    ProfilePicturePath = nonprofit.ProfilePicturePath,
                    Province = address != null ? address.Province : string.Empty,
                    ZipCode = address != null ? address.ZipCode : string.Empty,
                    Name = nonprofit.Name,
                    Type = nonprofit.ProviderType,
                    HasPrograms = nonprofit.HasPrograms,
                    Programs = GetNonprofitPrograms(nonprofit),
                    WorkforceSkillsList = GetWorkforceSkillsList(),
                    SoftSkillsList = GetSoftSkillsList(),
                };
                return npo;
            }
        }

        private Program[] GetNonprofitPrograms(Nonprofit nonprofit)
        {
            Dictionary<int, string> ethnicities = db.Ethnicities.ToDictionary(y => y.Id, y => y.EthnicityName);
            return nonprofit.NonprofitPrograms.Select(x => new Program
            {
                EthnicityId = x.EthnicityId,
                Ethnicity = x.Ethnicity != null ? x.Ethnicity.EthnicityName : "",
                Gender = x.Gender,
                Id = x.Id,
                MaxAge = x.MaxAge,
                MinAge = x.MinAge,
                ProgramName = x.ProgramName,
                NonprofitId = x.NonprofitId,
                NonprofitName = x.Nonprofit != null ? x.Nonprofit.Name : "",
                SoftSkillsOffered = GetSoftSkillsOffered(nonprofit, x.Id),
                WorkforceSkillsOffered = GetWorkforceSkillsOffered(nonprofit, x.Id),
                EthnicityList = ethnicities
            }).ToArray();
        }

        private string[] GetWorkforceSkillsList()
        {
            return db.Skills.Where(x => x.IsWorkforce).Select(x => x.Name).ToArray();
        }

        private string[] GetSoftSkillsList()
        {
            return db.Skills.Where(x => !x.IsWorkforce).Select(x => x.Name).ToArray();
        }

        private string GetWorkforceSkillsOffered(Nonprofit nonprofit, int programId)
        {
            return string.Join(", ", nonprofit.NonprofitSkills.Any(x => x.Skill != null && x.Skill.IsWorkforce && x.ProgramId == programId) ? nonprofit.NonprofitSkills.Where(x => x.Skill != null && x.Skill.IsWorkforce && x.ProgramId == programId).Select(x => x.Skill.Name) : new string[0]);
        }

        private string GetSoftSkillsOffered(Nonprofit nonprofit, int programId)
        {
            return string.Join(", ", nonprofit.NonprofitSkills.Any(x => x.Skill != null && !x.Skill.IsWorkforce && x.ProgramId == programId) ? nonprofit.NonprofitSkills.Where(x => x.Skill != null && !x.Skill.IsWorkforce && x.ProgramId == programId).Select(x => x.Skill.Name) : new string[0]);
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
                nonprofit.ProviderType = model.Type;
                nonprofit.HasPrograms = model.HasPrograms;
                UpdateNonprofitPhone(model, nonprofit);
                UpdateNonprofitAddress(model, nonprofit);
                UpdateNonprofitPrograms(nonprofit, model);
                db.SaveChanges();
            }
        }

        private void UpdateNonprofitPhone(NonprofitProfile model, Nonprofit nonprofit)
        {
            PhoneNumber phone = model.Phone != null ? db.PhoneNumbers.Where(x => x.Number == model.Phone).FirstOrDefault() : null;
            if (phone == null)
            {
                phone = new PhoneNumber { Number = model.Phone, Extension = model.PhoneExtension };
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

        private void UpdateNonprofitPrograms(Nonprofit nonprofit, NonprofitProfile model)
        {
            foreach (var program in model.Programs)
            {
                NonprofitProgram npoProgram = new NonprofitProgram();
                if (program.Id.HasValue && program.Id.Value > 0)
                    npoProgram = nonprofit.NonprofitPrograms.FirstOrDefault(x => x.Id == program.Id.Value);

                npoProgram.EthnicityId = program.EthnicityId;
                npoProgram.Gender = program.Gender;
                npoProgram.MaxAge = program.MaxAge;
                npoProgram.MinAge = program.MinAge;
                npoProgram.ProgramName = !string.IsNullOrEmpty(program.ProgramName) ? program.ProgramName : model.Name;

                if (!nonprofit.NonprofitPrograms.Any(x => x.Id == program.Id))
                    nonprofit.NonprofitPrograms.Add(npoProgram);

                db.SaveChanges();

                UpdateNonprofitSkills(nonprofit, npoProgram.Id, program.SoftSkillsOffered, program.WorkforceSkillsOffered);
            }
        }

        private void UpdateNonprofitSkills(Nonprofit nonprofit, int programId, string softSkillsOffered, string workforceSkillsOffered)
        {
            List<Skill> skills = db.Skills.ToList();
            string[] newWorkforceSkills = workforceSkillsOffered != null ? workforceSkillsOffered.Split(',') : new string[0];
            string[] newSoftSkills = softSkillsOffered != null ? softSkillsOffered.Split(',') : new string[0];
            foreach (string skill in newWorkforceSkills)
            {
                string trimSkill = skill.Trim();
                if (!string.IsNullOrEmpty(trimSkill))
                {
                    if (skills.Select(x => x.Name.ToLower()).Contains(trimSkill.ToLower()))
                    {
                        Skill existingSkill = skills.Single(x => x.Name.ToLower() == trimSkill.ToLower());
                        if (!nonprofit.NonprofitSkills.Select(x => x.SkillId).Contains(existingSkill.Id))
                            nonprofit.NonprofitSkills.Add(new NonprofitSkill { Skill = existingSkill, ProgramId = programId });
                    }
                    else
                    {
                        Skill newSkill = new Skill { Name = trimSkill, IsWorkforce = true };
                        db.Skills.Add(newSkill);
                        nonprofit.NonprofitSkills.Add(new NonprofitSkill { Skill = newSkill, ProgramId = programId });
                    }
                }
            }
            foreach (string skill in newSoftSkills)
            {
                string trimSkill = skill.Trim();
                if (!string.IsNullOrEmpty(trimSkill))
                {
                    if (skills.Select(x => x.Name.ToLower()).Contains(trimSkill.ToLower()))
                    {
                        Skill existingSkill = skills.Single(x => x.Name.ToLower() == trimSkill.ToLower());
                        if (!nonprofit.NonprofitSkills.Select(x => x.SkillId).Contains(existingSkill.Id))
                            nonprofit.NonprofitSkills.Add(new NonprofitSkill { Skill = existingSkill, ProgramId = programId });
                    }
                    else
                    {
                        Skill newSkill = new Skill { Name = trimSkill, IsWorkforce = false };
                        db.Skills.Add(newSkill);
                        nonprofit.NonprofitSkills.Add(new NonprofitSkill { Skill = newSkill, ProgramId = programId });
                    }
                }
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
            return npo.NonprofitStaffs.Select(x => x.CaseManager).ToArray();
        }

        public FamilyEntity[] GetMembers(int id)
        {
            List<FamilyEntity> families = new List<FamilyEntity>();
            Nonprofit npo = db.Nonprofits.Find(id);
            if(npo != null)
            {
                MemberService service = new MemberService(this.db);
                foreach(var member in npo.NonprofitMembers)
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
                CaseManager caseManager = db.CaseManagers.Find(caseManagerId);
                if (caseManager == null)
                    throw new ApplicationException($"Case Manager with Id of {caseManagerId} does not exist.");
                else
                    nonprofitMember.NonprofitStaffs.Add(new NonprofitStaff { CaseManager = caseManager, NonprofitId = nonprofitId, Role = Constants.NonprofitRoles.CaseManager });
                db.SaveChanges();
            }
        }

        public void UploadClients(int nonprofitId, HttpPostedFileBase file)
        {
            List<ClientList> clientList = new List<ClientList>();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                try
                {
                    using (StreamReader streamReader = new StreamReader(file.InputStream))
                    {
                        using (CsvReader reader = new CsvReader(streamReader))
                        {
                            while (reader.Read())
                            {
                                if(!string.IsNullOrEmpty(reader.GetField<string>("FirstName")) && !string.IsNullOrEmpty(reader.GetField<string>("LastName")))
                                {
                                    clientList.Add(new ClientList
                                    {
                                        SSN = reader.GetField<string>("SSN"),
                                        FirstName = reader.GetField<string>("FirstName"),
                                        MiddleName = reader.GetField<string>("MiddleName"),
                                        LastName = reader.GetField<string>("LastName"),
                                        Gender = reader.GetField<string>("Gender"),
                                        Birthday = reader.GetField<DateTime?>("Birthday"),
                                        IsHeadOfHousehold = reader.GetField<bool?>("IsHeadOfHousehold"),
                                        Income = reader.GetField<decimal?>("Income"),
                                        IsMilitary = reader.GetField<bool?>("IsMilitary"),
                                        MilitaryBranch = reader.GetField<string>("MilitaryBranch"),
                                        MilitaryLastPayGrade = reader.GetField<string>("MilitaryLastPayGrade"),
                                        MilitaryCurrentServiceStatus = reader.GetField<string>("MilitaryCurrentServiceStatus"),
                                        Address1 = reader.GetField<string>("Address1"),
                                        Address2 = reader.GetField<string>("Address2"),
                                        City = reader.GetField<string>("City"),
                                        State = reader.GetField<string>("State"),
                                        ZipCode = reader.GetField<string>("ZipCode"),
                                        Country = reader.GetField<string>("Country"),
                                        Skills = reader.GetField<string>("Skills"),
                                        Interests = reader.GetField<string>("Interests"),
                                        PhoneNumber = reader.GetField<string>("PhoneNumber"),
                                        Email = reader.GetField<string>("Email"),
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            CreateMembersFromClientList(nonprofitId, clientList);
        }

        public void CreateMembersFromClientList(int nonprofitId, List<ClientList> clientList)
        {
            MemberService service = new MemberService(this.db);
            foreach (ClientList client in clientList)
            {
                if(!service.MemberExists(client.FirstName, client.MiddleName, client.LastName, client.Suffix, client.Address1, client.Address2, client.City, client.State, client.ZipCode, client.Gender, client.Birthday, client.Email))
                {
                    Member member = new Member
                    {
                        FirstName = client.FirstName,
                        MiddleName = client.MiddleName,
                        Birthday = client.Birthday,
                        LastName = client.LastName,
                        CreatedDate = DateTime.UtcNow,
                        Email = client.Email,
                        Gender = client.Gender,
                        Income = client.Income,
                        IsHeadOfHousehold = client.IsHeadOfHousehold,
                        IsMilitary = client.IsMilitary,
                        SSN = client.SSN,
                        Suffix = client.Suffix,
                    };
                    if (client.IsMilitary ?? false)
                        service.UpdateMemberMilitary(null, client.MilitaryBranch, client.MilitaryLastPayGrade, client.MilitaryCurrentServiceStatus, member);
                    service.UpdateMemberAddress(client.Address1, client.Address2, client.City, client.State, client.ZipCode, client.Country, member);
                    service.UpdateMemberPhone(client.PhoneNumber, member);
                    service.MatchOrCreateFamily(member);
                    if (!string.IsNullOrEmpty(client.Skills))
                        service.UpdateMemberSkills(client.Skills, member, true, nonprofitId);
                    if (!string.IsNullOrEmpty(client.Interests))
                        service.UpdateMemberInterests(client.Interests, member);

                    db.Members.Add(member);
                    member.NonprofitMembers.Add(new NonprofitMember { NonprofitId = nonprofitId, Start = DateTime.UtcNow });
                }

                db.SaveChanges();
            }
        }
    }
}