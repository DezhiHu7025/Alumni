using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace KCIS_Biz
{
    //=====================================================================================================
    /// <summary>
    /// MS SQL相關class
    /// </summary>
    public class DBOperation
    {
        //--------------------------------------------------------------------
        /// <summary>
        /// 建立資料庫連接
        /// </summary>
        /// <param name="M_str_DBName">資料庫名稱</param>
        /// <returns>SqlConnection</returns>
        public SqlConnection getcon(string M_str_DBName)
        {
            string M_str_sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings[M_str_DBName].ToString();

            try
            {
                SqlConnection myCon = new SqlConnection(M_str_sqlcon);
                return myCon;
            }
            catch(SqlException)
            {
                return null;
            }
        }

        //--------------------------------------------------------------------
        /// <summary>
        /// 執行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL語句</param>
        public void GetCommand(string M_str_sqlstr, string M_str_DBName)
        {
            using (SqlConnection sqlcon = this.getcon(M_str_DBName))
            {
                try
                {
                    sqlcon.Open();
                    using (SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon))
                    {
                        sqlcom.ExecuteNonQuery();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        //--------------------------------------------------------------------
        /// <summary>
        /// 創建一個SqlDataReader
        /// </summary>
        /// <param name="M_str_sqlstr">SQL語句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader GetDataRead(string M_str_sqlstr, string M_str_DBName)
        {
            //using (SqlConnection sqlcon = this.getcon(M_str_DBName))
            //{
                SqlConnection sqlcon = this.getcon(M_str_DBName);
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
                SqlDataReader sqlread = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
                return sqlread;
            //}
        }

        //--------------------------------------------------------------------
        /// <summary>
        /// 創建一個Scalar對象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL語句</param>
        /// <returns>Scalar</returns>
        public object GetScalar(string M_str_sqlstr, string M_str_DBName)
        {
            using (SqlConnection sqlcon = this.getcon(M_str_DBName))
            {
                try
                {
                    sqlcon.Open();
                    using (SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon))
                    {
                        Object scalar = sqlcom.ExecuteScalar();
                        return scalar;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        //--------------------------------------------------------------------
        /// <summary>
        /// 創建一個DataSet對象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL語句</param>
        /// <param name="M_str_table">表名</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string M_str_sqlstr, string M_str_table, string M_str_DBName)
        {
            using (SqlConnection sqlcon = this.getcon(M_str_DBName))
            {
                try
                {
                    using (SqlDataAdapter sqlda = new SqlDataAdapter(M_str_sqlstr, sqlcon))
                    {
                        DataSet myds = new DataSet();
                        sqlda.Fill(myds, M_str_table);
                        return myds;
                    }
                }
                catch
                {
                    //
                    return null;
                }
            }
        }

        //--------------------------------------------------------------------
        /// <summary>
        /// 創建一個DataTable對象
        /// </summary>
        /// <param name="M_str_sqlstr"></param>
        /// <param name="M_str_DBName"></param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string M_str_sqlstr, string M_str_DBName)
        {
            using (SqlConnection sqlcon = this.getcon(M_str_DBName))
            {
                try
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlapt = new SqlDataAdapter(M_str_sqlstr, sqlcon);
                    using (DataSet ds = new DataSet())
                    {
                        sqlapt.Fill(ds, "myTable");
                        return ds.Tables["myTable"];
                    }
                }
                catch
                {
                    //
                    return null;
                }
            }
        }
        //--------------------------------------------------------------------
    }
    //=====================================================================================================
}
