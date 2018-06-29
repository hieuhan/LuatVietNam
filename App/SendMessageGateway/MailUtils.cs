using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace SendMTGateway
{
    public class MailUtils
    {
        public MailUtils()
        {

        }

        public static string sendMessage(string mailTo, string subject, string bodyMail)
        {
            string mail_host = ConfigurationManager.AppSettings["MAIL_SERVER_HOST"];
            string mail_user = ConfigurationManager.AppSettings["MAIL_USER"];
            string mail_pass = ConfigurationManager.AppSettings["MAIL_PASS"];
            string mail_domain = ConfigurationManager.AppSettings["MAIL_DOMAIN"];
            string mail_urlbase = ConfigurationManager.AppSettings["MAIL_URLBASE"];

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
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
        }
        public static string sendMessage(string mailFrom, string mailTo, string subject, string bodyMail)
        {
            if(ConfigurationManager.AppSettings["USING_AWS"] == "1")
            {
                return sendMessageUsingAWS(mailFrom, mailTo, subject, bodyMail);
                
            }
            string mail_host = ConfigurationManager.AppSettings["MAIL_SERVER_HOST"];
            string mail_user = ConfigurationManager.AppSettings["MAIL_USER"];
            string mail_pass = ConfigurationManager.AppSettings["MAIL_PASS"];
            string mail_domain = ConfigurationManager.AppSettings["MAIL_DOMAIN"];
            string mail_urlbase = ConfigurationManager.AppSettings["MAIL_URLBASE"];

            SmtpClient mail = new SmtpClient(mail_host);

            MailAddress from = new MailAddress(mailFrom);

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
                return "success";

            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
        }
        public static string sendMessageUsingAWS(string mailFrom, string mailTo, string subject, string bodyMail)
        {
            try
            {
                String username = ConfigurationManager.AppSettings["AWS_MAIL_USER"];  // Replace with your SMTP username.
                String password = ConfigurationManager.AppSettings["AWS_MAIL_PASS"];  // Replace with your SMTP password.
                String host = ConfigurationManager.AppSettings["AWS_MAIL_SERVER_HOST"];
                int port = 587;

                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(mailFrom);
                message.To.Add(new MailAddress(mailTo));
                message.Subject = subject;
                message.Body = bodyMail;
               
                // Create and configure a new SmtpClient
                SmtpClient client =
                    new SmtpClient(host, port);
                // Pass SMTP credentials
                client.Credentials =
                    new NetworkCredential(username, password);
                // Enable SSL encryption
                client.EnableSsl = true;

                // Send the email. 
                try
                {
                    
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return "success";

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
