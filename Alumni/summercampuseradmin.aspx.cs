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
    public partial class Alumniuseradmin : System.Web.UI.Page
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
                myControlBind();
            }
            myTableBind(false);
        }

        private void myControlBind()
        {
            ListItem myItem1 = new ListItem();
            myItem1.Text = "----请选择----" ;
            myItem1.Value = "aaa";
            ddlStatus.Items.Add(myItem1);

            string sqlstr = "select * from [db_forminf].[dbo].[product] where shop_id='S0000029' order by id";
            DataTable dt = lw.GetDataTable1(sqlstr);
            DataView myView = dt.DefaultView;
            foreach (DataRowView myRow in myView)
            {
                ListItem myItem = new ListItem();
                myItem.Text = myRow["product_id"].ToString().Trim() + "_" + myRow["product_name"].ToString().Trim();
                myItem.Value = myRow["product_id"].ToString().Trim();
                ddlStatus.Items.Add(myItem);
            }
        }

        private void myTableBind(bool DoExport)
        {
            string product = ddlStatus.SelectedValue;

            string KeyWord = txtKeyword.Text.Trim();
            //string RowFilter = String.Empty;
            string RowFilter1 = String.Empty;

            string CssClass;
            int PageSize = 20;
            int StartNum = (Convert.ToInt32(ddlPageNum.SelectedValue) - 1) * PageSize;
            int EndNum = (Convert.ToInt32(ddlPageNum.SelectedValue) * PageSize) - 1;
            int i = 0;

            PlaceHolderList.Controls.Clear();

            string sqlstr = "select b.orderInfo,b.inExtData,b.remark as remark_b,a.amount,a.centerSeqId,a.remark as remark_a,a.noticetime,b.mchSeqNo  from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a on a.merchantSeq=b.merchantSeq where a.id is not null  and a.refundtime is null and a.id>22 and orderInfo='线上校友专区'  order by a.noticetime desc";
            DataTable dt = lw.GetDataTable(sqlstr);
            DataView myView = dt.DefaultView;

            foreach (DataRowView myRow in myView)
            {
                string noticetime = Convert.ToDateTime(myRow["noticetime"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
                string orderInfo = myRow["orderInfo"].ToString().Trim();
                string inExtData = myRow["inExtData"].ToString().Trim();
                string remark_b = myRow["remark_b"].ToString().Trim();
                string amount = Convert.ToString(Convert.ToInt32(myRow["amount"].ToString().Trim()) * 0.01);
                string centerSeqId = myRow["centerSeqId"].ToString().Trim();
                string remark_a = myRow["remark_a"].ToString().Trim();
                string mchSeqNo = myRow["mchSeqNo"].ToString().Trim();

                string sqlstr1 = "SELECT a.*,b.product_id as Bproduct_id,b.product_time  FROM [db_forminf].[dbo].[summer_indent] a left join [db_forminf].[dbo].[product] b on a.product_id = b.id where b.shop_id='S0000029' and  a.mchSeqNo = '" + mchSeqNo + "' ";
                DataTable dt1 = lw.GetDataTable1(sqlstr1);
                DataView myView1 = dt1.DefaultView;


                if (ddlStatus.SelectedValue != "aaa")
                {
                    RowFilter1 = " Bproduct_id = '" + ddlStatus.SelectedValue + "' ";
                }
            
                if (txtKeyword.Text != String.Empty)
                {
                    if (RowFilter1 != String.Empty)
                    {
                        RowFilter1 += " AND ";
                    }
                    RowFilter1 += " stuname like '%" + KeyWord + "%' OR phone like '%" + KeyWord + "%' OR mchSeqNo like '%" + KeyWord + "%' ";
                    //myView.RowFilter = string.Format("EmployeeID = '{0}' OR Name like '%{1}%'", KeyWord, KeyWord);
                }
                myView1.RowFilter = RowFilter1;
                foreach (DataRowView myRow1 in myView1)
                {
                    if (i >= StartNum && i <= EndNum)
                    {
                        string stunum = myRow1["stunum"].ToString().Trim();
                        string stuname = myRow1["stuname"].ToString().Trim();
                        string sex = myRow1["sex"].ToString().Trim();
                        string jiguan = myRow1["jiguan"].ToString().Trim();
                        string address = myRow1["address"].ToString().Trim();
                        string grade = myRow1["grade"].ToString().Trim();
                        string schoolname = myRow1["schoolname"].ToString().Trim();
                        string pname = myRow1["pname"].ToString().Trim();
                        string phone = myRow1["phone"].ToString().Trim();
                        string product_id = myRow1["Bproduct_id"].ToString().Trim();
                        string introducer = myRow1["introducer"].ToString().Trim();
                        string Is_zhusu = myRow1["Is_zhusu"].ToString().Trim();
                        string zhusu_num = myRow1["zhusu_num"].ToString().Trim();
                        string birthday = myRow1["birthday"].ToString().Trim(); 
                        CssClass = i % 2 == 0 ? "RowOne" : "RowTwo";
                        //Color1 = CoughtTwoWeek == "True" ? "Red" : "Gray";
                        //Color2 = CoughtWithSputum == "True" ? "Red" : "Gray";
                        //Color3 = FeelChestPains == "True" ? "Red" : "Gray";
                        //Color4 = NoAppetite == "True" ? "Red" : "Gray";
                        //Color5 = WeightLoss == "True" ? "Red" : "Gray";
                        //Color6 = TotalScore != String.Empty && Convert.ToInt32(TotalScore) >= 5 ? "Red" : "Black";
                        PlaceHolderList.Controls.Add(new LiteralControl("<tr style=\"font-size:13px; height:40px;\" class=\"" + CssClass + "\" onmouseover=\"this.className='RowOver'\" onmouseout=\"this.className='" + CssClass + "'\">"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + product_id + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + inExtData + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + remark_b + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + stunum + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + stuname + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + sex + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + birthday + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + jiguan + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + address + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + grade + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + schoolname + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + pname + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + phone + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + amount + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + Is_zhusu + "(" + zhusu_num + "周)" + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + centerSeqId + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + noticetime + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + remark_a + "</span></td>"));
                        PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + introducer + "</span></td>"));
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
            string product = ddlStatus.SelectedValue;

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
            DataColumn dc1 = new DataColumn("支付项目ID", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("支付项目", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("开课时间", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("学号", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("学生姓名", Type.GetType("System.String"));
            DataColumn dc6 = new DataColumn("性别", Type.GetType("System.String"));
            DataColumn dc7 = new DataColumn("生日", Type.GetType("System.String"));
            DataColumn dc8 = new DataColumn("国籍/籍贯", Type.GetType("System.String"));
            DataColumn dc9 = new DataColumn("居住地", Type.GetType("System.String"));
            DataColumn dc10 = new DataColumn("现就读年级", Type.GetType("System.String"));
            DataColumn dc11 = new DataColumn("现就读学校", Type.GetType("System.String"));
            DataColumn dc12 = new DataColumn("家长姓名", Type.GetType("System.String"));
            DataColumn dc13 = new DataColumn("联系电话", Type.GetType("System.String"));
            DataColumn dc14 = new DataColumn("实付金额", Type.GetType("System.String"));
            DataColumn dc15 = new DataColumn("是否住宿", Type.GetType("System.String"));
            DataColumn dc16 = new DataColumn("交易单号", Type.GetType("System.String"));
            DataColumn dc17 = new DataColumn("实付时间", Type.GetType("System.String"));
            DataColumn dc18 = new DataColumn("交易状态", Type.GetType("System.String"));
            DataColumn dc19 = new DataColumn("介绍人", Type.GetType("System.String"));

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
            dt_upload.Columns.Add(dc13);
            dt_upload.Columns.Add(dc14);
            dt_upload.Columns.Add(dc15);
            dt_upload.Columns.Add(dc16);
            dt_upload.Columns.Add(dc17);
            dt_upload.Columns.Add(dc18);
            dt_upload.Columns.Add(dc19);



            string sqlstr = "select b.orderInfo,b.inExtData,b.remark as remark_b,a.amount,a.centerSeqId,a.remark as remark_a,a.noticetime,b.mchSeqNo  from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a on a.merchantSeq=b.merchantSeq where a.id is not null  and a.refundtime is null and a.id>22 and orderInfo='线上校友专区'  order by a.noticetime asc";
            DataTable dt = lw.GetDataTable(sqlstr);
            DataView myView = dt.DefaultView;

            foreach (DataRowView myRow in myView)
            {
                string noticetime = Convert.ToDateTime(myRow["noticetime"].ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
                string orderInfo = myRow["orderInfo"].ToString().Trim();
                string inExtData = myRow["inExtData"].ToString().Trim();
                string remark_b = myRow["remark_b"].ToString().Trim();
                string amount = Convert.ToString(Convert.ToInt32(myRow["amount"].ToString().Trim()) * 0.01);
                string centerSeqId = myRow["centerSeqId"].ToString().Trim();
                string remark_a = myRow["remark_a"].ToString().Trim();
                string mchSeqNo = myRow["mchSeqNo"].ToString().Trim();

                string sqlstr1 = "SELECT a.*,b.product_id as Bproduct_id,b.product_time  FROM [db_forminf].[dbo].[summer_indent] a left join [db_forminf].[dbo].[product] b on a.product_id = b.id where b.shop_id='S0000029' and  a.mchSeqNo = '" + mchSeqNo + "' ";
                DataTable dt1 = lw.GetDataTable1(sqlstr1);
                DataView myView1 = dt1.DefaultView;


                if (ddlStatus.SelectedValue != "aaa")
                {
                    RowFilter1 = " Bproduct_id = '" + ddlStatus.SelectedValue + "' ";
                }

                if (txtKeyword.Text != String.Empty)
                {
                    if (RowFilter1 != String.Empty)
                    {
                        RowFilter1 += " AND ";
                    }
                    RowFilter1 += " stuname like '%" + KeyWord + "%' OR phone like '%" + KeyWord + "%' OR mchSeqNo like '%" + KeyWord + "%' ";
                    //myView.RowFilter = string.Format("EmployeeID = '{0}' OR Name like '%{1}%'", KeyWord, KeyWord);
                }
                myView1.RowFilter = RowFilter1;
                foreach (DataRowView myRow1 in myView1)
                {
                    //if (i >= StartNum && i <= EndNum)
                    //{
                    string stunum = myRow1["stunum"].ToString().Trim();
                    string stuname = myRow1["stuname"].ToString().Trim();
                    string sex = myRow1["sex"].ToString().Trim();
                    string jiguan = myRow1["jiguan"].ToString().Trim();
                    string address = myRow1["address"].ToString().Trim();
                    string grade = myRow1["grade"].ToString().Trim();
                    string schoolname = myRow1["schoolname"].ToString().Trim();
                    string pname = myRow1["pname"].ToString().Trim();
                    string phone = myRow1["phone"].ToString().Trim();
                    string product_id = myRow1["Bproduct_id"].ToString().Trim();
                    string introducer = myRow1["introducer"].ToString().Trim();
                    string Is_zhusu = myRow1["Is_zhusu"].ToString().Trim();
                    string zhusu_num = myRow1["zhusu_num"].ToString().Trim();
                    string birthday = myRow1["birthday"].ToString().Trim();
                    //    CssClass = i % 2 == 0 ? "RowOne" : "RowTwo";
                    //    //Color1 = CoughtTwoWeek == "True" ? "Red" : "Gray";
                    //    //Color2 = CoughtWithSputum == "True" ? "Red" : "Gray";
                    //    //Color3 = FeelChestPains == "True" ? "Red" : "Gray";
                    //    //Color4 = NoAppetite == "True" ? "Red" : "Gray";
                    //    //Color5 = WeightLoss == "True" ? "Red" : "Gray";
                    //    //Color6 = TotalScore != String.Empty && Convert.ToInt32(TotalScore) >= 5 ? "Red" : "Black";
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<tr style=\"font-size:13px; height:40px;\" class=\"" + CssClass + "\" onmouseover=\"this.className='RowOver'\" onmouseout=\"this.className='" + CssClass + "'\">"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + product_id + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + inExtData + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + remark_b + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + stunum + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + stuname + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + sex + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + birthday + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + jiguan + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + address + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + grade + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + schoolname + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + pname + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + phone + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + amount + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + Is_zhusu + "(" + zhusu_num + "周)" + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + centerSeqId + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + noticetime + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + remark_a + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:#117ED0;\">" + introducer + "</span></td>"));
                    //    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color1 + ";\">" + CoughtTwoWeek + "</span></td>"));
                    //    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color2 + ";\">" + CoughtWithSputum + "</td>"));
                    //    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color3 + ";\">" + FeelChestPains + "</span></td>"));
                    //    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color4 + ";\">" + NoAppetite + "</span></td>"));
                    //    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color5 + ";\">" + WeightLoss + "</span></td>"));
                    //    //PlaceHolderList.Controls.Add(new LiteralControl("<td align=\"center\"><span style=\"color:" + Color6 + ";\">" + TotalScore + "</span></td>"));
                    //    PlaceHolderList.Controls.Add(new LiteralControl("</tr>\n"));
                    //}

                    //i++;



                    DataRow drt = dt_upload.NewRow();
                    drt["支付项目ID"] = product_id;
                    drt["支付项目"] = inExtData;
                    drt["开课时间"] = remark_b;
                    drt["学号"] = stunum;
                    drt["学生姓名"] = stuname;
                    drt["性别"] = sex;
                    drt["生日"] = birthday;
                    drt["国籍/籍贯"] = jiguan;
                    drt["居住地"] = address;
                    drt["现就读年级"] = grade;
                    drt["现就读学校"] = schoolname;
                    drt["家长姓名"] = pname;
                    drt["联系电话"] = phone;
                    drt["实付金额"] = amount;
                    drt["是否住宿"] = Is_zhusu + "(" + zhusu_num + "周)";
                    drt["交易单号"] = centerSeqId;
                    drt["实付时间"] = noticetime;
                    drt["交易状态"] = remark_a;
                    drt["介绍人"] = introducer;

                    dt_upload.Rows.Add(drt);



                }


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
                    CreateExcel(dt_upload, "Alumni_" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + ".xls");
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


        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
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