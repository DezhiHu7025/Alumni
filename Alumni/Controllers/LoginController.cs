using Alumni.Db;
using Alumni.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginIndex()
        {
            return View();
        }

        public ActionResult doLogin(UserModel model)
        {
            if (string.IsNullOrEmpty(model.Account)|| string.IsNullOrEmpty(model.Password))
            {
                string errorMsg ="【登录失败】「帐号」或[密码]栏未输入！";
                return Json(new FlagTips { IsSuccess = false, Msg = errorMsg });
            }
            else
            {
                try
                {
                    //获取登入账号信息
                    LoginController con = new LoginController();
                    UserModel user = con.getEmpInfo(model.Account);
                    if (user ==null)
                    {
                        return Json(new FlagTips { IsSuccess = false, Msg = "账号异常" });
                    }
                    model.fullname = user.fullname;
                    model.DeptName = user.DeptName;
                    model.Cname = user.Cname;
                    
                    //sourcetype='A' 是教职员行政，B是小学部 I是中学部，K是幼儿园
                    if (user.status == "N" && user.sourcetype !="A")//学生账号 失效后才可以登录
                    {
                        if(model.Password == user.password2)
                        {
                            model.GroupName = "校友";
                            return Json(new FlagTips { IsSuccess = true, Msg = "student" });
                        }
                        else
                        {
                            return Json(new FlagTips { IsSuccess = false, Msg = "密码错误" });
                        }
                    }
                    else if(user.status != "N" && user.sourcetype != "A")
                    {
                        return Json(new FlagTips { IsSuccess = false, Msg = "账号异常" });
                    }

                    LoginController login = new LoginController();
                    if (login.checkAccount("192.168.80.222", model.Account, model.Password))
                    {
                        //教职员登录
                        if (user.sourcetype =="A")
                        {
                            if (!string.IsNullOrEmpty(user.GroupId))
                            {
                                model.GroupName = user.GroupName;
                                return Json(new FlagTips { IsSuccess = true, Msg = "manager" });
                            }
                            else
                            {
                                model.GroupName = "教职员";
                                return Json(new FlagTips { IsSuccess = true, Msg = "teacher" });
                            }
                        }
                    }
                    else
                    {
                        return Json(new FlagTips { IsSuccess = false, Msg = "登录失败，账号或密码不正确。Login failed. The account or password is incorrect." });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
                }
                finally
                {

                    Response.Cookies["Account"].Value = model.Account;
                    Response.Cookies["DeptName"].Value = model.DeptName;
                    Response.Cookies["fullname"].Value = model.fullname;
                    Response.Cookies["GroupName"].Value = model.GroupName;
                    Response.Cookies["cname"].Value = model.Cname;
                    Response.Cookies["Account"].Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies["DeptName"].Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies["GroupName"].Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies["fullname"].Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies["cname"].Expires = DateTime.Now.AddMonths(1);

                }
                return Json(new FlagTips { IsSuccess = true, Msg = "" });
            }
        }

        public Boolean checkAccount(string LDAP, string EmployeeID, string EmployeePasswd)
        {
            string LDAP_STRING = "LDAP://" + LDAP;

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(LDAP_STRING, EmployeeID, EmployeePasswd))
                {
                    object nativeObject = entry.NativeObject;
                    return true;
                }
            }
            catch (DirectoryServicesCOMException)
            {
                return false;
            }
        }

        public ActionResult LoginOut(UserModel model)
        {
            System.Web.HttpContext.Current.Response.Cookies.Remove("Account");//用这句cookies还在。
            System.Web.HttpContext.Current.Response.Cookies.Remove("DeptName");//用这句cookies还在。
            System.Web.HttpContext.Current.Response.Cookies.Remove("fullname");//用这句cookies还在。
            System.Web.HttpContext.Current.Response.Cookies.Remove("cname");//用这句cookies还在。
            System.Web.HttpContext.Current.Response.Cookies.Remove("GroupName");//用这句cookies还在。
            System.Web.HttpContext.Current.Response.Cookies["Account"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["DeptName"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["fullname"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["GroupName"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["cname"].Expires = DateTime.Now.AddDays(-1);
            return Json(new FlagTips { IsSuccess = true, Msg = "" });
        }

        public UserModel getEmpInfo(string Account)
        {
            UserModel user = new UserModel();
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
where a.AccountID = @Account";
                user = db.Query<UserModel>(userSql, new { Account }).FirstOrDefault();

            }
            return user;
        }
    }
}