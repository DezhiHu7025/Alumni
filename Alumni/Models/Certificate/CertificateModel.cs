using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Certificate
{
    public class CertificateModel
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string KmchSeqNo { get; set; }

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
        /// 证件类型
        /// </summary>
        public string IDCard { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string IDcard_Number { get; set; }

        /// <summary>
        /// 护照英文名
        /// </summary>
        public string PassportEname { get; set; }

        /// <summary>
        /// 户籍地
        /// </summary>
        public string Hcountry { get; set; }

        /// <summary>
        /// 邮寄地址
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string NewPhone { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime timenow { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public string AddTime { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string IS_PASS { get; set; }
    }
}