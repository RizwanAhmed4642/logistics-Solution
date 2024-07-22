using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Controllers
{
    public class SendEmailController : Controller
    {
        // GET: SendEmail
        public ActionResult SendMail(string fname,string lastname,string phone,string message,string mailFrom,string mailTo)
        {
            string from = ConfigurationManager.AppSettings["Email"].ToString();
            string host = ConfigurationManager.AppSettings["host"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            MailMessage mail = new MailMessage(from, mailTo);
            mail.Subject = "Email From Team Profile";
            mail.Body = message;
            mail.IsBodyHtml = true;


            SmtpClient client = new SmtpClient();
            client.Port = port;
            client.Host = host;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(from, password);
            client.Send(mail);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SendMailCarrer(string fname, string lastname, string contet, string message, 
            string mailFrom, string mailTo,string subject,string position)
        {
            //string from = ConfigurationManager.AppSettings["Email"].ToString();
            //string host = ConfigurationManager.AppSettings["host"].ToString();
            //string password = ConfigurationManager.AppSettings["password"].ToString();
            //int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            //MailMessage mail = new MailMessage(from, mailTo);
            //mail.Subject = "Email From Team Profile";
            //mail.Body = message;
            //mail.IsBodyHtml = true;


            //SmtpClient client = new SmtpClient();
            //client.Port = port;
            //client.Host = host;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential(from, password);
            //client.Send(mail);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SendMailServiceDesk(string fname, string lastname, string contet, string message, string mailFrom,
            string mailTo, string subject, string Location)
        {
            //string from = ConfigurationManager.AppSettings["Email"].ToString();
            //string host = ConfigurationManager.AppSettings["host"].ToString();
            //string password = ConfigurationManager.AppSettings["password"].ToString();
            //int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            //MailMessage mail = new MailMessage(from, mailTo);
            //mail.Subject = "Email From Team Profile";
            //mail.Body = message;
            //mail.IsBodyHtml = true;


            //SmtpClient client = new SmtpClient();
            //client.Port = port;
            //client.Host = host;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential(from, password);
            //client.Send(mail);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SendEMailus(string fname, string lastname, string contet, string message, string mailFrom,
          string mailTo, string subject, string Location)
        {
            //string from = ConfigurationManager.AppSettings["Email"].ToString();
            //string host = ConfigurationManager.AppSettings["host"].ToString();
            //string password = ConfigurationManager.AppSettings["password"].ToString();
            //int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            //MailMessage mail = new MailMessage(from, mailTo);
            //mail.Subject = "Email From Team Profile";
            //mail.Body = message;
            //mail.IsBodyHtml = true;


            //SmtpClient client = new SmtpClient();
            //client.Port = port;
            //client.Host = host;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential(from, password);
            //client.Send(mail);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SendMailPress(string fname, string lastname, string contet, string message, string mailFrom,string mailTo)
        {
            //string from = ConfigurationManager.AppSettings["Email"].ToString();
            //string host = ConfigurationManager.AppSettings["host"].ToString();
            //string password = ConfigurationManager.AppSettings["password"].ToString();
            //int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            //MailMessage mail = new MailMessage(from, mailTo);
            //mail.Subject = "Email From Team Profile";
            //mail.Body = message;
            //mail.IsBodyHtml = true;


            //SmtpClient client = new SmtpClient();
            //client.Port = port;
            //client.Host = host;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential(from, password);
            //client.Send(mail);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult SendMailPartnership(string fname, string lastname, string message, string mailFrom, string mailTo)
        {
            //string from = ConfigurationManager.AppSettings["Email"].ToString();
            //string host = ConfigurationManager.AppSettings["host"].ToString();
            //string password = ConfigurationManager.AppSettings["password"].ToString();
            //int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

            //MailMessage mail = new MailMessage(from, mailTo);
            //mail.Subject = "Email From Team Profile";
            //mail.Body = message;
            //mail.IsBodyHtml = true;


            //SmtpClient client = new SmtpClient();
            //client.Port = port;
            //client.Host = host;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential(from, password);
            //client.Send(mail);
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}