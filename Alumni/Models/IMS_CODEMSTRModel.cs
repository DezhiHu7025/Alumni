using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models
{
    public class IMS_CODEMSTRModel
    {

        /// <summary>
        /// 唯一编号
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public string VALUE { get; set; }

        /// <summary>
        /// text
        /// </summary>
        public string TEXT { get; set; }

        /// <summary>
        /// 扩展代码
        /// </summary>
        public string ETD1 { get; set; }

        /// <summary>
        /// 扩展代码
        /// </summary>
        public string ETD2 { get; set; }

        /// <summary>
        /// 扩展代码
        /// </summary>
        public string ETD3 { get; set; }

        /// <summary>
        /// 扩展代码
        /// </summary>
        public string ETD4 { get; set; }
    }
}