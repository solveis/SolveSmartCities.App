using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolveChicago.Entities;

namespace SolveChicago.Web.Controllers
{
    public class CorporationsController : BaseController, IDisposable
    {
        public CorporationsController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public CorporationsController() : base() { }

        public new void Dispose()
        {
            base.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // GET: Corporations
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
