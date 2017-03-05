using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Web.Controllers;
using SolveChicago.Web.Data;

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

        public new void Dispose()
        {
            base.Dispose();
        }


        public ActionResult Index()
        {
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