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
    public partial class UpdateOrder : System.Web.UI.Page
    {
        LeaveWord lw = new LeaveWord();//声明并且实例化一个对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlstr = "SELECT a.*,b.product_id as Bproduct_id,b.product_time  FROM [db_forminf].[dbo].[summer_indent] a left join [db_forminf].[dbo].[product] b on a.product_id = b.id where b.shop_id='S0000029' ";
            DataTable dt1 = lw.GetDataTable1(sqlstr);
            DataView myView1 = dt1.DefaultView;

            foreach (DataRowView myRow1 in myView1)
            {
                if (myRow1["ispay"].ToString().Trim() == "N")
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
                            }
                        }
               }
            }
        }
    }
}