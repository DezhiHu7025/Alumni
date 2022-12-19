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
    public partial class info : System.Web.UI.Page
    {
        string strCon = "Data Source=192.168.80.247;Database=db_forminf;Uid=StudentLifeDB;Pwd=Kcisp@StudentLifeDB";
        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected int id = 0;
        public static string ceate_ip = "";
        string KmchSeqNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                GetTaskContent(id);
                this.text_stu_name.Text = "（必填）";
                this.txt_country.Text = "（必填）";
                this.txt_add.Text = "（必填）";
                //this.txt_gread.Text = "（必填）";
                this.drp_gread.SelectedValue = "0";
                this.txt_school.Text = "（必填）";
                this.txt_parents.Text = "（必填）";
                this.txt_phone.Text = "（11位手机号码）";
                this.txt_introducer.Text = "（必填,若无介绍人请填'无'）";
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                this.btn_post.Visible = true;

                BindYear();
                BindMonth();
                BindDay();

                string userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (null == userHostAddress || userHostAddress == String.Empty)
                {
                    userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (null == userHostAddress || userHostAddress == String.Empty)
                {
                    userHostAddress = HttpContext.Current.Request.UserHostAddress;
                }
                //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
                if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
                {
                    ceate_ip = userHostAddress;
                }
                else
                {
                    ceate_ip = "127.0.0.1";
                }
                
            }
            
        }
        protected void BindYear()
        {
            drp_year.Items.Clear();
            int startYear = DateTime.Now.Year - 15;
            int currentYear = DateTime.Now.Year;
            for (int i = startYear; i <= currentYear; i++)
            {
                drp_year.Items.Add(new ListItem(i.ToString()));
            }
            drp_year.SelectedValue = currentYear.ToString();
        }
        protected void BindMonth()
        {
            drp_month.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                drp_month.Items.Add(i.ToString());
            }
        }
        protected void BindDay()
        {
            drp_day.Items.Clear();
            string year = drp_year.SelectedValue;
            string month = drp_month.SelectedValue;
            int days = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));
            for (int i = 1; i <= days; i++)
            {
                drp_day.Items.Add(i.ToString());
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
        }

        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        protected void GetTaskContent(int ida)
        {

            string product_name, product_time, fee;
            string sqlstr = "SELECT * FROM [db_forminf].[dbo].[product] where shop_id='S0000029' and  (Is_inner ='Z' OR Is_inner = 'N') AND Is_open = 'Y'  and id =" + ida;
            DataSet myViewDate = lw.ReturnDataSet(sqlstr, "product");
            if (myViewDate.Tables[0].Rows.Count > 0)
            {
                product_name = myViewDate.Tables[0].Rows[0]["product_name"].ToString().Trim();
                product_time = myViewDate.Tables[0].Rows[0]["product_time"].ToString().Trim();
                fee = Convert.ToString(float.Parse(myViewDate.Tables[0].Rows[0]["fee"].ToString().Trim()) * 0.01);
                id = Convert.ToInt32(myViewDate.Tables[0].Rows[0]["id"].ToString().Trim());
                this.lbl_course.Text = product_name;
                this.lbl_time.Text = product_time;
                this.lbl_fee.Text = fee;
                this.HiddenField1.Value =myViewDate.Tables[0].Rows[0]["id"].ToString().Trim();
                this.HiddenField2.Value = fee;
                this.HiddenField3.Value = myViewDate.Tables[0].Rows[0]["num_max"].ToString().Trim();
                this.HiddenField4.Value = myViewDate.Tables[0].Rows[0]["product_name"].ToString().Trim();
                this.HiddenField7.Value = myViewDate.Tables[0].Rows[0]["product_id"].ToString().Trim();

                ListItem myItem1 = new ListItem();
                myItem1.Text = "----不住宿(点击选择)----";
                myItem1.Value = "aaa";
                dbl_Iszhusu.Items.Add(myItem1);
                //该课程支持住宿
                if (Convert.ToChar(myViewDate.Tables[0].Rows[0]["Is_zhusu"].ToString().Trim()) == 'Y')
                {
                    this.Iszhusu.Visible = true;

                    //绑定droplist
                 
                    for (int cc = 0; cc < Convert.ToInt32(myViewDate.Tables[0].Rows[0]["zhusu_Maxnum"].ToString().Trim()); cc++)
                    {
                        int bb = cc + 1;
                        ListItem myItem = new ListItem();
                        myItem.Text = Convert.ToString(bb) + "周";
                        //这里的住宿费以元为单位
                        //myItem.Value = Convert.ToString(bb * 0.01);
                        myItem.Value = Convert.ToString(bb * float.Parse(myViewDate.Tables[0].Rows[0]["zhusu_fee"].ToString().Trim()));
                        dbl_Iszhusu.Items.Add(myItem);
                    }
                }
                else
                {
                    this.Iszhusu.Visible = false;
                }
            }
            else
            {

                Response.Write("<script language=javascript>alert('该课程报名已满！');window.location = 'course.aspx';</script>");
            }
        }

        //是否住宿,显示的金额有个BUG
        protected void dbl_Iszhusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dbl_Iszhusu.SelectedValue == "aaa")
            {
                this.lbl_fee.Text = Convert.ToString(this.HiddenField2.Value).Trim();
            }
            else
            {
                this.lbl_fee.Text = Convert.ToString(float.Parse(this.HiddenField2.Value.Trim()) + float.Parse(dbl_Iszhusu.SelectedValue));
            }

        }

        protected bool CheckIsMax()
        {
            bool result = false;
            string sqlstr1 = @"select COUNT(*) as ok_num
 from [KsisecPay].[dbo].[Pay_Before] b left join  [KsisecPay].[dbo].[Pay_After] a 
 on a.merchantSeq=b.merchantSeq  where a.id is not null  and a.refundtime is null and  a.remark='订单交易成功' and b.remark = '" + this.HiddenField7.Value.Trim() + "'";
            DataSet myViewDate1 = lw.ReturnDataSet2(sqlstr1, "Pay_Before");
            if (myViewDate1.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(myViewDate1.Tables[0].Rows[0]["ok_num"].ToString().Trim()) < Convert.ToInt32(this.HiddenField3.Value.Trim()))
                {
                    result = true;
                    return result;
                }
                else
                {
                    //如果报名人数已满，更新关闭课程
                    string sqlstr2 = "update  [db_forminf].[dbo].[product] set Is_open = 'N' where shop_id='S0000029' and  product_id = '" + this.HiddenField7.Value.Trim() + "'";
                    int aa = lw.EXECCommand(sqlstr2);
                    result = false;
                    return result;
                }
            }
            else
            {
                return result;
            }
        }

        protected void baoming_Click(object sender, EventArgs e)
        {

             
                if (text_stu_name.Text.Length == 0 || text_stu_name.Text == "（必填）")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！学生姓名必填！');</Script>");
                    return;
                }
                if (txt_country.Text.Length == 0 || txt_country.Text == "（必填）")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！国籍/籍贯必填！');</Script>");
                    return;
                }
                if (txt_add.Text.Length == 0 || txt_add.Text == "（必填）")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！请填写居住地！');</Script>");
                    return;
                }
            //判断此小学学生生日必须在区间2007.9.1~2012.8.31
                string Sshengri = drp_year.SelectedValue + "/" + drp_month.SelectedValue + "/" + drp_day.SelectedValue;
                string Saa = CompanyDate(Sshengri, "2008/9/1") ;
                string Sbb = CompanyDate(Sshengri, "2015/8/31");
                if (Saa == "小于" || Sbb == "大于")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！学生生日需要在2008年9月1日~2015年8月31日之间，若有特殊情况请咨询学校招生办！');</Script>");
                    return;
                }
                //if (txt_gread.Text.Length == 0 || txt_gread.Text == "（必填）")
                //{
                //    Response.Write("<Script Language=JavaScript>alert('新增失败！请填写学生现读年级！');</Script>");
                //    return;
                //}
                if (Convert.ToInt32(this.drp_gread.SelectedValue)== 0)
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！请选择学生现读年级，一年级G1~五年级G5！，若有特殊情况请咨询学校招生办！');</Script>");
                    return;
                }
                if (txt_school.Text.Length == 0 || txt_school.Text == "（必填）")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！请填写学生现读学校！');</Script>");
                    return;
                }
                if (txt_school.Text.Contains("康桥") || txt_school.Text.Contains("康橋"))
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！若您现读学校是康桥学校，请回到首页选择校内家长登录报名！');</Script>");
                    return;
                }
                if (txt_parents.Text.Length == 0 || txt_parents.Text == "（必填）")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！请填写家长姓名！');</Script>");
                    return;
                }
                if (txt_phone.Text.Length == 0 || txt_phone.Text == "（11位手机号码）")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！请填写家长手机号码！');</Script>");
                    return;
                }
                if (txt_phone.Text.Length != 11)
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！手机号码应为11位！');</Script>");
                    return;
                }
                if (txt_introducer.Text.Length == 0 || txt_introducer.Text == "（必填,若无介绍人请填'无'）")
                {
                    Response.Write("<Script Language=JavaScript>alert('新增失败！请填写介绍人，若无介绍人请填写‘无’！');</Script>");
                    return;
                }

                if (CheckIsMax())
                {
                    if (DoAdd())
                    {
                        //Response.Write("<Script Language=JavaScript>alert('新增成功！');</Script>");
                        //生成订单
                        //Response.Redirect("summer_ok.aspx");
                        if (DoSeparate())
                        {
                            string url = string.Format("{0}?no={1}", "http://school.kcistz.org.cn/Kweixinorder/weixinorderpc.aspx", KmchSeqNo);
                            Response.Redirect(url);
                            //string url = string.Format("{0}?no={1}", "http://school.kcisec.com/weixinorder/weixinorderpc.aspx", KmchSeqNo);
                            //Response.Redirect(url);
                        }
                        else
                        {
                            Response.Write("<Script Language=JavaScript>alert('DoSeparate新增失败！');</Script>");
                        }

                    }
                    else
                    {
                        Response.Write("<Script Language=JavaScript>alert('DoAdd新增失败！');</Script>");
                    }
                }
                else
                {
                    
                    Response.Write("<script language=javascript>alert('该课程报名已满！');window.location = 'course.aspx';</script>");
                
                }

        }
        public string  CompanyDate(string dateStr1, string dateStr2)
        {
            //将日期字符串转换为日期对象
            DateTime t1 = Convert.ToDateTime(dateStr1);
            DateTime t2 = Convert.ToDateTime(dateStr2);
            //通过DateTIme.Compare()进行比较（）
            int compNum = DateTime.Compare(t1, t2);
            string msg = "";
            //t1> t2
            if (compNum > 0)
            {
                msg = "大于";
            }
            //t1= t2
            if (compNum == 0)
            {
                msg = "等于";
            }
            //t1< t2
            if (compNum < 0)
            {
                msg = "小于";
            }
            return msg;
        }

        protected bool DoAdd()
        {
            this.btn_post.Visible = false; 
            KmchSeqNo = "WCB" + Utils.Nmrandom();
            bool result = false;
            string InsertStr = "";
            InsertStr = @"insert into summer_indent(addtime,stuname,sex,jiguan,address,grade,schoolname,pname,
phone,mchSeqNo,product_id,product_id_bianhao,product_name,Is_inner,num,fee,Is_zhusu,zhusu_num,zhusu_fee,total_fee,ip,introducer,ispay,birthday)
values(@addtime,@stuname,@sex,@jiguan,@address,@grade,@schoolname,@pname,@phone,@mchSeqNo,@product_id,@product_id_bianhao,@product_name,'N',
1,@fee,@Is_zhusu,@zhusu_num,@zhusu_fee,@total_fee,@ip,@introducer,@ispay,@birthday)";
            SqlConnection con = new SqlConnection(strCon);
            con.Open();//打开数据库连接
            SqlCommand cmd = new SqlCommand(InsertStr, con);
            SqlParameter p1 = new SqlParameter("@addtime", DateTime.Now);
            SqlParameter p2 = new SqlParameter("@stuname", text_stu_name.Text.Trim());
            SqlParameter p3 = new SqlParameter("@sex", RadioButton1.Checked ? "M" : "F");
            SqlParameter p4 = new SqlParameter("@jiguan", txt_country.Text.Trim());
            SqlParameter p5 = new SqlParameter("@address", txt_add.Text.Trim());
            //SqlParameter p6 = new SqlParameter("@grade", txt_gread.Text.Trim());
            SqlParameter p6 = new SqlParameter("@grade", this.drp_gread.SelectedValue);
            SqlParameter p7 = new SqlParameter("@schoolname", txt_school.Text.Trim());
            SqlParameter p8 = new SqlParameter("@pname", txt_parents.Text.Trim());
            SqlParameter p9 = new SqlParameter("@phone", txt_phone.Text.Trim());
            SqlParameter p10 = new SqlParameter("@mchSeqNo",KmchSeqNo);
            SqlParameter p11 = new SqlParameter("@product_id", this.HiddenField1.Value);
            SqlParameter p12 = new SqlParameter("@product_name", this.lbl_course.Text);
            SqlParameter p17 = new SqlParameter("@product_id_bianhao", this.HiddenField7.Value);
            SqlParameter p13 = new SqlParameter("@fee", Convert.ToString(float.Parse(this.lbl_fee.Text.Trim()) * 100));
            SqlParameter p18 = new SqlParameter("@Is_zhusu","N");
            SqlParameter p19 = new SqlParameter("@zhusu_num","0");
            SqlParameter p20 = new SqlParameter("@zhusu_fee","");

            if (dbl_Iszhusu.SelectedValue.Trim() != "aaa")
            {
                p18 = new SqlParameter("@Is_zhusu", "Y");
                p19 = new SqlParameter("@zhusu_num", Convert.ToInt32(dbl_Iszhusu.SelectedItem.Text.Trim().Substring(0, 1)));
                p20 = new SqlParameter("@zhusu_fee", dbl_Iszhusu.SelectedValue.Trim());
            }

            SqlParameter p14 = new SqlParameter("@total_fee", Convert.ToString(float.Parse(this.lbl_fee.Text.Trim()) * 100));
            SqlParameter p15 = new SqlParameter("@ip",ceate_ip);
            SqlParameter p21 = new SqlParameter("@introducer", txt_introducer.Text.Trim());
            SqlParameter p22 = new SqlParameter("@ispay","N");
            SqlParameter p23 = new SqlParameter("@birthday", drp_year.SelectedValue + "/" + drp_month.SelectedValue + "/" + drp_day.SelectedValue);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);
            cmd.Parameters.Add(p10);
            cmd.Parameters.Add(p11);
            cmd.Parameters.Add(p12);
            cmd.Parameters.Add(p13);
            cmd.Parameters.Add(p14);
            cmd.Parameters.Add(p15);

            cmd.Parameters.Add(p17);
            cmd.Parameters.Add(p18);
            cmd.Parameters.Add(p19);
            cmd.Parameters.Add(p20);
            cmd.Parameters.Add(p21);
            cmd.Parameters.Add(p22);
            cmd.Parameters.Add(p23);
            try
            {
                cmd.ExecuteNonQuery();//调用Le类中的EXECCommand方法,执行SQL语句
                con.Close();//关闭数据库连接
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }



        protected bool DoSeparate()
        {
            bool result = false;
                string InsertStr2 = "";
                InsertStr2 = @"insert into [db_forminf].[dbo].[order]([addtime],[mchSeqNo],[amount],[orderInfo],[remark],[inExtData])
values(@addtime,@mchSeqNo,@amount,@orderInfo,@remark,@inExtData)";
                SqlConnection con = new SqlConnection(strCon);
                con.Open();//打开数据库连接
                SqlCommand cmd1 = new SqlCommand(InsertStr2, con);
                SqlParameter p1 = new SqlParameter("@addtime", DateTime.Now);
                SqlParameter p2 = new SqlParameter("@mchSeqNo",KmchSeqNo);
                SqlParameter p3 = new SqlParameter("@amount", Convert.ToString(float.Parse(this.lbl_fee.Text.Trim()) * 100));
                SqlParameter p4 = new SqlParameter("@orderInfo", "线上校友专区");
                SqlParameter p5 = new SqlParameter("@remark", this.HiddenField7.Value);
                if (dbl_Iszhusu.SelectedValue.Trim() != "aaa")
                {
                    p5 = new SqlParameter("@remark", this.lbl_time.Text + " 住宿" + dbl_Iszhusu.SelectedItem.Text.Trim());
                }
                SqlParameter p6 = new SqlParameter("@inExtData", this.lbl_course.Text);
                cmd1.Parameters.Add(p1);
                cmd1.Parameters.Add(p2);
                cmd1.Parameters.Add(p3);
                cmd1.Parameters.Add(p4);
                cmd1.Parameters.Add(p5);
                cmd1.Parameters.Add(p6);
                try
                {
                    cmd1.ExecuteNonQuery();//调用Le类中的EXECCommand方法,执行SQL语句
                    con.Close();//关闭数据库连接
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