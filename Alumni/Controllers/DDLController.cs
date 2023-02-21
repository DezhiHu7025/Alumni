using Alumni.Db;
using Alumni.Models;
using Alumni.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Controllers
{
    public class DDLController : Controller
    {
        /// <summary>
        /// 证件类型
        /// </summary>
        /// <returns></returns>
        public ActionResult getIDCard(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("IDCard", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getISPASS(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("IS_PASS", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getAuditState(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("AuditState", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 成绩单类型
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getReportCard(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("ReportCard", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 申请学年
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getTxtyyyy(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("txt_yyyy", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 申请学期
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getTxtmm(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("txt_mm", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 申请用途
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getUseFor(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("txt_UseFor", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 申请份数
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getCopies(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("txt_Copies", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 收取成绩单方式
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult gettakeWay(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("txt_takeWay", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否在G12从康桥毕业
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getGraduationStatus(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("GraduationStatus", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 就读院校
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getCollege(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("College", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否愿意加入康桥校友会
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getWillJoin(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("WillJoin", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 康桥毕业年份
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getGraduationYear(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("GraduationYear", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 目前发展状况
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getCurrentDevelopment(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("CurrentDevelopment", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 最高学历状态
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getHighestEducation(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("HighestEducation", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 最高学历状态
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getHighestEducationStatus(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("HighestEducationStatus", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否曾经转学
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getTransferred(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("Transferred", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 表单名称
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getFormName(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("Form_Name", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 群组名称
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult getGroupName(string keyWord)
        {
            SchoolDb db = new SchoolDb();
            CommonService cms = new CommonService();
            var list = cms.GetIMSCodeMstr("GroupName", keyWord);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}