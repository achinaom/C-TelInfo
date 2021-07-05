using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace DAL
{
   public interface ITelephonistDAL
    {
        public List<Telephonist> GetAllTelephonist();

        public void AddTelephonis(Telephonist telephonist);
        public void DeleteTelephonist(int id);

        public void updateTelephonist(Telephonist telephonist);
    }
}
