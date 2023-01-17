using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Bill;
using Alumni.Models.Taotaldata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class TaotaldataController : Controller
    {
        // GET: Taotaldata
        public ActionResult TaotaldataIndex()
        {
            return View();
        }

        public ActionResult GetData(queryBillModel model)
        {
            var list = new List<TaotaldataModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.mchSeqNo,
       a.form_name,
       ims.text is_pass,
       a.stunum Stu_Empno,
       a.stuname Stu_Name,
       a.EmailAdress,
       a.Phone,
       CONVERT(VARCHAR(100),a.addtime,120) AddTime,
       b.form_id AS Bproduct_id
FROM [db_forminf].[dbo].[OldStudent_Onlin_List] a
    LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
        ON a.form_name = b.form_name
    LEFT JOIN [db_forminf].[dbo].[IMS_CODEMSTR] ims
        ON ims.code = 'AuditState'
           AND a.is_pass = ims.value
WHERE b.form_id IN ( 'P0000002', 'P0000003' )");
                    if (!string.IsNullOrEmpty(model.Stu_Empno))
                    {
                        sql += " and (a.stunum = @Stu_Empno or a.phone = @Stu_Empno )";
                    }
                    if (!string.IsNullOrEmpty(model.Form_Name))
                    {
                        sql += " and b.form_id = @Form_Name ";
                    }
                    if (!string.IsNullOrEmpty(model.IS_PASS))
                    {
                        sql += " and a.is_pass = @IS_PASS ";
                    }
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<TaotaldataModel>(sql,model).ToList();
                }
                return Json(list);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}