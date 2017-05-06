using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Web.Controllers;
using SolveChicago.Entities;
using SolveChicago.Service;
using SolveChicago.Common;
using SolveChicago.Common.Models;

namespace SolveChicago.Web.Controllers
{
    public class HomeController : BaseController, IDisposable
    {
        public HomeController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }

        public HomeController() : base() { }

        public new void Dispose()
        {
            base.Dispose();
        }


        public ActionResult Index()
        {

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                return UserRedirect();
            else
            {
                ContactModel model = new ContactModel();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            string communicationType = Constants.Communication.Inquiry;
            EmailService service = new EmailService(db);
            service.DeliverSendGridMessage(
                Settings.Mail.InfoEmail,
                model.Name,
                model.Subject,
                "16ed84a1-3258-4965-ae84-29c5e96c2a81",
                new Dictionary<string, string>
                {
                    { "-Name-", model.Name },
                    { "-Phone-", model.Phone },
                    { "-Email-", model.Email },
                    { "-Subject-", model.Subject },
                    { "-Body-", model.Body },
                    { "-year-", DateTime.UtcNow.Year.ToString() },
                },
                model.Email,
                communicationType,
                "",
                null
            ).Wait();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}