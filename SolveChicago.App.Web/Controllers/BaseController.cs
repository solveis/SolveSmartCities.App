using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SolveChicago.App.Web.Models;
using SolveChicago.App.Common;
using SolveChicago.App.Web.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using SolveChicago.Entities;
using SolveChicago.App.Service;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using SolveChicago.App.Common.Entities;
using System.Collections.Generic;

namespace SolveChicago.App.Web.Controllers
{
    public class BaseController : Controller
    {
        protected SolveChicagoEntities db = new SolveChicagoEntities();
        protected ApplicationSignInManager _signInManager;
        protected ApplicationUserManager _userManager;

        public BaseController(SolveChicagoEntities db)
        {
            this.db = db;
        }

        public BaseController()
        {
            this.db = new SolveChicagoEntities();
        }
        
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            protected set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        public new Profile Profile
        {
            get
            {
                return base.Profile as Profile;
            }
        }

        public class StateModel
        {
            public string DisplayName { get; set; }
            public string FirstName { get; set; }
            public string UserName { get; set; }
            public List<int> MemberIds { get; set; }
            public List<MemberEntity> Members { get; set; }
            public List<int> CaseManagerIds { get; set; }
            public List<CaseManagerEntity> CaseManagers { get; set; }
            public List<int> CorporationIds { get; set; }
            public List<CorporationEntity> Corporations { get; set; }
            public List<int> NonprofitIds { get; set; }
            public List<NonprofitEntity> Nonprofits { get; set; }
            public List<int> AdminIds { get; set; }
            public List<AdminEntity> Admins { get; set; }
            public Enumerations.Role[] Roles { get; set; }
        }

        private StateModel _state;

        public StateModel State
        {
            get
            {
                if (_state == null)
                {
                    _state = new StateModel();
                    if ((User != null) && (User.Identity != null) && (User.Identity.IsAuthenticated))
                    {
                        RefreshState(User.Identity.Name);
                    }

                }

                return _state;
            }
        }

        protected void RefreshState(string userName)
        {
            _state = new StateModel();
            _state.UserName = userName;
            _state.Roles = GetRoles(userName);
            int userId = GetUserId(userName);
            _state.FirstName = _state.DisplayName != null ? _state.DisplayName.Split(' ')[0] : "";
            ViewBag.Email = userName;

            if (_state.Roles.Contains(Enumerations.Role.Member))
            {
                MemberEntity member = new MemberEntity().Map(db.UserProfiles.Single(x => x.Id == userId).Members.First());
                
                if (member != null)
                {
                    if (_state.Members == null)
                        _state.Members = new List<MemberEntity>();
                    if (_state.MemberIds == null)
                        _state.MemberIds = new List<int>();
                    _state.Members.Add(member);
                    _state.MemberIds.Add(member.Id);
                    if (member.CaseManagers != null && member.CaseManagers.Count()> 0)
                    {
                        if (_state.CaseManagers == null)
                            _state.CaseManagers = new List<CaseManagerEntity>();
                        if (_state.CaseManagerIds == null)
                            _state.CaseManagerIds = new List<int>();
                        _state.CaseManagers.AddRange(member.CaseManagers);
                        _state.CaseManagerIds.AddRange(member.CaseManagers.Select(x => x.Id));
                    }
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.CaseManager))
            {
                CaseManagerEntity caseManager = new CaseManagerEntity().Map(db.UserProfiles.Single(x => x.Id == userId).CaseManagers.First());

                if (caseManager != null)
                {
                    if (_state.CaseManagers == null)
                        _state.CaseManagers = new List<CaseManagerEntity>();
                    if (_state.CaseManagerIds == null)
                        _state.CaseManagerIds = new List<int>();
                    _state.CaseManagers.Add(caseManager);
                    _state.CaseManagerIds.Add(caseManager.Id);
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.Corporation))
            {
                CorporationEntity corporation = new CorporationEntity().Map(db.UserProfiles.Single(x => x.Id == userId).Corporations.First());

                if (corporation != null)
                {
                    if (_state.Corporations == null)
                        _state.Corporations = new List<CorporationEntity>();
                    if (_state.CorporationIds == null)
                        _state.CorporationIds = new List<int>();
                    _state.Corporations.Add(corporation);
                    _state.CorporationIds.Add(corporation.Id);
                }
            }
            if (_state.Roles.Contains(Enumerations.Role.Nonprofit))
            {
                NonprofitEntity nonprofit = new NonprofitEntity().Map(db.UserProfiles.Single(x => x.Id == userId).Nonprofits.First());

                if (nonprofit != null)
                {
                    if (_state.Nonprofits == null)
                        _state.Nonprofits = new List<NonprofitEntity>();
                    if (_state.NonprofitIds == null)
                        _state.NonprofitIds = new List<int>();
                    _state.Nonprofits.Add(nonprofit);
                    _state.NonprofitIds.Add(nonprofit.Id);
                }
            }

            if (_state.Roles.Contains(Enumerations.Role.Admin))
            {
                AdminEntity admin = new AdminEntity().Map(db.UserProfiles.Single(x => x.Id == userId).Admins.First());

                if (admin != null)
                {
                    if (_state.Admins == null)
                        _state.Admins = new List<AdminEntity>();
                    if (_state.AdminIds == null)
                        _state.AdminIds = new List<int>();
                    _state.Admins.Add(admin);
                    _state.AdminIds.Add(admin.Id);
                }
            }
        }


        public ActionResult MemberRedirect(int memberId)
        {
            MemberEntity entity = new MemberEntity().Map(db.Members.Single(x => x.Id == memberId));
            return MemberRedirect(entity);
        }

        public ActionResult MemberRedirect(MemberEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Address1) || string.IsNullOrEmpty(entity.City) || string.IsNullOrEmpty(entity.Country)
                 || string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.ProfilePicturePath)
                 || string.IsNullOrEmpty(entity.Province))
            {
                return RedirectToAction("Member", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Member");
            }
            // TODO add more cases for member enrollment
        }

        public ActionResult CaseManagerRedirect(int caseManagerId)
        {
            CaseManagerEntity entity = new CaseManagerEntity().Map(db.CaseManagers.Single(x => x.Id == caseManagerId));
            return CaseManagerRedirect(entity);
        }

        public ActionResult CaseManagerRedirect(CaseManagerEntity entity)
        {
            if(string.IsNullOrEmpty(entity.Address1) || string.IsNullOrEmpty(entity.City) || string.IsNullOrEmpty(entity.Country)
                 || string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.ProfilePicturePath) 
                 || string.IsNullOrEmpty(entity.Province))
            {
                return RedirectToAction("CaseManager", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "CaseManager");
            }
        }

        public ActionResult CorporationRedirect(int corporationId)
        {
            CorporationEntity entity = new CorporationEntity().Map(db.Corporations.Single(x => x.Id == corporationId));
            return CorporationRedirect(entity);
        }

        public ActionResult CorporationRedirect(CorporationEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Address1) || string.IsNullOrEmpty(entity.City) || string.IsNullOrEmpty(entity.Country)
                    || string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.Province))
            {
                return RedirectToAction("Corporation", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Corporation");
            }
        }

        public ActionResult NonprofitRedirect(int nonprofitId)
        {
            NonprofitEntity entity = new NonprofitEntity().Map(db.Nonprofits.Single(x => x.Id == nonprofitId));
            return NonprofitRedirect(entity);
        }

        public ActionResult NonprofitRedirect(NonprofitEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Address1) || string.IsNullOrEmpty(entity.City) || string.IsNullOrEmpty(entity.Country)
                       || string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.Province))
            {
                return RedirectToAction("Nonprofit", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Nonprofit");
            }
        }

        public ActionResult AdminRedirect(int adminId)
        {
            AdminEntity entity = new AdminEntity().Map(db.Admins.Single(x => x.Id == adminId));
            return AdminRedirect(entity);
        }

        public ActionResult AdminRedirect(AdminEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Address1) || string.IsNullOrEmpty(entity.City) || string.IsNullOrEmpty(entity.Country)
                 || string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Phone) || string.IsNullOrEmpty(entity.ProfilePicturePath)
                 || string.IsNullOrEmpty(entity.Province))
            {
                return RedirectToAction("Admin", "Profile");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected void AssertRole(string roleName)
        {
            if(!db.AspNetRoles.Any(x => x.Name == roleName))
                db.AspNetRoles.Add(new AspNetRole { Name = roleName });
        }

        protected Enumerations.Role[] GetRoles(string userName)
        {
            List<Enumerations.Role> result = new List<Enumerations.Role>();
            var userRoles = db.AspNetUsers.Single(x => x.UserName == userName).AspNetRoles.Select(x => x.Name).ToArray();
            foreach (var role in Enum.GetValues(typeof(Enumerations.Role)).Cast<Enumerations.Role>())
            {
                if (userRoles.Contains(role.ToString()))
                    result.Add(role);
                //if (result == Enumerations.Role.Admin)
                //    break;
            }
            return result.ToArray();
        }

        protected int GetUserId(string userName)
        {
            return db.UserProfiles.First(x => x.UserName == userName).Id;
        }


        protected async Task<ActionResult> CreateAccount(string userName, string password, Enumerations.Role role)
        {
            var user = new ApplicationUser { UserName = userName, Email = userName };
           
            if (ModelState.IsValid)
            {
                var result = await UserManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        
                }
                AddErrors(result);
            }
            switch (role)
            {
                case Enumerations.Role.Member:
                {
                    AssertRole(Common.Constants.Roles.Member);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Member);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (MemberService innerService = new MemberService(db))
                            {
                                int memberId = innerService.Create(userName, userId);
                                return MemberRedirect(memberId);
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.CaseManager:
                {
                    AssertRole(Common.Constants.Roles.CaseManager);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.CaseManager);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (CaseManagerService innerService = new CaseManagerService(db))
                            {
                                int caseManagerId = innerService.Create(userName, userId);
                                return CaseManagerRedirect(caseManagerId);
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.Nonprofit:
                {
                    AssertRole(Common.Constants.Roles.Nonprofit);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Nonprofit);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (NonprofitService innerService = new NonprofitService(db))
                            {
                                int nonprofitId = innerService.Create(userName, userId);
                                return NonprofitRedirect(nonprofitId);
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.Corporation:
                {
                    AssertRole(Common.Constants.Roles.Corporation);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Corporation);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (CorporationService innerService = new CorporationService(db))
                            {
                                int corporationid = innerService.Create(userName, userId);
                                return CorporationRedirect(corporationid);
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                case Enumerations.Role.Admin:
                {
                    AssertRole(Common.Constants.Roles.Admin);
                    await this.UserManager.AddToRoleAsync(user.Id, Common.Constants.Roles.Admin);
                    using (UserProfileService service = new UserProfileService(db))
                    {
                        int userId = service.Create(user.UserName, user.Id);
                        if (!service.UserProfileHasValidMappings(userId))
                        {
                            using (AdminService innerService = new AdminService(db))
                            {
                                int adminId = innerService.Create(userName, userId);
                                return AdminRedirect(adminId);
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        protected static Uri UploadToBlob(string containerName, string fileName, byte[] fileBytes)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromByteArray(fileBytes, 0, fileBytes.Length);
            return blockBlob.Uri;
        }

        protected static Uri UploadToBlob(string containerName, string fileName, Stream stream)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromStream(stream);
            return blockBlob.Uri;
        }

        protected static Uri UploadToBlob(string containerName, string fileName, Stream stream, string name, string description)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            if (name != null)
                blockBlob.Metadata["name"] = name;
            if (description != null)
                blockBlob.Metadata["description"] = description;

            blockBlob.UploadFromStream(stream);
            return blockBlob.Uri;
        }
    }
}