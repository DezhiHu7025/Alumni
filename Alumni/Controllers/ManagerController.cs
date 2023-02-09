using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Manager;
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

        /// <summary>
        /// 获取导航栏菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult getMenu(string account)
        {
            List<PermissionModel> permissionModel = new List<PermissionModel>();
            string LiMsg = null;
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string menuSql = string.Format(@"SELECT distinct a.LiMsg,a.seq
FROM [db_forminf].[dbo].[MenuGroup] a
    LEFT JOIN [db_forminf].[dbo].[UserGroup] b
        ON a.groupid = b.groupid
		WHERE b.account = @account order by a.seq asc ");
                    permissionModel = db.Query<PermissionModel>(menuSql, new { account }).ToList();
                }
                foreach (var t in permissionModel)
                {
                    LiMsg = LiMsg + t.LiMsg;
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(new FlagTips { IsSuccess = true, Msg = LiMsg });
        }
    }
}