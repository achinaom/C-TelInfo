using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace DAL
{
   public interface ICallsDAL
    {
        public List<Calls> GetAllCals();
        public Calls AddCalls(Calls calls);
        public void DeleteCall(int id);
        public void updateCalls(Calls calls);
        public List<Calls> GetAllCalsForTelephonistInCompany(int idTelephonistInCompany);



    }
}
