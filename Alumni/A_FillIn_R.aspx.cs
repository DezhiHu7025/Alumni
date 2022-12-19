using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using KCIS_Biz;

namespace Alumni
{
    public partial class A_FillIn_R : System.Web.UI.Page
    {

        Employee Emp = new Employee();
        ADOperation ADOp = new ADOperation();
        DBOperation DBOp = new DBOperation();
        BasicOperation BOp = new BasicOperation();
        Scripts Script = new Scripts();
        protected int id = 0;
        string sQueryString, DBName;

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (Server.MachineName.ToUpper().IndexOf("QS-MISWB01") == -1)
            {
                Page.Title = "康桥学校 - 教职员系统";
                lblDEMO.Visible = false;
            }
            else
            {
                Page.Title = "康桥学校 - 教职员系统 (Demo Site)";
                lblDEMO.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                txtAccount.Attributes["onkeydown"] = "SkipNext();";

                //看Cookie中是否有之前輸入過的Account, 有的話就自動帶出
                if (Request.Cookies["myAccount"] != null)
                {
                    txtAccount.Text = Request.Cookies["myAccount"].Value;
                    txtPassword.Focus();
                    return;
                }
            }
            txtAccount.Focus();
        }


        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 登入處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            bool LoginSuccess = false;
            string Account = txtAccount.Text.Trim().ToLower();
            string Password = txtPassword.Text.Trim();
            string Domain = DropDownListDomain.SelectedValue;
            string DeptCode;
            if (Account == String.Empty)
            {
                LiteralResult.Text = "<span style=\"color:Red; font-size:13px;\">【登录失败】「帐号」栏未输入！</span>";
                return;
            }
            else
            {
                try
                {
                    if (ADOp.checkAccount(Domain, Account, Password))
                    {
                        Emp = (Employee)ADOp.getEmp(Domain, Account, Password);

                        ////寫入登錄紀錄
                        //sQueryString = "EXEC [LO_insert_Alumni] '" + Emp.ID + "',N'" + Emp.Name + "','" + Request.UserHostAddress + "'";
                        //DBName = "db_forminf";
                        //DBOp.GetCommand(sQueryString, DBName);
                        Session["Employee"] = Emp;
                        Session["EmployeeName"] = Emp.Name;
                        Session["EmployeeAccount"] = Emp.Account;
                        Session["DeptCode"] = Emp.DeptCode;
                        LoginSuccess = true;
                    }
                    else
                    {
                        LiteralResult.Text = "<span style=\"color:Red; font-size:13px;\">【登录失败】「帐号」或「密码」错误！</span><br /><span style=\"color:Blue; font-size:12px;\">(＊请使用登录学校电脑的帐号及密码登入)</span>";
                    }
                }
                catch (Exception ex)
                {
                    //LiteralResult.Text = "<span style=\"color:Red; font-size:13px;\">【系统错误】资料库异常或连线异常！</span>";
                    LiteralResult.Text = ex.Message;
                }
                finally
                {
                    if (LoginSuccess)
                    {
                        //把帳號及校部資訊寫入Cookie
                        Response.Cookies["myAccount"].Value = Account;
                        Response.Cookies["myAccount"].Expires = DateTime.Now.AddMonths(1);
                        
                        Response.Redirect("A_info_r.aspx?id=1");//传入id为1
                       

                    }
                }
            }
        }
    }
}