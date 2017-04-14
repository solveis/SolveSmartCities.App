using SolveChicago.Common;
using SolveChicago.Entities;
using SolveChicago.Marketing.Models;
using SolveChicago.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveChicago.Marketing.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(SolveChicagoEntities entities = null) : base()
        {
            if (entities == null)
            {
                db = new SolveChicagoEntities();
            }
            else
            {
                db = entities;
            }
        }
        public HomeController() { db = new SolveChicagoEntities(); }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        protected SolveChicagoEntities db;

        public ActionResult Index()
        {
            ContactModel model = new ContactModel();
            return View(model);
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
    }
}