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
    public partial class showFor_C : System.Web.UI.Page
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
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                this.btn_post.Visible = true;
            }
        }
        protected void GetFormList(int ida)
        {

            string form_name1, stunumC, stunameC, reportCard, txt_yyyy, portEname, UseFor, is_pass, yyyy_MM, FCopies, THEWay, SendAdress, Cphone, passOrnot, CmchSeqNo;
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[Achievement_indent] where form_name='成绩单申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and id =" + ida;
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "Achievement_indent");
            if (myViewDate.Tables[0].Rows.Count > 0)
            {
                form_name1 = myViewDate.Tables[0].Rows[0]["form_name"].ToString().Trim();
                stunumC = myViewDate.Tables[0].Rows[0]["stu_empno"].ToString().Trim();
                stunameC = myViewDate.Tables[0].Rows[0]["stu_name"].ToString().Trim();
                reportCard = myViewDate.Tables[0].Rows[0]["reportCard"].ToString().Trim();
                portEname = myViewDate.Tables[0].Rows[0]["passportEname"].ToString().Trim();
                txt_yyyy = myViewDate.Tables[0].Rows[0]["txt_yyyy"].ToString().Trim();
                yyyy_MM = myViewDate.Tables[0].Rows[0]["txt_mm"].ToString().Trim();
                UseFor = myViewDate.Tables[0].Rows[0]["UseFor"].ToString().Trim();
                FCopies = myViewDate.Tables[0].Rows[0]["Copies"].ToString().Trim();
                THEWay = myViewDate.Tables[0].Rows[0]["takeWay"].ToString().Trim();
                SendAdress = myViewDate.Tables[0].Rows[0]["SendAdress"].ToString().Trim();
                is_pass = myViewDate.Tables[0].Rows[0]["is_pass"].ToString().Trim();
                Cphone = myViewDate.Tables[0].Rows[0]["Cphone"].ToString().Trim();
                passOrnot = myViewDate.Tables[0].Rows[0]["is_pass"].ToString().Trim();
                CmchSeqNo = myViewDate.Tables[0].Rows[0]["CmchSeqNo"].ToString().Trim(); 
                id = Convert.ToInt32(myViewDate.Tables[0].Rows[0]["id"].ToString().Trim());
                this.lbl_form_name.Text = form_name1;
                this.lbl_stunum.Text = stunumC;
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

                Response.Write("<script language=javascript>alert('该单据已审核过，无需再审核！');window.location = 'A_schoolReportUseradmin.aspx';</script>");
            }
        }
        protected void review_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"].ToString());
            string form_name1, stunumC, stunameC, reportCard, txt_yyyy, portEname, UseFor,  is_pass, yyyy_MM, FCopies, THEWay, SendAdress, Cphone, passOrnot, CmchSeqNo;
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[Achievement_indent] where form_name='成绩单申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and id = '" + id + "'";
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "Achievement_indent");
            if (myViewDate.Tables[0].Rows.Count > 0)
            {
                form_name1 = myViewDate.Tables[0].Rows[0]["form_name"].ToString().Trim();
                stunumC = myViewDate.Tables[0].Rows[0]["stu_empno"].ToString().Trim();
                stunameC = myViewDate.Tables[0].Rows[0]["stu_name"].ToString().Trim();
                reportCard = myViewDate.Tables[0].Rows[0]["reportCard"].ToString().Trim();
                portEname = myViewDate.Tables[0].Rows[0]["passportEname"].ToString().Trim();
                txt_yyyy = myViewDate.Tables[0].Rows[0]["txt_yyyy"].ToString().Trim();
                yyyy_MM = myViewDate.Tables[0].Rows[0]["txt_mm"].ToString().Trim();
                UseFor = myViewDate.Tables[0].Rows[0]["UseFor"].ToString().Trim();
                FCopies = myViewDate.Tables[0].Rows[0]["Copies"].ToString().Trim();
                THEWay = myViewDate.Tables[0].Rows[0]["takeWay"].ToString().Trim();
                SendAdress = myViewDate.Tables[0].Rows[0]["SendAdress"].ToString().Trim();
                is_pass = myViewDate.Tables[0].Rows[0]["is_pass"].ToString().Trim();
                Cphone = myViewDate.Tables[0].Rows[0]["Cphone"].ToString().Trim();
                passOrnot = myViewDate.Tables[0].Rows[0]["is_pass"].ToString().Trim();
                CmchSeqNo = myViewDate.Tables[0].Rows[0]["CmchSeqNo"].ToString().Trim(); 
                id = Convert.ToInt32(myViewDate.Tables[0].Rows[0]["id"].ToString().Trim());

                if (RadioButton1.Checked == true)
                {
                    string sqlstr2 = "update  [db_forminf].[dbo].[Achievement_indent] set is_pass = 'Y' where form_name='成绩单申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and id ='" + id + "' ";
                    int aa = lw.EXECCommand(sqlstr2);
                    string sqlstr3 = "update  [db_forminf].[dbo].[OldStudent_Onlin_List] set is_pass = 'Y' where form_name='成绩单申请' and  is_pass = 'N'  and mchSeqNo ='" + CmchSeqNo + "' ";
                    int aa1 = lw.EXECCommand(sqlstr3);
                    Response.Write("<script language=javascript>alert('审核提交成功！');window.location = 'A_schoolReportUseradmin.aspx';</script>");
                }
                else if (RadioButton2.Checked == true)
                {
                    string sqlstr2 = "update  [db_forminf].[dbo].[Achievement_indent] set is_pass = 'D' where form_name='成绩单申请' and  (Is_inner ='Z' OR Is_inner = 'N') AND is_pass = 'N'  and id ='" + id + "' ";
                    int aa = lw.EXECCommand(sqlstr2);
                    string sqlstr3 = "update  [db_forminf].[dbo].[OldStudent_Onlin_List] set is_pass = 'D' where form_name='成绩单申请' and   is_pass = 'N'  and mchSeqNo ='" + CmchSeqNo + "' ";
                    int aa1 = lw.EXECCommand(sqlstr3);
                    Response.Write("<script language=javascript>alert('审核提交成功！');window.location = 'A_schoolReportUseradmin.aspx';</script>");
                }






                //该课程支持住宿

            }
            else
            {

                Response.Write("<script language=javascript>alert('该单据已审核过，无需再审核！');window.location = 'A_schoolReportUseradmin.aspx';</script>");
            }
        }
        
    }
}