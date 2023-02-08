using Alumni.Db;
using Alumni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Service
{
    public class SendMailService
    {
        public bool doMail(string subject)
        {
            bool result = false;
            EmailModel emailModel = new EmailModel();
            try
            {
                using (SchoolDb db = new SchoolDb())
                {
                    string mailSetSql = string.Format(@" SELECT *
                                                   FROM [db_forminf].[dbo].[MailSetting]
                                                   WHERE strSystem='线上校友专区' and subject = @subject");
                    emailModel = db.Query<EmailModel>(mailSetSql, new { subject }).FirstOrDefault();
                    emailModel.emailid = Convert.ToString(System.Guid.NewGuid());
                    if (emailModel != null)
                    {
                        string mailSql = string.Format(@"Insert into [Common].[dbo].[oa_emaillog](pid,emailid ,actiontype ,toaddr,toname ,fromaddr ,fromname,subject,body
                                  , attch , remark ,createdate ) 
                                   values(@pid, @emailid , @actiontype , @toaddr, @toname , 'automail@kcisec.com' , @strSystem, @subject, @body
                                  , @attch , @remark, getdate())");
                        Dictionary<string, object> trans = new Dictionary<string, object>();
                        trans.Add(mailSql, emailModel);
                        db.DoExtremeSpeedTransaction(trans);
                    }
                }
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}