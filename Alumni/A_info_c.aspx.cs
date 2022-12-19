using System;
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
    public partial class A_info_c : System.Web.UI.Page
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
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                this.text_stu_empno.Text = "";
                this.text_stu_name.Text = "";
                //this.txt_IDcard.Text = "";
                //this.txt_IDcard.SelectedValue = "0";
                //this.txt_gread.Text = "";
                this.txt_passportEname.Text = "";
                this.txt_reportCard.SelectedValue = "0";
                this.txt_yyyy.SelectedValue = "0";
                this.txt_mm.SelectedValue = "0";
                this.txt_UseFor.SelectedValue = "0";
                this.txt_Copies.SelectedValue = "0";
                this.txt_takeWay.SelectedValue = "0";
                this.txt_SendAdress.Text = "";
                this.txt_Cphone.Text = "（11位手机号码）";
               

                this.btn_post.Visible = true;

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
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        protected void SelectFirstSort_Change(object sender, EventArgs e)
        {
            if (txt_reportCard.SelectedValue == "正式") 
             {
                 
                 //Response.Write("<Script Language=JavaScript>alert('xxxxxxx！');</Script>");
                 //return;
             } 
        } 

        protected void baoming_Click(object sender, EventArgs e)
        {


            if (text_stu_empno.Text.Length == 0 || text_stu_empno.Text == "")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败!学号必填！');</Script>");
                return;
            }
            if (text_stu_name.Text.Length == 0 || text_stu_name.Text == "")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！姓名必填！');</Script>");
                return;
            }
            if (txt_reportCard.Text.Length == 0 || txt_reportCard.Text == "0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请选择成绩单类型！');</Script>");
                return;
            }
            if (this.txt_yyyy.SelectedValue =="0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请选择申请年份！');</Script>");
                return;
            }
            if (this.txt_mm.SelectedValue == "0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请选择申请学期！');</Script>");
                return;
            }
            if (txt_UseFor.Text.Length == 0 || txt_UseFor.Text == "0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请选择申请用途！');</Script>");
                return;
            }
            if (Convert.ToInt32(this.txt_Copies.SelectedValue) == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请选择申请份数！');</Script>");
                return;
            }
            if (txt_takeWay.Text.Length == 0 || txt_takeWay.Text == "0")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请选择收取方式！');</Script>");
                return;
            }
            if (txt_SendAdress.Text.Length == 0 || txt_SendAdress.Text == "")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！地址必填！');</Script>");
                return;
            }
            if (txt_Cphone.Text.Length == 0 || txt_Cphone.Text == "（11位手机号码）")
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！请填写手机号码！');</Script>");
                return;
            }
            if (txt_Cphone.Text.Length != 11)
            {
                Response.Write("<Script Language=JavaScript>alert('新增失败！手机号码应为11位！');</Script>");
                return;
            }

            if (DoAdd())
            {
                if (DoEmail())
                {
                    Response.Write("<Script Language=JavaScript>alert('提交成功！');window.location = 'A_myOrder.aspx'</Script>");
                }
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
                cmd.Parameters.Add("@toaddr", SqlDbType.NVarChar).Value = "scherrie_wang@kcisec.com";
                cmd.Parameters.Add("@toname", SqlDbType.NVarChar).Value = "scherrie_wang";
                cmd.Parameters.Add("@strSystem", SqlDbType.NVarChar).Value = "线上校友专区";
                cmd.Parameters.Add("@subject", SqlDbType.NVarChar).Value = "成绩单申请单据";
                cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = "Please access to the website <a href='http://portal.kcistz.org.cn/Alumni/A_Login.aspx' target='_blank' >[KCIS Score Application Form ]</a>, and fill in it soon. Thank you! <br /><br />請您進入 <a href='http://portal.kcistz.org.cn/Alumni/A_Login.aspx' target='_blank' >[线上校友专区成绩单]</a> 請盡快审核，謝謝！";
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
            this.btn_post.Visible = false;
            KmchSeqNo = "CJD" + Utils.Nmrandom();
            bool result = false;
            string InsertStr = "";
            string InsertStr1 = "";

            try
            {
                SqlConnection con = new SqlConnection(strCon);
                con.Open();//打开数据库连接
                InsertStr = @"insert into Achievement_indent(CmchSeqNo,form_name,stu_empno,stu_name,passportEname,reportCard,txt_yyyy,txt_mm,UseFor,Copies,takeWay,
SendAdress,Cphone,is_pass,Is_inner,addtime)values(@CmchSeqNo,@form_name,@stu_empno,@stu_name,@passportEname,@reportCard,@txt_yyyy,@txt_mm,@UseFor,@Copies,@takeWay,@SendAdress,@Cphone,'N','N',@addtime)";

                SqlCommand cmd = new SqlCommand(InsertStr, con);
                SqlParameter p0 = new SqlParameter("@form_name", "成绩单申请");
                SqlParameter p1 = new SqlParameter("@stu_empno", text_stu_empno.Text.Trim());
                SqlParameter p2 = new SqlParameter("@stu_name", text_stu_name.Text.Trim());
                SqlParameter p3 = new SqlParameter("@passportEname", txt_passportEname.Text.Trim());
                SqlParameter p4 = new SqlParameter("@reportCard", txt_reportCard.Text.Trim());
                SqlParameter p5 = new SqlParameter("@txt_yyyy", txt_yyyy.Text.Trim());
                SqlParameter p6 = new SqlParameter("@txt_mm", txt_mm.Text.Trim());
                SqlParameter p7 = new SqlParameter("@UseFor", txt_UseFor.Text.Trim());
                SqlParameter p8 = new SqlParameter("@Copies", txt_Copies.Text.Trim());
                SqlParameter p9 = new SqlParameter("@takeWay", txt_takeWay.Text.Trim());
                SqlParameter p10 = new SqlParameter("@SendAdress", txt_SendAdress.Text.Trim());
                SqlParameter p11 = new SqlParameter("@Cphone", txt_Cphone.Text.Trim());
                SqlParameter p12 = new SqlParameter("@addtime", DateTime.Now);
                SqlParameter p13 = new SqlParameter("@CmchSeqNo", KmchSeqNo);

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
                cmd.Parameters.Add(p13);
                cmd.ExecuteNonQuery();//调用Le类中的EXECCommand方法,执行SQL语句


                InsertStr1 = @"insert into OldStudent_Onlin_List(mchSeqNo,form_name,stunum,stuname,is_Mail,is_pass,EmailAdress,addtime,Phone)
values(@mchSeqNo,@form_name,@stunum,@stuname,'N','N',@EmailAdress,@addtime,@Phone)";//三张表总的用来update邮件和是否同意

                SqlCommand cmd1 = new SqlCommand(InsertStr1, con);
                SqlParameter p00 = new SqlParameter("@form_name", "成绩单申请");
                SqlParameter p01 = new SqlParameter("@stunum", text_stu_empno.Text.Trim());
                SqlParameter p02 = new SqlParameter("@stuname", text_stu_name.Text.Trim());
                SqlParameter p03 = new SqlParameter("@EmailAdress", txt_SendAdress.Text.Trim());
                SqlParameter p04 = new SqlParameter("@addtime", DateTime.Now);
                SqlParameter p05 = new SqlParameter("@Phone", txt_Cphone.Text.Trim());
                SqlParameter p06 = new SqlParameter("@mchSeqNo", KmchSeqNo);

                cmd1.Parameters.Add(p00);
                cmd1.Parameters.Add(p01);
                cmd1.Parameters.Add(p02);
                cmd1.Parameters.Add(p03);
                cmd1.Parameters.Add(p04);
                cmd1.Parameters.Add(p05);
                cmd1.Parameters.Add(p06);
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