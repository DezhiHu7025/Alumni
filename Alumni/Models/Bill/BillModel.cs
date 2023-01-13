using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Bill
{
    public class BillModel
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string SeqNo { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string Form_Name { get; set; }

        /// <summary>
        /// 邮寄/邮箱 地址
        /// </summary>
        public string SendAdress { get; set; }

        /// <summary>
        /// 表单状态
        /// </summary>
        public string IS_PASS { get; set; }

        /// <summary>
        /// 入校时间
        /// </summary>
        public DateTime intoDate { get; set; }

        /// <summary>
        /// 第二行数据标题
        /// </summary>
        public string TitleName { get; set; }
    }
}