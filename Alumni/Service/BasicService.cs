using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Service
{
    public class BasicService
    {
        public const string DEFAULTPARAMETER = "DEFAULTPARAMETER";


        public static void CkPriAdd(string area, string ctrl, string action)
        {
            var pri = AspNetUtil.Authority.GetPrivilegeMVC(area, ctrl, action);
            if (!pri.CanAdd)
                throw new Exception("无权限操作");
        }

        public static void CkPriDel(string area, string ctrl, string action)
        {
            var pri = AspNetUtil.Authority.GetPrivilegeMVC(area, ctrl, action);
            if (!pri.CanDelete)
                throw new Exception("无权限操作");
        }
        public static void CkPriEdit(string area, string ctrl, string action)
        {
            var pri = AspNetUtil.Authority.GetPrivilegeMVC(area, ctrl, action);
            if (!pri.CanEdit)
                throw new Exception("无权限操作");
        }
        public static void CkPriQry(string area, string ctrl, string action)
        {
            var pri = AspNetUtil.Authority.GetPrivilegeMVC(area, ctrl, action);
            if (!pri.CanQuery)
                throw new Exception("无权限操作");
        }

        public static void GetPriQry(string area, string ctrl, string action, string workaction)
        {
            var pri = AspNetUtil.Authority.GetPrivilegeMVC(area, ctrl, action);
            if (!pri.CanQuery)
            {
                string str = string.Format("该用户组只能查询和下载{0}", workaction);
                throw new Exception(str);
            }
        }
    }
}