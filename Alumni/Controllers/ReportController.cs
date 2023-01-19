using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Bill;
using Alumni.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportIndex()
        {
            return View();
        }

        public ActionResult ReportSave(ReportModel model)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string checkSql = string.Format(@" SELECT COUNT(*)
                                                   FROM [db_forminf].[dbo].[OldStudent_Onlin_List] a
                                                       LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
                                                           ON a.form_name = b.form_name
                                                   WHERE b.shopForm_id = 'S0000001' and b.form_id = 'P0000003' is_pass='N'
                                                         AND (a.stunum = @Stu_Empno OR a.Phone = @txt_Cphone) ");
                    string cnt = db.Query<int>(checkSql, model).FirstOrDefault().ToString();
                    if (Convert.ToInt32(cnt) > 0)
                    {
                        return Json(new FlagTips
                        {
                            IsSuccess = false,
                            Msg = "已递交成绩单申请，请耐心等待审核；若长时间未审核通过，请洽询相关老师。 The transcript application has been submitted, please wait patiently for review; If it is not approved for a long time, please contact the relevant teacher."
                        });
                    }

                    model.CmchSeqNo = "CJD" + Utils.Nmrandom();
                    model.timenow = DateTime.Now;
                    model.Form_Name = "成绩单申请";
                    string insertSql = string.Format(@" insert into [db_forminf].[dbo].[Achievement_indent](CmchSeqNo,form_name,stu_empno,stu_name,passportEname,reportCard,
                                                      txt_yyyy,txt_mm,UseFor,Copies,takeWay,SendAdress,Cphone,is_pass,Is_inner,addtime)
                                                    values(@CmchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,@PassportEname,@ReportCard,
                                                      @txt_yyyy,@txt_mm,@txt_UseFor,@txt_Copies,@txt_takeWay,@txt_SendAdress,@txt_Cphone,'N','N',@timenow)");
                    string insertSql2 = string.Format(@"insert into [db_forminf].[dbo].[OldStudent_Onlin_List](mchSeqNo,form_name,stunum,stuname,is_Mail,is_pass,EmailAdress,addtime,Phone)
                                                       values(@CmchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,'N','N',@txt_SendAdress,@timenow,@txt_Cphone)");
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(insertSql, model);
                    trans.Add(insertSql2, model);
                    db.DoExtremeSpeedTransaction(trans);
                }
                return Json(new FlagTips { IsSuccess = true });

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        public ActionResult TotalReportIndex()
        {
            return View();
        }

        public ActionResult GetReportData(queryBillModel model)
        {
            var list = new List<ReportModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.CmchSeqNo,
       a.form_name,
       a.stu_empno,
       a.stu_name,
       a.passportEname,
       a.reportCard,
       a.txt_yyyy,
       a.txt_mm,
       a.UseFor txt_UseFor,
       a.Copies txt_Copies,
       a.takeWay txt_takeWay,
       a.SendAdress txt_SendAdress,
       a.Cphone txt_Cphone,
       ims.text is_pass,
       a.Is_inner,
       CONVERT(VARCHAR(100), a.addtime, 120) AddTime,
       b.form_id AS Bproduct_id
FROM [db_forminf].[dbo].[Achievement_indent] a
    LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
        ON a.form_name = b.form_name
    LEFT JOIN [db_forminf].[dbo].[IMS_CODEMSTR] ims
        ON ims.code = 'AuditState'
           AND a.is_pass = ims.value
WHERE b.shopForm_id = 'S0000001' ");
                    if (!string.IsNullOrEmpty(model.Stu_Empno))
                    {
                        sql += " and (a.stu_empno = @Stu_Empno or a.Cphone = @Stu_Empno )";
                    }
                    if (!string.IsNullOrEmpty(model.IS_PASS))
                    {
                        sql += " and a.is_pass = @IS_PASS ";
                    }
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<ReportModel>(sql, model).ToList();
                }
                return Json(list);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        public ActionResult GetReport(string CmchSeqNo)
        {
            ReportModel model = new ReportModel();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.CmchSeqNo,
       a.form_name,
       a.stu_empno,
       a.stu_name,
       a.passportEname,
       a.reportCard,
       a.txt_yyyy,
       a.txt_mm,
       a.UseFor txt_UseFor,
       a.Copies txt_Copies,
       a.takeWay txt_takeWay,
       a.SendAdress txt_SendAdress,
       a.Cphone txt_Cphone,
       ims.text is_pass,
       a.Is_inner,
       CONVERT(VARCHAR(100), a.addtime, 120) AddTime,
       b.form_id AS Bproduct_id
FROM [db_forminf].[dbo].[Achievement_indent] a
    LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
        ON a.form_name = b.form_name
    LEFT JOIN [db_forminf].[dbo].[IMS_CODEMSTR] ims
        ON ims.code = 'AuditState'
           AND a.is_pass = ims.value
WHERE b.shopForm_id = 'S0000001' and a.CmchSeqNo = @CmchSeqNo ");

                    model = db.Query<ReportModel>(sql, new { CmchSeqNo }).FirstOrDefault();
                }
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}