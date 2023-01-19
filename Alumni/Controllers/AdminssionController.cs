using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Adminssion;
using Alumni.Models.Bill;
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
    public class AdminssionController:Controller
    {
        /// <summary>
        /// 入校申请
        /// </summary>
        /// <returns></returns>
        public ActionResult InstructionsForAdminission()
        {
            return View();
        }

        public ActionResult TotalAdminissionIndex()
        {
            return View();
        }

        public ActionResult GetAdminssionData(queryBillModel model)
        {
            var list = new List<AdminssionModel>();
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

        public ActionResult AdminissionExport(queryBillModel model)
        {
            try
            {
                var dt = querList(model);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //TODO: excel template
                string templatePath = string.Format("~\\Excel\\Adminission.xlsx");
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templatePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Workbook wb = new Workbook(fs);
                Worksheet sheet = wb.Worksheets[0];
                sheet.Name = "入校申请";

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
                    sheet.Cells[i + 1, 2].PutValue(dt[i].teacnerName);
                    sheet.Cells[i + 1, 3].PutValue(dt[i].DeptName);
                    sheet.Cells[i + 1, 4].PutValue(dt[i].alumnusEmp);
                    sheet.Cells[i + 1, 5].PutValue(dt[i].alumnusName);
                    sheet.Cells[i + 1, 6].PutValue(dt[i].LeaveClass);
                    sheet.Cells[i + 1, 7].PutValue(dt[i].intoDate2);
                    sheet.Cells[i + 1, 8].PutValue(dt[i].alumnusPhone);
                    sheet.Cells[i + 1, 9].PutValue(dt[i].teacnerName2);
                    sheet.Cells[i + 1, 10].PutValue(dt[i].remarks);
                    sheet.Cells[i + 1, 11].PutValue(dt[i].AddTime2);
                  //  sheet.Cells[i + 1, 19].PutValue(Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd") == "0001/01/01" ? "" : Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd"));                   
                }
                using(MemoryStream ms = new MemoryStream())
                {
                    wb.Save(ms, new OoxmlSaveOptions(SaveFormat.Xlsx));
                    var Base64Content = Convert.ToBase64String(ms.ToArray());
                    return Json(new FlagTips
                    {
                        IsSuccess = true,
                        data = Base64Content,
                        FileName = string.Format("Adminission_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff")),
                        FileType = "application/vnd.ms-excel"
                    },JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        public List<AdminssionModel> querList(queryBillModel model)
        {
            var list = new List<AdminssionModel>();
            try
            {
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
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<AdminssionModel>(sql, model).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
    }
}