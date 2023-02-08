using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class HomeController: Controller
    {

        [App_Start.AuthFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}