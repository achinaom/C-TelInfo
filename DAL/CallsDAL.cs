using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.models;
namespace DAL
{
   public class CallsDAL:ICallsDAL
    {
        //שליפת כל השיחות מהמסד נתונים
        public List<Calls> GetAllCals()
        {
            var calsList = DataBaseList.callList;
                return calsList;
        }

        //שליפת כל השיחות על פי מספר טלפנית בחברה 
        public List<Calls> GetAllCalsForTelephonistInCompany(int idTelephonistInCompany)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
               List<Calls>calls=new List<Calls>();
                foreach (var item in DataBaseList.callList)
                {
                    if (item.IdTelephonist == idTelephonistInCompany)
                        calls.Add(item);
                }
                DataBaseList.callList = context.Calls.ToList();
                return calls;
            }
        }

        //הוספת שיחה חדשה למסד נתונים
        public Calls AddCalls(Calls calls)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
               
                context.Calls.Add(calls);
                context.SaveChanges();
                DataBaseList.callList = context.Calls.ToList();

                Calls calls1 = GetAllCals()[GetAllCals().Count - 1];
                return calls1;
            }
        }

        //מחיקת שיחה חדשה למסד נתונים
        public void DeleteCall(int id)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                Calls  calls= DataBaseList.callList.Where(c => c.Id == id).FirstOrDefault();
                if (calls != null)
                {
                    context.Calls.Remove(calls);
                    context.SaveChanges();
                    DataBaseList.callList = context.Calls.ToList();

                }
            }
        }

       //עדכון שיחה במסד נתונים
        public void updateCalls(Calls calls)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {

                context.Calls.Update(calls);
                context.SaveChanges();
                DataBaseList.callList = context.Calls.ToList();


            }

        }
    }
}
