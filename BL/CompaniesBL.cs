using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL;
using AutoMapper;
using System.IO;
using System.Linq;
using DAL.models;

namespace BL
{
    public class CompaniesBL : ICompaniesBL
    {
        ICompaniesDAL _CompaniesDAL;
        ITelephonistDAL _TelephonistDAL;
        IPhone_numbersDAL _phone_NumbersDAL;
        ITelephonistCompaniesBL _telephonistCompaniesBL;
        IMapper iMapper;
        public CompaniesBL(ICompaniesDAL companiesDAL, ITelephonistDAL telephonistDAL, ITelephonistCompaniesBL telephonistCompaniesBL, IPhone_numbersDAL phone_NumbersDAL)
        {
            _phone_NumbersDAL = phone_NumbersDAL;
            _TelephonistDAL = telephonistDAL;
            _CompaniesDAL = companiesDAL;
            _telephonistCompaniesBL = telephonistCompaniesBL;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
        }
        //שליפץת כל החברות ממערכת
        public List< CompaniesDTO> GetAllCompany()
        {
            List<Companies> companiesList = _CompaniesDAL.GetCompanies();
            List<CompaniesDTO> allcompanies = iMapper.Map<List<Companies>, List<CompaniesDTO>>(companiesList);
            return allcompanies;

        }
        //הוספת חברה
        public void AddCompany(CompaniesDTO companiesDTO)
        {
            Companies company = iMapper.Map<CompaniesDTO, Companies>(companiesDTO);
            _CompaniesDAL.AddCompany(company);
        }
        //עדכון חברה
        public void UpdateCompany(CompaniesDTO companiesDTO)
        {
            Companies company = iMapper.Map<CompaniesDTO, Companies>(companiesDTO);
            _CompaniesDAL.updateCompanies(company);
        }
        //שליפת חברה מסויימת לפי מייל וסיסמא
        public CompaniesDTO GetCompany(string mail, string password)
        {
            bool degel = false;
            List<Companies> companiesList = _CompaniesDAL.GetCompanies();
            CompaniesDTO companiesDTO = new CompaniesDTO();
            foreach (var item in companiesList)
            {
                if (item.Mail == mail && item.ManagerPassword == password)
                {
                    companiesDTO = iMapper.Map<Companies, CompaniesDTO>(item);
                }
            }
            return companiesDTO;
        }

        public CompaniesDTO GetCompanyByMailAndTz(string mail, string tz)
        {
            bool degel = false;
            List<Companies> companiesList = _CompaniesDAL.GetCompanies();
            CompaniesDTO companiesDTO = new CompaniesDTO();
            foreach (var item in companiesList)
            {
                if (item.Mail == mail && item.ManagerTz == tz)
                {
                    companiesDTO = iMapper.Map<Companies, CompaniesDTO>(item);
                }
            }
            return companiesDTO;
        }

        //שליפת כל הטלפניות של מוסד מסויים עם פרטים כוללים של טלפנית במוסד וגם פרטי טלפנית- איחוד מחלקות
        public List<TelephonistInCompanyDetails> getAlltelephonistInCimpany(int id)
        {
            List<Companies> companiesList = _CompaniesDAL.GetCompanies();
            Companies companies = companiesList.Where(c => c.Id == id).FirstOrDefault();
            List<TelephonistCompaniesDTO> telephonistsIn = _telephonistCompaniesBL.GetTelephonistCompaniesById(id);
            List<Telephonist> telephonists = _TelephonistDAL.GetAllTelephonist();
            List<TelephonistInCompanyDetails> list = new List<TelephonistInCompanyDetails>();
            foreach (var item in telephonistsIn)
            {
                foreach (var item1 in telephonists)
                {
                    if (item.IdTelephonist == item1.Id)
                    {
                        TelephonistInCompanyDetails telephonistInCompanyDetails = new TelephonistInCompanyDetails();

                        telephonistInCompanyDetails.IdCompany = item.IdCompany;
                        telephonistInCompanyDetails.IdTelephonist = item.IdTelephonist;
                        telephonistInCompanyDetails.Password = item.Password;
                        telephonistInCompanyDetails.IdTelephonistInCompany = item.Id;
                        telephonistInCompanyDetails.Telephone = item1.Telephone;
                        telephonistInCompanyDetails.Name = item1.Name;
                        telephonistInCompanyDetails.Telephone = item1.Telephone;
                        telephonistInCompanyDetails.Mail = item1.Mail;
                        telephonistInCompanyDetails.DateBirth = item1.DateBirth;
                        list.Add(telephonistInCompanyDetails);
                    }
                }
            }
            return list;
        }
        public List<Phone_numbersDTO> getAllPhoneForCompany(int id)
        {
            List<PhoneNumbers> phoneNumbers = _phone_NumbersDAL.GetPhoneNumbers();
            List<PhoneNumbers> p1 = phoneNumbers.Where(x => x.IdCompanies == id).ToList();
            List<Phone_numbersDTO> p2 = iMapper.Map<List<PhoneNumbers>, List<Phone_numbersDTO>>(p1);
            return p2;
        }


    }
}

