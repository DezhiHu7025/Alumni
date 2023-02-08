using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Information;
using Alumni.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class InformationController : Controller
    {
        // GET: Information
        [App_Start.AuthFilter]
        public ActionResult InformationIndex()
        {
            return View();
        }

        public ActionResult InformationSave(InformationModel model)
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
                    model.InchSeqNo = "Info" + Utils.Nmrandom();
                    model.CreateTime = DateTime.Now;
                    model.Form_Name = "问卷调查";
                    string insertSql = string.Format(@" insert into [db_forminf].[dbo].[ContactInformation](InchSeqNo,Form_Name,Stu_Empno,Stu_Name,Stu_Eame,GraduationStatus,College,Email,WeChat,                                              
                                                      NewPhone,WillJoin,LatestPhoto,CreateTime)
                                                    values(@InchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,@Stu_Eame,@GraduationStatus,@College,@Email,@WeChat,
                                                      @NewPhone,@WillJoin,@LatestPhoto,@CreateTime)");
                    FileUploadService upfile = new FileUploadService();
                    var uploadModel = upfile.FileLoad(httpPostedFileBase,model.Stu_Empno);
                    if (uploadModel.IsSuccess == true)
                    {
                        model.LatestPhoto = uploadModel.Msg;
                    }
                    else
                    {
                        return Json(new FlagTips { IsSuccess = false,Msg = "照片上传失败 Photo upload failed" });
                    }
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(insertSql, model);
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