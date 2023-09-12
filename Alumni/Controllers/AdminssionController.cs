using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Adminssion;
using Alumni.Models.Bill;
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
    public class AdminssionController : Controller
    {
        /// <summary>
        /// 入校申请
        /// </summary>
        /// <returns></returns>
        [App_Start.AuthFilter]
        public ActionResult InstructionsForAdminission()
        {
            return View();
        }

        [App_Start.AuthFilter]
        public ActionResult TotalAdminissionIndex()
        {
            return View();
        }

        [App_Start.AuthFilter]
        public ActionResult AdminssionApply()
        {
            return View();
        }

        public ActionResult AdminissionSave(AdminssionModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.teacherAD) || string.IsNullOrEmpty(model.teacnerName) || string.IsNullOrEmpty(model.DeptName))
                {
                    return Json(new FlagTips
                    {
                        IsSuccess = false,
                        Msg = "账号信息获取异常，请检查登录状态。Account information acquisition exception, please check login status"
                    });
                }
                DateTime today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                if (model.intoDate < today)
                {
                    string tipday = DateTime.Now.ToString("yyyy/MM/dd");
                    return Json(new FlagTips { IsSuccess = false, Msg = string.Format("校友入校日期不得早于当前日期{0}", tipday) });
                }
                using (SchoolDb db = new SchoolDb())
                {
                    string checkSql = string.Format(@" SELECT COUNT(*)
                                                   FROM [db_forminf].[dbo].[OldStudent_Onlin_List] a
                                                       LEFT JOIN [db_forminf].[dbo].[OldStudentOnlin] b
                                                           ON a.form_name = b.form_name
                                                   WHERE b.shopForm_id = 'S0000001' and b.form_id = 'P0000001' and is_pass = 'N'
                                                          AND  DATEDIFF (day, convert(nvarchar(10),getdate(),120), convert(date, a.intoDate))  >= 0
                                                         AND (a.stunum = @alumnusEmp OR a.Phone = @alumnusPhone) ");
                    string cnt = db.Query<int>(checkSql, model).FirstOrDefault().ToString();
                    if (Convert.ToInt32(cnt) > 0)
                    {
                        return Json(new FlagTips
                        {
                            IsSuccess = false,
                            Msg = "请勿重复递交！若长时间未审核通过，请洽询相关老师。The application for transfer out/reading certificate has been submitted, please wait patiently for review; If you fail to pass the review for a long time, please contact the relevant teachers."
                        });
                    }

                    model.intoDate2 = Convert.ToDateTime(model.intoDate).ToShortDateString();
                    model.RmchSeqNo = "RXD" + Utils.Nmrandom();
                    model.AddTime = DateTime.Now;
                    model.Form_Name = "校友入校申请";
                    string insertSql = string.Format(@" insert into [db_forminf].[dbo].[EntryApply_indent](RmchSeqNo,form_name,teacnerName,teacherAD,DeptName,alumnusEmp,alumnusName,LeaveClass,intoDate,alumnusPhone,teacnerName2,remarks,is_pass,is_inner,addtime)
                                                         values(@RmchSeqNo,@form_name,@teacnerName,@teacherAD,@DeptName,@alumnusEmp,@alumnusName,@LeaveClass,@intoDate2,@alumnusPhone,@teacnerName2,@remarks,'N','N',@addtime)");
                    string insertSql2 = string.Format(@"insert into [db_forminf].[dbo].[OldStudent_Onlin_List](mchSeqNo,form_name,stunum,stuname,is_Mail,is_pass,EmailAdress,addtime,Phone,intodate)
                                                       values(@RmchSeqNo,@Form_Name,@alumnusEmp,@alumnusName,'N','N',@LeaveClass,@AddTime,@alumnusPhone,@intoDate2)");
                    SendMailService sendMailService = new SendMailService();
                    bool mailFlag = sendMailService.doMail(model.Form_Name);
                    if (mailFlag)
                    {
                        Dictionary<string, object> trans = new Dictionary<string, object>();
                        trans.Add(insertSql, model);
                        trans.Add(insertSql2, model);
                        db.DoExtremeSpeedTransaction(trans);
                    }
                    else
                    {
                        return Json(new FlagTips { IsSuccess = false, Msg = "邮件发送失败，提交失败 Mail sending failed, submission failed" });
                    }

                }
                return Json(new FlagTips { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 入校申请信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult GetAdminssionData(queryBillModel model)
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
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
            return Json(list);
        }

        public List<AdminssionModel> querList(string Stu_Empno, string IS_PASS)
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
                    if (!string.IsNullOrEmpty(Stu_Empno))
                    {
                        sql += " and (a.alumnusEmp = @Stu_Empno or a.alumnusPhone = @Stu_Empno )";
                    }
                    if (!string.IsNullOrEmpty(IS_PASS))
                    {
                        sql += " and a.is_pass = @IS_PASS ";
                    }
                    sql += " ORDER BY a.addtime DESC ";

                    list = db.Query<AdminssionModel>(sql, new { Stu_Empno, IS_PASS }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        /// <summary>
        /// 入校申请信息下载
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AdminissionExport(string Stu_Empno, string IS_PASS)
        {
            try
            {
                var dt = querList(Stu_Empno, IS_PASS);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
                MemoryStream bookStream = new MemoryStream();//创建文件流
                wb.Save(bookStream, new OoxmlSaveOptions(SaveFormat.Xlsx)); //文件写入流（向流中写入字节序列）
                bookStream.Seek(0, SeekOrigin.Begin);//输出之前调用Seek，把0位置指定为开始位置
                return File(bookStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("Adminission_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff")));//最后以文件形式返回
            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        [App_Start.AuthFilter]
        public ActionResult SignAdminissionVw()
        {
            return View();
        }

        /// <summary>
        /// 入校申请具体信息
        /// </summary>
        /// <param name="CmchSeqNo"></param>
        /// <returns></returns>
        public ActionResult GetAdminssion(string RmchSeqNo)
        {
            AdminssionModel model = new AdminssionModel();
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
WHERE b.shopForm_id = 'S0000001' and a.RmchSeqNo = @RmchSeqNo ");

                    model = db.Query<AdminssionModel>(sql, new { RmchSeqNo }).FirstOrDefault();
                }
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 审核入校申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SignAdminssion(AdminssionModel model)
        {
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    DateTime now = DateTime.Now;
                    RecordModel record = new RecordModel();
                    record.GUID = Guid.NewGuid().ToString();
                    record.ChSeqNo = model.RmchSeqNo;
                    record.Form_Name = model.Form_Name;

                    HttpCookie cookie = Request.Cookies["fullname"];
                    record.Signer = HttpUtility.UrlDecode(cookie.Value);
                    record.SignTime = now;
                    record.Comments = model.Comments;
                    record.IS_PASS = model.SignStatus;

                    model.IS_PASS = model.SignStatus;
                    DateTime today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    if (model.intoDate < today && model.IS_PASS == "Y")
                    {
                        return Json(new FlagTips { IsSuccess = false, Msg = string.Format("入校日期已过期，不可审核通过") });
                    }

                    string sql1 = @"update  [db_forminf].[dbo].[EntryApply_indent] set is_pass =@IS_PASS where form_name=@Form_Name and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N' and RmchSeqNo =@RmchSeqNo ";
                    string sql2 = @"update  [db_forminf].[dbo].[OldStudent_Onlin_List] set is_pass =@IS_PASS where form_name=@Form_Name and  is_pass = 'N'  and mchSeqNo =@RmchSeqNo ";
                    string sqlRecord = @"INSERT INTO  [db_forminf].[dbo].[Record] ([GUID],[ChSeqNo],[Form_Name],[Signer],[SignTime],[Comments],[IS_PASS])
                                           VALUES(@GUID,@ChSeqNo,@Form_Name,@Signer,@SignTime,@Comments,@IS_PASS);";

                    //发送邮件
                    string mailSql = string.Format(@"Insert into [Common].[dbo].[oa_emaillog](pid,emailid ,actiontype ,toaddr,toname ,fromaddr ,fromname,subject,body
                                  ,createdate ) 
                                   values(@pid, @emailid , @actiontype , @toaddr, @toname , 'automail@kcisec.com' , @strSystem, @subject, @body
                                  ,getdate())");//
                    EmailModel emailModel = new EmailModel();
                    var newModel = db.Query<AdminssionModel>("select a.*,a.intoDate intoDate2 from [db_forminf].[dbo].[EntryApply_indent] a where a.form_name=@Form_Name and a.RmchSeqNo =@RmchSeqNo", new { model.Form_Name,model.RmchSeqNo }).FirstOrDefault();
                    string strBody = newModel.teacnerName + "老师您好：您提交的入校申请单，详情如下：<br /><br />";
                    strBody += "单据编号：" + newModel.RmchSeqNo + "<br /><br />";
                    strBody += "校友学号：" + newModel.alumnusEmp + "<br /><br />";
                    strBody += "校友姓名：" + newModel.alumnusName + "<br /><br />";
                    strBody += "校友电话：" + newModel.alumnusPhone + "<br /><br />";
                    strBody += "校友离校班级：" + newModel.LeaveClass + "<br /><br />";
                    strBody += "校友入校日期：" + newModel.intoDate2 + "<br /><br />";
                    if (model.IS_PASS == "Y")
                    {
                        strBody += "该校友入校申请已审核通过";
                    }
                    else if (model.IS_PASS == "D")
                    {
                        strBody += "该校友入校申请审核不通过<br /><br />原因：" + model.Comments;
                    }
                    else
                    {
                        strBody += "该校友入校申请" + model.IS_PASS;
                    }
                    emailModel.pid = "sys_flowengin";
                    emailModel.emailid = Convert.ToString(System.Guid.NewGuid());
                    emailModel.actiontype = "email";
                    emailModel.toaddr = newModel.teacherAD + "@kcisec.com";
                    emailModel.toname = newModel.teacnerName;
                    emailModel.strSystem = "线上校友专区";
                    emailModel.subject = "校友入校申请";
                    emailModel.body = strBody;
                    
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(sql1, model);
                    trans.Add(sql2, model);
                    trans.Add(sqlRecord, record);
                    trans.Add(mailSql, emailModel);
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