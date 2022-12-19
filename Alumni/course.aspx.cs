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
    public partial class course : System.Web.UI.Page
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
            string product_name, product_d, product_time, fee,img_route;
            PlaceHolderList.Controls.Clear();

            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[product] a left join [db_forminf].[dbo].[OA_CourseMapping] b on a.product_id = b.PID where shop_id='S0000000' and (Is_inner ='Z' OR Is_inner = 'N') AND Is_open = 'Y'  order by id";
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "db_forminf");
            DataTable dt = myViewDate.Tables[0];//指定第0个表
            DataView myView = dt.DefaultView;
            foreach (DataRowView myRow in myView)
            {
//                string sqlstr1 = @"select COUNT(*) as ok_num
// from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a 
// on a.merchantSeq=b.merchantSeq  where a.id is not null  and a.refundtime is null and inExtData = '" + myRow["product_name"].ToString().Trim() + "'";
//                DataSet myViewDate1 = lw.ReturnDataSet2(sqlstr1, "Pay_Before");
                int ok_num = 0;
                if (Convert.ToChar(myRow["IstoCheckNum"].ToString().Trim()) == 'Y')
                {
                    if (myRow["SID"].ToString().Trim() == null)
                    {
                        Response.Write("<Script Language=JavaScript>alert('课程加载失败，此课程在校内的报名中未存在！');</Script>");
                        return;
                    }
                    else
                    {
                        //string sqlstr2 = "SELECT * FROM [WebApp].[dbo].[OA_SchoolActivity_OrderList] where SID = 'S20181025001'  and Enabled = 'Y'";
                        string sqlstr2 = "SELECT * FROM [WebApp].[dbo].[OA_SchoolActivity_OrderList] where SID = '" + myRow["SID"].ToString().Trim() + "'";
                        DataSet myViewDate2 = lw.ReturnDataSet(sqlstr2, "WebApp");
                        //和校内的选课比较的话，因减去校内已经选课的人数
                        ok_num = Convert.ToInt32(myRow["num_max"].ToString().Trim()) - myViewDate2.Tables[0].Rows.Count;
                    }
                }
                else
                {
                    string sqlstr1 = @"select *  from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a 
 on a.merchantSeq=b.merchantSeq  where a.id is not null  and a.refundtime is null  and a.remark='订单交易成功' and b.remark = '" + myRow["product_id"].ToString().Trim() + "'";
                    DataSet myViewDate1 = lw.ReturnDataSet2(sqlstr1, "KsisecPay");
                    ok_num = Convert.ToInt32(myRow["num_max"].ToString().Trim()) - myViewDate1.Tables[0].Rows.Count;
                }

                if (ok_num > 0 )
                {
                    product_name = myRow["product_name"].ToString().Trim();
                    product_d = myRow["product_d"].ToString().Trim();
                    product_time = myRow["product_time"].ToString().Trim();
                    fee = Convert.ToString(float.Parse(myRow["fee"].ToString().Trim()) * 0.01);
                    img_route = myRow["img_route"].ToString().Trim();
                    id = Convert.ToInt32(myRow["id"].ToString().Trim());


                    PlaceHolderList.Controls.Add(new LiteralControl("<li><a class=\"item-content ajax\" href=\"course_d.aspx?id=" + id + "\"><div class=\"item-media\"><span class=\"coachbg\" style=\"background-image: url(" + img_route + ");\"></span>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("</div><div class=\"item-inner\"><div class=\"item-title-row\"><div class=\"item-title\">" + product_name + "</div>"));
                    PlaceHolderList.Controls.Add(new LiteralControl(" <div class=\"item-after\">报名</div></div><div class=\"item-text\">" + product_d + "</div><div class=\"item-subtitle\">" + product_time + " <span>价格 " + fee + "元</span></div></div></a></li>"));
                }
                else
                {
                    //如果报名人数已满，更新关闭课程
                    string sqlstr2 = "update  [db_forminf].[dbo].[product] set Is_open = 'N' where shop_id='S0000029' and  product_id = '" + myRow["product_id"].ToString().Trim() + "'";
                    int aa = lw.EXECCommand(sqlstr2);
                }
               
            }
        }
    }
}