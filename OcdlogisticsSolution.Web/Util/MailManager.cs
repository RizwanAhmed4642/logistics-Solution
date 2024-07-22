using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace OcdlogisticsSolution.Web.Util
{
    public class MailManager
    {
        public static void SendMail(String to, String Message, string subject)
        {
            string from = ConfigurationManager.AppSettings["from"].ToString();
            string host = ConfigurationManager.AppSettings["host"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            MailMessage mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = Message;
            mail.IsBodyHtml = true;


            SmtpClient client = new SmtpClient();
            client.Port = port;
            client.Host = host;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(from, password);
            client.Send(mail);
        }

        public static void SendContactMail(String Message, string subject)
        {
            string from = ConfigurationManager.AppSettings["from"].ToString();
            string to = ConfigurationManager.AppSettings["to"].ToString();
            string host = ConfigurationManager.AppSettings["host"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            MailMessage mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = Message;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Port = port;
            client.Host = host;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(from, password);
            client.Send(mail);
        }
    }
}

