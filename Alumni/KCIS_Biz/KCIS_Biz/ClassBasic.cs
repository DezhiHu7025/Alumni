using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Security;
using System.Web.UI;
using System.IO;

namespace KCIS_Biz
{
    [Serializable]
    public struct Employee
    {
        public string Account;
        public string ID;
        public string IDteacherID;
        public string Name;
        public string Password;
        public string DepartmentID; //部門
        public string DeptCode; //部門代碼 (EIP資料)
        public string EMail;
        public bool IDTeacher; //是否為英文班老師 (E-Reader用)
        public string SchoolSystem;
        public string UniformSystem;
        public string LDAP;
        public bool Admin;  //判斷是否為系統管理員
        public int Degree; //判斷職級 (對照EIP上Title表)
    }

    public class BasicOperation
    {
        DBOperation DBOp = new DBOperation();
        string sQueryString, DBName;

        //----------------------------------------------------------------------------------
        /// <summary>
        /// 寫錯誤記錄
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="UI"></param>
        /// <param name="Message"></param>
        public void WriteErrorLog(string EmployeeID, string UI, string Message)
        {
            try
            {
                sQueryString = "EXEC L0_InsertErrorLog '" + EmployeeID + "','" + UI + "','" + Message + "'";
                DBName = "KCIS_Admin";
                DBOp.GetCommand(sQueryString, DBName);
            }
            catch
            {
                //
            }
        }

        public void WriteOperationLog(string EmployeeID, string Operation)
        {
            sQueryString = "EXEC L0_InsertOperationLog '" + EmployeeID + "','" + Operation + "'";
            DBName = "KCIS_Admin";
            try
            {
                DBOp.GetCommand(sQueryString, DBName);
            }
            catch
            {
                //
            }
        }

        public int GetEmployeeData(string EmployeeID, DataTable Emps)
        {
            try
            {
                string expression = "EmployeeID = '" + EmployeeID.Trim() + "'";
                return (Emps.Select(expression)).Length;
            }
            catch
            {
                return 0;
            }
        }

        //清除所有Online使用者
        public void OnlineStaffClearAll()
        {
            DBName = "KCIS_Admin";
            sQueryString = "EXEC A0_DeleteAllOnlineStaff";
            try
            {
                DBOp.GetCommand(sQueryString, DBName);
            }
            catch
            {
                //
            }
        }

        //清除某Online使用者
        public void OnlineStaffRemove(string EmployeeID)
        {
            DBName = "KCIS_Admin";
            sQueryString = "EXEC A0_DeleteOnlineStaff '" + EmployeeID + "'";
            try
            {
                DBOp.GetCommand(sQueryString, DBName);
            }
            catch
            {
                //
            }
        }

        //加入某Online使用者
        public void OnlineStaffAdd(string EmployeeID)
        {
            OnlineStaffRemove(EmployeeID);
            DBName = "KCIS_Admin";
            sQueryString = "EXEC A0_InsertOnlineStaff '" + EmployeeID + "'";
            try
            {
                DBOp.GetCommand(sQueryString, DBName);
            }
            catch
            {
                //
            }
        }

        //清除系統訊息
        public void SystemMessageClearAll()
        {
            DBName = "KCIS_Admin";
            sQueryString = "EXEC G0_DeleteSystemMsg";
            try
            {
                DBOp.GetCommand(sQueryString, DBName);
            }
            catch
            {
                throw;
            }
        }

        //產生系統訊息
        public void SystemMessageAdd(string SystemMessage)
        {
            DBName = "KCIS_Admin";
            sQueryString = "EXEC G0_InsertSystemMsg '" + SystemMessage + "'";
            try
            {
                DBOp.GetCommand(sQueryString, DBName);
            }
            catch
            {
                throw;
            }
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 判斷使用者是否能切換校部的權限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="SchoolSystem"></param>
        /// <returns></returns>
        public bool HasUniformSystemAuthority(string ID, string SchoolSystem)
        {
            //sQueryString = "EXEC S1_GetUniformSystemAuthority '" + ID + "'";
            ////要用SchoolSystem去判斷
            //DBName = "DB0" + SchoolSystem + "_DSA";
            //try
            //{
            //    using (SqlDataReader dr = DBOp.GetDataRead(sQueryString, DBName))
            //    {
            //        if (dr.HasRows)
            //            return true;
            //        else
            //            return false;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}
            return false;
        }

        //------------------------------------------------------------------------------
        public string QueryEncode(string InputString)
        {
            try
            {
                return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(InputString)).Replace("+", "%2B");
            }
            catch
            {
                return String.Empty;
            }
        }

        //------------------------------------------------------------------------------
        public string QueryDecode(string InputString)
        {
            try
            {
                return System.Text.Encoding.Default.GetString(Convert.FromBase64String(InputString.Replace("%2B", "+")));
            }
            catch
            {
                return String.Empty;
            }
        }

        //------------------------------------------------------------------------------
        //還原被轉換過的的String
        public string RestoreSafeString(string InputString)
        {
            InputString = InputString.Trim();
            InputString = InputString.Replace("''", "'");
            InputString = InputString.Replace("＜", "<");
            InputString = InputString.Replace("＞", ">");
            InputString = InputString.Replace("-", "--");

            return InputString;
        }

        //------------------------------------------------------------------------------
        //清除可能造成Injection的String
        public string SafeString(string InputString)
        {
            InputString = InputString.Trim();
            InputString = InputString.Replace("'", "''");
            InputString = InputString.Replace("<", "＜");
            InputString = InputString.Replace(">", "＞");
            InputString = InputString.Replace("--", "-");

            return InputString;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        /// 編碼轉換
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public string CP850ToBIG5(string sourceStr)
        {
            return Encoding.GetEncoding("big5").GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(sourceStr));
        }
        //------------------------------------------------------------------------------
    }

    //--------------------------------------------------------------------------------------
    //
    //   用來產生Script碼
    //
    //--------------------------------------------------------------------------------------
    public class Scripts
    {
        public string GoHomeScript()
        {
            string HomeScript = "top.location.href='../Login.aspx';";
            return GenScript(HomeScript);
        }

        //中文
        public string DBErrorMsg()
        {
            string DBErrorMsg = "alert('【錯誤】資料庫連線錯誤！');";
            return GenScript(DBErrorMsg);
        }

        public string SuccessMsg(string Msg)
        {
            string SuccessMsg = "alert('【完成】" + Msg + "');";
            return GenScript(SuccessMsg);
        }

        public string ErrorMsg(string Msg)
        {
            string ErrorMsg = "alert('【錯誤】" + Msg + "');";
            return GenScript(ErrorMsg);
        }

        public string AlertMsg(string Msg)
        {
            string ErrorMsg = "alert('【注意】" + Msg + "');";
            return GenScript(ErrorMsg);
        }

        //英文
        public string SuccessEMsg(string Msg)
        {
            string SuccesseMsg = "alert('【Success】" + Msg + "');";
            return GenScript(SuccesseMsg);
        }

        public string ErrorEMsg(string Msg)
        {
            string ErrorMsg = "alert('【Error】" + Msg + "');";
            return GenScript(ErrorMsg);
        }

        public string AlertEMsg(string Msg)
        {
            string ErrorMsg = "alert('【Notice】" + Msg + "');";
            return GenScript(ErrorMsg);
        }

        public string GenScript(string instructions)
        {
            StringBuilder Sb = new StringBuilder();

            Sb.Append("<script type=\"text/javascript\">");
            Sb.Append(instructions);
            Sb.Append("</script>");
            return Sb.ToString();
        }

        //======= New ======
        public void aGoHomeScript(Page page)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "GoHomePage", "top.location.href='../Login.aspx';", true);
        }

        public void aDBErrorMsg(Page page)
        {
            string DBErrorMsg = "alert('【錯誤】資料庫連線錯誤！');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "myScript", DBErrorMsg, true);
        }

        public void aSuccessMsg(Page page, string Msg)
        {
            string SuccessMsg = "alert('【完成】" + Msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "myScript", SuccessMsg, true);
        }

        public void aErrorMsg(Page page, string Msg)
        {
            string ErrorMsg = "alert('【錯誤】" + Msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "myScript", ErrorMsg, true);
        }

        public void aAlertMsg(Page page, string Msg)
        {
            string ErrorMsg = "alert('【注意】" + Msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "myScript", ErrorMsg, true);
        }

        public void aGenScript(Page page, string  instructions)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "myScript", instructions, true);
        }

        public void ResizeParent(Page page)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "JustResize", "setPIH();", true);
        }
    }

    //--------------------------------------------------------------------------------------
    //
    //   字串處理
    //
    //--------------------------------------------------------------------------------------
    public class StringOperation
    {
        public bool IsLetterOrNumeric(string str)
        {
            System.Text.RegularExpressions.Regex myReg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return myReg.IsMatch(str);
        }

        public bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex myReg = new System.Text.RegularExpressions.Regex(@"^[0-9]+$");
            return myReg.IsMatch(str);
        }

        public bool IsLetter(string str)
        {
            System.Text.RegularExpressions.Regex myReg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
            return myReg.IsMatch(str);
        }

        public bool IsMail(string str)
        {
            System.Text.RegularExpressions.Regex myReg = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9_]+@[a-zA-Z0-9\._]+$");
            return myReg.IsMatch(str);
        }

        public bool IsDate(string str)
        {
            try
            {
                DateTime myDate = Convert.ToDateTime(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 16位：ComputeHash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string getMd5Method(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] myData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < myData.Length; i++)
            {
                sBuilder.Append(myData[i].ToString("x"));
            }
            return string.Format(sBuilder.ToString());
        }

        /// <summary>
        /// 32位加密：ComputeHash
        /// </summary>
        /// <param name="input"></param>
        /// <returns><</returns>
        public string getMd5Method2(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] myData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < myData.Length; i++)
            {
                sBuilder.Append(myData[i].ToString("x2"));
            }

            return string.Format(sBuilder.ToString());
        }

        /// <summary>
        /// 32位加密：直接使用HashPasswordForStoringInConfigFile
        /// </summary>
        /// <param name="input"></param>
        /// <returns><</returns>
        public string getMd5Method3(string input)
        {
            string myReturn = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");

            return string.Format(myReturn.ToString());
        }

        public string DESEncode(string source)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes("81958864");
            byte[] iv = Encoding.ASCII.GetBytes("88648864");
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);

            des.Key = key;
            des.IV = iv;
            string encrypt = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }


        public string DESDecode(string encrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes("81958864");
            byte[] iv = Encoding.ASCII.GetBytes("88648864");
            des.Key = key;
            des.IV = iv;

            if (encrypt == String.Empty || !encrypt.Contains("="))
            {
                return encrypt;
            }

            byte[] dataByteArray = Convert.FromBase64String(encrypt);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}
