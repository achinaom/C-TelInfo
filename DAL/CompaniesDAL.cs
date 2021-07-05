using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.models;
namespace DAL
{
  public  class CompaniesDAL:ICompaniesDAL
    {
        //שליפת כל החברות שבמערכת
        public List<Companies> GetCompanies()
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                var CompaniesList = context.Companies.ToList();
                return CompaniesList;
            }

        }
        //הוספת חברה חדשה למערכת

        public void AddCompany(Companies companies)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                context.Companies.Add(companies);
                context.SaveChanges();
            }
        }
        //מחיקת חברה מהמערכת
        public void DeleteCompanies(int id)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                Companies companies = context.Companies.Where(c => c.Id == id).FirstOrDefault();
                if (companies != null)
                {
                    context.Companies.Remove(companies);
                    context.SaveChanges();
                }
            }
        }
        //עדכון חברה במערכת
        public void updateCompanies(Companies companies)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {

                context.Companies.Update(companies);
                context.SaveChanges();
            }

        }
    }
}
