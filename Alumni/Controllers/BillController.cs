using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Adminssion;
using Alumni.Models.Bill;
using Alumni.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        [App_Start.AuthFilter]
        public ActionResult BillIndex()
        {
            return View();
        }

        public ActionResult query(queryBillModel model)
        {
            return Json(queryBill(model).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public List<BillModel> queryBill(queryBillModel model)
        {
            var list = new List<BillModel>();
            using (SchoolDb db = new SchoolDb())
            {
                string querySql = string.Format(@"SELECT a.mchSeqNo SeqNo,
                                                         a.form_name,
                                                         a.stunum,
                                                         a.is_pass,
                                                         ims.text IS_PASS,
                                                         CASE a.form_name
                                                             WHEN '校友入校申请' THEN
                                                                 '入校时间'
                                                             WHEN '转出/在读证明' THEN
                                                                 '邮寄地址'
                                                             WHEN '成绩单申请' THEN
                                                                 '邮寄/邮箱地址'
                                                             ELSE
                                                                 a.form_name
                                                         END AS TitleName,
                                                         CASE a.form_name
                                                             WHEN '校友入校申请' THEN
                                                                 a.intoDate
                                                             ELSE
                                                                 a.EmailAdress
                                                         END AS SendAdress,
                                                         b.form_id AS Bproduct_id
                                                   FROM [db_forminf].[dbo].[OldStudent_Onlin_List] a
                                                       LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
                                                           ON a.form_name = b.form_name
                                                       LEFT JOIN [db_forminf].[dbo].[IMS_CODEMSTR] ims
                                                           ON ims.code = 'IS_PASS'
                                                           AND a.is_pass = ims.value
                                                   WHERE b.shopForm_id = 'S0000001'");

                    if (!string.IsNullOrEmpty(model.Stu_Empno))
                    {
                        querySql += " and a.stunum = @Stu_Empno ";
                    }
                    if (!string.IsNullOrEmpty(model.Form_Name))
                    {
                        querySql += " and b.form_id = @Form_Name ";
                    }
                    if (!string.IsNullOrEmpty(model.IS_PASS))
                    {
                        querySql += " and a.is_pass = @IS_PASS ";
                    }
                list = db.Query<BillModel>(querySql, model).ToList();
            }
            return list;
        }

        [App_Start.AuthFilter]
        public ActionResult BillQueryVw()
        {
            return View();
        }

        public ActionResult queryBillApply(queryBillModel model)
        {
            var list = new List<AdminssionModel>();
            using (SchoolDb db = new SchoolDb())
            {
                string sql = string.Format(@"SELECT a.*,
       CONVERT(VARCHAR(100), a.addtime, 120) AddTime,
	   a.intoDate intoDate2,
	   a.addtime addtime2,
	   ims.text is_pass,
       b.form_id AS Bproduct_id
FROM [db_forminf].[dbo].[EntryApply_indent] a
    LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
        ON a.form_name = b.form_name
    LEFT JOIN [db_forminf].[dbo].[IMS_CODEMSTR] ims
        ON ims.code = 'AuditState'
           AND a.is_pass = ims.value
WHERE b.shopForm_id = 'S0000001' ");

                if (!string.IsNullOrEmpty(model.Stu_Empno))
                {
                    sql += " and (a.alumnusEmp = @Stu_Empno or a.alumnusPhone = @Stu_Empno )";
                }
                if (!string.IsNullOrEmpty(model.IS_PASS))
                {
                    sql += " and a.is_pass = @IS_PASS ";
                }
                if (!string.IsNullOrEmpty(model.Account))
                {
                    sql += " and a.teacherAD = @Account ";
                }
                sql += " ORDER BY a.addtime DESC ";
                list = db.Query<AdminssionModel>(sql, model).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [App_Start.AuthFilter]
        public ActionResult BillDetailVw()
        {
            return View();
        }

        public ActionResult ReSendMail()
        {
            SendMailService sendMailService = new SendMailService();
            bool mailFlag = sendMailService.doMail("入校申请稽催邮件");
            try
            {
                if (mailFlag)
                {
                    return Json(new FlagTips { IsSuccess = true, Msg = "已发送稽催邮件" });
                }
                else
                {
                    return Json(new FlagTips { IsSuccess = false, Msg = "邮件发送失败" });
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}