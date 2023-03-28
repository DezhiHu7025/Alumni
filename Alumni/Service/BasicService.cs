using Alumni.Db;
using Alumni.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Service
{
    public class BasicService
    {
        public const string DEFAULTPARAMETER = "DEFAULTPARAMETER";
        
        public UserGroupModel CkPri()
        {
            //string Account = HttpContext.Current.Request.Cookies["Account"].Value;
            //string GroupName = HttpContext.Current.Request.Cookies["GroupName"].Value;decodeURIComponent
            string Account = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["Account"].Value, Encoding.GetEncoding("UTF-8"));
            string GroupName = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["GroupName"].Value, Encoding.GetEncoding("UTF-8"));
            UserGroupModel model = new UserGroupModel();
            using (SchoolDb db = new SchoolDb())
            {
                string sql = @"SELECT * FROM [db_forminf].[dbo].[UserGroup] WHERE account = @Account AND GroupName = @GroupName ";
                model = db.Query<UserGroupModel>(sql, new { Account, GroupName }).FirstOrDefault();
            }
            return model;
        }

    }
}