using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace KCIS_Biz
{
    public class EIPOperation
    {
        DBOperation DBOp = new DBOperation();
        string sQueryString, DBName;

        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAccount">AD帳號</param>
        /// <returns>Degree</returns>
        public int getmyDegree(string myAccount)
        {
            DBName = "WebEIP3";
            sQueryString = "SELECT Isnull(A.Degree,'0') AS Degree FROM AFS_Account AS A WITH(NOLOCK) WHERE A.AccountID = '" + myAccount + "';";
            using (SqlDataReader dr = DBOp.GetDataRead(sQueryString, DBName))
            {
                if (dr.Read())
                {
                    try
                    {
                        return Convert.ToInt32(dr["Degree"]);
                    }
                    catch
                    {
                        //
                    }
                }
            }
            return 0;
        }

        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDeptID">EIP上部門代碼</param>
        /// <param name="myDegree">Degree</param>
        /// <returns></returns>
        public string getmyMasterID(string myDeptID, int myDegree)
        {
            string MasterList = String.Empty;
            int MaxDegree = myDegree;

            DBName = "WebEIP3";
            sQueryString = @"SELECT Replace(A.Skype,'C','') AS EmployeeID, A.Degree, A.FullName
                                        FROM AFS_Account AS A WITH(NOLOCK)
                                        WHERE A.Status = '1' AND A.DeptID = '" + myDeptID + "' AND A.Degree > " + myDegree +
                                        "ORDER BY A.Degree ASC";
            using (SqlDataReader dr = DBOp.GetDataRead(sQueryString, DBName))
            {
                string EmployeeID, FullName;
                while (dr.Read())
                {
                    MaxDegree = Convert.ToInt32(dr["Degree"]);
                    EmployeeID = dr["EmployeeID"].ToString().Trim();
                    FullName = dr["FullName"].ToString().Trim();

                    //MasterList += FullName + ";";
                    MasterList += EmployeeID + ";";
                }
            }

            if (myDeptID.Length == 4)   //到該部門最上層就停止搜尋
            {
                return MasterList == String.Empty ? MasterList : MasterList.Substring(0, MasterList.Length - 1);
            }
            else
            {
                sQueryString = "SELECT ParentDeptID FROM AFS_Dept WITH(NOLOCK) WHERE DeptID = '" + myDeptID + "'";
                using (SqlDataReader dr = DBOp.GetDataRead(sQueryString, DBName))
                {
                    string ParentDeptID;

                    if (dr.Read())
                    {
                        ParentDeptID = dr["ParentDeptID"].ToString().Trim();

                        return MasterList + getmyMasterID(ParentDeptID, MaxDegree);
                    }
                    else
                    {
                        //找不到上層代碼 (理論上不會發生)
                        return MasterList == String.Empty ? MasterList : MasterList.Substring(0, MasterList.Length - 1);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDeptID">EIP上部門代碼</param>
        /// <param name="myDegree">Degree</param>
        /// <returns></returns>
        public string getmyMasterIDToQSPrincipal(string myDeptID, int myDegree)
        {
            string MasterList = String.Empty;
            int MaxDegree = myDegree;

            DBName = "WebEIP3";
            sQueryString = @"SELECT Replace(A.Skype,'C','') AS EmployeeID, A.Degree, A.FullName
                                        FROM AFS_Account AS A WITH(NOLOCK)
                                        WHERE A.Status = '1' AND A.DeptID = '" + myDeptID + "' AND A.Degree > " + myDegree +
                                        "ORDER BY A.Degree ASC";
            using (SqlDataReader dr = DBOp.GetDataRead(sQueryString, DBName))
            {
                string EmployeeID, FullName;
                while (dr.Read())
                {
                    MaxDegree = Convert.ToInt32(dr["Degree"]);
                    EmployeeID = dr["EmployeeID"].ToString().Trim();
                    FullName = dr["FullName"].ToString().Trim();

                    //MasterList += FullName + ";";
                    MasterList += EmployeeID + ";";
                }
            }

            if (myDeptID == "6011")   //到青山校長室就停止
            {
                return MasterList == String.Empty ? MasterList : MasterList.Substring(0, MasterList.Length - 1);
            }
            else
            {
                sQueryString = "SELECT ParentDeptID FROM AFS_Dept WITH(NOLOCK) WHERE DeptID = '" + myDeptID + "'";
                using (SqlDataReader dr = DBOp.GetDataRead(sQueryString, DBName))
                {
                    string ParentDeptID;

                    if (dr.Read())
                    {
                        ParentDeptID = dr["ParentDeptID"].ToString().Trim();

                        return MasterList + getmyMasterIDToQSPrincipal(ParentDeptID, MaxDegree);
                    }
                    else
                    {
                        //找不到上層代碼 (理論上不會發生)
                        return MasterList == String.Empty ? MasterList : MasterList.Substring(0, MasterList.Length - 1);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 從WebEIP上抓取(副)主任級的[ AFS_Account ]資料
        /// </summary>
        /// <returns></returns>
        public DataTable getQSDirectorTable()
        {
            DBName = "WebEIP3";
            sQueryString = @"SELECT *
                                        FROM AFS_Account WITH(NOLOCK)
                                        WHERE Degree >= 4
                                        AND DeptID IN (SELECT DeptID FROM AFS_Dept WITH(NOLOCK) WHERE ParentDeptID='6011')";
            return DBOp.GetDataTable(sQueryString, DBName);
        }

        //-------------------------------------------------------------------------------------------
    }
}
