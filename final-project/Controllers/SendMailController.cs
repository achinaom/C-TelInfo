using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using System.Net.Mail;
using System.Text;


namespace HadarFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        //IAddressesBLL _addressBll;
        //DataObject.AddressesDTO a;

        //public SendMailController(IAddressesBLL addressBll)
        //{
        //    _addressBll = addressBll;
        //    a = new DataObject.AddressesDTO();
        //}
        string emailAddress = "";
        string NameAddresses = "";
        [Route("sendMail/{email}/{name}")]
        [HttpGet("{email}/{name}")]
        public IActionResult sendMail(string email, string name)
        {
            //פונקציה כניסה ראשונית של לקוח בה הוא מכניס שם וכתובת מייל
            try
            {
                //a.emailAddress = "7744hadar@gmail.com";
                //a.NameAddresses = "הדר";
                emailAddress = email;
                NameAddresses = name;
                string pass = SendMail.Password();
                string massage = SendMail.Func(emailAddress, NameAddresses, pass);
                if (massage == "מייל לא תקין")
                    return Ok(false);
                password = pass;
                // _addressBll.AddAddresses(a);
                return Ok(true);
            }
            catch
            {
                return NotFound();
            }

        }
        static string password;
        [Route("CheckPass/{pass}")]
        [HttpGet("{pass}")]
        public IActionResult CheckPass(string pass)
        {
            //פונקציה המופעלת לאחר שהלקוח מזין את הסיסמה הזמנית
            try
            {
                if (pass == password)
                    return Ok(true);
                return Ok(false);
            }
            catch
            {
                return NotFound();
            }
        }

        //[Route("saveAddress/{pass}/{phone}")]
        //[HttpGet("{pass}/{phone}")]
        //public IActionResult saveAddress(string pass, string phone)
        //{
        //    //פונקציה המתבצעת לאחר שהלקוח מזין את הפרטים הסופיים
        //    try
        //    {
        //        a.password = pass;
        //        a.PhoneAddresses = phone;
        //        _addressBll.AddAddresses(a);
        //        return Ok(true);
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}


        internal const string MessegesFilePath = @"D:\strauss&porush\מעודכן 16.07.19\ServerSide\BLL\BLL\MessegesXML.xml";
        [Route("SendEmail/{contactAddress}/{subject}/{body}")]
        [HttpGet]
        public IActionResult SendEmail(string contactAddress, string subject, string body)
        {
            string FromMail = contactAddress;
            string emailTo = "telinfo55555@gmail.com";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("telinfo55555@gmail.com", "achinoam1234");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);


            FromMail = "telinfo55555@gmail.com";
            emailTo = contactAddress;
            mail = new MailMessage();
            SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = "telInfo - תודה שבחרתך להצטרף לקהילה שלנו";
            mail.Body = "שלום לך לקוח יקר, \n קיבלנו את פניתך אנו ניצור עימך קשר בהקדם  ... \n";
            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("telinfo55555@gmail.com", "achinoam1234");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            return Ok();
        }




        [Route("SendEmail_subscribe/{contactAddress}")]
        [HttpGet]
        public ActionResult<string> SendEmail_subscribe(string contactAddress)

        {
            try
            {
                string FromMail = "telinfo55555@gmail.com";
                string emailTo = contactAddress;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(FromMail);
                mail.To.Add(contactAddress);
                mail.To.Add("telinfo55555@gmail.com");
                mail.Subject = "telInfo - תודה שבחרתך להצטרף לקהילה שלנו";
                mail.Body = "שלום לך לקוח יקר, \n מעכשיו תוכל לקבל את שלל העידכונים החדשים שבחברתינו, מבצעים ,הנחות ועוד ... \n";
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("telinfo55555@gmail.com", "achinoam1234");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return Ok();
            }
            catch
            {
                return NotFound("כתובת המייל שהזנת לא תקינה");
            }
        }




    }
}
