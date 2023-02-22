using Alumni.Db;
using Alumni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Service
{
    public class CommonService
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<IMS_CODEMSTRModel> GetIMSCodeMstr(string code, string keyWord)
        {
            using (SchoolDb db = new SchoolDb())
            {
                string sql = string.Format("select * from  [db_forminf].[dbo].[IMS_CODEMSTR] t where 1=1 {0} {1} order by id asc",
                    string.IsNullOrEmpty(code) ? "" : "and t.code = @code",
                    string.IsNullOrEmpty(keyWord) ? "" :
                    string.Format(
                    "and (t.value like '%{0}%' or t.text like '%{0}%' or t.etd1 like '%{0}%')", keyWord)
                    );
                var list = db.Query<IMS_CODEMSTRModel>(sql, new { code });
                return list;
            }
        }

        public IEnumerable<IMS_CODEMSTRModel> GetIMSCodeMstrI(string code, string keyWord)
        {
            using (SchoolDb db = new SchoolDb())
            {
                string sql = string.Format("select * from [db_forminf].[dbo].[IMS_CODEMSTR] t where 1=1 {0} {1} order by text",
                    string.IsNullOrEmpty(code) ? "" : "and t.code = @code",
                    string.IsNullOrEmpty(keyWord) ? "" : "and t.value = @keyWord");
                var list = db.Query<IMS_CODEMSTRModel>(sql, new { code, keyWord });
                return list;
            }
        }

        protected bool DoEmail(EmailModel model)
        {
            bool result = false;
            try
            {
                using (EmailDb mail = new EmailDb())
                {
                    string strSQL = @"Insert into [Common].[dbo].[oa_emaillog](pid,emailid ,actiontype ,toaddr,toname ,fromaddr ,fromname,subject,body
                                  , attch , remark ,createdate ) 
                                   values(@pid, @emailid , @actiontype , @toaddr, @toname , 'automail@kcisec.com' , @strSystem, @subject, @body
                                  , @attch , @remark, getdate())";
                    mail.Execute(strSQL, model);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;

        }

    }
        
}