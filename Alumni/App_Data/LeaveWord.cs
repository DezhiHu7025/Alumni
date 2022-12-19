using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;//引入命名空间

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class LeaveWord
{
	public LeaveWord()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    #region  连接数据库
    /// <summary>
    /// 创建时间:2008-6-18
    /// 连接数据库
    /// </summary
    /// >
    /// 
    public SqlConnection getcon3()
    {
        string strCon = "Data Source=192.168.80.247;Database=Common;Uid=kcis_db;Pwd=db2008_kcis";
        SqlConnection sqlCon = new SqlConnection(strCon);
        return sqlCon;
    }
    public SqlConnection getcon()
    {
        string strCon = "Data Source=192.168.80.247;Database=db_forminf;Uid=kcis_db;Pwd=db2008_kcis";
        SqlConnection sqlCon = new SqlConnection(strCon);
        return sqlCon;
    }
    #endregion
    public SqlConnection getcon1()
    {
        string strCon = "Data Source=172.26.29.110;Database=KsisecPay;Uid=webapp;Pwd=db2008_kcis";
        SqlConnection sqlCon = new SqlConnection(strCon);
        return sqlCon;
    }

    #region  执行SQL语句，返回受影响的行数
    /// <summary>
    /// 创建时间:2008-5-18
    /// 执行SQL语句，返回受影响的行数
    /// 返回值：返回整数类型
    /// 参数：SqlStr 要执行的Sql语句
    /// </summary>
    public  int EXECCommand(string SqlStr)
    {
        int i;
        SqlConnection con =this.getcon();
        con.Open();//打开数据库连接
        SqlCommand amd = new SqlCommand(SqlStr, con);
        i = amd.ExecuteNonQuery();
        con.Close();//关闭数据库连接
        con.Dispose();
        return i;
    }
    #endregion

    #region  将字段绑定到DropDownList控件中
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是将字段绑定到DropDownList控件中
    /// 参数：DDL 要绑定的DropDownList控件名
    /// 参数：SqlStr 要执行的Sql语句
    /// 参数：DTF 要绑定的字段名称
    /// </summary>
    public void EXECDropDownList(DropDownList DDL, string SqlStr, string DTF)
    {
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        DDL.DataSource = ds;
        DDL.DataTextField = DTF;
        DDL.DataBind();
        con.Close();//关闭数据库连接
        con.Dispose();
    }
    #endregion

    #region 将数据绑定到GridView控件中
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是将数据绑定到GridView控件中
    /// 参数：gv 要绑定的GridView控件名
    /// 参数：SqlStr 要执行的Sql语句
    /// </summary>
    public void EXECGridView(GridView gv, string SqlStr)
    {
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        gv.DataSource = ds;
        gv.DataBind();
        con.Close();//关闭数据库连接
        con.Dispose();
    }
    #endregion

    #region 执行Sql语句并且返回数据集
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是执行Sql语句并且返回数据源的数据集
    /// 返回值：数据集DataSet
    /// 参数：SqlStr 执行的Sql语句
    /// 参数：tablename 数据表的名称
    /// </summary>
    /// <param name="SqlStr"></param>
    /// <param name="tablename"></param>
    /// <returns></returns>
    public DataSet ReturnDataSet(string SqlStr, string tablename)
    {
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds, tablename);
        con.Close();
        con.Dispose();
        return ds;
    }
    public DataSet ReturnDataSet2(string SqlStr, string tablename)
    {
        SqlConnection con = this.getcon1();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds, tablename);
        con.Close();
        con.Dispose();
        return ds;
    }


    public DataSet ReturnDataSet3(string SqlStr, string tablename)
    {
        SqlConnection con = this.getcon3();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds, tablename);
        con.Close();
        con.Dispose();
        return ds;
    }

    #endregion


    #region 将数据绑定到DataList控件中
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是将数据绑定到DataList控件中
    /// 参数：dl 要绑定的DataList控件的控件的名称
    /// 参数：SqlStr 执行的Sql语句
    /// 参数：DNK  要绑定的字段名称 
    /// </summary>
    /// <param name="dl"></param>
    /// <param name="SqlStr"></param>
    /// <param name="DNK"></param>
    public void EXECDataList(DataList dl, string SqlStr, string DNK)
    {
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        dl.DataSource = ds;
        dl.DataKeyField = DNK;
        dl.DataBind();
        con.Close();//关闭数据库连接
        con.Dispose();
    }
    #endregion

    #region 将数据绑定到DataList控件中
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是将数据绑定到DataList控件中
    /// 参数：dl 要绑定的DataList控件的控件的名称
    /// 参数：SqlStr 执行的Sql语句
    public void EXECDataList1(DataList dl, string SqlStr)
    {
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        dl.DataSource = ds;
        dl.DataBind();
        con.Close();//关闭数据库连接
        con.Dispose();
    }
    #endregion

    #region 截取字符串的长度
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是截取字符串的长度
    /// 返回类型：字符串类型
    /// 参数：sString 要截取的字符串
    /// 参数：nLeng 要截取的字符串的长度
    /// </summary>
    public string SubStr(string sString, int nLeng)
    {
        if (sString.Length <= nLeng)
        {
            return sString;
        }
        int nStrLeng = nLeng - 1;
        string sNewStr = sString.Substring(0, nStrLeng);
        sNewStr = sNewStr + "...";
        return sNewStr;
    }
    #endregion

    #region 将数据绑定到DataList控件中
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是将数据绑定到DataList控件中
    /// 参数：dl 要绑定的DataList控件的控件的名称
    /// 参数：SqlStr 执行的Sql语句
    public void EXECBindDataList(DataList dl, string SqlStr)
    {
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        SqlDataAdapter ada = new SqlDataAdapter(SqlStr, con);
        DataSet ds = new DataSet();
        ada.Fill(ds);
        dl.DataSource = ds;
        dl.DataBind();
        con.Close();//关闭数据库连接
        con.Dispose();
    }
    #endregion

    #region 返回数据表中记录的数目
    /// <summary>
    /// 创建时间：2008-5-18
    /// 该方法实现的是返回数据表中记录的数目
    /// 参数：SqlStr 要执行的Sql语句
    /// </summary>
    public int EXECuteScalar(string SqlStr)
    {
        int i;
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        SqlCommand com = new SqlCommand(SqlStr, con);
        i = Convert.ToInt32(com.ExecuteScalar());
        con.Close();//关闭数据库连接
        con.Dispose();
        return i;
    }
    #endregion


    #region 读取数据源进行读取
    /// <summary>
    /// 创建时间：2008-4-18
    /// 该方法实现的是读取表中的数据
    /// 参数：SqlStr 要执行的Sql语句
    /// </summary>
    /// <param name="SqlStr"></param>
    /// <returns></returns>
    public SqlDataReader ExceRead(string SqlStr)
    {
        SqlConnection con = this.getcon();
        con.Open();//打开数据库连接
        //创建一个SqlCommand对象
        SqlCommand cmd = new SqlCommand(SqlStr, con);
        SqlDataReader sdr = cmd.ExecuteReader();
        con.Close();
        con.Dispose();
        return sdr;
    }
    #endregion

    public void ReturnDataSet()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public DataTable GetDataTable(string sql)
    {
        SqlConnection conn = this.getcon1();
        conn.Open();//打开数据库连接
        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        conn.Close();//关闭数据库连接
        return dt;
    }
    public DataTable GetDataTable1(string sql)
    {
        SqlConnection conn = this.getcon();
        conn.Open();//打开数据库连接
        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        conn.Close();//关闭数据库连接
        return dt;
    }
}
