using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public class TelephonistCompaniesDTO
    {
        public int Id { get; set; }
        public int? IdTelephonist { get; set; }
        public string Password { get; set; }
        public int? IdCompany { get; set; }

    }
}
