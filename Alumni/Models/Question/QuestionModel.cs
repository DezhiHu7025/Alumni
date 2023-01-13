using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Question
{
    public class QuestionModel
    {
        /// <summary>
        /// 问卷流水号
        /// </summary>
        public string QchSeqNo { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string Form_Name { get; set; }

        /// <summary>
        /// 康桥学号
        /// </summary>
        public string Stu_Empno { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string Stu_Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string Stu_Eame { get; set; }

        /// <summary>
        /// 康桥毕业年份
        /// </summary>
        public string GraduationYear { get; set; }

        /// <summary>
        /// 目前发展状况
        /// </summary>
        public string CurrentDevelopment { get; set; }

        /// <summary>
        /// 目前发展状况其他说明
        /// </summary>
        public string CurrentDevelopmentText { get; set; }

        /// <summary>
        /// 工作单位名称
        /// </summary>
        public string WorkUnitName { get; set; }

        /// <summary>
        /// 工作职称
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// 内容概述
        /// </summary>
        public string ContentOverview { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public string HighestEducation { get; set; }

        /// <summary>
        /// 最高学历状态
        /// </summary>
        public string HighestEducationStatus { get; set; }

        /// <summary>
        /// 最高学历状态其他说明
        /// </summary>
        public string HighestEducationText { get; set; }

        /// <summary>
        /// 大学名称
        /// </summary>
        public string UniversityName { get; set; }

        /// <summary>
        /// 大学科系名称
        /// </summary>
        public string UniversityDepartmentName { get; set; }

        /// <summary>
        /// 研究所名称
        /// </summary>
        public string InstituteName { get; set; }

        /// <summary>
        /// 研究所科系名称
        /// </summary>
        public string InstituteDepartmentName { get; set; }

        /// <summary>
        /// 博士班名称
        /// </summary>
        public string DoctoralClassName { get; set; }

        /// <summary>
        /// 博士班科系名称
        /// </summary>
        public string DoctoralDepartmentName { get; set; }

        /// <summary>
        /// 是否曾经转学
        /// </summary>
        public string Transferred { get; set; }

        /// <summary>
        /// 是否曾经转学 就读说明
        /// </summary>
        public string TransferredText { get; set; }

        /// <summary>
        /// 其他补充
        /// </summary>
        public string OtherSupplements { get; set; }

        /// <summary>
        /// 填写问卷时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}