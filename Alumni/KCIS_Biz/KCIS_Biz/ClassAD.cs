using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Data;
using System.Data.SqlClient;

namespace KCIS_Biz
{
    public class ADOperation
    {
        //AD抓取資料用帳號 (改密碼後需同步更改)
        string AdminAccount = "administrator";
        string AdminPassword = "Kcisp@ssmis";

        //---------------------------------------------------------------------------------
        public DataTable getAccountEIPinfo(string LDAP)
        {
            string LDAP_STRING = "LDAP://" + LDAP;

            using (DataTable Emps = new DataTable())
            {
                Emps.Columns.Add(new DataColumn("givenName", typeof(String)));      //員編
                Emps.Columns.Add(new DataColumn("sAMAccountName", typeof(String)));     //AD帳號
                Emps.Columns.Add(new DataColumn("physicalDeliveryOfficeName", typeof(String)));
                DataRow EmpRow;

                using (DirectoryEntry entry = new DirectoryEntry(LDAP_STRING, AdminAccount, AdminPassword))
                {
                    DirectorySearcher adSeacher = new DirectorySearcher(entry);
                    DirectoryEntry userEntry;

                    adSeacher.Filter = "(&(objectCategory=person))";
                    SearchResultCollection userResultCollection = adSeacher.FindAll();
                    foreach (SearchResult userResult in userResultCollection)
                    {
                        EmpRow = Emps.NewRow();
                        userEntry = userResult.GetDirectoryEntry();
                        try
                        {
                            if (userEntry.Properties["givenName"] != null)
                            {
                                EmpRow["givenName"] = userEntry.Properties["givenName"].Value.ToString();

                                if (userEntry.Properties["sAMAccountName"] != null)
                                {
                                    EmpRow["sAMAccountName"] = userEntry.Properties["sAMAccountName"].Value.ToString();
                                }

                                if (userEntry.Properties["physicalDeliveryOfficeName"] != null)
                                {
                                    EmpRow["physicalDeliveryOfficeName"] = userEntry.Properties["physicalDeliveryOfficeName"].Value.ToString();
                                }
                                Emps.Rows.Add(EmpRow);
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                    return Emps;
                }
            }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LDAP"></param>
        /// <param name="EmployeeID">EmployeeID</param>
        /// <param name="UserData">physicalDeliveryOfficeName</param>
        /// <returns></returns>
        public bool modifyAccount(string LDAP, string EmployeeID, string UserData)
        {
            string LDAP_STRING = "LDAP://" + LDAP;

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(LDAP_STRING, AdminAccount, AdminPassword))
                {
                    object nativeObject = entry.NativeObject;
                    DirectorySearcher adSeacher = new DirectorySearcher(entry);
                    DirectoryEntry userEntry;

                    adSeacher.Filter = "(&(objectClass=user)(givenName=" + EmployeeID + "))";
                    userEntry = adSeacher.FindOne().GetDirectoryEntry();
                    if (userEntry != null)
                    {
                        userEntry.Properties["physicalDeliveryOfficeName"].Value = UserData;
                        userEntry.CommitChanges();

                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// 抓取AD帳號資料(自己)
        /// </summary>
        /// <param name="LDAP">LDAP</param>
        /// <param name="EmployeeID">員工帳號</param>
        /// <param name="EmployeePasswd">密碼</param>
        /// <returns>帳號資料</returns>
        public Employee getEmp(string LDAP, string Account, string EmployeePasswd)
        {
            string LDAP_STRING = "LDAP://" + LDAP;
            Employee Emp = new Employee();

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(LDAP_STRING, Account, EmployeePasswd))
                {
                    string FullName, Name;//DeptCode;
                    object nativeObject = entry.NativeObject;
                    DirectorySearcher adSeacher = new DirectorySearcher(entry);
                    DirectoryEntry userEntry;

                    adSeacher.Filter = "(&(objectClass=user)(sAMAccountName=" + Account + "))";
                    userEntry = adSeacher.FindOne().GetDirectoryEntry();

                    FullName = userEntry.Properties["cn"].Value.ToString();
                    Name = FullName.Contains('(') ? (FullName.Split('('))[0] : FullName;
                    //DeptCode=userEntry.Properties["Department"].Value.ToString();
                    //Emp.DeptCode = DeptCode;
                    Emp.Account = Account;
                    Emp.Name = Name;

                    if (userEntry.Properties["givenName"].Value != null)
                        Emp.ID = userEntry.Properties["givenName"].Value.ToString();
                    if (userEntry.Properties["mail"].Value != null)
                        Emp.EMail = userEntry.Properties["mail"].Value.ToString();

                    return Emp;
                }
            }
            catch (DirectoryServicesCOMException)
            {
                Emp.ID = String.Empty;
                Emp.Name = String.Empty;
                Emp.EMail = String.Empty;
                //Emp.DeptCode = String.Empty;
                return Emp;
            }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// 查詢AD使用者帳號資料
        /// </summary>
        /// <param name="LDAP"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="EmployeePasswd"></param>
        /// <param name="OtherEmployeeID"></param>
        /// <returns></returns>
        public DataTable getOtherEmps(string LDAP, string Account, string EmployeePasswd, string OtherEmployeeName)
        {
            string LDAP_STRING = "LDAP://" + LDAP;

            DataTable Emps = new DataTable();
            Emps.Columns.Add(new DataColumn("EmployeeID", typeof(String)));
            Emps.Columns.Add(new DataColumn("Name", typeof(String)));
            Emps.Columns.Add(new DataColumn("Account", typeof(String)));
            Emps.Columns.Add(new DataColumn("Department", typeof(String)));
            DataRow EmpRow;

            using (DirectoryEntry entry = new DirectoryEntry(LDAP_STRING, Account, EmployeePasswd))
            {
                string FullName, Name;
                DirectorySearcher adSeacher = new DirectorySearcher(entry);
                DirectoryEntry userEntry;

                adSeacher.Filter = "(&(objectCategory=person)(cn=*" + OtherEmployeeName + "*))";
                SearchResultCollection userResultCollection = adSeacher.FindAll();
                foreach (SearchResult userResult in userResultCollection)
                {
                    EmpRow = Emps.NewRow();
                    userEntry = userResult.GetDirectoryEntry();
                    FullName = userEntry.Properties["cn"].Value.ToString();
                    Name = FullName.Contains('(') ? (FullName.Split('('))[0] : FullName;

                    try
                    {
                        EmpRow["EmployeeID"] = userEntry.Properties["givenName"].Value.ToString();
                        EmpRow["Name"] = Name;
                        EmpRow["Department"] = userEntry.Properties["Department"].Value.ToString();
                        EmpRow["Account"] = userEntry.Properties["sAMAccountName"].Value.ToString();
                        Emps.Rows.Add(EmpRow);
                    }
                    catch
                    {
                        //
                    }
                }
                return Emps;
            }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// 抓取AD所有帳號資料
        /// </summary>
        /// <param name="LDAP">LDAP</param>
        /// <returns></returns>
        public DataTable getAllEmps(string LDAP)
        {
            string LDAP_STRING = "LDAP://" + LDAP;

            using (DataTable Emps = new DataTable())
            {
                Emps.Columns.Add(new DataColumn("EmployeeID", typeof(String)));
                Emps.Columns.Add(new DataColumn("Name", typeof(String)));
                Emps.Columns.Add(new DataColumn("Account", typeof(String)));
                Emps.Columns.Add(new DataColumn("Department", typeof(String)));
                Emps.Columns.Add(new DataColumn("DeptCode", typeof(String)));
                Emps.Columns.Add(new DataColumn("TitleCode", typeof(String)));
                Emps.Columns.Add(new DataColumn("EMail", typeof(String)));
                DataRow EmpRow;

                using (DirectoryEntry entry = new DirectoryEntry(LDAP_STRING, AdminAccount, AdminPassword))
                {

                    DirectorySearcher adSeacher = new DirectorySearcher(entry);
                    DirectoryEntry userEntry;

                    try
                    {
                        adSeacher.Filter = "(&(objectCategory=person))";
                        SearchResultCollection userResultCollection = adSeacher.FindAll();
                        foreach (SearchResult userResult in userResultCollection)
                        {
                            EmpRow = Emps.NewRow();
                            userEntry = userResult.GetDirectoryEntry();
                            try
                            {
                                int flags = (int)userEntry.Properties["userAccountControl"].Value;

                                if (Convert.ToBoolean(flags & 0x0002)) //帳號為Disable
                                {
                                    continue;
                                }

                                string EmployeeID = userEntry.Properties["givenName"].Value != null ? userEntry.Properties["givenName"].Value.ToString() : String.Empty;
                                string FullName = userEntry.Properties["cn"].Value != null ? userEntry.Properties["cn"].Value.ToString() : String.Empty;
                                string TitleCode = userEntry.Properties["title"].Value != null ? userEntry.Properties["title"].Value.ToString() : String.Empty;
                                string DeptCode = userEntry.Properties["department"].Value != null ? userEntry.Properties["department"].Value.ToString() : String.Empty;
                                string Description = userEntry.Properties["description"].Value != null ? userEntry.Properties["description"].Value.ToString() : String.Empty;
                                string EMail = userEntry.Properties["mail"].Value != null ? userEntry.Properties["mail"].Value.ToString() : String.Empty;
                                string Account = userEntry.Properties["sAMAccountName"].Value != null ? userEntry.Properties["sAMAccountName"].Value.ToString() : String.Empty;
                                char[] delimiterChars = { '[', ']' };
                                string[] words = Description.Split(delimiterChars);
                                string Dept = words.Length > 1? words[1] : String.Empty;
                                //string Dept = String.Empty;
                                string Name = FullName.Contains('(') ? (FullName.Split('('))[0] : FullName;

                                EmpRow["EmployeeID"] = EmployeeID;
                                EmpRow["Name"] = Name;
                                EmpRow["Account"] = Account;
                                EmpRow["Department"] = Dept;
                                EmpRow["DeptCode"] = DeptCode;
                                EmpRow["TitleCode"] = TitleCode;
                                EmpRow["EMail"] = EMail;

                                if (EmployeeID != String.Empty)
                                {
                                    Emps.Rows.Add(EmpRow);
                                }
                            }
                            catch
                            {
                                throw;
                            }
                        }
                        return Emps;
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }        
                }
            }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// AD帳號認證
        /// </summary>
        /// <param name="LDAP">LDAP</param>
        /// <param name="EmployeeID">員工帳號</param>
        /// <param name="EmployeePasswd">密碼</param>
        /// <returns>認證結果</returns>
        public Boolean checkAccount(string LDAP, string EmployeeID, string EmployeePasswd)
        {
            string LDAP_STRING = "LDAP://" + LDAP;

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(LDAP_STRING, EmployeeID, EmployeePasswd))
                {
                    object nativeObject = entry.NativeObject;
                    return true;
                }
            }
            catch (DirectoryServicesCOMException)
            {
                return false;
            }
        }
        //---------------------------------------------------------------------------------
    }
}
