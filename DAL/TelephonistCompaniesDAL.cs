using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.models;
namespace DAL
{
   public  class TelephonistCompaniesDAL:ITelephonistCompaniesDAL
    {
        //שליפת כל הטלפניות שבחברות ממסד הנתונים 
        public List<TelephonistInCompanies> GetTelephonistInCompanies()
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                var TelephonistInCompaniesList = context.TelephonistInCompanies.ToList();
                return TelephonistInCompaniesList;
            }
        }

        //הוספת טלפנית בחברה למסד הנתונים 

        public void AddTelephonistInCompanies(TelephonistInCompanies telephonistInCompanies)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                context.TelephonistInCompanies.Add(telephonistInCompanies);
                context.SaveChanges();
            }
        }

        //מחיקת טלפנית בחברה ממסד הנתונים 

        public void DeleteTelephonistInCompanies(int id)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                TelephonistInCompanies telephonistInCompanies = context.TelephonistInCompanies.Where(c => c.Id == id).FirstOrDefault();
                if (telephonistInCompanies != null)
                {
                    context.TelephonistInCompanies.Remove(telephonistInCompanies);
                    context.SaveChanges();
                }
            }
        }
        //עדכון טלפנית בחברה במסד הנתונים 

        public void updateTelephonistInCompanies(TelephonistInCompanies telephonistInCompanies)

        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                context.TelephonistInCompanies.Update(telephonistInCompanies);
                context.SaveChanges();
            }

        }

    }
}
