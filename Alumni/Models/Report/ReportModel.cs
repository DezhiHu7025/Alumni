using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Report
{
    public class ReportModel
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string CmchSeqNo { get; set; }

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
        /// 护照英文名
        /// </summary>
        public string PassportEname { get; set; }

        /// <summary>
        /// 成绩单类型
        /// </summary>
        public string ReportCard { get; set; }

        /// <summary>
        /// 申请学年/学期
        /// </summary>
        public string txt_yyyy { get; set; }

        /// <summary>
        /// 申请学年/学期
        /// </summary>
        public string txt_mm { get; set; }

        /// <summary>
        /// 申请用途
        /// </summary>
        public string txt_UseFor { get; set; }

        /// <summary>
        /// 申请份数
        /// </summary>
        public string txt_Copies { get; set; }

        /// <summary>
        /// 收取成绩单方式
        /// </summary>
        public string txt_takeWay { get; set; }

        /// <summary>
        /// 邮寄/邮箱地址
        /// </summary>
        public string txt_SendAdress { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string txt_Cphone { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime timenow { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string IS_PASS { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public string AddTime { get; set; }
    }
}