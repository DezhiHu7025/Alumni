using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Adminssion
{
    public class AdminssionModel
    {
        /// <summary>
        /// 流水号
        /// </summary>

        public string RmchSeqNo { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>

        public string Form_Name { get; set; }

        /// <summary>
        /// 教职员姓名
        /// </summary>

        public string teacnerName { get; set; }

        /// <summary>
        /// 教职员部门
        /// </summary>

        public string DeptName { get; set; }

        /// <summary>
        /// teacherAD
        /// </summary>

        public string teacherAD { get; set; }

        /// <summary>
        /// 校友学号
        /// </summary>

        public string alumnusEmp { get; set; }

        /// <summary>
        /// 校友姓名
        /// </summary>

        public string alumnusName { get; set; }

        /// <summary>
        /// 校友离校班级
        /// </summary>

        public string LeaveClass { get; set; }

        /// <summary>
        /// 校友入校日期
        /// </summary>

        public string intoDate2 { get; set; }

        /// <summary>
        /// 校友入校日期
        /// </summary>

        public DateTime intoDate { get; set; }

        /// <summary>
        /// 校友电话
        /// </summary>

        public string alumnusPhone { get; set; }

        /// <summary>
        /// 其他拜访师长姓名
        /// </summary>

        public string teacnerName2 { get; set; }

        /// <summary>
        /// 其他需求或备注
        /// </summary>

        public string remarks { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>

        public DateTime AddTime { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>

        public string AddTime2 { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string IS_PASS { get; set; }


        /// <summary>
        /// 审核意见
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Signer { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime SignTime { get; set; }

        /// <summary>
        /// 选择审核状态
        /// </summary>
        public string SignStatus { get; set; }

    }

}