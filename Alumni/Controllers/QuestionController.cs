using Alumni.Db;
using Alumni.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alumni.Models;
using Alumni.Models.Bill;
using OfficeOpenXml;
using System.IO;
using Aspose.Cells;
using Alumni.Service;
using System.Text;

namespace Alumni.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        [App_Start.AuthFilter]
        public ActionResult QuestionIndex()
        {
            return View();
        }

        public ActionResult QuestionSave(QuestionModel model)
        {
            try
            {
                HttpPostedFileBase httpPostedFileBase = Request.Files["file"];
                ControllerContext.HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
                ControllerContext.HttpContext.Response.Charset = "UTF-8";
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
                    model.QchSeqNo = "QUE" + Utils.Nmrandom();
                    model.CreateTime = DateTime.Now;
                    model.Form_Name = "问卷调查";
                    string insertSql2 = string.Format(@"INSERT INTO [db_forminf].[dbo].[Questionnaire _Investigation] 
                                                               (QchSeqNo,Form_Name,Stu_Empno,Stu_Name,Stu_Eame,GraduationYear,CurrentDevelopment,CurrentDevelopmentText,WorkUnitName,
                                                               JobTitle,ContentOverview,HighestEducation,HighestEducationStatus,HighestEducationText,UniversityName,UniversityDepartmentName,
                                                               InstituteName,InstituteDepartmentName,DoctoralClassName,DoctoralDepartmentName,Transferred,TransferredText,OtherSupplements,CreateTime,
                                                               GraduationStatus,GraduationYearText,Email,WeChat,NewPhone,WillJoin,LatestPhoto)
                                                                VALUES(@QchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,@Stu_Eame,@GraduationYear,@CurrentDevelopment,@CurrentDevelopmentText,@WorkUnitName,
                                                               @JobTitle,@ContentOverview,@HighestEducation,@HighestEducationStatus,@HighestEducationText,@UniversityName,@UniversityDepartmentName,
                                                               @InstituteName,@InstituteDepartmentName,@DoctoralClassName,@DoctoralDepartmentName,@Transferred,@TransferredText,@OtherSupplements,@CreateTime,
                                                               @GraduationStatus,@GraduationYearText,@Email,@WeChat,@NewPhone,@WillJoin,@LatestPhoto)");
                    if (httpPostedFileBase != null)
                    {
                        FileUploadService upfile = new FileUploadService();
                        var uploadModel = upfile.FileLoad(httpPostedFileBase, model.Stu_Empno);
                        if (uploadModel.IsSuccess == true)
                        {
                            model.LatestPhoto = uploadModel.Msg;
                        }
                        else
                        {
                            return Json(new FlagTips { IsSuccess = false, Msg = "照片上传失败 Photo upload failed" });
                        }
                    }
                    Dictionary<string, object> trans = new Dictionary<string, object>();
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

        [App_Start.AuthFilter]
        public ActionResult TotalQuestionIndex()
        {
            return View();
        }

        /// <summary>
        /// 校友联络信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetQuestionData(queryBillModel model)
        {
            var list = new List<QuestionModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.*,
       CONVERT(VARCHAR(100), a.CreateTime, 120) CreateTime2,
       CASE a.GraduationStatus
           WHEN '其他' THEN
               a.GraduationStatus + '   ' + a.GraduationYearText
           ELSE
               a.GraduationStatus
       END AS GraduationStatus,
       CASE a.CurrentDevelopment
           WHEN '其他' THEN
               a.CurrentDevelopment + '   ' + a.CurrentDevelopmentText
           ELSE
               a.CurrentDevelopment
       END AS CurrentDevelopment,
       CASE a.HighestEducationStatus
           WHEN '其他' THEN
               a.HighestEducationStatus + '   ' + a.HighestEducationText
           ELSE
               a.HighestEducationStatus
       END AS HighestEducationStatus,
       CASE a.Transferred
           WHEN '是' THEN
               a.Transferred + '   ' + a.TransferredText
           ELSE
               a.Transferred
       END AS Transferred
FROM [db_forminf].[dbo].[Questionnaire _Investigation] a
where 1=1");
                    if (!string.IsNullOrEmpty(model.Stu_Empno))
                    {
                        sql += " and (a.Stu_Empno = @Stu_Empno )";
                    }
                    sql += " ORDER BY a.CreateTime DESC ";

                    list = db.Query<QuestionModel>(sql, model).ToList();
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(list);
        }

        public List<QuestionModel> querList(string Stu_Empno)
        {
            var list = new List<QuestionModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.*,
       CASE a.GraduationStatus
           WHEN '其他' THEN
               a.GraduationStatus + '   ' + a.GraduationYearText
           ELSE
               a.GraduationStatus
       END AS GraduationStatus,
       CASE a.CurrentDevelopment
           WHEN '其他' THEN
               a.CurrentDevelopment + '   ' + a.CurrentDevelopmentText
           ELSE
               a.CurrentDevelopment
       END AS CurrentDevelopment,
       CASE a.HighestEducationStatus
           WHEN '其他' THEN
               a.HighestEducationStatus + '   ' + a.HighestEducationText
           ELSE
               a.HighestEducationStatus
       END AS HighestEducationStatus,
       CASE a.Transferred
           WHEN '是' THEN
               a.Transferred + '   ' + a.TransferredText
           ELSE
               a.Transferred
       END AS Transferred
FROM [db_forminf].[dbo].[Questionnaire _Investigation] a
where 1=1 ");
                    if (!string.IsNullOrEmpty(Stu_Empno))
                    {
                        sql += " and (a.Stu_Empno = @Stu_Empno )";
                    }
                    sql += " ORDER BY a.CreateTime DESC ";

                    list = db.Query<QuestionModel>(sql, new { Stu_Empno }).ToList();
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
        public ActionResult QuestionExport(string Stu_Empno)
        {
            try
            {
                var dt = querList(Stu_Empno);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                string templatePath = string.Format("~\\Excel\\Question.xlsx");
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templatePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Workbook wb = new Workbook(fs);
                Worksheet sheet = wb.Worksheets[0];
                sheet.Name = "问卷调查";

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
                    sheet.Cells[i + 1, 1].PutValue(dt[i].Stu_Empno);
                    sheet.Cells[i + 1, 2].PutValue(dt[i].Stu_Name);
                    sheet.Cells[i + 1, 3].PutValue(dt[i].Stu_Eame);
                    sheet.Cells[i + 1, 4].PutValue(dt[i].GraduationStatus);
                    sheet.Cells[i + 1, 5].PutValue(dt[i].GraduationYear);
                    sheet.Cells[i + 1, 6].PutValue(dt[i].CurrentDevelopment);
                    sheet.Cells[i + 1, 7].PutValue(dt[i].WorkUnitName);
                    sheet.Cells[i + 1, 8].PutValue(dt[i].JobTitle);
                    sheet.Cells[i + 1, 9].PutValue(dt[i].ContentOverview);

                    sheet.Cells[i + 1, 10].PutValue(dt[i].HighestEducation);
                    sheet.Cells[i + 1, 11].PutValue(dt[i].HighestEducationStatus); 
                    sheet.Cells[i + 1, 12].PutValue(dt[i].UniversityName);
                    sheet.Cells[i + 1, 13].PutValue(dt[i].UniversityDepartmentName);
                    sheet.Cells[i + 1, 14].PutValue(dt[i].InstituteName);
                    sheet.Cells[i + 1, 15].PutValue(dt[i].InstituteDepartmentName);
                    sheet.Cells[i + 1, 16].PutValue(dt[i].DoctoralClassName);
                    sheet.Cells[i + 1, 17].PutValue(dt[i].DoctoralDepartmentName);
                    sheet.Cells[i + 1, 18].PutValue(dt[i].Transferred);
                    sheet.Cells[i + 1, 19].PutValue(dt[i].Email);

                    sheet.Cells[i + 1, 20].PutValue(dt[i].WeChat);
                    sheet.Cells[i + 1, 21].PutValue(dt[i].NewPhone);
                    sheet.Cells[i + 1, 22].PutValue(dt[i].WillJoin);
                    sheet.Cells[i + 1, 23].PutValue(dt[i].OtherSupplements);
                    sheet.Cells[i + 1, 24].PutValue(Convert.ToDateTime(dt[i].CreateTime).ToString("yyyy/MM/dd") == "0001/01/01" ? "" : Convert.ToDateTime(dt[i].CreateTime).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                MemoryStream bookStream = new MemoryStream();//创建文件流
                wb.Save(bookStream, new OoxmlSaveOptions(SaveFormat.Xlsx)); //文件写入流（向流中写入字节序列）
                bookStream.Seek(0, SeekOrigin.Begin);//输出之前调用Seek，把0位置指定为开始位置
                return File(bookStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Question_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff")));//最后以文件形式返回
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}