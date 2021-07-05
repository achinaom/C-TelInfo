using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class Companies
    {
        public Companies()
        {
            PhoneNumbers = new HashSet<PhoneNumbers>();
            TelephonistInCompanies = new HashSet<TelephonistInCompanies>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public string ManagerTz { get; set; }
        public string ManagerPassword { get; set; }
        public string Mail { get; set; }

        public virtual ICollection<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual ICollection<TelephonistInCompanies> TelephonistInCompanies { get; set; }
    }
}
