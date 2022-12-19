using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace KCIS_Biz
{
    public class SendMail
    {
        /// <summary>
        /// Send
        /// </summary>
        /// <param name="Sender">Sender</param>
        /// <param name="Reciever">Reciever(複數使用;隔開)</param>
        /// <param name="MailSubject">MailSubject</param>
        /// <param name="MailBody">MailBody</param>
        public string Send(string Sender, string Reciever, string MailSubject, string MailBody)
        {
            //string MailServer = "mail.kcbs.ntpc.edu.tw";
            string MailServer = "mail.kcisec.com";

            MailMessage mail = new MailMessage();

            foreach (string myRec in Reciever.Split(';'))
            {
                if (myRec != String.Empty)
                {
                    mail.To.Add(myRec);
                }
            }

            mail.Subject = MailSubject;
            mail.From = new System.Net.Mail.MailAddress(Sender);
            mail.IsBodyHtml = true;
            mail.Body = MailBody;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Port = 25;
            try
            {
                smtp.Send(mail);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MailServer"></param>
        /// <param name="Sender"></param>
        /// <param name="Reciever"></param>
        /// <param name="MailSubject"></param>
        /// <param name="MailBody"></param>
        /// <returns></returns>
        public string SendByMS(string MailServer, string Sender, string Reciever, string MailSubject, string MailBody)
        {
            MailMessage mail = new MailMessage();

            foreach (string myRec in Reciever.Split(';'))
            {
                if (myRec != String.Empty)
                {
                    mail.To.Add(myRec);
                }
            }

            mail.Subject = MailSubject;
            mail.From = new System.Net.Mail.MailAddress(Sender);
            mail.IsBodyHtml = true;
            mail.Body = MailBody;

            SmtpClient smtp = new SmtpClient(MailServer);
            smtp.Port = 25;
            try
            {
                smtp.Send(mail);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
