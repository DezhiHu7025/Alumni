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
    public class MailController : Controller
    {
        // GET: 邮件设定
        [App_Start.AuthFilter]
        public ActionResult MailIndex()
        {
            return View();
        }

        /// <summary>
        /// 邮件设定列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult getMailList(EmailModel model)
        {
            var list = new List<EmailModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.*,CONVERT(VARCHAR(100), a.updatetime, 120) updatetime2 from [db_forminf].[dbo].[MailSetting] a where 1=1 ");
                    if (!string.IsNullOrEmpty(model.subject))
                    {
                        sql += " and a.subject = @subject ";
                    }
                    sql += " ORDER BY a.updatetime desc";

                    list = db.Query<EmailModel>(sql, model).ToList();
                }
                return Json(list);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 新增修改页面
        /// </summary>
        /// <returns></returns>
        [App_Start.AuthFilter]
        public ActionResult AddMailVw()
        {
            return View();
        }

        /// <summary>
        /// 修改邮件设定页面
        /// </summary>
        /// <returns></returns>
        [App_Start.AuthFilter]
        public ActionResult UpdateMailVw()
        {
            return View();
        }

        /// <summary>
        /// 邮件设定详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult getMail(string subject)
        {
            var model = new EmailModel();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.*,CONVERT(VARCHAR(100), a.updatetime, 120) updatetime2 from [db_forminf].[dbo].[MailSetting] a where a.subject = @subject ");

                    model = db.Query<EmailModel>(sql, new { subject }).FirstOrDefault();
                }
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 新增/修改邮件设定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveMail(EmailModel model)
        {
            try
            {
                var list = new List<EmailModel>();
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = "";
                    model.updatetime = DateTime.Now;
                    //处理html标签
                    model.body = model.body.Replace("sqm", "'").Replace("anchor", "<").Replace("tail", ">");
                    //新增、修改
                    if (model.type == "update")
                    {
                        sql = string.Format(@" update [db_forminf].[dbo].[MailSetting] 
                                                set  toaddr=@toaddr,toname=@toname, body = @body,attch = @attch, remark = @remark, updateuser = @updateuser, updatetime = @updatetime
                                                     where subject = @subject ");
                    }
                    else if (model.type == "add")
                    {
                        BasicService ser = new BasicService();
                        var newmodel = ser.CkPri();
                        if (newmodel.DoAdd != "Y")
                        {
                            return Json(new FlagTips { IsSuccess = false, Msg = "您没有新增权限" });
                        }
                        string checkSql = @"SELECT a.*  from [db_forminf].[dbo].[MailSetting] a where a.subject = @subject";
                        list = db.Query<EmailModel>(checkSql, model).ToList();
                        if (list.Count() != 0)
                        {
                            return Json(new FlagTips { IsSuccess = false, Msg = "已存在关于"+model.subject+"的邮件设定，请勿重复"});
                        }
                        sql = string.Format(@" INSERT INTO [db_forminf].[dbo].[MailSetting] 
                                                   (pid,actiontype,toaddr,toname,strSystem,subject,body,attch,remark,updateuser,updatetime)
                                            VALUES('sys_flowengin','email',@toaddr,@toname,@strSystem,@subject,@body,@attch,@remark,@updateuser,@updatetime)");
                    }
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(sql, model);
                    db.DoExtremeSpeedTransaction(trans);
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new FlagTips { IsSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除邮件设定
        /// </summary>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        public ActionResult DeleteMail(List<EmailModel> deleteList)
        {
            try
            {
                BasicService ser = new BasicService();
                var newmodel = ser.CkPri();
                if (newmodel.DoDelete != "Y")
                {
                    return Json(new FlagTips { IsSuccess = false, Msg = "您没有删除权限" });
                }
                List<string> newarry = new List<string>();
                foreach (var d in deleteList)
                {
                    newarry.Add(d.subject);
                }
                string deletestring = string.Join(",", newarry.ToArray());
                string[] deletestr = deletestring.Split(
                           new[] { "\n", ",", "\n\r" },
                           StringSplitOptions.None
                       );
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@" DELETE FROM [db_forminf].[dbo].[MailSetting] WHERE subject in @deletestr");
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(sql, new { deletestr });
                    db.DoExtremeSpeedTransaction(trans);
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(new FlagTips { IsSuccess = true });
        }
    }
}