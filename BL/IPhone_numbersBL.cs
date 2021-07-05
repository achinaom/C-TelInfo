using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL.models;
namespace BL
{
    public interface IPhone_numbersBL
    {
        //public List<Phone_numbersDTO> GetPhone_numbers_for_today(int id);
        public List<Phone_numbersDTO> GetAll();
        public Phone_numbersDTO GetPhone_number(int id);
        public List<LeadsAndCallsDTO> addXL(int id, string name);

        public void AddLeads(Phone_numbersDTO phone_NumbersDTO);
        //החזרת כל הלדים של טלפנית 
        public List<Phone_numbersDTO> getLeadForTelephonist(int idtelephonist);
        //החזרת כל הלדים של טלפנית לחודש זה
        public List<Phone_numbersDTO> getLeadForMonthForTelephonist(int idtelephonist);
        //החזרת כל הלדים של טלפנית ליום זה
        public List<Phone_numbersDTO> getLeadForDayForTelephonist(int idtelephonist);
        //החזרת כל הלידים לחודש זה של חברה מסויימת 
        public List<Phone_numbersDTO> getLeadForMonthForCompany(int idCompany);

        //החזרת כל הלידים של חברה מסויימת 
        public List<Phone_numbersDTO> getAllLeadForCompany(int idCompany);
        //עדכון רשומה 
        public Phone_numbersDTO Update_phone(Phone_numbersDTO phone_);
        public Phone_numbersDTO Add_phone(Phone_numbersDTO phone_,int id);
        public List<Phone_numbersDTO> getAllPhoneForTelephonistInCompany(int idtelephonist);
        //החזרת כל מבפרי הטלפון שנכנסו אלינו והטלפנית  אחראית עליהם
        public List<Phone_numbersDTO> getPhoneComForTelephonistInCompany(int idtelephonist);
        //החזרת כל מבפרי הטלפון שהטלפנית ייצרה של טלפנית
        public List<Phone_numbersDTO> getPhoneForTelephonistInCompany(int idtelephonist);
        //החזרת כל הלדים של טלפנית
        public List<Phone_numbersDTO> getLeadForTelephonistInCompany(int idtelephonist);





    }
}
