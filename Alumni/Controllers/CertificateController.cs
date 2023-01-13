using Alumni.Db;
using Alumni.Models;
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
    }
}