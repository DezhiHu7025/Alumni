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
    public partial class A_New_d3 : System.Web.UI.Page
    {
        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                GetTaskContent(id);
            }
        }
        protected void GetTaskContent(int ida)
        {
            string form_name, form_d, fee, img_route, form_id;
            PlaceHolderList.Controls.Clear();
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[OldStudentOnlin] where shopForm_id='S0000001' and  (Is_inner ='Z' OR Is_inner = 'N') AND Is_open = 'Y'  and  id =" + ida;
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "product");
            if (myViewDate.Tables[0].Rows.Count > 0)
            {
                form_name = myViewDate.Tables[0].Rows[0]["form_name"].ToString().Trim();
                form_d = myViewDate.Tables[0].Rows[0]["form_d"].ToString().Trim();
                fee = Convert.ToString(float.Parse(myViewDate.Tables[0].Rows[0]["fee"].ToString().Trim()) * 0.01);
                img_route = myViewDate.Tables[0].Rows[0]["img_route"].ToString().Trim();
                id = Convert.ToInt32(myViewDate.Tables[0].Rows[0]["id"].ToString().Trim());

                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"content-block-title\" style=\"margin-top: 0\">基本信息</div>"));
                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block media-list\"><ul><li><div class=\"item-link ajax item-content coach\"><div class=\"item-media\"><span class=\"coachbg\" style=\"background-image: url(" + img_route + ");\"></span></div><div class=\"item-inner\"><div class=\"item-title\"><br />" + form_name + "</div></div></div></li>"));
                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"content-block-title\">表单使用介绍</div><div class=\"content-block\"> " + form_d + "</div>"));

            }
            else
            {
                Response.Write("<script language=javascript>alert('该表单暂时不可填写！');window.location = 'A_New.aspx';</script>");
            }
        }
    }
}