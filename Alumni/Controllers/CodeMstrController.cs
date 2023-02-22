using Alumni.Db;
using Alumni.Models;
using Alumni.Service;
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
    public class CodeMstrController : Controller
    {
        // GET: CodeMstr
        [App_Start.AuthFilter]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult getCodeMstrList(IMS_CODEMSTRModel model)
        {
            var list = new List<IMS_CODEMSTRModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.* from [db_forminf].[dbo].[IMS_CODEMSTR] a where 1=1 ");
                    if (!string.IsNullOrEmpty(model.CODE))
                    {
                        sql += " and a.CODE = @CODE ";
                    }
                    if (!string.IsNullOrEmpty(model.VALUE))
                    {
                        sql += " and a.VALUE = @VALUE  ";
                    }
                    if (!string.IsNullOrEmpty(model.TEXT))
                    {
                        sql += " and a.TEXT = @TEXT  ";
                    }
                    sql += " ORDER BY a.code ASC ,a.ID asc";

                    list = db.Query<IMS_CODEMSTRModel>(sql, model).ToList();
                }
                return Json(list);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 新增修改页面
        /// </summary>
        /// <returns></returns>
        [App_Start.AuthFilter]
        public ActionResult AddCodeMstrVw()
        {
            return View();
        }

        /// <summary>
        /// 修改参数信息页面
        /// </summary>
        /// <returns></returns>
        [App_Start.AuthFilter]
        public ActionResult UpdateCodeMstrVw()
        {
            return View();
        }

        /// <summary>
        /// 参数信息详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult getCodeMstr(string ID)
        {
            var model = new IMS_CODEMSTRModel();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.* from [db_forminf].[dbo].[IMS_CODEMSTR] a where a.ID = @ID ");

                    model = db.Query<IMS_CODEMSTRModel>(sql, new { ID = ID }).FirstOrDefault();
                }
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        public ActionResult DeleteCodeMstr(List<IMS_CODEMSTRModel> deleteList)
        {
            try
            {
                BasicService ser = new BasicService();
                var newmodel = ser.CkPri();
                if (newmodel.DoDelete != "Y")
                {
                    return Json(new FlagTips { IsSuccess = false, Msg = "您没有删除权限" });
                }
                List<string> newarry = new List<string>();
                foreach (var d in deleteList)
                {
                    newarry.Add(d.ID);
                }
                string deletestring = string.Join(",", newarry.ToArray());
                string[] deletestr = deletestring.Split(
                           new[] { "\n", ",", "\n\r" },
                           StringSplitOptions.None
                       );
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@" DELETE FROM [db_forminf].[dbo].[IMS_CODEMSTR] WHERE ID in @deletestr");
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(sql, new { deletestr });
                    db.DoExtremeSpeedTransaction(trans);
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(new FlagTips { IsSuccess = true });
        }

        /// <summary>
        /// 新增/修改参数信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AddCodeMstr(IMS_CODEMSTRModel model)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = "";
                    //新增
                    if (!string.IsNullOrEmpty(model.ID))
                    {
                        sql = string.Format(@" update [db_forminf].[dbo].[IMS_CODEMSTR] 
                                                set  value=@value, text = @text,etd1 = @etd1, etd2 = @etd2, etd3 = @etd1, etd4 = @etd1
                                                     where ID = @ID ");
                    }
                    else
                    {
                        sql = string.Format(@" INSERT INTO [db_forminf].[dbo].[IMS_CODEMSTR] 
                                                   (code,value,text,etd1,etd2,etd3,etd4)
                                            VALUES(@code,@value,@text,@etd1,@etd2,@etd3,@etd4)");                       
                    }
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(sql, model);
                    db.DoExtremeSpeedTransaction(trans);
                }
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(new FlagTips { IsSuccess = true });
        }

        public List<IMS_CODEMSTRModel> querList(string CODE, string VALUE, string TEXT)
        {
            var list = new List<IMS_CODEMSTRModel>();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string sql = string.Format(@"SELECT a.* from [db_forminf].[dbo].[IMS_CODEMSTR] a where 1=1 ");
                    if (!string.IsNullOrEmpty(CODE))
                    {
                        sql += " and a.CODE = @CODE ";
                    }
                    if (!string.IsNullOrEmpty(VALUE))
                    {
                        sql += " and a.VALUE = @VALUE  ";
                    }
                    if (!string.IsNullOrEmpty(TEXT))
                    {
                        sql += " and a.TEXT = @TEXT  ";
                    }
                    sql += " ORDER BY a.code ASC ,a.ID asc";
                    list = db.Query<IMS_CODEMSTRModel>(sql, new { CODE, VALUE, TEXT }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        /// <summary>
        /// 导出参数信息
        /// </summary>
        /// <param name="CODE"></param>
        /// <param name="VALUE"></param>
        /// <param name="TEXT"></param>
        /// <returns></returns>
        public ActionResult CodeMstrExport(string CODE, string VALUE, string TEXT)
        {
            try
            {
                var dt = querList(CODE, VALUE, TEXT);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                string templatePath = string.Format("~\\Excel\\CodeMstr.xlsx");
                FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(templatePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Workbook wb = new Workbook(fs);
                Worksheet sheet = wb.Worksheets[0];
                sheet.Name = "参数设定";

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
                    sheet.Cells[i + 1, 0].PutValue(dt[i].CODE);
                    sheet.Cells[i + 1, 1].PutValue(dt[i].VALUE);
                    sheet.Cells[i + 1, 2].PutValue(dt[i].TEXT);
                    sheet.Cells[i + 1, 3].PutValue(dt[i].ETD1);
                    sheet.Cells[i + 1, 4].PutValue(dt[i].ETD2);
                    sheet.Cells[i + 1, 5].PutValue(dt[i].ETD3);
                    sheet.Cells[i + 1, 6].PutValue(dt[i].ETD4);
                    //  sheet.Cells[i + 1, 19].PutValue(Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd") == "0001/01/01" ? "" : Convert.ToDateTime(dt[i].CDATE).ToString("yyyy/MM/dd"));                   
                }
                MemoryStream bookStream = new MemoryStream();//创建文件流
                wb.Save(bookStream, new OoxmlSaveOptions(SaveFormat.Xlsx)); //文件写入流（向流中写入字节序列）
                bookStream.Seek(0, SeekOrigin.Begin);//输出之前调用Seek，把0位置指定为开始位置
                return File(bookStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("CodeMstr_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff")));//最后以文件形式返回
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}