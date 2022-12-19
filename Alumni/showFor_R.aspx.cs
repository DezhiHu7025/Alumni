using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;
using System.Threading;

namespace Alumni
{
    public partial class showFor_R : System.Web.UI.Page
    {
        string strCon = "Data Source=192.168.80.247;Database=db_forminf;Uid=StudentLifeDB;Pwd=Kcisp@StudentLifeDB";
        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                GetFormList(id);
                RadioButtonY.Checked = false;
                RadioButtonN.Checked = false;
                this.btn_post.Visible = true;
            }
        }
        protected void GetFormList(int ida)
        {

            string form_name, teacner_Name, Teacher_Dept, old_empno, old_Name, old_Class, intoDate, old_Phone, teacner_Name2, remarks, addtime, is_pass;
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[EntryApply_indent] where form_name='校友入校申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and id =" + ida;
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "EntryApply_indent");
            if (myViewDate.Tables[0].Rows.Count > 0)
            {
                 form_name = myViewDate.Tables[0].Rows[0]["form_name"].ToString().Trim();
                 teacner_Name = myViewDate.Tables[0].Rows[0]["teacnerName"].ToString().Trim();
                 Teacher_Dept = myViewDate.Tables[0].Rows[0]["DeptName"].ToString().Trim();
                 old_empno = myViewDate.Tables[0].Rows[0]["alumnusEmp"].ToString().Trim();
                 old_Name = myViewDate.Tables[0].Rows[0]["alumnusName"].ToString().Trim();
                 old_Class = myViewDate.Tables[0].Rows[0]["LeaveClass"].ToString().Trim();
                 intoDate = myViewDate.Tables[0].Rows[0]["intoDate"].ToString().Trim();
                 old_Phone = myViewDate.Tables[0].Rows[0]["alumnusPhone"].ToString().Trim();
                 teacner_Name2 = myViewDate.Tables[0].Rows[0]["teacnerName2"].ToString().Trim();
                 remarks = myViewDate.Tables[0].Rows[0]["remarks"].ToString().Trim();
                 addtime = myViewDate.Tables[0].Rows[0]["addtime"].ToString().Trim();
                 is_pass = myViewDate.Tables[0].Rows[0]["is_pass"].ToString().Trim();


                id = Convert.ToInt32(myViewDate.Tables[0].Rows[0]["id"].ToString().Trim());
                this.lbl_form_name.Text = form_name;
                this.lbl_stunum.Text = old_empno;
                this.lbl_stuname.Text = old_Name;
                this.lbl_opinion.Text = is_pass;
                if (is_pass == "N")
                {
                    this.lbl_opinion.Text = "暂未审核";
                }
                if (is_pass == "Y")
                {
                    this.lbl_opinion.Text = "已审核";
                }






                //该课程支持住宿

            }
            else
            {

                Response.Write("<script language=javascript>alert('无需再审核！');window.location = 'A_MoralEducationGroupuserAdmin.aspx';</script>");
            }
        }
        protected void review_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"].ToString());
            string form_name, teacner_Name, Teacher_Dept, old_empno, old_Name, old_Class, intoDate, old_Phone, teacner_Name2, remarks, addtime, is_pass, RmchSeqNo;
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[EntryApply_indent] where form_name='校友入校申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and id = '" + id + "'";
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "Achievement_indent");
            if (myViewDate.Tables[0].Rows.Count > 0)
            {

                form_name = myViewDate.Tables[0].Rows[0]["form_name"].ToString().Trim();
                teacner_Name = myViewDate.Tables[0].Rows[0]["teacnerName"].ToString().Trim();
                Teacher_Dept = myViewDate.Tables[0].Rows[0]["DeptName"].ToString().Trim();
                old_empno = myViewDate.Tables[0].Rows[0]["alumnusEmp"].ToString().Trim();
                old_Name = myViewDate.Tables[0].Rows[0]["alumnusName"].ToString().Trim();
                old_Class = myViewDate.Tables[0].Rows[0]["LeaveClass"].ToString().Trim();
                intoDate = myViewDate.Tables[0].Rows[0]["intoDate"].ToString().Trim();
                old_Phone = myViewDate.Tables[0].Rows[0]["alumnusPhone"].ToString().Trim();
                teacner_Name2 = myViewDate.Tables[0].Rows[0]["teacnerName2"].ToString().Trim();
                remarks = myViewDate.Tables[0].Rows[0]["remarks"].ToString().Trim();
                addtime = myViewDate.Tables[0].Rows[0]["addtime"].ToString().Trim();
                is_pass = myViewDate.Tables[0].Rows[0]["is_pass"].ToString().Trim();
                RmchSeqNo = myViewDate.Tables[0].Rows[0]["RmchSeqNo"].ToString().Trim();
                id = Convert.ToInt32(myViewDate.Tables[0].Rows[0]["id"].ToString().Trim());

                if (RadioButtonY.Checked == true)
                {
                    

                    if (DoEmail())
                    {
                        
                        Response.Write("<script language=javascript>alert('审核同意提交成功！');window.location = 'A_MoralEducationGroupuserAdmin.aspx';</script>");
                    }

                   
                }
                else if (RadioButtonN.Checked == true)
                {
                    if (DoEmail2())
                    {

                        Response.Write("<script language=javascript>alert('审核同意提交成功！');window.location = 'A_MoralEducationGroupuserAdmin.aspx';</script>");
                    }
                   
                   
                   
                }






                //该课程支持住宿

            }
            else
            {

                Response.Write("<script language=javascript>alert('该单据已审核过，无需再审核！');window.location = 'A_MoralEducationGroupuserAdmin.aspx';</script>");
            }
        }
        protected bool DoEmail()
        {
            bool result = false;
            SqlConnection cn = new SqlConnection("Data Source=192.168.80.247;Database=Common;Uid=kcis_db;Pwd=db2008_kcis");
            cn.Open();
            string strList = "";
           
            string strSQL = @"Insert into [Common].[dbo].[oa_emaillog](pid,emailid ,actiontype ,toaddr,toname ,fromaddr ,fromname,subject,body
                                  , attch , remark ,createdate ) 
                                   values(@pid, @emailid , @actiontype , @toaddr, @toname , 'automail@kcisec.com' , @strSystem, @subject, @body
                                  , @attch , @remark, getdate())";

            string sqlstr = " select m.AccountID , m.fullname ,  m.email, dt.teacherAD,dt.alumnusEmp,dt.alumnusName,dt.alumnusPhone,dt.LeaveClass,dt.RmchSeqNo ";
            sqlstr = sqlstr + "from [Common].[dbo].[kcis_account] m ";
            sqlstr = sqlstr + "join[db_forminf].[dbo].[EntryApply_indent] dt on dt.teacherAD = m.AccountID where ";
            sqlstr = sqlstr + "form_name='校友入校申请' and  (Is_inner ='Z' OR Is_inner = 'N')   and id ='" + id + "'";
            DataSet myViewDate1 = lw.ReturnDataSet(sqlstr, "EntryApply_indent");
            DataTable dt1 = myViewDate1.Tables[0];//指定第0个表 
            DataView myView1 = dt1.DefaultView;
            strList += myView1[0]["fullname"].ToString() + "老师您好：";
            strList += "您提交的入校申请单，详情如下：" + "<br />" + "<br />";
            strList += "单据编号：" + myView1[0]["RmchSeqNo"].ToString() + "<br />" + "<br />";
            strList += "校友学号：" + myView1[0]["alumnusEmp"].ToString() + "<br />" + "<br />";
            strList += "校友姓名：" + myView1[0]["alumnusName"].ToString() + "<br />" + "<br />";
            strList += "校友电话：" + myView1[0]["alumnusPhone"].ToString() + "<br />" + "<br />";
            strList += "校友离校班级：" + myView1[0]["LeaveClass"].ToString() + "<br />" + "<br />";
            strList += "该校友入校申请已通过审核";
            foreach (DataRowView myRow in myView1)
            {
                    //新增到表格
                    SqlCommand cmd = new SqlCommand(strSQL, cn);
                    cmd.Parameters.Add("@pid", SqlDbType.NVarChar).Value = "sys_flowengin";
                    cmd.Parameters.Add("@emailid", SqlDbType.NVarChar).Value = Convert.ToString(System.Guid.NewGuid());
                    cmd.Parameters.Add("@actiontype", SqlDbType.NVarChar).Value = "email";
                    cmd.Parameters.Add("@toaddr", SqlDbType.NVarChar).Value = myRow["email"].ToString().Trim();
                    cmd.Parameters.Add("@toname", SqlDbType.NVarChar).Value = myRow["AccountID"].ToString().Trim();
                    cmd.Parameters.Add("@strSystem", SqlDbType.NVarChar).Value = "线上校友专区";
                    cmd.Parameters.Add("@subject", SqlDbType.NVarChar).Value = "校友入校申请";
                    cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = strList;
                    cmd.Parameters.Add("@attch", SqlDbType.NVarChar).Value = "";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                  

           }
            string RmchSeqNo = myViewDate1.Tables[0].Rows[0]["RmchSeqNo"].ToString().Trim();
            string sqlstr2 = "update  [db_forminf].[dbo].[EntryApply_indent] set is_pass = 'Y' where form_name='校友入校申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and id ='" + id + "' ";
            int aa = lw.EXECCommand(sqlstr2);
            string sqlstr3 = "update  [db_forminf].[dbo].[OldStudent_Onlin_List] set is_pass = 'Y' where form_name='校友入校申请'   and mchSeqNo ='" + RmchSeqNo + "' ";
            int aa1 = lw.EXECCommand(sqlstr3);
            try
            {
                cn.Close();//关闭数据库连接
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;

        }
        protected bool DoEmail2()
        {
            bool result = false;
            SqlConnection cn = new SqlConnection("Data Source=192.168.80.247;Database=Common;Uid=kcis_db;Pwd=db2008_kcis");
            cn.Open();
            string strList = "";

            string strSQL = @"Insert into [Common].[dbo].[oa_emaillog](pid,emailid ,actiontype ,toaddr,toname ,fromaddr ,fromname,subject,body
                                  , attch , remark ,createdate ) 
                                   values(@pid, @emailid , @actiontype , @toaddr, @toname , 'automail@kcisec.com' , @strSystem, @subject, @body
                                  , @attch , @remark, getdate())";

            string sqlstr = " select m.AccountID , m.fullname ,  m.email, dt.teacherAD,dt.alumnusEmp,dt.alumnusName,dt.alumnusPhone,dt.LeaveClass,dt.RmchSeqNo ";
            sqlstr = sqlstr + "from [Common].[dbo].[kcis_account] m ";
            sqlstr = sqlstr + "join[db_forminf].[dbo].[EntryApply_indent] dt on dt.teacherAD = m.AccountID where ";
            sqlstr = sqlstr + "form_name='校友入校申请' and  (Is_inner ='Z' OR Is_inner = 'N')   and id ='" + id + "'";
            DataSet myViewDate1 = lw.ReturnDataSet(sqlstr, "EntryApply_indent");
            DataTable dt1 = myViewDate1.Tables[0];//指定第0个表 
            DataView myView1 = dt1.DefaultView;
            strList += myView1[0]["fullname"].ToString() + "老师您好：";
            strList += "您提交的入校申请单，详情如下：" + "<br />" + "<br />";
            strList += "单据编号：" + myView1[0]["RmchSeqNo"].ToString() + "<br />" + "<br />";
            strList += "校友学号：" + myView1[0]["alumnusEmp"].ToString() + "<br />" + "<br />";
            strList += "校友姓名：" + myView1[0]["alumnusName"].ToString() + "<br />" + "<br />";
            strList += "校友电话：" + myView1[0]["alumnusPhone"].ToString() + "<br />" + "<br />";
            strList += "校友离校班级：" + myView1[0]["LeaveClass"].ToString() + "<br />" + "<br />";
            strList += "该校友入校申请已驳回";
            foreach (DataRowView myRow in myView1)
            {
                //新增到表格
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                cmd.Parameters.Add("@pid", SqlDbType.NVarChar).Value = "sys_flowengin";
                cmd.Parameters.Add("@emailid", SqlDbType.NVarChar).Value = Convert.ToString(System.Guid.NewGuid());
                cmd.Parameters.Add("@actiontype", SqlDbType.NVarChar).Value = "email";
                cmd.Parameters.Add("@toaddr", SqlDbType.NVarChar).Value = myRow["email"].ToString().Trim();
                cmd.Parameters.Add("@toname", SqlDbType.NVarChar).Value = myRow["AccountID"].ToString().Trim();
                cmd.Parameters.Add("@strSystem", SqlDbType.NVarChar).Value = "线上校友专区";
                cmd.Parameters.Add("@subject", SqlDbType.NVarChar).Value = "校友入校申请";
                cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@body", SqlDbType.NVarChar).Value = strList;
                cmd.Parameters.Add("@attch", SqlDbType.NVarChar).Value = "";
                cmd.ExecuteNonQuery();
                cmd.Dispose();


            }
            string RmchSeqNo = myViewDate1.Tables[0].Rows[0]["RmchSeqNo"].ToString().Trim();
            string sqlstr2 = "update  [db_forminf].[dbo].[EntryApply_indent] set is_pass = 'D' where form_name='校友入校申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'   and id ='" + id + "' ";
            int aa = lw.EXECCommand(sqlstr2);
            string sqlstr3 = "update  [db_forminf].[dbo].[OldStudent_Onlin_List] set is_pass = 'D' where form_name='校友入校申请'  and mchSeqNo ='" + RmchSeqNo + "' ";
            int aa1 = lw.EXECCommand(sqlstr3);
            try
            {
                cn.Close();//关闭数据库连接
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