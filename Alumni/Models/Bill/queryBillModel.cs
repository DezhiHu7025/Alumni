using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Bill
{
    public class queryBillModel
    {
        /// <summary>
        /// 康桥学号
        /// </summary>
        public string Stu_Empno { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string Form_Name  { get;set;}

        /// <summary>
        /// 表单状态
        /// </summary>
        public string IS_PASS { get; set; }
    }
}