using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models.Manager
{
    public class PermissionModel
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public string GroupId {get; set; }
        
        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 菜单标签html语句
        /// </summary>
        public string LiMsg { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DoAdd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DoEdit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DoSearch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DoDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DoDownload { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DoUpload { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DoSign{ get; set; }
    }
}