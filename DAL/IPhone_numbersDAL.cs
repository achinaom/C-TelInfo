using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace DAL
{
    public interface IPhone_numbersDAL
    {
        public List<PhoneNumbers> GetPhoneNumbers();


        public void AddPhoneNumbers(PhoneNumbers phoneNumbers);


        public void DeletePhoneNumber(int id);

        public void updatePhoneNumber(PhoneNumbers phoneNumbers);

    }
}
