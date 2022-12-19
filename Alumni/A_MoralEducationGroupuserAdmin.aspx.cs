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
    public partial class A_MoralEducationGroupuserAdmin : System.Web.UI.Page
    {
        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmployeeName"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }
                Label1.Text = Session["EmployeeName"].ToString();
                Label2.Text = Session["EmployeeAccount"].ToString();
                
                myControlBind();
            }
            myTableBind(false);
        }

        private void myControlBind()
        {
            ListItem myItem1 = new ListItem();
            myItem1.Text = "----请选择----";
            myItem1.Value = "aaa";
            ddlStatus1.Items.Add(myItem1);

            string sqlstr = "select * from [db_forminf].[dbo].[OldStudentOnlin] where shopForm_id='S0000001'and form_id='P0000001' order by id";
            DataTable dt = lw.GetDataTable1(sqlstr);
            DataView myView = dt.DefaultView;
            foreach (DataRowView myRow in myView)
            {
                ListItem myItem = new ListItem();
                myItem.Text = myRow["form_id"].ToString().Trim() + "_" + myRow["form_name"].ToString().Trim();
                myItem.Value = myRow["form_id"].ToString().Trim();
                ddlStatus1.Items.Add(myItem);
            }
        }

        private void myTableBind(bool DoExport)
        {
            string product = ddlStatus1.SelectedValue;

            string KeyWord = txtKeyword.Text.Trim();
            //string RowFilter = String.Empty;
            string RowFilter1 = String.Empty;

            string CssClass;
            int PageSize = 10;
            int StartNum = (Convert.ToInt32(ddlPageNum.SelectedValue) - 1) * PageSize;
            int EndNum = (Convert.ToInt32(ddlPageNum.SelectedValue) * PageSize) - 1;
            int i = 0;

            PlaceHolderList.Controls.Clear();

            //string sqlstr = "select b.orderInfo,b.inExtData,b.remark as remark_b,a.amount,a.centerSeqId,a.remark as remark_a,a.noticetime,b.mchSeqNo  from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a on a.merchantSeq=b.merchantSeq where a.id is not null  and a.refundtime is null and a.id>22 and orderInfo='线上校友专区'  order by a.noticetime desc";



            //string addtime = Convert.ToDateTime(myRow["addtime"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
            //string form_name = myRow["form_name"].ToString().Trim();
            //// string stunum = myRow["stunum"].ToString().Trim();
            //string stuname = myRow["stuname"].ToString().Trim();
            //string Phone = myRow["Phone"].ToString().Trim();
            //string is_pass = myRow["is_pass"].ToString().Trim();
            //string amount = Convert.ToString(Convert.ToInt32(myRow["amount"].ToString().Trim()) * 0.01);
            //string centerSeqId = myRow["centerSeqId"].ToString().Trim();
            //string remark_a = myRow["remark_a"].ToString().Trim();
            //string mchSeqNo = myRow["mchSeqNo"].ToString().Trim();

            string sqlstr1 = "SELECT a.*,b.form_id as Bproduct_id  FROM [db_forminf].[dbo].[EntryApply_indent] a left join [db_forminf].[dbo].[OldStudentOnlin] b on a.form_name = b.form_name where b.shopForm_id='S0000001' order by a.addtime desc ";
            DataTable dt1 = lw.GetDataTable1(sqlstr1);
            DataView myView1 = dt1.DefaultView;


            if (ddlStatus1.SelectedValue != "aaa")
            {
                RowFilter1 = " Bproduct_id = '" + ddlStatus1.SelectedValue + "' ";
            }

            if (txtKeyword.Text != String.Empty)
            {
                if (RowFilter1 != String.Empty)
                {
                    RowFilter1 += " AND ";
                }

                RowFilter1 += " stunum like '%" + KeyWord + "%' OR alumnusPhone like '%" + KeyWord + "%' OR form_name like '%" + KeyWord + "%' ";
                //myView1.RowFilter1 = string.Format("stunum like '%' OR form_name like '%'", KeyWord, KeyWord);
            }

            myView1.RowFilter = RowFilter1;
            foreach (DataRowView myRow1 in myView1)
            {
                if (i >= StartNum && i <= EndNum)
                {
                    string form_name = myRow1["form_name"].ToString().Trim();
                    string teacner_Name = myRow1["teacnerName"].ToString().Trim();
                    string Teacher_Dept = myRow1["DeptName"].ToString().Trim();
                    string old_empno = myRow1["alumnusEmp"].ToString().Trim();
                    string old_Name = myRow1["alumnusName"].ToString().Trim();
                    string old_Class = myRow1["LeaveClass"].ToString().Trim();
                    string intoDate = myRow1["intoDate"].ToString().Trim();
                    string old_Phone = myRow1["alumnusPhone"].ToString().Trim();
                    string teacner_Name2 = myRow1["teacnerName2"].ToString().Trim();
                    string remarks = myRow1["remarks"].ToString().Trim();
                    string addtime = myRow1["addtime"].ToString().Trim();
                    string is_pass = myRow1["is_pass"].ToString().Trim();
                    string id = myRow1["id"].ToString().Trim();
                    string rmchSeqNo = myRow1["RmchSeqNo"].ToString().Trim();
                    // string Label1 = Session["EmployeeName"].ToString();
                    //string Label2 = Session["EmployeeName"].ToString();

                    CssClass = i % 2 == 0 ? "RowOne" : "RowTwo";
                    //Color1 = CoughtTwoWeek == "True" ? "Red" : "Gray";
                    //Color2 = CoughtWithSputum == "True" ? "Red" : "Gray";
                    //Color3 = FeelChestPains == "True" ? "Red" : "Gray";
                    //Color4 = NoAppetite == "True" ? "Red" : "Gray";
                    //Color5 = WeightLoss == "True" ? "Red" : "Gray";
                    //Color6 = TotalScore != String.Empty && Convert.ToInt32(TotalScore) >= 5 ? "Red" : "Black";
                    PlaceHolderList.Controls.Add(new LiteralControl("<tr style=\"font-size:13px; height:40px;\" class=\"" + CssClass + "\" onmouseover=\"this.className='RowOver'\" onmouseout=\"this.className='" + CssClass + "'\">"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><a class=\"item-content ajax\" href=\"showFor_R.aspx?id=" + id + "\">" + form_name + "</td>"));

                    if (is_pass == "N")
                    {
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">未审核</span></td>"));
                    }
                    else if (is_pass == "Y")
                    {
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">已审核通过</span></td>"));
                    }
                    else
                    {
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">审核不通过</span></td>"));
                    }
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + Label1 + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + teacner_Name + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + Teacher_Dept + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + old_empno + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + old_Name + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + old_Class + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + intoDate + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + old_Phone + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + teacner_Name2 + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + remarks + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + addtime + "</span></td>"));


                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + phone + "</span></td>"));
                    // PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + amount + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + Is_zhusu + "(" + zhusu_num + "周)" + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + centerSeqId + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + noticetime + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + remark_a + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + introducer + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color1 + ";\">" + CoughtTwoWeek + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color2 + ";\">" + CoughtWithSputum + "</td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color3 + ";\">" + FeelChestPains + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color4 + ";\">" + NoAppetite + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color5 + ";\">" + WeightLoss + "</span></td>"));
                    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color6 + ";\">" + TotalScore + "</span></td>"));
                    PlaceHolderList.Controls.Add(new LiteralControl("</tr>\n"));
                }

                i++;
            }





            if (i == 0) //查無資料
            {
                PlaceHolderList.Controls.Add(new LiteralControl("<tr style=\"font-size:14px; height:28px;\">"));
                PlaceHolderList.Controls.Add(new LiteralControl("<td colspan=\"15\" align=\"center\"><span style=\"color:Blue;\">無相關紀錄</span></td>"));
                PlaceHolderList.Controls.Add(new LiteralControl("</tr>"));
            }
            else
            {
                if (DoExport)
                {
                    //CreateExcel(myViewDate, "Tuberculosis_" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString());
                    //WriteExcel(myViewDate, "ceshi");
                    //ExcelOperation EOp = new ExcelOperation();

                    ////寫入到客戶端
                    //using (MemoryStream ms = EOp.RenderDataTableToExcel(myView.ToTable(), "TuberculosisSelfScreen"))
                    //{
                    //    Response.AddHeader("Content-Disposition", string.Format("attachment; filename=TuberculosisSelfScreen_" + DateTime.Today.ToString("yyyyMM") + ".xlsx"));
                    //    Response.BinaryWrite(ms.ToArray());
                    //    Response.Flush();
                    //    Response.End();
                    //}
                }
            }

            LiteralNum.Text = "(共" + i.ToString() + "筆資料) ";
            int CurrentPageNum = Convert.ToInt32(ddlPageNum.SelectedValue);
            ddlPageNum.Items.Clear();
            int TotalPage = ((i - 1) / PageSize) + 1;
            for (int j = 1; j <= TotalPage; j++)
            {
                ListItem item = new ListItem();
                item.Text = "第" + j.ToString() + "頁";
                item.Value = j.ToString();
                ddlPageNum.Items.Add(item);
            }
            //判斷頁數是否變少
            try
            {
                ddlPageNum.SelectedValue = CurrentPageNum > TotalPage ? (CurrentPageNum - 1).ToString() : ddlPageNum.SelectedValue = CurrentPageNum.ToString();
            }
            catch
            {
                //
            }
        }






        private void myTableBind_upload(bool DoExport)
        {
            string product = ddlStatus1.SelectedValue;

            string KeyWord = txtKeyword.Text.Trim();
            //string RowFilter = String.Empty;
            string RowFilter1 = String.Empty;

            //string CssClass;
            //int PageSize = 20;
            //int StartNum = (Convert.ToInt32(ddlPageNum.SelectedValue) - 1) * PageSize;
            //int EndNum = (Convert.ToInt32(ddlPageNum.SelectedValue) * PageSize) - 1;
            //int i = 0;

            //PlaceHolderList.Controls.Clear();




            DataTable dt_upload = new DataTable();
            DataColumn dc1 = new DataColumn("单据名称", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("审核状态", Type.GetType("System.String"));
            DataColumn dc3= new DataColumn("教职员姓名", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("教职员部门", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("校友学号", Type.GetType("System.String"));
            DataColumn dc6 = new DataColumn("校友姓名", Type.GetType("System.String"));
            DataColumn dc7 = new DataColumn("校友离校班级", Type.GetType("System.String"));
            DataColumn dc8 = new DataColumn("校友入校日期", Type.GetType("System.String"));
            DataColumn dc9 = new DataColumn("校友电话", Type.GetType("System.String"));
            DataColumn dc10 = new DataColumn("其他拜访师长姓名", Type.GetType("System.String"));
            DataColumn dc11 = new DataColumn("其他需求或备注", Type.GetType("System.String"));
            DataColumn dc12= new DataColumn("提交时间", Type.GetType("System.String"));
          

            dt_upload.Columns.Add(dc1);
            dt_upload.Columns.Add(dc2);
            dt_upload.Columns.Add(dc3);
            dt_upload.Columns.Add(dc4);
            dt_upload.Columns.Add(dc5);
            dt_upload.Columns.Add(dc6);
            dt_upload.Columns.Add(dc7);
            dt_upload.Columns.Add(dc8);
            dt_upload.Columns.Add(dc9);
            dt_upload.Columns.Add(dc10);
            dt_upload.Columns.Add(dc11);
            dt_upload.Columns.Add(dc12);




            string sqlstr1 = "SELECT a.*,b.form_id as Bproduct_id  FROM [db_forminf].[dbo].[EntryApply_indent] a left join [db_forminf].[dbo].[OldStudentOnlin] b on a.form_name = b.form_name where b.shopForm_id='S0000001' order by a.addtime desc";
            DataTable dt1 = lw.GetDataTable1(sqlstr1);
            DataView myView1 = dt1.DefaultView;


            if (ddlStatus1.SelectedValue != "aaa")
            {
                RowFilter1 = " Bproduct_id = '" + ddlStatus1.SelectedValue + "' ";
            }

            if (txtKeyword.Text != String.Empty)
            {
                if (RowFilter1 != String.Empty)
                {
                    RowFilter1 += " AND ";
                }
                RowFilter1 += " stuname like '%" + KeyWord + "%' OR form_name like '%" + KeyWord + "%' ";
                //myView.RowFilter = string.Format("EmployeeID = '{0}' OR Name like '%{1}%'", KeyWord, KeyWord);
            }
            myView1.RowFilter = RowFilter1;
            foreach (DataRowView myRow1 in myView1)
            {
                

                string form_name = myRow1["form_name"].ToString().Trim();
                string teacner_Name = myRow1["teacnerName"].ToString().Trim();
                string Teacher_Dept = myRow1["DeptName"].ToString().Trim();
                string old_empno = myRow1["alumnusEmp"].ToString().Trim();
                string old_Name = myRow1["alumnusName"].ToString().Trim();
                string old_Class = myRow1["LeaveClass"].ToString().Trim();
                string intoDate = myRow1["intoDate"].ToString().Trim();
                string old_Phone = myRow1["alumnusPhone"].ToString().Trim();
                string teacner_Name2 = myRow1["teacnerName2"].ToString().Trim();
                string remarks = myRow1["remarks"].ToString().Trim();
                string addtime = myRow1["addtime"].ToString().Trim();
                string is_pass = myRow1["is_pass"].ToString().Trim();


                DataRow drt = dt_upload.NewRow();
                drt["单据名称"] = form_name;
                drt["审核状态"] = is_pass;
                drt["教职员姓名"] = teacner_Name;
                drt["教职员部门"] = Teacher_Dept;
                drt["校友学号"] = old_empno;
                drt["校友姓名"] = old_Name;
                drt["校友离校班级"] = old_Class;
                drt["校友入校日期"] = intoDate;
                drt["校友电话"] = old_Phone;
                drt["其他拜访师长姓名"] = teacner_Name2;
                drt["其他需求或备注"] = remarks;
                drt["提交时间"] = addtime;


                dt_upload.Rows.Add(drt);



            }




            //if (i == 0) //查無資料
            //{
            //    PlaceHolderList.Controls.Add(new LiteralControl("<tr style=\"font-size:14px; height:28px;\">"));
            //    PlaceHolderList.Controls.Add(new LiteralControl("<td colspan=\"15\" align=\"center\"><span style=\"color:Blue;\">無相關紀錄</span></td>"));
            //    PlaceHolderList.Controls.Add(new LiteralControl("</tr>"));
            //}
            //else
            //{
            if (DoExport)
            {
                CreateExcel(dt_upload, "OldStudent_Onlin_" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".xls");
                //WriteExcel(myViewDate, "ceshi");
                //ExcelOperation EOp = new ExcelOperation();

                ////寫入到客戶端
                //using (MemoryStream ms = EOp.RenderDataTableToExcel(myView.ToTable(), "TuberculosisSelfScreen"))
                //{
                //    Response.AddHeader("Content-Disposition", string.Format("attachment; filename=TuberculosisSelfScreen_" + DateTime.Today.ToString("yyyyMM") + ".xlsx"));
                //    Response.BinaryWrite(ms.ToArray());
                //    Response.Flush();
                //    Response.End();
                //}
            }
            //}

            //LiteralNum.Text = "(共" + i.ToString() + "筆資料) ";
            //int CurrentPageNum = Convert.ToInt32(ddlPageNum.SelectedValue);
            //ddlPageNum.Items.Clear();
            //int TotalPage = ((i - 1) / PageSize) + 1;
            //for (int j = 1; j <= TotalPage; j++)
            //{
            //    ListItem item = new ListItem();
            //    item.Text = "第" + j.ToString() + "頁";
            //    item.Value = j.ToString();
            //    ddlPageNum.Items.Add(item);
            //}
            ////判斷頁數是否變少
            //try
            //{
            //    ddlPageNum.SelectedValue = CurrentPageNum > TotalPage ? (CurrentPageNum - 1).ToString() : ddlPageNum.SelectedValue = CurrentPageNum.ToString();
            //}
            //catch
            //{
            //    //
            //}
        }




        protected void NavigationButtonClick(object sender, EventArgs e)
        {
            string direction = ((LinkButton)sender).CommandName;

            switch (direction)
            {
                case "Previous":
                    if (ddlPageNum.SelectedIndex == 0)
                    {
                        return;
                    }

                    ddlPageNum.SelectedIndex = Math.Max(ddlPageNum.SelectedIndex - 1, 0);
                    break;
                case "Next":
                    if (ddlPageNum.SelectedIndex == ddlPageNum.Items.Count - 1)
                    {
                        return;
                    }
                    ddlPageNum.SelectedIndex = Math.Min(ddlPageNum.SelectedIndex + 1, ddlPageNum.Items.Count - 1);
                    break;
                default:
                    break;
            }

            myTableBind(false);
        }


        protected void ddlStatus1_SelectedIndexChanged(object sender, EventArgs e)
        {
            myTableBind(false);
        }

        //---------------------------------------------------------------------------------------
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            myTableBind(false);
        }

        protected void ddlPageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            myTableBind(false);
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            myTableBind_upload(true);
        }



        public void CreateExcel(DataTable ds, string FileName)
        {
            HttpResponse resp;
            resp = Page.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            string colHeaders = "", ls_item = "";

            //定义表对象与行对象，同时用DataSet对其值进行初始化 
            DataTable dt = ds;
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;

            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符 
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                }
                else
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                }

            }
            resp.Write(colHeaders);
            //向HTTP输出流中写入取得的数据信息 

            //逐行处理数据   
            foreach (DataRow row in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加n
                    {
                        ls_item += row[i].ToString() + "\n";
                    }
                    else
                    {
                        ls_item += row[i].ToString() + "\t";
                    }

                }
                resp.Write(ls_item);
                ls_item = "";

            }
            resp.End();
        }
    }
}