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
    public partial class A_myOrder : System.Web.UI.Page
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
            string KeyWord1 = txtKeyword.Text.Trim();
            string sqlstr = "SELECT a.*,b.form_id as Bproduct_id  FROM [db_forminf].[dbo].[OldStudent_Onlin_List] a left join [db_forminf].[dbo].[OldStudentOnlin] b on a.form_name = b.form_name where b.shopForm_id='S0000001'  and (a.stunum like '%" + KeyWord + "%' OR a.Phone like '%" + KeyWord1 + "%' )";
            DataTable dt1 = lw.GetDataTable1(sqlstr);

            if (dt1.Rows.Count == 0)
            {

                PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block\">没有表单。</div>"));
            }
            else
            {
                    foreach (DataRow myRow1 in dt1.Rows)
                    {
                        if (myRow1["is_pass"].ToString().Trim() == "N")
                    {
                        string form_name = myRow1["form_name"].ToString().Trim();
                        string adress = myRow1["EmailAdress"].ToString().Trim();
                        string into = myRow1["intoDate"].ToString().Trim();
                        string sendTime = myRow1["addtime"].ToString().Trim();
                        string danhao = myRow1["mchSeqNo"].ToString().Trim();
                        string is_pass = "表单已接收审核中";
                        PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block\"> <ul><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单名称:</div><div class=\"item-after\" style=\"color: #aaa;\">" + form_name + "</div></div></li>"));
                        if (form_name == "校友入校申请")
                        {
                            //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">入校时间:</div><div class=\"item-after\" style=\"color: #aaa;\">" + into + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color:#a62ebe;\">" + is_pass + "</div></div></li></ul></div>"));

                        }
                        else if (form_name == "转出/在读证明")
                        {
                            //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">邮寄地址:</div><div class=\"item-after\" style=\"color: #aaa;\">" + adress + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #a62ebe;\">" + is_pass + "</div></div></li></ul></div>"));
                           }
                        else if (form_name == "成绩单申请")
                        {
                            //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">邮寄/邮箱地址:</div><div class=\"item-after\" style=\"color: #aaa;\">" + adress + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #a62ebe;\">" + is_pass + "</div></div></li></ul></div>"));
                        }
                        
                    }
                        else if (myRow1["is_pass"].ToString().Trim() == "Y" )
                    {

                        string form_name = myRow1["form_name"].ToString().Trim();
                        string adress = myRow1["EmailAdress"].ToString().Trim();
                        string into = myRow1["intoDate"].ToString().Trim();
                        string sendTime = myRow1["addtime"].ToString().Trim();
                        string danhao = myRow1["mchSeqNo"].ToString().Trim();
                        string is_pass = "表单已审核通过";
                        PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block\"> <ul><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单名称:</div><div class=\"item-after\" style=\"color: #aaa;\">" + form_name + "</div></div></li>"));
                        if (form_name == "校友入校申请")
                        {
                            //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">入校时间:</div><div class=\"item-after\" style=\"color: #aaa;\">" + into + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #0a9811;\">" + is_pass + "</div></div></li></ul></div>"));

                        }
                        else if (form_name == "转出/在读证明")
                        {
                            //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">邮寄地址:</div><div class=\"item-after\" style=\"color: #aaa;\">" + adress + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #0a9811;\">" + is_pass + "</div></div></li></ul></div>"));
                        }
                        else if (form_name == "成绩单申请")
                        {
                            //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">邮寄/邮箱地址:</div><div class=\"item-after\" style=\"color: #aaa;\">" + adress + "</div></div></li>"));
                            PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #0a9811;\">" + is_pass + "</div></div></li></ul></div>"));
                        }
                            
                        
                    }
                        else if (myRow1["is_pass"].ToString().Trim() == "D")
                        {

                            string form_name = myRow1["form_name"].ToString().Trim();
                            string adress = myRow1["EmailAdress"].ToString().Trim();
                            string into = myRow1["intoDate"].ToString().Trim();
                            string sendTime = myRow1["addtime"].ToString().Trim();
                            string danhao = myRow1["mchSeqNo"].ToString().Trim();
                            string is_pass = "表单审核状态为驳回";
                            PlaceHolderList.Controls.Add(new LiteralControl("<div class=\"list-block\"> <ul><li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单名称:</div><div class=\"item-after\" style=\"color: #aaa;\">" + form_name + "</div></div></li>"));
                            if (form_name == "校友入校申请")
                            {
                                //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                                PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">入校时间:</div><div class=\"item-after\" style=\"color: #aaa;\">" + into + "</div></div></li>"));
                                PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #ff0000;\">" + is_pass + "</div></div></li></ul></div>"));

                            }
                            else if (form_name == "转出/在读证明")
                            {
                                //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                                PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">邮寄地址:</div><div class=\"item-after\" style=\"color: #aaa;\">" + adress + "</div></div></li>"));
                                PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #ff0000;\">" + is_pass + "</div></div></li></ul></div>"));
                            }
                            else if (form_name == "成绩单申请")
                            {
                                //PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">单号:</div><div class=\"item-after\" >" + danhao + "</div></div></li>"));
                                PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">邮寄/邮箱地址:</div><div class=\"item-after\" style=\"color: #aaa;\">" + adress + "</div></div></li>"));
                                PlaceHolderList.Controls.Add(new LiteralControl("<li class=\"item-content\"><div class=\"item-inner\"><div class=\"item-title\">表单状态:</div><div class=\"item-after\" style=\"color: #ff0000;\">" + is_pass + "</div></div></li></ul></div>"));
                            }


                        }
                }

                
               
            }

        }
    }
}