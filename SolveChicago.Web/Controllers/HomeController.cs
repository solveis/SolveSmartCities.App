using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Web.Controllers;
using SolveChicago.Entities;

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
            if (User.Identity.IsAuthenticated)
                return UserRedirect();
            else
                return View();
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