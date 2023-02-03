using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Bill;
using Alumni.Models.Report;
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
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportIndex()
        {
            return View();
        }

        /// <summary>
        /// 提交成绩单申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ReportSave(ReportModel model)
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
                                                   WHERE b.shopForm_id = 'S0000001' and b.form_id = 'P0000003'and is_pass='N'
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

        public ActionResult SignReportVw()
        {
            return View();
        }

        /// <summary>
        /// 成绩单信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 成绩单具体信息
        /// </summary>
        /// <param name="CmchSeqNo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 审核成绩单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SignReport(ReportModel model)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    DateTime now = DateTime.Now;
                    RecordModel record = new RecordModel();
                    record.GUID = Guid.NewGuid().ToString();
                    record.ChSeqNo = model.CmchSeqNo;
                    record.Form_Name = model.Form_Name;
                    //todo:获取登录名
                    record.Signer = "admin";
                    record.SignTime = now;
                    record.Comments = model.Comments;
                    record.IS_PASS = model.SignStatus;

                    model.IS_PASS = model.SignStatus;
                    string sql1 = @"update  [db_forminf].[dbo].[Achievement_indent] set is_pass = @IS_PASS where form_name=@Form_Name and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and CmchSeqNo =@CmchSeqNo ";
                    string sql2 = @"update  [db_forminf].[dbo].[OldStudent_Onlin_List] set is_pass = @IS_PASS where form_name=@Form_Name and  is_pass = 'N'  and mchSeqNo =@CmchSeqNo ";
                    string sqlRecord = @"INSERT INTO  [db_forminf].[dbo].[Record] ([GUID],[ChSeqNo],[Form_Name],[Signer],[SignTime],[Comments],[IS_PASS])
                                           VALUES(@GUID,@ChSeqNo,@Form_Name,@Signer,@SignTime,@Comments,@IS_PASS);";

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

        public List<ReportModel> querList(string Stu_Empno, string IS_PASS)
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
                    if (!string.IsNullOrEmpty(Stu_Empno))
                    {
                        sql += " and (a.stu_empno = @Stu_Empno or a.Cphone = @Stu_Empno )";
                    }
                    if (!string.IsNullOrEmpty(IS_PASS))
                    {
                        sql += " and a.is_pass = @IS_PASS ";
                    }
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<ReportModel>(sql, new { Stu_Empno, IS_PASS }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        /// <summary>
        /// 成绩单信息下载
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ReportExport(string Stu_Empno, string IS_PASS)
        {
            try
            {
                var dt = querList(Stu_Empno, IS_PASS);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                string templatePath = string.Format("~\\Excel\\Report.xlsx");
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templatePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Workbook wb = new Workbook(fs);
                Worksheet sheet = wb.Worksheets[0];
                sheet.Name = "成绩单申请";

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
                    sheet.Cells[i + 1, 4].PutValue(dt[i].PassportEname);
                    sheet.Cells[i + 1, 5].PutValue(dt[i].ReportCard);
                    sheet.Cells[i + 1, 6].PutValue(dt[i].txt_yyyy);
                    sheet.Cells[i + 1, 7].PutValue(dt[i].txt_mm);
                    sheet.Cells[i + 1, 8].PutValue(dt[i].txt_UseFor);
                    sheet.Cells[i + 1, 9].PutValue(dt[i].txt_Copies);
                    sheet.Cells[i + 1, 10].PutValue(dt[i].txt_takeWay);
                    sheet.Cells[i + 1, 11].PutValue(dt[i].txt_SendAdress);
                    sheet.Cells[i + 1, 12].PutValue(dt[i].txt_Cphone);
                    sheet.Cells[i + 1, 13].PutValue(dt[i].AddTime);
                    //  sheet.Cells[i + 1, 19].PutValue(Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd") == "0001/01/01" ? "" : Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd"));                   
                }
                MemoryStream bookStream = new MemoryStream();//创建文件流
                wb.Save(bookStream, new OoxmlSaveOptions(SaveFormat.Xlsx)); //文件写入流（向流中写入字节序列）
                bookStream.Seek(0, SeekOrigin.Begin);//输出之前调用Seek，把0位置指定为开始位置
                return File(bookStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Report_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff")));//最后以文件形式返回
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}