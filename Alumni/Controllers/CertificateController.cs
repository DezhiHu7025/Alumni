using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Bill;
using Alumni.Models.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class CertificateController : Controller
    {
        /// <summary>
        /// 转出/在读证明
        /// </summary>
        /// <returns></returns>
        public ActionResult CertificateIndex()
        {
            return View();
        }

        public ActionResult CertificateSave(CertificateModel model)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string checkSql = string.Format(@" SELECT COUNT(*)
                                                   FROM [db_forminf].[dbo].[OldStudent_Onlin_List] a
                                                       LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
                                                           ON a.form_name = b.form_name
                                                   WHERE b.shopForm_id = 'S0000001' and b.form_id = 'P0000002' and is_pass = 'N'
                                                         AND (a.stunum = @Stu_Empno OR a.Phone = @NewPhone) ");
                    string cnt = db.Query<int>(checkSql, model).FirstOrDefault().ToString();
                    if (Convert.ToInt32(cnt) > 0)
                    {
                        return Json(new FlagTips
                        {
                            IsSuccess = false,
                            Msg = "已递交转出/在读证明申请，请耐心等待审核；若长时间未审核通过，请洽询相关老师。The application for transfer out/reading certificate has been submitted, please wait patiently for review; If you fail to pass the review for a long time, please contact the relevant teachers."
                        });
                    }

                    model.KmchSeqNo = "ZCZ" + Utils.Nmrandom();
                    model.timenow = DateTime.Now;
                    model.Form_Name = "转出/在读证明";
                    string insertSql = string.Format(@" insert into [db_forminf].[dbo].[Turn_indent](ZmchSeqNo,form_name,stunum,stuname,IDcard,IDcard_number,passportEname,Hcountry,adress,
                                                      EmailAdress,txt_Newphone,is_pass,Is_inner,addtime)
                                                    values(@KmchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,@IDCard,@IDcard_Number,@PassportEname,@Hcountry,@Adress,
                                                      @Email,@NewPhone,'N','N',@timenow)");
                    string insertSql2 = string.Format(@"insert into [db_forminf].[dbo].[OldStudent_Onlin_List](mchSeqNo,form_name,stunum,stuname,is_Mail,is_pass,EmailAdress,addtime,Phone)
                                                       values(@KmchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,'N','N',@Adress,@timenow,@NewPhone)");
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

        public ActionResult TotalCertificateIndex()
        {
            return View();
        }

        public ActionResult GetCertificateData(queryBillModel model)
        {
            var list = new List<CertificateModel>();
            try
            {
                list = querList(model);
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(list);
        }

        public List<CertificateModel> querList(queryBillModel model)
        {
            var list = new List<CertificateModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.ZmchSeqNo KmchSeqNo,
       a.form_name,
       a.stunum Stu_Empno,
       a.stuname Stu_Name,
       ims.text is_pass,
       a.IDcard,
       a.IDcard_number,
       a.passportEname,
       a.adress,
       a.Hcountry,
       a.txt_Newphone NewPhone,
       CONVERT(VARCHAR(100), a.addtime, 120) AddTime,
	   a.EmailAdress Email,
       b.form_id AS Bproduct_id
FROM [db_forminf].[dbo].[Turn_indent] a
    LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
        ON a.form_name = b.form_name
    LEFT JOIN [db_forminf].[dbo].[IMS_CODEMSTR] ims
        ON ims.code = 'AuditState'
           AND a.is_pass = ims.value
WHERE b.shopForm_id = 'S0000001' ");
                    if (!string.IsNullOrEmpty(model.Stu_Empno))
                    {
                        sql += " and (a.stunum = @Stu_Empno or a.txt_Newphone = @Stu_Empno )";
                    }
                    if (!string.IsNullOrEmpty(model.IS_PASS))
                    {
                        sql += " and a.is_pass = @IS_PASS ";
                    }
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<CertificateModel>(sql, model).ToList();
                }
            }
            catch (Exception ex)
            {
               
            }
            return list;
        }
    }
}