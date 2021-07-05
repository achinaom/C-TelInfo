using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL;
using AutoMapper;
using System.Linq;
using DAL.models;
namespace BL
{
    public class TelephonistBL : ITelephonistBL
    {

        ITelephonistDAL _telephonistDAL;
        ITelephonistCompaniesBL _TelephonistCompaniesBL;

        IMapper iMapper;
        public TelephonistBL(ITelephonistDAL telephonistDAL, ITelephonistCompaniesBL telephonistCompaniesBL)
        {
            _telephonistDAL = telephonistDAL;
            _TelephonistCompaniesBL = telephonistCompaniesBL;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
        }



        //שליפת טלפנית בחברה לפי מייל וססיסמא
        public TelephonistCompaniesDTO GetTelephonist(string mail, string password)
        {
            List<Telephonist> telephonistList = _telephonistDAL.GetAllTelephonist();
            TelephonistDTO telephonistDTO = new TelephonistDTO();
            List<TelephonistCompaniesDTO> TelephonistCompanies = _TelephonistCompaniesBL.GetTelephonistCompanies();
            TelephonistCompaniesDTO telephonistCompaniesDTO = new TelephonistCompaniesDTO();

            foreach (var item in telephonistList)
            {
                if (item.Mail == mail)
                {
                    foreach (var item1 in TelephonistCompanies)
                    {
                        if (item1.IdTelephonist == item.Id && item1.Password == password)
                        {
                            telephonistCompaniesDTO = TelephonistCompanies.FirstOrDefault(x => x.IdTelephonist == item.Id && item1.Password == password);
                            break;
                        }
                        else
                            continue;
                    }

                }
            }

            return telephonistCompaniesDTO;
        }


        public List<TelephonistCompaniesDTO> GetTelephonistByMailAndTz(string mail, string tz)
        {
            List<Telephonist> telephonistList = _telephonistDAL.GetAllTelephonist();
            TelephonistDTO telephonistDTO = new TelephonistDTO();
            List<TelephonistCompaniesDTO> TelephonistCompanies = _TelephonistCompaniesBL.GetTelephonistCompanies();
            List<TelephonistCompaniesDTO> telephonistCompaniesDTO = new List<TelephonistCompaniesDTO>();

            foreach (var item in telephonistList)
            {
                if (item.Mail == mail && item.Tz == tz)
                {
                    foreach (var item1 in TelephonistCompanies)
                    {
                        if (item1.IdTelephonist == item.Id)
                        {
                            telephonistCompaniesDTO.Add(item1);
                        }

                    }

                }
            }
            if (telephonistCompaniesDTO.Count >= 0)
                return telephonistCompaniesDTO;
            else
                return null;
        }
        //הוספת טלפנית לחברה מסויימת
        public void Add_telephonnist(TelephonistDTO telephonistDTO, int idCompany, String password)
        {
            Telephonist telephonist = iMapper.Map<TelephonistDTO, Telephonist>(telephonistDTO);
            _telephonistDAL.AddTelephonis(telephonist);
            Telephonist telephonist1 = _telephonistDAL.GetAllTelephonist()[_telephonistDAL.GetAllTelephonist().Count() - 1];
            TelephonistCompaniesDTO telephonistInCompanies = new TelephonistCompaniesDTO();
            telephonistInCompanies.IdTelephonist = telephonist1.Id;
            telephonistInCompanies.IdCompany = idCompany;
            telephonistInCompanies.Password = password;
            _TelephonistCompaniesBL.AddTelephonistCompanies(telephonistInCompanies);
        }

        public void update_telephonist(TelephonistInCompanyDetails telephonistInCompanyDetails)
        {
            Telephonist t = new Telephonist();
            t.Id = (int)telephonistInCompanyDetails.IdTelephonist;
            t.Mail = telephonistInCompanyDetails.Mail;
            t.Name = telephonistInCompanyDetails.Name;
            t.Telephone = telephonistInCompanyDetails.Telephone;
            t.DateBirth = telephonistInCompanyDetails.DateBirth;
            t.Tz = telephonistInCompanyDetails.tz;
            _telephonistDAL.updateTelephonist(t);
            TelephonistCompaniesDTO t1 = new TelephonistCompaniesDTO();
            t1.Id = telephonistInCompanyDetails.IdTelephonistInCompany;
            t1.IdCompany = telephonistInCompanyDetails.IdCompany;
            t1.Password = telephonistInCompanyDetails.Password;
            t1.IdTelephonist = telephonistInCompanyDetails.IdTelephonist;
            _TelephonistCompaniesBL.UpdateTelephonistCompanies(t1);

        }
    }

}

