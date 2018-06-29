using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace ICSoft.HelperLib
{
    public class MailHelpers
    {
        public MailHelpers()
        {

        }
        public static byte sendMessage2(string mailTo, string subject, string bodyMail)
        {
            byte RetVal = SendStatusHelpers.Success;
            string mail_host = ConfigurationManager.AppSettings["MAIL_SERVER_HOST"];
            string mail_user = ConfigurationManager.AppSettings["MAIL_USER"];
            string mail_pass = ConfigurationManager.AppSettings["MAIL_PASS"];
            string mail_domain = ConfigurationManager.AppSettings["MAIL_DOMAIN"];
            string mail_urlbase = ConfigurationManager.AppSettings["MAIL_URLBASE"];
            int mail_port = int.Parse(ConfigurationManager.AppSettings["MAIL_PORT"]);
            SmtpClient mail = new SmtpClient(mail_host);

            MailAddress from = new MailAddress(mail_user);

            MailAddress to = new MailAddress(mailTo);

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(mail_user, mailTo);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.IsBodyHtml = true;

            message.Body = bodyMail;

            message.Subject = subject;
            mail.EnableSsl = true;
            mail.UseDefaultCredentials = false;
            mail.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass);
            mail.Host = mail_host;
            mail.Port = mail_port;
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                mail.Send(message);
                
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                RetVal = SendStatusHelpers.Error;
            }
            return RetVal;
        }
        public static byte sendMessage(string mailTo, string subject, string bodyMail)
        {
            byte RetVal = SendStatusHelpers.Success;
            string mail_host = ConstantHelpers.MAIL_SERVER_HOST;
            string mail_user = ConstantHelpers.MAIL_USER;
            string mail_pass = ConstantHelpers.MAIL_PASS;
            string mail_domain = ConstantHelpers.MAIL_DOMAIN;
            string mail_urlbase = ConstantHelpers.MAIL_URLBASE;

            SmtpClient mail = new SmtpClient(mail_host);

            MailAddress from = new MailAddress(mail_user + "@" + mail_domain);

            MailAddress to = new MailAddress(mailTo);

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.IsBodyHtml = true;

            message.Body = bodyMail;

            message.Subject = subject;
            mail.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass, mail_domain);
            try
            {
                mail.Send(message);
                
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                RetVal = SendStatusHelpers.Error;
            }
            return RetVal;
        }
        public static byte sendMessage(string mailFrom, string mailTo, string subject, string bodyMail)
        {
            byte RetVal = SendStatusHelpers.Success;
            string mail_host = ConstantHelpers.MAIL_SERVER_HOST;
            string mail_user = ConstantHelpers.MAIL_USER;
            string mail_pass = ConstantHelpers.MAIL_PASS;
            string mail_domain = ConstantHelpers.MAIL_DOMAIN;
            string mail_urlbase = ConstantHelpers.MAIL_URLBASE;
            int mail_port = ConstantHelpers.MAIL_PORT;
            SmtpClient mail = new SmtpClient(mail_host);

            MailAddress from = new MailAddress(mailFrom);

            MailAddress to = new MailAddress(mailTo);

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(mailFrom, mailTo);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.IsBodyHtml = true;

            message.Body = bodyMail;

            message.Subject = subject;
            mail.EnableSsl = ConstantHelpers.MAIL_SSL;
            mail.UseDefaultCredentials = false;
            mail.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass);
            mail.Host = mail_host;
            mail.Port = mail_port;
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                mail.Send(message);

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                RetVal = SendStatusHelpers.Error;
            }
            return RetVal;
        }
    }
}
