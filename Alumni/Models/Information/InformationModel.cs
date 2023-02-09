using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alumni.Models.Information
{
    public class InformationModel
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string InchSeqNo { get; set; }

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
        /// 是否在G12从康桥毕业
        /// </summary>
        public string GraduationStatus { get; set; }

        /// <summary>
        /// 就读院校
        /// </summary>
        public string College { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 微信账号
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string NewPhone { get; set; }

        /// <summary>
        /// 是否愿意加入康桥校友会
        /// </summary>
        public string WillJoin { get; set; }

        /// <summary>
        /// 近照
        /// </summary>
        public string LatestPhoto { get; set; }

        /// <summary>
        /// 填写时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string CreateTime2 { get; set; }

    }
}