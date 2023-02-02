using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.App_Start
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpCookie cookie = HttpContext.Current.Request.Cookies["info"];
            // cookie = null;
            //if (cookie == null )
            //{
            //    cookie = new HttpCookie("Info");
            //    cookie["CityID"] = HttpContext.Current.Server.UrlEncode(cityID);
            //    cookie["CityName"] = HttpContext.Current.Server.UrlEncode(CityName);
            //    cookie.Expires = DateTime.Now.AddDays(10);
            //    HttpContext.Current.Response.Cookies.Add(cookie);
            //} else
            //{
            //    //直接读值，注意编码 解码、不然汉字会出现乱码。
            //}
            //如果用户未登录，且action未明确标识可跳过登录授权，则跳转到登录页面
            if (System.Web.HttpContext.Current.Response.Cookies["Account"] == null)
            {
                const string loginUrl = "~/LoginIndex/Login";
                filterContext.Result = new RedirectResult(loginUrl);
            }
            //if (!CacheUtil.IsLogin && !filterContext.ActionDescriptor.IsDefined(typeof(AuthEscape), false))
            //{
            //    const string loginUrl = "~/LoginIndex/Login";
            //    filterContext.Result = new RedirectResult(loginUrl);
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}