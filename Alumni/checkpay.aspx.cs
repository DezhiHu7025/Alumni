using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Alumni
{
    public partial class checkpay : System.Web.UI.Page
    {
        protected int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
        }
    }
}