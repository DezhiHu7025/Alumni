using Alumni.Db;
using Alumni.Models;
using Alumni.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class DDLController : Controller
    {
        /// <summary>
        /// 证件类型
        /// </summary>
        /// <returns></returns>
        public ActionResult getIDCard(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("IDCard", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}