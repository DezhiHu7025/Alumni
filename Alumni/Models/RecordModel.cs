using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models
{
    public class RecordModel
    {
        /// <summary>
        /// 记录流水号
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// 表单流水号
        /// </summary>
        public string ChSeqNo { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string Form_Name { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Signer { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime SignTime { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string IS_PASS { get; set; }
    }
}