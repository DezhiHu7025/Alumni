using Alumni.Db;
using Alumni.Models.Bill;
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
        public ActionResult BillIndex()
        {
            return View();
        }

        public ActionResult query(queryBillModel model)
        {
            return Json(queryBill(model).ToArray());
        }

        public List<BillModel> queryBill(queryBillModel model)
        {
            //var model = new queryBillModel();
            //model.Form_Name = Form_Name;
            //model.IS_PASS = IS_PASS;
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
                    querySql += " and a.stunum = 'H22080031' ";
                }
                if (!string.IsNullOrEmpty(model.Form_Name))
                {
                    querySql += " and a.form_name = @Form_Name ";
                }
                if (!string.IsNullOrEmpty(model.IS_PASS))
                {
                    querySql += " and a.is_pass = @IS_PASS ";
                }
                list = db.Query<BillModel>(querySql, model).ToList();
            }
            return list;
        }
    }
}