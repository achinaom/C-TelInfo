using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
   public class TelephonistInCompanyDetails
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string tz { get; set; }
        public string Mail { get; set; }
        public DateTime? DateBirth { get; set; }
        public int? FamilyStatus { get; set; }
        public int IdTelephonistInCompany { get; set; }
        public int? IdTelephonist { get; set; }
        public string Password { get; set; }
        public int? IdCompany { get; set; }


    }
}
