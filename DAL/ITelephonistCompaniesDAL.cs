using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace DAL
{
   public interface ITelephonistCompaniesDAL
    {
        public List<TelephonistInCompanies> GetTelephonistInCompanies();


        public void AddTelephonistInCompanies(TelephonistInCompanies telephonistInCompanies);

        public void DeleteTelephonistInCompanies(int id);


        public void updateTelephonistInCompanies(TelephonistInCompanies telephonistInCompanies);

       
    }
}
