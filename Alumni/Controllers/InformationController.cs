using Alumni.Db;
using Alumni.Models;
using Alumni.Models.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class InformationController : Controller
    {
        // GET: Information
        public ActionResult InformationIndex()
        {
            return View();
        }

        public ActionResult InformationSave(InformationModel model)
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
                    model.InchSeqNo = "Info" + Utils.Nmrandom();
                    model.CreateTime = DateTime.Now;
                    model.Form_Name = "问卷调查";
                    string insertSql = string.Format(@" insert into [db_forminf].[dbo].[ContactInformation](InchSeqNo,Form_Name,Stu_Empno,Stu_Name,Stu_Eame,GraduationStatus,College,Email,WeChat,                                              
                                                      NewPhone,WillJoin,LatestPhoto,CreateTime)
                                                    values(@InchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,@Stu_Eame,@GraduationStatus,@College,@Email,@WeChat,
                                                      @NewPhone,@WillJoin,@LatestPhoto,@CreateTime)");
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

        public HttpPostedFileBase PictureSave(HttpPostedFileBase picture)
        {
            try
            {
                if (picture != null && picture.ContentLength > 0)
                {
                    if (!System.IO.Directory.Exists(Server.MapPath("~/picture")))
                    {
                        //无文件夹则先创建文件夹
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/picture"));
                    }
                    //获取扩展名
                    string img = System.IO.Path.GetExtension(picture.FileName);
                    //文件名 加时间防止重名
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + img;
                    //保存路径
                    string filePath = Server.MapPath("~/picture") + fileName;
                    picture.SaveAs(filePath);
                }
                return picture;
                //return Json(new FlagTips { IsSuccess = true });
            }
            catch (Exception ex)
            {
                return picture;
               // return Json(new FlagTips { IsSuccess = false, Msg = ex.Message });
            }
        }
    }
}