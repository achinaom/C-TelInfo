using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.models;
namespace DAL
{
   public class Phone_numbersDAL:IPhone_numbersDAL
    {
        //שליפת כל מספרי הטלפון מהמסד נתונים
        public List<PhoneNumbers> GetPhoneNumbers()
        {
     
            var PhoneNumbers_List = DataBaseList.PhoneNumbersList;
            return PhoneNumbers_List;
        }

        //הוספת מספר טלפון למסד נתונים

        public void AddPhoneNumbers(PhoneNumbers phoneNumbers)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                context.PhoneNumbers.Add(phoneNumbers);
                context.SaveChanges();
                DataBaseList.PhoneNumbersList = context.PhoneNumbers.ToList();
            }
        }

        //מחיקת מספר טלפון מהמסד נתונים

        public void DeletePhoneNumber(int id)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                PhoneNumbers phoneNumbers = DataBaseList.PhoneNumbersList.Where(c => c.Id == id).FirstOrDefault();
                if (phoneNumbers != null)
                {
                    context.PhoneNumbers.Remove(phoneNumbers);
                    context.SaveChanges();
                    DataBaseList.PhoneNumbersList = context.PhoneNumbers.ToList();

                }
            }
        }


        //עדכון מספר טלפון בהמסד נתונים

        public void updatePhoneNumber(PhoneNumbers phoneNumbers)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {

                context.PhoneNumbers.Update(phoneNumbers);
                context.SaveChanges();
                DataBaseList.PhoneNumbersList = context.PhoneNumbers.ToList();

            }

        }
    }
}
