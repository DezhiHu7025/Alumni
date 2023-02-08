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
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Account"];
            if (cookie == null)//判断session是否为null
            {
                filterContext.HttpContext.Response.Redirect("~/Login/LoginIndex");//跳转到登陆界面
            }
            base.OnActionExecuting(filterContext);
        }
    }
}