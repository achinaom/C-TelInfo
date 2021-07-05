using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL.models;
namespace BL
{
    public interface ICompaniesBL
    {
        public CompaniesDTO GetCompany(string mail, string password);
        public List<TelephonistInCompanyDetails> getAlltelephonistInCimpany(int id);
        public List<Phone_numbersDTO> getAllPhoneForCompany(int id);
        public List<CompaniesDTO> GetAllCompany();
        public void AddCompany(CompaniesDTO companiesDTO);
        public void UpdateCompany(CompaniesDTO companiesDTO);
        public CompaniesDTO GetCompanyByMailAndTz(string mail, string tz);





    }
}
