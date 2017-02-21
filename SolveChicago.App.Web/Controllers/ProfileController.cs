using SolveChicago.App.Common;
using SolveChicago.App.Common.Entities;
using SolveChicago.App.Service;
using SolveChicago.App.Web.Controllers;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveChicago.App.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        // GET: Profile/Member
        public ActionResult Member()
        {
            MemberEntity model = State.Members.FirstOrDefault();
            return View(model);
        }

        // POST: Profile/Member
        [HttpPost]
        public ActionResult Member(MemberEntity model)
        {
            if(ModelState.IsValid)
            {
                if (model.ProfilePicture != null)
                {
                    Uri pictureUri = null;
                    using (MemoryStream target = new MemoryStream())
                    {
                        model.ProfilePicture.InputStream.CopyTo(target);
                        byte[] fileBytes = Helpers.ConvertImageToPng(target.ToArray(), 500, 500);
                        pictureUri = UploadToBlob(Constants.Upload.MemberPhotos, string.Format("{0}.png", model.Id), fileBytes);
                        model.ProfilePicturePath = Helpers.RemoveSchemeFromUri(pictureUri);
                    }
                }

                using (MemberService service = new MemberService(db))
                {
                    service.UpdateProfile(model);
                }
            }
            return MemberRedirect(model);
        }

        // GET: Profile/CaseManager
        public ActionResult CaseManager()
        {
            CaseManagerEntity model = State.CaseManagers.FirstOrDefault();
            return View(model);
        }

        // POST: Profile/CaseManager
        [HttpPost]
        public ActionResult CaseManager(CaseManagerEntity model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProfilePicture != null)
                {
                    Uri pictureUri = null;
                    using (MemoryStream target = new MemoryStream())
                    {
                        model.ProfilePicture.InputStream.CopyTo(target);
                        byte[] fileBytes = Helpers.ConvertImageToPng(target.ToArray(), 500, 500);
                        pictureUri = UploadToBlob(Constants.Upload.CaseManagerPhotos, string.Format("{0}.png", model.Id), fileBytes);
                        model.ProfilePicturePath = Helpers.RemoveSchemeFromUri(pictureUri);
                    }
                }

                using (CaseManagerService service = new CaseManagerService(db))
                {
                    service.UpdateProfile(model);
                }
            }
            return RedirectToAction("Index", "CaseManager");
        }

        // GET: Profile/Nonprofit
        public ActionResult Nonprofit()
        {
            NonprofitEntity model = State.Nonprofits.FirstOrDefault();
            return View(model);
        }

        // POST: Profile/CaseManager
        [HttpPost]
        public ActionResult Nonprofit(NonprofitEntity model)
        {
            if (ModelState.IsValid)
            {
                using (NonprofitService service = new NonprofitService(db))
                {
                    service.UpdateProfile(model);
                }
            }
            return RedirectToAction("Index", "Nonprofit");
        }

        // GET: Profile/Corporation
        public ActionResult Corporation()
        {
            CorporationEntity model = State.Corporations.FirstOrDefault();
            return View(model);
        }

        // POST: Profile/Corporation
        [HttpPost]
        public ActionResult Corporation(CorporationEntity model)
        {
            if (ModelState.IsValid)
            {
                using (CorporationService service = new CorporationService(db))
                {
                    service.UpdateProfile(model);
                }
            }
            return RedirectToAction("Index", "Corporation");
        }

        // GET: Profile/Admin
        public ActionResult Admin()
        {
            AdminEntity model = State.Admins.FirstOrDefault();
            return View(model);
        }

        // POST: Profile/Admin
        [HttpPost]
        public ActionResult Admin(AdminEntity model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProfilePicture != null)
                {
                    Uri pictureUri = null;
                    using (MemoryStream target = new MemoryStream())
                    {
                        model.ProfilePicture.InputStream.CopyTo(target);
                        byte[] fileBytes = Helpers.ConvertImageToPng(target.ToArray(), 500, 500);
                        pictureUri = UploadToBlob(Constants.Upload.AdminPhotos, string.Format("{0}.png", model.Id), fileBytes);
                        model.ProfilePicturePath = Helpers.RemoveSchemeFromUri(pictureUri);
                    }
                }

                using (AdminService service = new AdminService(db))
                {
                    service.UpdateProfile(model);
                }
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}