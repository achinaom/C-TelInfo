using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace BLL
{
    public class SendMail 
    {
        public static string Func(string adressMail, string name, string pass)
        {
           return Mail.SendEmail(adressMail, "סיסמה זמנית", "שלום " + name + " \n על מנת להכנס לאתר עליך להקיש סיסמה זמנית. \n סיסמתך הזמנית: " + pass + " \n \n אתר על הדרך...");
        }

        public static string Password()
        {
            Random random = new Random();
            int pass = random.Next(111111, 999999);
            return pass.ToString();
        }

    }
}
