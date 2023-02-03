using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Bill;
using Alumni.Models.Certificate;
using Aspose.Cells;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// 提交转出在读证明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CertificateSave(CertificateModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Stu_Empno) || string.IsNullOrEmpty(model.Stu_Name))
                {
                    return Json(new FlagTips
                    {
                        IsSuccess = false,
                        Msg = "账号信息获取异常，请检查登录状态。Account information acquisition exception, please check login status"
                    });
                }
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

        /// <summary>
        /// 转出在读证明 信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetCertificateData(queryBillModel model)
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
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(list);
        }

        public ActionResult SignCertificateVw()
        {
            return View();
        }

        /// <summary>
        /// 转出在读证明具体信息
        /// </summary>
        /// <param name="CmchSeqNo"></param>
        /// <returns></returns>
        public ActionResult GetCertificate(string KmchSeqNo)
        {
            CertificateModel model = new CertificateModel();
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
WHERE b.shopForm_id = 'S0000001' and a.ZmchSeqNo = @KmchSeqNo ");

                    model = db.Query<CertificateModel>(sql, new { KmchSeqNo }).FirstOrDefault();
                }
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 审核转出在读证明
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SignCertificate(CertificateModel model)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    DateTime now = DateTime.Now;
                    RecordModel record = new RecordModel();
                    record.GUID = Guid.NewGuid().ToString();
                    record.ChSeqNo = model.KmchSeqNo;
                    record.Form_Name = model.Form_Name;
                    //todo:获取登录名
                    record.Signer = "admin";
                    record.SignTime = now;
                    record.Comments = model.Comments;
                    record.IS_PASS = model.SignStatus;

                    model.IS_PASS = model.SignStatus;
                    string sql1 = @"update  [db_forminf].[dbo].[Turn_indent] set is_pass =@IS_PASS where form_name=@Form_Name and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N' and ZmchSeqNo =@KmchSeqNo ";
                    string sql2 = @"update  [db_forminf].[dbo].[OldStudent_Onlin_List] set is_pass =@IS_PASS where form_name=@Form_Name and  is_pass = 'N'  and mchSeqNo =@KmchSeqNo ";
                    string sqlRecord = @"INSERT INTO  [db_forminf].[dbo].[Record] ([GUID],[ChSeqNo],[Form_Name],[Signer],[SignTime],[Comments],[IS_PASS])
                                           VALUES(@GUID,@ChSeqNo,@Form_Name,@Signer,@SignTime,@Comments,@IS_PASS);";

                    model.timenow = now;
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(sql1, model);
                    trans.Add(sql2, model);
                    trans.Add(sqlRecord, record);
                    db.DoExtremeSpeedTransaction(trans);
                }
                return Json(new FlagTips { IsSuccess = true });

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        public List<CertificateModel> querList(string Stu_Empno, string IS_PASS)
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
WHERE b.form_id IN ( 'P0000002', 'P0000003' )");
                    if (!string.IsNullOrEmpty(Stu_Empno))
                    {
                        sql += " and (a.stunum = @Stu_Empno or a.txt_Newphone = @Stu_Empno )";
                    }
                    if (!string.IsNullOrEmpty(IS_PASS))
                    {
                        sql += " and a.is_pass = @IS_PASS ";
                    }
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<CertificateModel>(sql, new { Stu_Empno, IS_PASS}).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        /// <summary>
        /// 转出/在读证明下载
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CertificateExport(string Stu_Empno, string IS_PASS)
        {
            try
            {
                var dt = querList(Stu_Empno, IS_PASS);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                string templatePath = string.Format("~\\Excel\\Certificate.xlsx");
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templatePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Workbook wb = new Workbook(fs);
                Worksheet sheet = wb.Worksheets[0];
                sheet.Name = "转出在读证明";

                Cells cells = sheet.Cells;
                int columnCount = cells.MaxColumn;  //获取表页的最大列数
                int rowCount = cells.MaxRow;        //获取表页的最大行数

                for (int col = 0; col < columnCount; col++)
                {
                    sheet.AutoFitColumn(col, 0, rowCount);
                }
                for (int col = 0; col < columnCount; col++)
                {
                    cells.SetColumnWidthPixel(col, cells.GetColumnWidthPixel(col) + 30);
                }

                for (int i = 0; i < dt.Count; i++)//遍历DataTable行
                {
                    sheet.Cells[i + 1, 0].PutValue(dt[i].Form_Name);
                    sheet.Cells[i + 1, 1].PutValue(dt[i].IS_PASS);
                    sheet.Cells[i + 1, 2].PutValue(dt[i].Stu_Empno);
                    sheet.Cells[i + 1, 3].PutValue(dt[i].Stu_Name);
                    sheet.Cells[i + 1, 4].PutValue(dt[i].IDCard);
                    sheet.Cells[i + 1, 5].PutValue(dt[i].IDcard_Number);
                    sheet.Cells[i + 1, 6].PutValue(dt[i].PassportEname);
                    sheet.Cells[i + 1, 7].PutValue(dt[i].Hcountry);
                    sheet.Cells[i + 1, 8].PutValue(dt[i].Adress);
                    sheet.Cells[i + 1, 9].PutValue(dt[i].Email);
                    sheet.Cells[i + 1, 10].PutValue(dt[i].NewPhone);
                    sheet.Cells[i + 1, 11].PutValue(dt[i].AddTime);
                    //  sheet.Cells[i + 1, 19].PutValue(Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd") == "0001/01/01" ? "" : Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd"));                   
                }
                MemoryStream bookStream = new MemoryStream();//创建文件流
                wb.Save(bookStream, new OoxmlSaveOptions(SaveFormat.Xlsx)); //文件写入流（向流中写入字节序列）
                bookStream.Seek(0, SeekOrigin.Begin);//输出之前调用Seek，把0位置指定为开始位置
                return File(bookStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Certificate_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff")));//最后以文件形式返回
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}