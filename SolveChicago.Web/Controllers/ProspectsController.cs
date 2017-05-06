using SolveChicago.Common;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveChicago.Web.Controllers
{
    public class ProspectsController : BaseController
    {
        public ProspectsController(SolveChicagoEntities entities = null)
        {
            if (entities == null)
                db = new SolveChicagoEntities();
            else
                db = entities;
        }
        public ProspectsController() : base() { }
        // GET: Prospects
        public ActionResult Index(int? nonprofitId)
        {
            ImpersonateNonprofit(nonprofitId);
            Member[] model = db.Members.Where(x => x.SurveyStep == Constants.Member.SurveyStep.Complete && !x.NonprofitMembers.Any(y => y.End.HasValue)).ToArray(); // TODO restrict it to interested
            return View(model);
        }
    }
}