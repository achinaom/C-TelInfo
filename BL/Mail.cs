using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using DAL.models;
namespace BLL
{
    static class Mail
    {
        // internal const string MessegesFilePath = @"D:\strauss&porush\מעודכן 16.07.19\ServerSide\BLL\BLL\MessegesXML.xml";
        public static string SendEmail(string contactAddress, string subject, string body)
        {             
            string s = "הכנס קוד זמני";
            string FromMail = "telinfo55555@gmail.com";
            string emailTo = contactAddress;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FromMail);
            try
            {
                mail.To.Add(emailTo);
            }
            catch
            {
                s = "מייל לא תקין";
                return s;
            }
            mail.Subject = subject;
            mail.Body = body;
            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("telinfo55555@gmail.com", "achinoam1234");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            return s;
        }
    }
}
