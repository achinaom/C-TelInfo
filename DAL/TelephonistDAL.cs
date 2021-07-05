using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.models;
namespace DAL
{
  public  class TelephonistDAL:ITelephonistDAL
    {
        //שליפת כל הטלפניות שבמערכת
        public List<Telephonist> GetAllTelephonist()
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                var TelephonistList =  context.Telephonist.ToList();
                return TelephonistList;
            }
        }
        //הוספת טלפנית לבמערכת

        public void AddTelephonis(Telephonist telephonist)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                context.Telephonist.Add(telephonist);
                context.SaveChanges();
            }
        }
        //מחיקת טלפנית מהמערכת
        public void DeleteTelephonist(int id)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                Telephonist telephonist = context.Telephonist.Where(c => c.Id == id).FirstOrDefault();
                if (telephonist != null)
                {
                    context.Telephonist.Remove(telephonist);
                    context.SaveChanges();
                }
            }
        }
        //עדכון טלפנית מהמערכת

        public void updateTelephonist(Telephonist telephonist )
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {

                context.Telephonist.Update(telephonist);
                 context.SaveChanges();
            }

        }
    }
}
