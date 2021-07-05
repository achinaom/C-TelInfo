using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL.models;
namespace BL
{
    public interface ITelephonistBL
    {
        public TelephonistCompaniesDTO GetTelephonist(string mail, string password);
        public void Add_telephonnist(TelephonistDTO telephonistDTO, int idCompany, String password);
   
        public void update_telephonist(TelephonistInCompanyDetails telephonistInCompanyDetails);
        public List<TelephonistCompaniesDTO> GetTelephonistByMailAndTz(string mail, string tz);

    }
}
