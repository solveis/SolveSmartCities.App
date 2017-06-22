using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolveChicago.Web.Controllers
{
    public class SidebarController : BaseController
    {
        // GET: Sidebar/Admin
        public ActionResult Admin()
        {
            StateModel state = State;
            return PartialView("Sidebar/_Admin");
        }

        // GET: Sidebar/CaseManager
        public ActionResult CaseManager()
        {
            StateModel state = State;
            return PartialView("Sidebar/_CaseManager");
        }

        // GET: Sidebar/Corporation
        public ActionResult Corporation()
        {
            StateModel state = State;
            return PartialView("Sidebar/_Corporation");
        }

        // GET: Sidebar/Member
        public ActionResult Member()
        {
            StateModel state = State;
            return PartialView("Sidebar/_Member");
        }

        // GET: Sidebar/Nonprofit
        public ActionResult Nonprofit()
        {
            StateModel state = State;
            return PartialView("Sidebar/_Nonprofit");
        }
    }
}