using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Group
{
    public class UserGroupModel
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        public string DoAdd { get; set; }

        /// <summary>
        /// 编辑
        /// </summary>
        public string DoEdit { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        public string DoSearch { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public string DoDelete { get; set; }

        /// <summary>
        /// 下载
        /// </summary>
        public string DoDownload { get; set; }

        /// <summary>
        /// 上传
        /// </summary>
        public string DoupLoad { get; set; }

        /// <summary>
        /// 审核
        /// </summary>
        public string DoSign { get; set; }
    }
}