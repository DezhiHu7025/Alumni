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
    public partial class myorder : System.Web.UI.Page
    {
        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlaceHolderList.Controls.Clear();
                this.txtKeyword.Text = "";
            }
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            string RowFilter1 = String.Empty;
            if (txtKeyword.Text.Trim().Length == 0)
            {
                Response.Write("<Script Language=JavaScript>alert('请输入搜索条件！');</Script>");
                return;
            }
            string KeyWord = txtKeyword.Text.Trim();
            string sqlstr = "SELECT a.*,b.product_id as Bproduct_id,b.product_time  FROM [db_forminf].[dbo].[summer_indent] a left join [db_forminf].[dbo].[product] b on a.product_id = b.id where b.shop_id='S0000029' and (stuname like '%" + KeyWord + "%' OR phone like '%" + KeyWord + "%' )";
            DataTable dt1 = lw.GetDataTable1(sqlstr);
            if (dt1.Rows.Count == 0)
            {

                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block\">没有订单。</div>"));
            }
            else { 
            foreach (DataRow myRow1 in dt1.Rows) 
                    {
                        if (myRow1["ispay"].ToString().Trim() == "Y")
                        {
                            string product_name = myRow1["product_name"].ToString().Trim();
                            string product_time = myRow1["product_time"].ToString().Trim();
                            string fee = Convert.ToString(Convert.ToInt32(myRow1["fee"].ToString().Trim()) * 0.01);
                            string ispay = "订单交易成功";
                            string paytime = Convert.ToDateTime(myRow1["paytime"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
                            string paynum = myRow1["paynum"].ToString().Trim();
                            PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block\"> <ul><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">支付项目:</div><div class=\"item-after\" style=\"color: #aaa;\">" + product_name + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">开课时间:</div><div class=\"item-after\" style=\"color: #aaa;\">" + product_time + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">实付金额:</div><div class=\"item-after\" style=\"color: #aaa;\">" + fee + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">支付状态:</div><div class=\"item-after\" style=\"color: #aaa;\">" + ispay + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">交易时间:</div><div class=\"item-after\" style=\"color: #aaa;\">" + paytime + "</div></div></li><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">交易单号:</div><div class=\"item-after\" style=\"color: #aaa;\">" + paynum + "</div></div></li></ul></div>"));
                        }
                        else
                        {

                            string sqlstr1 = "select b.orderInfo as SorderInfo,b.inExtData as SinExtData,b.remark as remark_b,a.amount,a.centerSeqId as ScenterSeqId,a.remark as remark_a,a.noticetime as Snoticetime,b.mchSeqNo as SmchSeqNo from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a on a.merchantSeq=b.merchantSeq where a.id is not null  and a.refundtime is null and b.mchSeqNo='" + myRow1["mchSeqNo"].ToString().Trim() + "'  order by a.noticetime desc";

                            DataTable dt = lw.GetDataTable(sqlstr1);
                            DataView myView = dt.DefaultView;
                            foreach (DataRowView myRow in myView)
                            {
                                if (myRow["remark_a"].ToString().Trim() == "订单交易成功")
                                {

                                    string sqlstr2 = "update [db_forminf].[dbo].[summer_indent]  set ispay = 'Y',paytime='" + myRow["Snoticetime"].ToString().Trim() + "',paynum= '" + myRow["ScenterSeqId"].ToString().Trim() + "' where mchSeqNo = '" + myRow["SmchSeqNo"].ToString().Trim() + "'";
                                    int i = lw.EXECCommand(sqlstr2);

                                    string noticetime = Convert.ToDateTime(myRow["Snoticetime"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
                                    string orderInfo = myRow["SorderInfo"].ToString().Trim();
                                    string inExtData = myRow["SinExtData"].ToString().Trim();
                                    string remark_b = myRow["remark_b"].ToString().Trim();
                                    string amount = Convert.ToString(Convert.ToInt32(myRow["amount"].ToString().Trim()) * 0.01);
                                    string centerSeqId = myRow["ScenterSeqId"].ToString().Trim();
                                    string remark_a = myRow["remark_a"].ToString().Trim();
                                    PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block\"> <ul><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">支付项目:</div><div class=\"item-after\" style=\"color: #aaa;\">" + orderInfo + "  " + inExtData + "</div></div></li>"));
                                    PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">开课时间:</div><div class=\"item-after\" style=\"color: #aaa;\">" + remark_b + "</div></div></li>"));
                                    PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">实付金额:</div><div class=\"item-after\" style=\"color: #aaa;\">" + amount + "</div></div></li>"));
                                    PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">支付状态:</div><div class=\"item-after\" style=\"color: #aaa;\">" + remark_a + "</div></div></li>"));
                                    PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">交易时间:</div><div class=\"item-after\" style=\"color: #aaa;\">" + noticetime + "</div></div></li><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">交易单号:</div><div class=\"item-after\" style=\"color: #aaa;\">" + centerSeqId + "</div></div></li></ul></div>"));
                                }
                            }
                        }
                    }
            } 
               
        }
    }
}