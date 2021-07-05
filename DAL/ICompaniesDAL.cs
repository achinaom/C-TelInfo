using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace DAL
{
  public  interface ICompaniesDAL
    {
        public List<Companies> GetCompanies();
        public void AddCompany(Companies companies);
        public void DeleteCompanies(int id);
        public void updateCompanies(Companies companies);
    }
}
