using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        [App_Start.AuthFilter]
        public ActionResult GroupIndex()
        {
            return View();
        }

        /// <summary>
        /// 人员分组信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetGroupUserList(queryGroup model)
        {
            var list = new List<UserGroupModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.*,a.Account AccountName  FROM  [db_forminf].[dbo].[UserGroup] a where 1=1");
                    if (!string.IsNullOrEmpty(model.GroupId))
                    {
                        sql += " and a.GroupId = @GroupId ";
                    }
                    if (!string.IsNullOrEmpty(model.AccountName))
                    {
                        sql += " and a.Account = @AccountName ";
                    }
                    if (!string.IsNullOrEmpty(model.FullName))
                    {
                        sql += " and a.FullName like '%" + model.FullName + "%' ";
                    }
                    sql += " ORDER BY a.GroupId asc ";

                    list = db.Query<UserGroupModel>(sql, model).ToList();
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(list);
        }

        [App_Start.AuthFilter]
        public ActionResult AddGroupVw()
        {
            return View();
        }

        public ActionResult AddGroupUser(UserGroupModel model)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string checkSql = @"select * from [db_forminf].[dbo].[UserGroup]  where groupid=@groupid and account = @accountname ";
                    var num = db.Query<UserGroupModel>(checkSql, model).ToList().Count();
                    if(num != 0)
                    {
                        return Json(new FlagTips { IsSuccess = false, Msg = "请勿重复新增" });
                    }
                    else
                    {
                        string sql = string.Format(@" INSERT INTO [db_forminf].[dbo].[UserGroup] 
                                                   (groupid,groupname,account,fullname,deptname)
                                            VALUES(@groupid,@groupname,@accountname,@fullname,@deptname)");

                        Dictionary<string, object> trans = new Dictionary<string, object>();
                        trans.Add(sql, model);
                        db.DoExtremeSpeedTransaction(trans);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(new FlagTips { IsSuccess = true });
        }

        public ActionResult DeleteGroupUser(List<UserGroupModel> deleteList)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    var modelList = new List<UserGroupModel>();
                    foreach (var model in deleteList)
                    {
                        var deleteModel = new UserGroupModel();
                        deleteModel.GroupId = model.GroupId;
                        deleteModel.AccountName = model.AccountName;

                        string sql = string.Format(@" DELETE FROM [db_forminf].[dbo].[UserGroup] WHERE GROUPID = @GROUPID AND ACCOUNT = @AccountName ");

                        Dictionary<string, object> trans = new Dictionary<string, object>();
                        trans.Add(sql, deleteModel);
                        db.DoExtremeSpeedTransaction(trans);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(new FlagTips { IsSuccess = true });
        }

        public ActionResult getEmpInfo(string AccountName)
        {
            UserModel user = new UserModel();
            if (string.IsNullOrEmpty(AccountName))
            {
                return Json(new FlagTips { IsSuccess = false });
            }
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string userSql = @"SELECT a.AccountID Account,
       a.ename,
       a.cname,
       a.fullname,
       a.titlename,
       a.status,
       a.deptid2,
       a.sourcetype,
       a.email,
	   b.DeptName,
	   c.groupid,
	   c.groupname,a.password2
FROM [Common].[dbo].[kcis_account] a
LEFT JOIN [Common].[dbo].[AFS_Dept] B
ON a.deptid2 =b.DeptID_eip  
LEFT JOIN [db_forminf].[dbo].[UserGroup] c
ON a.AccountID = c.account
where sourcetype='A' and status = 'Y' and  a.AccountID = @AccountName or a.fullname like '%" + AccountName + "%' ";
                    user = db.Query<UserModel>(userSql, new { AccountName }).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}