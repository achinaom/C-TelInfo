using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL.models;

namespace BL
{
    public interface ITelephonistCompaniesBL
    {
        public List<TelephonistCompaniesDTO> GetTelephonistCompanies();
        public List<TelephonistCompaniesDTO> GetTelephonistCompaniesById(int id);
        public void DeleteTelephonistCompanies(int id);
        public int GetIdCompaniesByIdTelephonist(int id);
        public void AddTelephonistCompanies(TelephonistCompaniesDTO telephonistCompaniesDTO);
        public TelephonistDTO GetTelephonistByIdTelephonist(int id);
        public TelephonistCompaniesDTO GetTelephonistinCompanyByIdTelephonistInCopmany(int id);
        public void UpdateTelephonistCompanies(TelephonistCompaniesDTO telephonistCompaniesDTO);
    }
}
