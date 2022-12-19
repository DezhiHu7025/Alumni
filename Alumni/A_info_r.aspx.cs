using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
namespace Alumni
{
    public partial class A_info_r : System.Web.UI.Page
    {
        
        string strCon = "Data Source=192.168.80.247;Database=db_forminf;Uid=StudentLifeDB;Pwd=Kcisp@StudentLifeDB";
        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected int id = 0;
        public static string ceate_ip = "";
        string KmchSeqNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmployeeName"] == null)
                {
                    Response.Redirect("A_FillIn_R.aspx");
                    return;
                }
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                fillInTeacher.Text = Session["EmployeeName"].ToString();
                fillInTeacherAccount.Text = Session["EmployeeAccount"].ToString();
                this.Teacher_deptName.Text = "";
                this.oldSchool_empno.Text = "";
                this.oldSchool_name.Text = "";
                this.oldSchool_Lclass.Text = "";
                this.select_year.SelectedValue = "";
                this.oldStudent_Phone.Text = "（11位手机号码）";
                this.oldStudent_Phone.Text = "";
                //TeacherDept.Text = Session["DeptCode"].ToString();
                BindYear();
                BindMonth();
                BindDay();
                this.btn_post_R.Visible = true;
                string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (null == userHostAddress || userHostAddress == String.Empty)
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (null == userHostAddress || userHostAddress == String.Empty)
                {
                    userHostAddress = HttpContext.Current.Request.UserHostAddress;
                }
                //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
                if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
                {
                    ceate_ip = userHostAddress;
                }
                else
                {
                    ceate_ip = "127.0.0.1";
                }

            }

        }
        protected void BindYear()
        {
            select_year.Items.Clear();
            int startYear = DateTime.Now.Year;
            int currentYear = DateTime.Now.Year + 1;
            for (int i = startYear; i <= currentYear; i++)
            {
                select_year.Items.Add(new ListItem(i.ToString()));
            }
            select_year.SelectedValue = startYear.ToString();
        }
        protected void BindMonth()
        {
            select_month.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                select_month.Items.Add(i.ToString());
            }
        }
        protected void BindDay()
        {
            select_day.Items.Clear();
            string year = select_year.SelectedValue;
            string month = select_month.SelectedValue;
            int days = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));
            for (int i = 1; i <= days; i++)
            {
                select_day.Items.Add(i.ToString());
            }
        }
        protected void DownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
        }
        protected void DownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
        }

        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        protected void baoming_Click(object sender, EventArgs e)
        {


            if (Teacher_deptName.Text.Length == 0 || Teacher_deptName.Text == "")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败!教职员部门必填！');</Script>");
                return;
            }
            if (oldSchool_empno.Text.Length == 0 || oldSchool_empno.Text == "")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！校友学号必填！');</Script>");
                return;
            }
            if (oldSchool_name.Text.Length == 0 || oldSchool_name.Text == "0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！校友姓名必填！');</Script>");
                return;
            }
            if (oldSchool_Lclass.Text.Length == 0 || oldSchool_Lclass.Text == "0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！校友离校班级必填！');</Script>");
                return;
            }
            if (Convert.ToInt32(this.select_year.SelectedValue) == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请选择入校时间！');</Script>");
                return;
            }



            if (oldStudent_Phone.Text.Length == 0 || oldStudent_Phone.Text == "0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请填写校友手机号！');</Script>");
                return;
            }
            if (DoAdd())
            {
                if (DoEmail())
                {
                    Response.Write("<Script Language=JavaScript>alert('提交成功！');window.location = 'A_myOrder.aspx'</Script>");
                }

                //生成订单
                //Response.Redirect("login.aspx");

                //20211125可在此新增发送邮件功能或者给后台一个login页面能看到这个单据
            }

            else
            {

                Response.Write("<script language=javascript>alert('DoAdd新增失败！');window.location = 'A_New.aspx';</script>");

            }

        }
        protected bool DoEmail()
        {
            bool result = false;
            SqlConnection cn = new SqlConnection("Data Source=192.168.80.247;Database=Common;Uid=kcis_db;Pwd=db2008_kcis");
            cn.Open();
            string strSQL = @"Insert into [Common].[dbo].[oa_emaillog](pid,emailid ,actiontype ,toaddr,toname ,fromaddr ,fromname,subject,body
                                  , attch , remark ,createdate ) 
                                   values(@pid, @emailid , @actiontype , @toaddr, @toname , 'automail@kcisec.com' , @strSystem, @subject, @body
                                  , @attch , @remark, getdate())";



            try
            {
                //新增到表格
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                cmd.Parameters.Add("@pid", SqlDbType.NVarChar).Value = "sys_flowengin";
                cmd.Parameters.Add("@emailid", SqlDbType.NVarChar).Value = Convert.ToString(System.Guid.NewGuid());
                cmd.Parameters.Add("@actiontype", SqlDbType.NVarChar).Value = "email";
                cmd.Parameters.Add("@toaddr", SqlDbType.NVarChar).Value = "ifanchen@kcisec.com";
                cmd.Parameters.Add("@toname", SqlDbType.NVarChar).Value = "ifanchen";
                cmd.Parameters.Add("@strSystem", SqlDbType.NVarChar).Value = "线上校友专区";
                cmd.Parameters.Add("@subject", SqlDbType.NVarChar).Value = "校友入校申请";
                cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = "Please access to the website <a href='http://portal.kcistz.org.cn/Alumni/A_Login.aspx' target='_blank' >[KCIS Alumni admission Form ]</a>, and fill in it soon. Thank you! <br /><br />請您進入 <a href='http://portal.kcistz.org.cn/Alumni/A_Login.aspx' target='_blank' >[线上校友专区校友入校申请]</a> 請盡快审核，謝謝！";
                cmd.Parameters.Add("@attch", SqlDbType.NVarChar).Value = "";
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                cn.Close();//关闭数据库连接
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        protected bool DoAdd()
        {
            this.btn_post_R.Visible = false;
            KmchSeqNo = "RXD" + Utils.Nmrandom();
            bool result = false;
            string InsertStr = "";
            string InsertStr1 = "";

            try
            {
                SqlConnection con = new SqlConnection(strCon);
                con.Open();//打开数据库连接
                InsertStr = @"insert into EntryApply_indent(RmchSeqNo,form_name,teacnerName,teacherAD,DeptName,alumnusEmp,alumnusName,LeaveClass,intoDate,alumnusPhone,teacnerName2,remarks,
is_pass,is_inner,addtime)values(@RmchSeqNo,@form_name,@teacnerName,@teacherAD,@DeptName,@alumnusEmp,@alumnusName,@LeaveClass,@intoDate,@alumnusPhone,@teacnerName2,@remarks,'N','N',@addtime)";

                SqlCommand cmd = new SqlCommand(InsertStr, con);
                SqlParameter p0 = new SqlParameter("@form_name", "校友入校申请");
                SqlParameter p1 = new SqlParameter("@teacnerName", Session["EmployeeName"].ToString());
                SqlParameter p2 = new SqlParameter("@DeptName", Teacher_deptName.Text.Trim());
                SqlParameter p3 = new SqlParameter("@alumnusEmp", oldSchool_empno.Text.Trim());
                SqlParameter p4 = new SqlParameter("@alumnusName", oldSchool_name.Text.Trim());
                SqlParameter p5 = new SqlParameter("@LeaveClass", oldSchool_Lclass.Text.Trim());
                SqlParameter p6 = new SqlParameter("@intoDate", select_year.SelectedValue + "/" + select_month.SelectedValue + "/" + select_day.SelectedValue);
                SqlParameter p7 = new SqlParameter("@alumnusPhone", oldStudent_Phone.Text.Trim());
                SqlParameter p8 = new SqlParameter("@teacnerName2", otherTeacher_Name.Text.Trim());
                SqlParameter p9 = new SqlParameter("@remarks", remarks_needs.Text.Trim());
                SqlParameter p10 = new SqlParameter("@addtime", DateTime.Now);
                SqlParameter p11 = new SqlParameter("@RmchSeqNo", KmchSeqNo);
                SqlParameter p12 = new SqlParameter("@teacherAD", Session["EmployeeAccount"].ToString());
                cmd.Parameters.Add(p0);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);
                cmd.Parameters.Add(p12);
                cmd.ExecuteNonQuery();//调用Le类中的EXECCommand方法,执行SQL语句


                InsertStr1 = @"insert into OldStudent_Onlin_List(mchSeqNo,form_name,stunum,stuname,is_Mail,is_pass,EmailAdress,addtime,Phone,intoDate)
values(@mchSeqNo,@form_name,@stunum,@stuname,'N','N',@EmailAdress,@addtime,@Phone,@intoDate)";//三张表总的用来update邮件和是否同意

                SqlCommand cmd1 = new SqlCommand(InsertStr1, con);
                SqlParameter p00 = new SqlParameter("@form_name", "校友入校申请");
                SqlParameter p01 = new SqlParameter("@stunum", oldSchool_empno.Text.Trim());
                SqlParameter p02 = new SqlParameter("@stuname", oldSchool_name.Text.Trim());
                SqlParameter p03 = new SqlParameter("@intoDate", select_year.SelectedValue + "/" + select_month.SelectedValue + "/" + select_day.SelectedValue);
                SqlParameter p04 = new SqlParameter("@addtime", DateTime.Now);
                SqlParameter p05 = new SqlParameter("@Phone", oldStudent_Phone.Text.Trim());
                SqlParameter p06 = new SqlParameter("@mchSeqNo", KmchSeqNo);
                SqlParameter p07 = new SqlParameter("@EmailAdress", oldSchool_Lclass.Text.Trim());

                cmd1.Parameters.Add(p00);
                cmd1.Parameters.Add(p01);
                cmd1.Parameters.Add(p02);
                cmd1.Parameters.Add(p03);
                cmd1.Parameters.Add(p04);
                cmd1.Parameters.Add(p05);
                cmd1.Parameters.Add(p06);
                cmd1.Parameters.Add(p07);
                cmd1.ExecuteNonQuery();//调用Le类中的EXECCommand方法,执行SQL语句
                con.Close();//关闭数据库连接
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

    }
}