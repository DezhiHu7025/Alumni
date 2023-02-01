using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Totaldata
{
    public class TotaldataModel
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string mchSeqNo { get; set; }

        /// <summary>
        /// 单据名称
        /// </summary>
        public string Form_Name { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string IS_PASS { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string Stu_Empno { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Stu_Name { get; set; }

        /// <summary>
        /// 邮件/邮箱地址
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public string AddTime { get; set; }
        
    }
}