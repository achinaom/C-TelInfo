using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DataObject;
using AutoMapper;
using System.Linq;
using DAL.models;
namespace BL
{
    public class TelephonistCompaniesBL : ITelephonistCompaniesBL
    {
        ITelephonistCompaniesDAL _TelephonistCompaniesDAL;
        ITelephonistDAL _telephonistDAL;
        IMapper iMapper;
        public TelephonistCompaniesBL(ITelephonistCompaniesDAL TelephonistCompaniesDAL, ITelephonistDAL telephonistDAL)
        {
            _telephonistDAL = telephonistDAL;
            _TelephonistCompaniesDAL = TelephonistCompaniesDAL;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
        }

        //החזרת כל הטלפניות שבכל החברות
        public List<TelephonistCompaniesDTO> GetTelephonistCompanies()
        {
            List<TelephonistInCompanies> TelephonistCompaniesList = _TelephonistCompaniesDAL.GetTelephonistInCompanies();
            List<TelephonistCompaniesDTO> TelephonistCompaniesDTOList = new List<TelephonistCompaniesDTO>();
            TelephonistCompaniesDTOList = iMapper.Map<List<TelephonistInCompanies>, List<TelephonistCompaniesDTO>>(TelephonistCompaniesList);
            return TelephonistCompaniesDTOList;

        }

        //כל הטלפניות שבחברה מסויימת
        public List<TelephonistCompaniesDTO> GetTelephonistCompaniesById(int id)
        {
            List<TelephonistInCompanies> TelephonistCompaniesList = _TelephonistCompaniesDAL.GetTelephonistInCompanies().Where(x => x.IdCompany == id).ToList();
            List<TelephonistCompaniesDTO> TelephonistCompaniesDTOList = iMapper.Map<List<TelephonistInCompanies>, List<TelephonistCompaniesDTO>>(TelephonistCompaniesList);
            return TelephonistCompaniesDTOList;
        }

        //כל הטלפניות שבחברה מסויימת עם פרטים של כל טלפנית
        public List<TelephonistInCompanyDetails> GetTelephonistDetailsCompaniesById(int id)
        {
            List<TelephonistInCompanies> TelephonistCompaniesList = _TelephonistCompaniesDAL.GetTelephonistInCompanies().Where(x => x.IdCompany == id).ToList();
            List<Telephonist> telephonists = _telephonistDAL.GetAllTelephonist();
            List<TelephonistInCompanyDetails> telephonistInCompanyDetailsList = new List<TelephonistInCompanyDetails>();
            TelephonistInCompanyDetails telephonistInCompanyDetails1 = new TelephonistInCompanyDetails();
            foreach (var item in TelephonistCompaniesList)
            {
                foreach (var item1 in telephonists)
                {
                    if (item.IdTelephonist == item1.Id)
                    {
                        telephonistInCompanyDetails1 = new TelephonistInCompanyDetails();
                        telephonistInCompanyDetails1.IdCompany = id;
                        telephonistInCompanyDetails1.IdTelephonist = item1.Id;
                        telephonistInCompanyDetails1.Mail = item1.Mail;
                        telephonistInCompanyDetails1.Name = item1.Name;
                        telephonistInCompanyDetails1.Password = item.Password;
                        telephonistInCompanyDetails1.FamilyStatus = item1.FamilyStatus;
                        telephonistInCompanyDetails1.DateBirth = item1.DateBirth;
                        telephonistInCompanyDetails1.IdTelephonistInCompany = item.Id;
                        telephonistInCompanyDetailsList.Add(telephonistInCompanyDetails1);
                    }
                }

            }
            return telephonistInCompanyDetailsList;
        }

        //מחזיר מספר חברה על פי מספר טלפנית בחברה
        public int GetIdCompaniesByIdTelephonist(int id)
        {
            TelephonistInCompanies telephonistCompanies = _TelephonistCompaniesDAL.GetTelephonistInCompanies().Where(x => id == id).FirstOrDefault();
            return (int)telephonistCompanies.IdCompany;
        }

        //הוספת טלפנית בחברה
        public void AddTelephonistCompanies(TelephonistCompaniesDTO telephonistCompaniesDTO)
        {
            TelephonistInCompanies telephonistInCompanies = iMapper.Map<TelephonistCompaniesDTO, TelephonistInCompanies>(telephonistCompaniesDTO);
            _TelephonistCompaniesDAL.AddTelephonistInCompanies(telephonistInCompanies);
        }

        //מחיקת טלפנית בחברה
        public void DeleteTelephonistCompanies(int id)
        {
            _TelephonistCompaniesDAL.DeleteTelephonistInCompanies(id);
        }

        //עדכון טלפנית בחברה
        public void UpdateTelephonistCompanies(TelephonistCompaniesDTO telephonistCompaniesDTO)
        {
            TelephonistInCompanies telephonistInCompanies = iMapper.Map<TelephonistCompaniesDTO, TelephonistInCompanies>(telephonistCompaniesDTO);
            _TelephonistCompaniesDAL.updateTelephonistInCompanies(telephonistInCompanies);
        }

        //מחזיר טלפנית על פי מספר טלפנית בחברה
        public TelephonistDTO GetTelephonistByIdTelephonist(int id)
        {
            TelephonistInCompanies telephonistCompanies=new TelephonistInCompanies();
            foreach (var item in _TelephonistCompaniesDAL.GetTelephonistInCompanies())
            {
                if(item.Id==id)
                {
                    telephonistCompanies = item;
                    break;
                }
            }
            //TelephonistInCompanies telephonistCompanies = _TelephonistCompaniesDAL.GetTelephonistInCompanies().FirstOrDefault(x => Id == id);
            Telephonist telephonist = _telephonistDAL.GetAllTelephonist().FirstOrDefault(x => x.Id == telephonistCompanies.IdTelephonist);
            TelephonistDTO telephonistDTO = iMapper.Map<Telephonist, TelephonistDTO>(telephonist);
            return telephonistDTO;
        }


        public TelephonistCompaniesDTO GetTelephonistinCompanyByIdTelephonistInCopmany(int id)
        {
            TelephonistInCompanies telephonistCompanies = new TelephonistInCompanies();

            foreach (var item in _TelephonistCompaniesDAL.GetTelephonistInCompanies())
            {
                if (item.Id == id)
                {
                    telephonistCompanies = item;
                    break;
                }
            }
            TelephonistCompaniesDTO telephonistCompaniesDTO = iMapper.Map<TelephonistInCompanies, TelephonistCompaniesDTO>(telephonistCompanies);
            return telephonistCompaniesDTO;
        }

    }
}
