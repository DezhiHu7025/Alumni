using Alumni.Db;
using Alumni.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alumni.Models;

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
                                                               InstituteName,InstituteDepartmentName,DoctoralClassName,DoctoralDepartmentName,Transferred,TransferredText,OtherSupplements,CreateTime)
                                                                VALUES(@QchSeqNo,@Form_Name,@Stu_Empno,@Stu_Name,@Stu_Eame,@GraduationYear,@CurrentDevelopment,@CurrentDevelopmentText,@WorkUnitName,
                                                               @JobTitle,@ContentOverview,@HighestEducation,@HighestEducationStatus,@HighestEducationText,@UniversityName,@UniversityDepartmentName,
                                                               @InstituteName,@InstituteDepartmentName,@DoctoralClassName,@DoctoralDepartmentName,@Transferred,@TransferredText,@OtherSupplements,@CreateTime)");
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
    }
}