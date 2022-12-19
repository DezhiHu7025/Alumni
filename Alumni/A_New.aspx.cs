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
    public partial class A_New : System.Web.UI.Page
    {

        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
            bund();
        }
        private void bund()
        {
            int id = 0;
            string form_name, form_d, fee, img_route, form_id;
            PlaceHolderList1.Controls.Clear();
            PlaceHolderList2.Controls.Clear();
            PlaceHolderList3.Controls.Clear();
            PlaceHolderList4.Controls.Clear();
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[OldStudentOnlin] a left join [db_forminf].[dbo].[OA_CourseMapping] b on a.form_id = b.PID where shopForm_id='S0000001' and (Is_inner ='Z' OR Is_inner = 'N') AND Is_open = 'Y'  order by id";
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "db_forminf");
            DataTable dt = myViewDate.Tables[0];//指定第0个表
            DataView myView = dt.DefaultView;
            foreach (DataRowView myRow in myView)
            {
                //                string sqlstr1 = @"select COUNT(*) as ok_num
                // from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a 
                // on a.merchantSeq=b.merchantSeq  where a.id is not null  and a.refundtime is null and inExtData = '" + myRow["form_name"].ToString().Trim() + "'";

                form_name = myRow["form_name"].ToString().Trim();
                form_d = myRow["form_d"].ToString().Trim();
                fee = Convert.ToString(float.Parse(myRow["fee"].ToString().Trim()) * 0.01);
                img_route = myRow["img_route"].ToString().Trim();
                id = Convert.ToInt32(myRow["id"].ToString().Trim());
                form_id = myRow["form_id"].ToString().Trim();
                if (form_id == "P0000001")
                {
                    PlaceHolderList1.Controls.Add(new LiteralControl("<a title=\" 校友入校申请\"  href=\"A_New_d1.aspx?id=" + id + "\"><img style='width:30%;height:30%' alt=\"Admission Apply 校友入校申请\" src=\"img/Links_search.png\" /><p>Admission Apply<br/>" + form_name + "</p>"));
                    //PlaceHolderList1.Controls.Add(new LiteralControl("<a title=\" 校友入校申请\" target=\"_blank\" href=\"A_New_d1.aspx?id=" + id + "\"><img alt=\"Admission Apply 校友入校申请\" src=\"img/Links_search.png\" /><p>Admission Apply<br/>" + form_name + "</p>"));
                   // PlaceHolderList1.Controls.Add(new LiteralControl("" + form_name + ""));
                }
                else if (form_id == "P0000002")
                {
                    PlaceHolderList2.Controls.Add(new LiteralControl("<a title=\"转出/在读申请\"   href=\"A_info_z.aspx?id=" + id + "\"><img style='width:30%;height:30%' alt=\"Reading / Transfer out/At school certificate 转出/在读申请\" src=\"img/Links_e-Resources.png\" /><p>Reading / Transfer out/At school certificate<br/>" + form_name + "</p>"));
                    //PlaceHolderList2.Controls.Add(new LiteralControl("<a title=\"转出/在读申请\" target=\"_blank\" href=\"A_info_z.aspx?id=" + id + "\"><img alt=\"Reading / Transfer out Certificate 转出/在读申请\" src=\"img/Links_e-Resources.png\" /><p>Reading / Transfer out Certificate<br/>" + form_name + "</p>"));
                    //PlaceHolderList2.Controls.Add(new LiteralControl("" + form_name + ""));
                }
                else  if (form_id == "P0000003")
                {
                    PlaceHolderList3.Controls.Add(new LiteralControl("<a title=\"成绩单申请\"   href=\"A_info_c.aspx?id=" + id + "\"><img style='width:30%;height:30%' alt=\"Transcript application 成绩单申请\" src=\"img/Links_account.png\" /><p>Transcript application<br/>" + form_name + "</p>"));
                    //PlaceHolderList3.Controls.Add(new LiteralControl("<a title=\"成绩单申请\" target=\"_blank\" href=\"A_info_c.aspx?id=" + id + "\"><img alt=\"School Report Apply 成绩单申请\" src=\"img/Links_account.png\" /><p>School Report Apply<br/>" + form_name + "</p>"));
                    //PlaceHolderList3.Controls.Add(new LiteralControl("" + form_name + ""));
                }
                else  if (form_id == "P0000004")
                {
                    PlaceHolderList4.Controls.Add(new LiteralControl("<a title=\"校友应聘\"   href=\"A_New_Recruit.aspx?id=" + id + "\"><img style='width:50%;height:50%' alt=\"Admission recruit 校友招聘\" src=\"img/Links_WebResources.png\" /><p>Admission recruit<br/>" + form_name + "</p>"));
                    //PlaceHolderList3.Controls.Add(new LiteralControl("<a title=\"成绩单申请\" target=\"_blank\" href=\"A_info_c.aspx?id=" + id + "\"><img alt=\"School Report Apply 成绩单申请\" src=\"img/Links_account.png\" /><p>School Report Apply<br/>" + form_name + "</p>"));
                    //PlaceHolderList3.Controls.Add(new LiteralControl("" + form_name + ""));
                }
               
             
            }
        }
    }
}