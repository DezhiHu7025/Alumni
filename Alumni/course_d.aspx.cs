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
    public partial class course_d : System.Web.UI.Page
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
            string product_name, product_d, product_time, fee, img_route;
            PlaceHolderList.Controls.Clear();
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[product] where shop_id='S0000029' and  (Is_inner ='Z' OR Is_inner = 'N') AND Is_open = 'Y'  and  id =" + ida;
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "product");
            if (myViewDate.Tables[0].Rows.Count > 0)
            {
                product_name = myViewDate.Tables[0].Rows[0]["product_name"].ToString().Trim();
                product_d = myViewDate.Tables[0].Rows[0]["product_d"].ToString().Trim();
                product_time = myViewDate.Tables[0].Rows[0]["product_time"].ToString().Trim();
                fee = Convert.ToString(float.Parse(myViewDate.Tables[0].Rows[0]["fee"].ToString().Trim()) * 0.01);
                img_route = myViewDate.Tables[0].Rows[0]["img_route"].ToString().Trim();
                id = Convert.ToInt32(myViewDate.Tables[0].Rows[0]["id"].ToString().Trim());

                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"content-block-title\" style=\"margin-top: 0\">基本信息</div>"));
                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block media-list\"><ul><li><div class=\"item-link ajax item-content coach\"><div class=\"item-media\"><span class=\"coachbg\" style=\"background-image: url(" + img_route + ");\"></span></div><div class=\"item-inner\"><div class=\"item-title\"><br />" + product_name + "</div></div></div></li>"));
                PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">费用： " + fee + "元</div></div></li><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">时间： " + product_time + "</div></div></li></ul></div>"));
                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"content-block-title\">课程简介</div><div class=\"content-block\"> " + product_d + "</div>"));

            }
            else
            {
                Response.Write("<script language=javascript>alert('该课程报名已满！');window.location = 'course.aspx';</script>");
            }
        }
    }
}