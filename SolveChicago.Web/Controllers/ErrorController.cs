using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveChicago.Web.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error/Error500
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

    }
}