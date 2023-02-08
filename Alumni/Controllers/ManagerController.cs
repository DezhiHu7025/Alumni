using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class ManagerController : Controller
    {

        [App_Start.AuthFilter]
        // GET: Manager
        public ActionResult ManagerIndex()
        {
            return View();
        }

        [App_Start.AuthFilter]
        public ActionResult InstructionIndex()
        {
            return View();
        }
    }
}