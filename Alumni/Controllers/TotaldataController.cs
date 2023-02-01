using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Bill;
using Alumni.Models.Totaldata;
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
    public class TotaldataController : Controller
    {
        // GET: Taotaldata
        public ActionResult TotaldataIndex()
        {
            return View();
        }

        /// <summary>
        /// 总数据信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetData(queryBillModel model)
        {
            var list = new List<TotaldataModel>();
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

                    list = db.Query<TotaldataModel>(sql,model).ToList();
                }
                return Json(list);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        public List<TotaldataModel> querList(string Stu_Empno, string IS_PASS,string Form_Name)
        {
            var list = new List<TotaldataModel>();
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
                    if (!string.IsNullOrEmpty(Stu_Empno))
                    {
                        sql += " and (a.stunum = @Stu_Empno or a.Phone = @Stu_Empno )";
                    }
                    if (!string.IsNullOrEmpty(IS_PASS))
                    {
                        sql += " and a.is_pass = @IS_PASS ";
                    }
                    if (!string.IsNullOrEmpty(Form_Name))
                    {
                        sql += " and b.form_id = @Form_Name ";
                    }
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<TotaldataModel>(sql, new { Stu_Empno, IS_PASS, Form_Name }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        /// <summary>
        /// 总数据下载
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult TotaldataExport(string Stu_Empno, string IS_PASS, string Form_Name)
        {
            try
            {
                var dt = querList(Stu_Empno, IS_PASS, Form_Name);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                string templatePath = string.Format("~\\Excel\\Totaldata.xlsx");
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templatePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Workbook wb = new Workbook(fs);
                Worksheet sheet = wb.Worksheets[0];
                sheet.Name = "总数据";

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
                    sheet.Cells[i + 1, 4].PutValue(dt[i].EmailAdress);
                    sheet.Cells[i + 1, 5].PutValue(dt[i].Phone);
                    sheet.Cells[i + 1, 6].PutValue(dt[i].AddTime);
                    //  sheet.Cells[i + 1, 19].PutValue(Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd") == "0001/01/01" ? "" : Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd"));                   
                }
                MemoryStream bookStream = new MemoryStream();//创建文件流
                wb.Save(bookStream, new OoxmlSaveOptions(SaveFormat.Xlsx)); //文件写入流（向流中写入字节序列）
                bookStream.Seek(0, SeekOrigin.Begin);//输出之前调用Seek，把0位置指定为开始位置
                return File(bookStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Totaldata_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff")));//最后以文件形式返回
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}