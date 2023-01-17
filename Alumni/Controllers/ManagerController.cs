using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult ManagerIndex()
        {
            return View();
        }

        public ActionResult InstructionIndex()
        {
            return View();
        }
    }
}